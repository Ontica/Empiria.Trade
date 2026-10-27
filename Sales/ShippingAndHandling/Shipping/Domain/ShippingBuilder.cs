/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Shipping Management                        Component : Domain Layer                            *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Partitioned Type / Information Holder   *
*  Type     : ShippingBuilder                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Generate data for Shipping.                                                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System.Collections.Generic;
using System.Linq;
using Empiria.Trade.Core;
using Empiria.Trade.Sales.ShippingAndHandling.Adapters;
using Empiria.Trade.Sales.ShippingAndHandling.Data;

namespace Empiria.Trade.Sales.ShippingAndHandling.Domain {

  /// <summary>Generate data for Shipping.</summary>
  internal class ShippingBuilder {


    #region Constructor

    public ShippingBuilder() {

    }


    #endregion Constructor


    #region Public methods

    static internal void ComparePackagesToRemoveFromPallet(string shippingPalletUID, string[] packagesUID) {

      var shippingPackages = ShippingData.GetShippingPackagesByPallet(shippingPalletUID);

      foreach (var shippingPackage in shippingPackages) {

        var packaging = PackagingEntry.Parse(shippingPackage.OrderPacking.Id);

        var xx = !packagesUID.Any(a => a.Equals(packaging.UID));
        var xxx = packagesUID.Any(b => b.Equals(packaging.UID));

        if (!packagesUID.Any(x => x.Equals(packaging.UID))) {
          ShippingData.DeleteShippingPackageById(shippingPackage.ShippingPackageId);
        }
      }
    }


    static internal void CreatePackagesIfNotExistInPallet(string shippingPalletUID, string[] packagesUID) {

      var shippingPackages = ShippingData.GetShippingPackagesByPallet(shippingPalletUID);

      foreach (var packageUID in packagesUID) {

        if (!shippingPackages.Any(x => x.OrderPacking.UID.Equals(packageUID))) {

          var pallet = ShippingPallet.Parse(shippingPalletUID);
          var shippingPackage = new ShippingPackage(packageUID, pallet);
          shippingPackage.Save();
        }
      }
    }


    internal void CreateShippingOrderItems(string shippingOrderUID, string[] orders) {

      var shipping = ShippingEntry.Parse(shippingOrderUID);

      foreach (var order in orders) {

        var shippingOrder = new ShippingOrderItem(order, shipping);
        shippingOrder.Save();
      }
    }


    internal void CreateShippingPallet(string shippingUID, ShippingPalletFields fields) {

      ShippingPallet pallet = new ShippingPallet(shippingUID, fields);

      var helper = new ShippingHelper();
      helper.ValidateIfExistShippingPackages(fields.Packages);

      pallet.Save();

      CreatePackagesForPallet(fields.Packages, pallet);
    }


    internal ShippingEntry CreateShippingOrder(ShippingFields fields) {

      var helper = new ShippingHelper();

      helper.ShippingValidations(helper.GetShippingOrderItemsByOrdersUID(fields.Orders));

      fields.ShippingData.Status = ShippingStatus.EnCaptura;

      ShippingEntry shippingOrder = new ShippingEntry(fields);

      shippingOrder.Save();

      CreateShippingOrderItems(shippingOrder.UID, fields.Orders);

      return shippingOrder;
    }


    internal void GetActionsByShippingQueryType(ShippingDto shipping, ShippingQueryType queryType) {

      var helper = new ShippingHelper();
      shipping.Actions = helper.GetActionsByShippingQueryType(shipping.ShippingData.Status, queryType);
    }


    static internal string[] GetSalesOrdersUID(string shippingOrderUID) {

      var shippingOrderItems = ShippingData.GetShippingOrderItems(shippingOrderUID);

      List<string> ordersUIDList = new List<string>();

      ordersUIDList.AddRange(shippingOrderItems.Select(x => x.SalesOrder.UID));
      
      return ordersUIDList.ToArray();
    }


    internal ShippingEntry GetShippingByOrders(string shippingOrderUID, string[] orders) {

      var helper = new ShippingHelper();

      FixedList<ShippingOrderItem> orderForShippingList = helper.GetShippingOrderItemsByOrdersUID(orders);

      helper.ShippingValidations(orderForShippingList);

      ShippingEntry shippingEntry = helper.GetShippingWithOrders(shippingOrderUID, orderForShippingList);

      helper.GetShippingNumber(shippingEntry);

      helper.GetShippingWithPallets(shippingEntry);

      return shippingEntry;
    }


    internal ShippingEntry GetShippingByOrderUID(string orderUID) {

      string orderId = SalesOrder.Parse(orderUID).Id.ToString();
      var ordersForShipping = ShippingData.GetShippingOrderItemsByOrdersID(orderId);

      var helper = new ShippingHelper();
      helper.GetShippingOrderItemsMeasurementUnits(ordersForShipping);

      ShippingEntry shipping = helper.GetShippingWithOrders("", ordersForShipping);

      helper.GetShippingNumber(shipping);

      return shipping;
    }


    internal ShippingEntry GetShippingByUID(string shippingOrderUID) {

      var ordersUID = GetSalesOrdersUID(shippingOrderUID);

      return GetShippingByOrders(shippingOrderUID, ordersUID);
    }


    internal ShippingEntry GetShippingEntry(string[] orders) {

      var helper = new ShippingHelper();
      return helper.GetShippingEntry(orders);
    }


    internal FixedList<ShippingEntry> GetShippingList(ShippingQuery query) {

      var helper = new ShippingHelper();

      FixedList<ShippingEntry> shippingList = ShippingData.GetShippingOrdersByQuery(query);

      shippingList = shippingList.Where(x => x.ShippingOrderId > 0).ToList().ToFixedList();

      foreach (var shipping in shippingList) {
        helper.GetShippingNumber(shipping);
      }

      helper.GetSalesOrdersForShippings(shippingList);

      return helper.GetOrderingShippingList(query, shippingList);
    }


    internal ShippingEntry UpdateShippingOrder(ShippingFields fields) {

      var helper = new ShippingHelper();

      FixedList<ShippingOrderItem> shippingOrderItems = helper.GetShippingOrderItemsByOrdersUID(
                                                                fields.Orders);
      
      helper.ShippingValidations(shippingOrderItems);

      ShippingEntry shippingOrder = ShippingEntry.Parse(fields.ShippingData.ShippingUID);

      shippingOrder.Update(fields);

      shippingOrder.Save();

      return shippingOrder;
    }

    #endregion Public methods


    #region Private methods

    private void CreatePackagesForPallet(string[] packages, ShippingPallet pallet) {

      foreach (var package in packages) {

        var shippingOrder = new ShippingPackage(package, pallet);
        shippingOrder.Save();
      }
    }


    

    private ShippingEntry CreateOrUpdateShipping(ShippingFields fields) {

      var shippingOrder = new ShippingEntry(fields);

      shippingOrder.Save();

      return shippingOrder;
    }


    #endregion Private methods

  } // class ShippingBuilder
}
