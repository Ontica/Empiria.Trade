﻿/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Shipping Management                        Component : Domain Layer                            *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Partitioned Type / Information Holder   *
*  Type     : ShippingBuilder                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Generate data for Shipping.                                                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using Empiria.Trade.Core.Common;
using Empiria.Trade.Orders;
using Empiria.Trade.Sales.ShippingAndHandling.Adapters;
using Empiria.Trade.Sales.ShippingAndHandling.Data;
using Empiria.Trade.Sales.UseCases;

namespace Empiria.Trade.Sales.ShippingAndHandling.Domain {

  /// <summary>Generate data for Shipping.</summary>
  internal class ShippingBuilder {


    #region Constructor

    public ShippingBuilder() {

    }


    #endregion Constructor


    #region Public methods


    internal void CreateOrdersForShipping(string shippingOrderUID, string[] orders) {

      var shipping = ShippingEntry.Parse(shippingOrderUID);

      foreach (var order in orders) {

        var shippingOrder = new ShippingOrderItem(order, shipping);
        shippingOrder.Save();

      }
    }


    internal void CreateShippingPallet(string shippingUID, ShippingPalletFields fields) {

      ValidateIfExistShippingPackages(fields.Packages);

      ShippingPallet pallet = new ShippingPallet(shippingUID, fields, "");

      pallet.Save();

      CreatePackagesForPallet(fields.Packages, pallet);
    }


    internal void UpdateShippingPallet(string shippingUID, string shippingPalletUID,
                                       ShippingPalletFields fields) {

      //VARIFICAR ELIMINADOS EN LISTA NUEVA Y ELIMINARLOS DE GUARDADOS
      ComparePackagesToRemoveFromPallet(shippingPalletUID, fields.Packages);
      //VERIFICAR LISTA NUEVA SI YA EXISTEN EN GUARDADOS Y AGREGAR LOS QUE NO EXISTEN

      CreatePackagesIfNotExistInPallet(shippingPalletUID, fields.Packages);

      //ValidateIfExistShippingPackages(fields.Packages);

      ShippingPallet pallet = new ShippingPallet(shippingUID, fields, shippingPalletUID);

      pallet.Save();
    }


    private void CreatePackagesIfNotExistInPallet(string shippingPalletUID, string[] packagesUID) {
      //CARGAR LISTA DE GUARDADOS
      var shippingPackages = ShippingData.GetShippingPackagesByPalletUID(shippingPalletUID);

      foreach (var packageUID in packagesUID) {

        if (!shippingPackages.Any(x => x.OrderPacking.UID.Equals(packageUID))) {

          var pallet = ShippingPallet.Parse(shippingPalletUID);
          var shippingPackage = new ShippingPackage(packageUID, pallet);
          shippingPackage.Save();
        }
      }
    }


    private void ComparePackagesToRemoveFromPallet(string shippingPalletUID, string[] packagesUID) {
      //CARGAR LISTA DE GUARDADOS
      var shippingPackages = ShippingData.GetShippingPackagesByPalletUID(shippingPalletUID);

      foreach (var shippingPackage in shippingPackages) {
        
        var packaging = PackageForItem.Parse(shippingPackage.OrderPacking.Id);

        if (!packagesUID.Any(x => x.Equals(packaging.UID))) {
          ShippingData.DeleteShippingPackageById(shippingPackage.ShippingPackageId);
        }
      }
    }


    private void CreatePackagesForPallet(string[] packages, ShippingPallet pallet) {

      foreach (var package in packages) {

        var shippingOrder = new ShippingPackage(package, pallet);
        shippingOrder.Save();

      }
    }


    private void ValidateIfExistShippingPackages(string[] packages) {

      string failMessage = string.Empty;

      foreach (var packageUID in packages) {

        var package = PackageForItem.Parse(packageUID);

        FixedList<ShippingPackage> shippingPackages = 
          ShippingData.GetShippingPackagesByPackageId(package.Id);

        if (shippingPackages.Count > 0) {
          failMessage += $"{package.PackageID}. ";
        }
      }

      if (failMessage != string.Empty) {
        Assertion.EnsureFailed($"Los paquetes {failMessage} ya estan en una tarima.");
      }

    }


    //TODO AGREGAR VALIDACION EN UPDATE
    private void ValidateIfExistPackageInPallet(string[] packages, ShippingPallet pallet) {

      string failMessage = string.Empty;

      foreach (var packageUID in packages) {

        var package = PackageForItem.Parse(packageUID);

        FixedList<ShippingPackage> shippingPackages = 
          ShippingData.GetShippingPackagesByPackageId(package.Id);

        if (shippingPackages.FindAll(x => x.ShippingPallet.Id == pallet.Id).Count > 0) {
          failMessage += $"El paquete {package.PackageID} ya existe en: {pallet.ShippingPalletName}. ";
        }
      }


      if (failMessage != string.Empty) {
        Assertion.EnsureFailed(failMessage);
      }

    }


    internal ShippingEntry CreateShippingOrder(ShippingFields fields) {

      var helper = new ShippingHelper();

      helper.ShippingValidations(helper.GetOrdersForShippingByOrders(fields.Orders));

      ShippingEntry shipping = CreateOrUpdateShipping(fields.ShippingData);

      CreateOrdersForShipping(shipping.ShippingUID, fields.Orders);

      return shipping;
    }


    internal ShippingEntry GetShippingByOrders(string shippingOrderUID, string[] orders) {

      var helper = new ShippingHelper();

      FixedList<ShippingOrderItem> orderForShippingList = helper.GetOrdersForShippingByOrders(orders);

      helper.ShippingValidations(orderForShippingList);

      ShippingEntry shippingEntry = helper.GetShippingWithOrders(orderForShippingList, shippingOrderUID);

      helper.GetShippingWithPallets(shippingEntry);

      return shippingEntry;

    }


    internal ShippingEntry GetShippingByOrderUID(string orderUID) {

      string orderId = Order.Parse(orderUID).Id.ToString();
      var ordersForShipping = ShippingData.GetOrdersForShippingByOrderUID(orderId);

      var helper = new ShippingHelper();
      helper.GetOrdersMeasurementUnits(ordersForShipping);

      ShippingEntry shipping = helper.GetShippingWithOrders(ordersForShipping, "");

      return shipping;
    }


    internal ShippingEntry GetShippingByUID(string shippingOrderUID) {

      return GetShippingByOrders(shippingOrderUID, GetOrdersUIDList(shippingOrderUID));
    }


    internal ShippingEntry GetShippingEntry(string[] orders) {

      var helper = new ShippingHelper();
      return helper.GetShippingEntry(orders);
    }


    internal FixedList<ShippingEntry> GetShippingList(ShippingQuery query) {

      var helper = new ShippingHelper();

      FixedList<ShippingEntry> shippingList = ShippingData.GetShippingOrders("");

      shippingList = shippingList.Where(x => x.ShippingOrderId > 0).ToList().ToFixedList();

      helper.GetOrdersForShippingByEntry(shippingList);

      shippingList = shippingList.OrderByDescending(x => x.CanEdit)
                         .ThenBy(x => x.ParcelSupplierId)
                         .ThenBy(x => x.ShippingDate)
                         .ThenBy(x => x.ShippingGuide).ToFixedList();

      return shippingList;
    }


    internal ShippingEntry UpdateShippingOrder(ShippingFields fields) {

      var helper = new ShippingHelper();

      helper.ShippingValidations(helper.GetOrdersForShippingByOrders(fields.Orders));

      return CreateOrUpdateShipping(fields.ShippingData);
    }

    #endregion Public methods


    #region Private methods


    private ShippingEntry CreateOrUpdateShipping(ShippingDataFields shippingData) {

      var shippingOrder = new ShippingEntry(shippingData);

      shippingOrder.Save();

      return shippingOrder;
    }


    internal string[] GetOrdersUIDList(string shippingOrderUID) {

      var ordersForShipping = ShippingData.GetOrdersForShippingByShippingId(shippingOrderUID);

      List<string> orderList = new List<string>();

      foreach (var order in ordersForShipping) {
        orderList.Add(order.Order.UID);
      }

      List<string> orderList2 = new List<string>();
      orderList2.AddRange(ordersForShipping.Select(x => x.Order.UID));

      return orderList.ToArray();
    }


    #endregion Private methods

  } // class ShippingBuilder
}
