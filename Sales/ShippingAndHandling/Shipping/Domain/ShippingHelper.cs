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
using System.Runtime.Remoting.Messaging;
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


    internal void GetOrdersForShippingByEntry(FixedList<ShippingEntry> shippingList) {

      foreach (var shipping in shippingList) {

        FixedList<ShippingOrderItem> ordersForShipping =
          ShippingData.GetOrdersForShippingByShippingId(shipping.ShippingUID);

        if (ordersForShipping.Count>0) {
          GetOrdersMeasurementUnits(ordersForShipping);

          shipping.OrdersForShipping = ordersForShipping;
          shipping.OrdersTotal = ordersForShipping.Sum(x => x.OrderTotal);
          shipping.CanEdit = ordersForShipping[0].Order.Status == OrderStatus.Shipping ? true : false;
        }
      }
    }


    internal FixedList<ShippingOrderItem> GetOrdersForShippingByOrders(string[] orders) {

      FixedList<ShippingOrderItem> shippingOrderItemList =
                                   ShippingData.GetOrdersForShippingByOrders(orders);

      FixedList<ShippingOrderItem> orderItemByPackingOrder =
                                   GetOrdersForShippingIfNotExist(orders, shippingOrderItemList);

      GetOrdersMeasurementUnits(orderItemByPackingOrder);

      return orderItemByPackingOrder;
    }


    internal void GetOrdersMeasurementUnits(FixedList<ShippingOrderItem> ordersForShipping) {

      foreach (var order in ordersForShipping) {

        var usecasePackage = PackagingUseCases.UseCaseInteractor();

        PackagedData packageInfo = usecasePackage.GetPackagedData(order.Order.UID);

        if (packageInfo.OrderUID != string.Empty) {

          var salesOrder = SalesOrder.Parse(order.Order.UID);
          salesOrder.CalculateSalesOrder(QueryType.SalesShipping);

          order.OrderTotal = salesOrder.OrderTotal;
          order.TotalPackages = packageInfo.TotalPackages;
          order.TotalWeight = packageInfo.Weight;
          order.TotalVolume = packageInfo.Volume;

          var packagedList = usecasePackage.GetPackagedForItemList(order.Order.UID);

          order.OrderPackages = GetPackagedListByOrder(packagedList);
        } 

      }
    }


    internal ShippingEntry GetShippingEntry(string[] orders) {

      FixedList<ShippingOrderItem> ordersForShipping =
                                   ShippingData.GetOrdersForShippingByOrders(orders);

      FixedList<ShippingOrderItem> ordersList;

      if (ordersForShipping.Count > 0) {

        ordersList = GetAllOrdersForShipping(ordersForShipping);
        ValidateGetOrdersByShipping(orders, ordersList);

      } else {

        ordersList = GetOrdersWithoutShipping(orders);
      }

      ValidateOrdersStatusForParcelDelivery(ordersList);

      ValidateShippingMethodForOrders(ordersList);

      GetOrdersMeasurementUnits(ordersList.ToFixedList());

      return GetShippingWithOrders(ordersList, "");

    }


    internal ShippingEntry GetShippingWithOrders(
      FixedList<ShippingOrderItem> orderForShippingList, string shippingUID) {

      if (shippingUID == string.Empty && orderForShippingList.Count == 0) {
        return new ShippingEntry();
      }

      ShippingEntry shipping = new ShippingEntry();

      if (shippingUID != string.Empty && orderForShippingList.Count == 0) {

        shipping = ShippingData.GetShippingOrders(shippingUID).FirstOrDefault();
      } else {

        shipping = ShippingData.GetShippingOrders(
                                orderForShippingList[0].ShippingOrder.ShippingUID)
                               .FirstOrDefault();

        shipping.OrdersForShipping = orderForShippingList;
        shipping.CanEdit = orderForShippingList[0].Order.Status == OrderStatus.Shipping ? true : false;
        shipping.OrdersTotal = orderForShippingList.Sum(x => x.OrderTotal);
      }

      return shipping;
    }


    internal void ShippingValidations(FixedList<ShippingOrderItem> ordersForShipping) {
      
      ValidateOrdersStatusForParcelDelivery(ordersForShipping);
      ValidateShippingByCustomer(ordersForShipping);

    }


    internal void ValidateIfExistOrderForShipping(string orderUID) {

      var order = SalesOrder.Parse(orderUID);
      var ordersForShipping = ShippingData.GetOrdersForShippingByOrderUID(order.Id.ToString());

      if (ordersForShipping.Count > 0) {
        Assertion.EnsureFailed($"El pedido {order.OrderNumber} ya pertenece a un envío.");
      }
    }


    #endregion Public methods


    #region Private methods


    private FixedList<ShippingOrderItem> GetAllOrdersForShipping(
      FixedList<ShippingOrderItem> ordersForShipping) {

      var ordersByShipping = ShippingData.GetOrdersForShippingByShippingId(
              ordersForShipping[0].ShippingOrder.ShippingUID);

      return ordersByShipping;
    }


    private FixedList<ShippingOrderItem> GetOrdersForShippingIfNotExist(string[] orders,
              FixedList<ShippingOrderItem> shippingOrderItemList) {

      var orderItemList = new List<ShippingOrderItem>(shippingOrderItemList);

      foreach (var orderUID in orders) {

        var existShippingOrderItem = orderItemList.FirstOrDefault(x => x.Order.UID == orderUID);

        if (existShippingOrderItem == null) {

          var order = SalesOrder.Parse(orderUID);
          var orderItem = new ShippingOrderItem();

          orderItem.OrderForShippingId = -1;
          orderItem.OrderForShippingUID = "";
          orderItem.ShippingOrder = ShippingEntry.Parse(-1);
          orderItem.Order = order;

          orderItemList.Add(orderItem);

        }
      }

      return orderItemList.ToFixedList();
    }


    private FixedList<ShippingOrderItem> GetOrdersWithoutShipping(string[] orders) {

      var ordersList = new List<ShippingOrderItem>();

      foreach (var orderUID in orders) {
        var order = SalesOrder.Parse(orderUID);
        var orderForShipping = new ShippingOrderItem();
        orderForShipping.OrderForShippingId = -1;
        orderForShipping.OrderForShippingUID = "";
        orderForShipping.ShippingOrder = ShippingEntry.Parse(-1);
        orderForShipping.Order = order;
        ordersList.Add(orderForShipping);
      }
      return ordersList.ToFixedList();
    }


    private FixedList<OrderPackageForShipping> GetPackagedListByOrder(
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


    private void ValidateGetOrdersByShipping(string[] orders,
                    FixedList<ShippingOrderItem> ordersByShipping) {

      foreach (var order in orders) {
        var orderForShipping = ordersByShipping.FirstOrDefault(x => x.Order.UID == order);

        if (orderForShipping == null) {
          Assertion.EnsureFailed("Uno o más pedidos no pertenecen al mismo envío.");
        }
      }

    }


    private void ValidateOrdersStatusForParcelDelivery(FixedList<ShippingOrderItem> ordersForShipping) {

      foreach (var order in ordersForShipping) {
        if (order.Order.Status != OrderStatus.Shipping && order.Order.Status != OrderStatus.Delivery) {
          Assertion.EnsureFailed("El estatus de los pedidos debe ser 'Envío' o 'Entrega'.");
        }
      }

    }


    private void ValidateShippingByCustomer(FixedList<ShippingOrderItem> ordersForShipping) {

      foreach (var order in ordersForShipping) {

        var countCustomerId = ordersForShipping.FindAll(x =>
                              x.Order.Customer.Id == order.Order.Customer.Id).Count;

        if (countCustomerId != ordersForShipping.Count) {
          Assertion.EnsureFailed("Uno o más pedidos no pertenecen al mismo cliente.");
        }
      }
    }


    private void ValidateShippingMethodForOrders(FixedList<ShippingOrderItem> ordersForShipping) {

      foreach (var order in ordersForShipping) {
        if (order.Order.ShippingMethod != "Paqueteria") {
          Assertion.EnsureFailed("La forma de envío de uno o mas pedidos no es 'Paquetería'.");
        }
      }

    }


    #endregion Private methods


  } // class ShippingHelper


} // namespace Empiria.Trade.Sales.ShippingAndHandling.Domain
