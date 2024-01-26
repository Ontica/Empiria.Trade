﻿/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Shipping Management                        Component : Domain Layer                            *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Partitioned Type / Information Holder   *
*  Type     : ShippingHelper                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Helper methods to build shipping structure.                                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Linq;
using Empiria.Trade.Orders;
using Empiria.Trade.Sales.ShippingAndHandling.Data;
using Empiria.Trade.Sales.ShippingAndHandling.UseCases;

namespace Empiria.Trade.Sales.ShippingAndHandling.Domain {

  /// <summary>Helper methods to build shipping structure.</summary>
  internal class ShippingHelper {

    #region Constructor and parsers

    public ShippingHelper() {

    }

    #endregion Constructor and parsers


    #region Public methods


    public FixedList<ShippingOrderItem> GetShippingOrderItemList(string[] orderUIDs) {

      FixedList<ShippingOrderItem> shippingOrderItemList =
                                   ShippingData.GetShippingOrderItemList(orderUIDs);

      GetShippingOrderItemMeasurementUnits(shippingOrderItemList);

      return shippingOrderItemList;
    }


    internal ShippingEntry GetShippingEntry(FixedList<ShippingOrderItem> orderForShippingList) {
      
      if (orderForShippingList.Count == 0) {
        return new ShippingEntry();
      }

      var shipping = orderForShippingList.FirstOrDefault().ShippingOrder;

      shipping.OrderForShipping = orderForShippingList;

      return shipping;
    }


    internal void ShippingDataValidations(FixedList<ShippingOrderItem> orderForShippingList) {

      foreach (var orderItem in orderForShippingList) {

        ValidateShippingDataByCustomerId(orderItem, orderForShippingList);
        ValidateShippingDataByStatus(orderItem);
        ValidateOrdersByShippingOrder(orderItem, orderForShippingList);

      }

    }


    private void ValidateOrdersByShippingOrder(ShippingOrderItem orderItem,
                                          FixedList<ShippingOrderItem> orderForShippingList) {

      var countShippingId = orderForShippingList.FindAll(x =>
                              x.ShippingOrder.Id == orderItem.ShippingOrder.Id).Count;

      if (countShippingId != orderForShippingList.Count) {
        Assertion.EnsureFailed("Uno o más pedidos no pertenecen al mismo envío!");
      }
    }


    private void ValidateShippingDataByStatus(ShippingOrderItem orderItem) {

      if (orderItem.Order.Status != OrderStatus.CarrierSelector) {
        Assertion.EnsureFailed($"El estatus de uno o más pedidos no es valido!");
      }

    }


    private void ValidateShippingDataByCustomerId(ShippingOrderItem orderItem,
                                                  FixedList<ShippingOrderItem> orderForShippingList) {
      
      var countCustomerId = orderForShippingList.FindAll(x =>
                              x.Order.Customer.Id == orderItem.Order.Customer.Id).Count;
      
      if (countCustomerId != orderForShippingList.Count) {
        Assertion.EnsureFailed("Uno o más pedidos no pertenecen al mismo cliente!");
      }
    }





    #endregion Public methods


    #region Private methods


    private void GetShippingOrderItemMeasurementUnits(FixedList<ShippingOrderItem> shippingOrderItemList) {

      foreach (var orderItem in shippingOrderItemList) {

        var usecasePackage = PackagingUseCases.UseCaseInteractor();

        PackagedData packageInfo = usecasePackage.GetPackagedData(orderItem.Order.UID);

        if (packageInfo.OrderUID != string.Empty) {

          orderItem.TotalPackages = packageInfo.TotalPackages;
          orderItem.TotalWeight = packageInfo.Weight;
          orderItem.TotalVolume = packageInfo.Volume;

        }

      }
    }


    #endregion Private methods



  } // class ShippingHelper


} // namespace Empiria.Trade.Sales.ShippingAndHandling.Domain
