/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Shipping Management                        Component : Domain Layer                            *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Partitioned Type / Information Holder   *
*  Type     : ShippingHelper                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Helper methods to build shipping structure.                                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Empiria.Trade.Orders;
using Empiria.Trade.Sales.Adapters;
using Empiria.Trade.Sales.ShippingAndHandling.Adapters;
using Empiria.Trade.Sales.ShippingAndHandling.Data;
using Empiria.Trade.Sales.ShippingAndHandling.UseCases;
using Empiria.Trade.Sales.UseCases;

namespace Empiria.Trade.Sales.ShippingAndHandling.Domain {

  /// <summary>Helper methods to build shipping structure.</summary>
  internal class ShippingHelper {

    #region Constructor and parsers


    public ShippingHelper() {

    }


    #endregion Constructor and parsers


    #region Public methods


    private FixedList<ShippingOrderItem> GetOrderItemByPackingOrder(string[] orders,
              FixedList<ShippingOrderItem> shippingOrderItemList) {

      var orderItemList = new List<ShippingOrderItem>(shippingOrderItemList);

      foreach (var orderUID in orders) {

        var existShippingOrderItem = orderItemList.FirstOrDefault(x => x.Order.UID == orderUID);

        if (existShippingOrderItem == null) {

          var order = SalesOrder.Parse(orderUID);
          var orderItem = new ShippingOrderItem();

          orderItem.ShippingOrderItemId = -1;
          orderItem.ShippingOrderItemUID = "";
          orderItem.ShippingOrder = ShippingEntry.Parse(-1);
          orderItem.Order = order;

          orderItemList.Add(orderItem);

        }
      }

      return orderItemList.ToFixedList();
    }


    internal ShippingEntry GetShippingWithOrders(FixedList<ShippingOrderItem> orderForShippingList) {

      if (orderForShippingList.Count == 0) {
        return new ShippingEntry();
      }

      ShippingEntry shipping = ShippingData.GetShippingOrders(
                                orderForShippingList[0].ShippingOrder.ShippingUID)
                              .FirstOrDefault();

      shipping.OrdersForShipping = orderForShippingList;
      shipping.CanEdit = orderForShippingList[0].Order.Status == OrderStatus.Shipping ? true : false;
      shipping.OrdersTotal = orderForShippingList.Sum(x => x.OrderTotal);

      return shipping;
    }


    internal void GetShippingOrderItemByEntry(FixedList<ShippingEntry> shippingList) {

      foreach (var shipping in shippingList) {

        FixedList<ShippingOrderItem> ordersForShipping = 
          ShippingData.GetOrdersForShippingByShippingId(shipping.ShippingOrderId);

        GetShippingOrderItemMeasurementUnits(ordersForShipping);

        shipping.OrdersForShipping = ordersForShipping;
        shipping.OrdersTotal = ordersForShipping.Sum(x => x.OrderTotal);

      }
      
    }


    internal FixedList<ShippingOrderItem> GetOrdersForShippingByOrders(string[] orders) {

      FixedList<ShippingOrderItem> shippingOrderItemList =
                                   ShippingData.GetOrdersForShippingByOrders(orders);

      FixedList<ShippingOrderItem> orderItemByPackingOrder =
                                   GetOrderItemByPackingOrder(orders, shippingOrderItemList);

      GetShippingOrderItemMeasurementUnits(orderItemByPackingOrder);

      return orderItemByPackingOrder;
    }


    internal void ShippingDataValidations(FixedList<ShippingOrderItem> orderForShippingList) {

      foreach (var orderItem in orderForShippingList) {

        ValidateShippingDataByCustomerId(orderItem, orderForShippingList);
        ValidateShippingDataByStatus(orderItem);
        //ValidateOrdersByShippingOrder(orderItem, orderForShippingList);

      }

    }


    #endregion Public methods


    #region Private methods


    private FixedList<OrderPackageForShipping> GetPackagedForItemListByOrder(
            FixedList<PackagedForItem> packagedForItem) {

      var packages = new List<OrderPackageForShipping>();

      foreach (var packaging in packagedForItem) {
        var package = new OrderPackageForShipping();

        package.PackingItemUID = packaging.UID;
        package.PackageID = packaging.PackageID;
        package.PackageTypeName = packaging.PackageTypeName;
        package.TotalVolume = packaging.PackageVolume;
        package.TotalWeight = packaging.PackageWeight;

        packages.Add(package);
      }

      return packages.ToFixedList();
    }


    internal void GetShippingOrderItemMeasurementUnits(FixedList<ShippingOrderItem> shippingOrderItemList) {

      foreach (var orderItem in shippingOrderItemList) {

        var usecasePackage = PackagingUseCases.UseCaseInteractor();

        PackagedData packageInfo = usecasePackage.GetPackagedData(orderItem.Order.UID);

        if (packageInfo.OrderUID != string.Empty) {

          var salesOrder = SalesOrder.Parse(orderItem.Order.UID);
          salesOrder.CalculateSalesOrder(QueryType.SalesShipping);

          orderItem.OrderTotal = salesOrder.OrderTotal;
          orderItem.TotalPackages = packageInfo.TotalPackages;
          orderItem.TotalWeight = packageInfo.Weight;
          orderItem.TotalVolume = packageInfo.Volume;

          var packagedForItem = usecasePackage.GetPackagedForItemList(orderItem.Order.UID);

          orderItem.OrderPackages = GetPackagedForItemListByOrder(packagedForItem);
        }

      }
    }


    internal void ValidateOrdersByShippingOrder(ShippingOrderItem orderItem,
                                          FixedList<ShippingOrderItem> orderForShippingList) {
      //TODO CAMBIAR LA VALIDACION CONSULTANDO EN BD LOS ID'S
      var countShippingId = orderForShippingList.FindAll(x =>
                              x.ShippingOrder.Id == orderItem.ShippingOrder.Id).Count;

      if (countShippingId != orderForShippingList.Count) {
        Assertion.EnsureFailed("Uno o más pedidos no pertenecen al mismo envío!");
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


    private void ValidateShippingDataByStatus(ShippingOrderItem orderItem) {

      if (orderItem.Order.Status != OrderStatus.Shipping &&
          orderItem.Order.Status != OrderStatus.Delivery) {
        Assertion.EnsureFailed($"El estatus de uno o más pedidos no es valido!");
      }

    }


    internal void ValidateIfExistOrderForShipping(string orderUID) {

      var order = SalesOrder.Parse(orderUID);
      var orderForShippingList = ShippingData.GetOrdersForShippingByOrderUID(order.Id.ToString());

      if (orderForShippingList.Count > 0) {
        Assertion.EnsureFailed($"El pedido {order.OrderNumber} ya pertenece a un envío!");
      }
    }


    #endregion Private methods



  } // class ShippingHelper


} // namespace Empiria.Trade.Sales.ShippingAndHandling.Domain
