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
using Empiria.Trade.Core.Catalogues;
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


    internal FixedList<ShippingEntry> GetOrderingShippingList(
      ShippingQuery query, FixedList<ShippingEntry> shippingList) {

      FixedList<ShippingEntry> orderingShippingList = new FixedList<ShippingEntry>();

      orderingShippingList = shippingList.OrderByDescending(x => x.Status)
                         .ThenBy(x => x.ParcelSupplierId)
                         .ThenBy(x => x.ShippingDate)
                         .ThenBy(x => x.ShippingGuide).ToFixedList();

      return orderingShippingList;
    }


    internal void GetOrdersForShippingByEntry(FixedList<ShippingEntry> shippingList) {

      foreach (var shipping in shippingList) {

        FixedList<ShippingOrderItem> ordersForShipping =
          ShippingData.GetOrdersForShippingByShippingId(shipping.ShippingUID);

        if (ordersForShipping.Count > 0) {
          GetOrdersMeasurementUnits(ordersForShipping);

          shipping.OrdersForShipping = ordersForShipping;
          shipping.OrdersTotal = ordersForShipping.Sum(x => x.OrderTotal);
          
          var customer = ordersForShipping.First().Order.Customer;
          shipping.Customer = new NamedEntity(customer.UID, customer.Name);
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


    internal ShippingEntry GetShippingEntry(string[] orders) {

      FixedList<ShippingOrderItem> ordersForShipping =
                                   ShippingData.GetOrdersForShippingByOrders(orders);

      FixedList<ShippingOrderItem> ordersList;

      if (ordersForShipping.Count > 0) {

        ordersList = ShippingData.GetOrdersForShippingByShippingId(
              ordersForShipping.First().ShippingOrder.ShippingUID);

        ValidateGetOrdersByShipping(orders, ordersList);

      } else {

        ordersList = GetOrdersWithoutShipping(orders);
      }

      ValidateShippingByCustomer(ordersList);

      ValidateOrdersStatusForParcelDelivery(ordersList);

      ValidateShippingMethodForOrders(ordersList);

      GetOrdersMeasurementUnits(ordersList.ToFixedList());

      ShippingEntry shippingEntry = GetShippingWithOrders(ordersList, "");

      GetShippingWithPallets(shippingEntry);

      return shippingEntry;
    }


    internal ShippingEntry GetShippingWithOrders(
      FixedList<ShippingOrderItem> orderForShippingList, string shippingUID) {

      if (shippingUID == string.Empty && orderForShippingList.Count == 0) {
        return new ShippingEntry();
      }

      ShippingEntry shipping = new ShippingEntry();

      if (shippingUID != string.Empty && orderForShippingList.Count == 0) {

        shipping = ShippingData.GetShippingOrdersByUID(shippingUID).FirstOrDefault();
      } else {

        shipping = ShippingData.GetShippingOrdersByUID(
                                orderForShippingList.First().ShippingOrder.ShippingUID)
                               .FirstOrDefault();

        var customer = orderForShippingList.First().Order.Customer;

        shipping.Customer = new NamedEntity(customer.UID, customer.Name);
        shipping.OrdersForShipping = orderForShippingList;
        shipping.CanEdit = orderForShippingList.First().Order.Status == OrderStatus.Shipping ? true : false;
        shipping.OrdersTotal = orderForShippingList.Sum(x => x.OrderTotal);
      }

      return shipping;
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


    private void GetTotalsForPackagesByPallet(ShippingPallet pallet,
      FixedList<ShippingPackage> shippingPackages) {

      foreach (var shippingPackage in shippingPackages) {

        var usecasePackage = PackagingUseCases.UseCaseInteractor();

        FixedList<PackingItem> packingItems = usecasePackage.GetPackingItemsByOrderPackingUID(
                                   shippingPackage.OrderPacking.OrderPackingUID);

        var packageForItem = PackageForItem.Parse(shippingPackage.OrderPacking.OrderPackingUID);
        var packageType = usecasePackage.GetPackageTypeById(packageForItem.PackageTypeId);

        pallet.TotalWeight += packingItems.Sum(x => x.ItemWeight);
        pallet.TotalVolume += packageType.TotalVolume;
      }

    }


    internal void GetShippingWithPallets(ShippingEntry shippingEntry) {

      if (shippingEntry.ShippingOrderId == -1) {

        shippingEntry.ShippingPallets = new FixedList<ShippingPallet>();

      } else {

        var shippingPallets = ShippingData.GetPalletByShippingUID(shippingEntry.ShippingUID);

        foreach (var pallet in shippingPallets) {

          var shippingPackages = ShippingData.GetShippingPackagesByPalletUID(pallet.ShippingPalletUID);

          pallet.ShippingPackages = shippingPackages.Select(x => x.OrderPacking.OrderPackingUID).ToArray();
          pallet.TotalPackages = shippingPackages.Count();
          GetTotalsForPackagesByPallet(pallet, shippingPackages);
        }

        shippingEntry.ShippingPallets = shippingPallets;

      }

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


    internal void ValidateIfExistShippingPackages(string[] packages) {

      string failPackages = string.Empty;

      foreach (var packageUID in packages) {

        var package = PackageForItem.Parse(packageUID);

        var shippingPackages = ShippingData.GetShippingPackagesByPackageId(package.Id);

        if (shippingPackages.Count > 0) {

          failPackages += $"{package.PackageID}. ";
        }
      }

      if (failPackages != string.Empty) {
        Assertion.EnsureFailed($"Los paquetes {failPackages} ya estan en una tarima.");
      }
    }


    internal void ValidateIfExistPackageInPallet(string[] packages, ShippingPallet pallet) {

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


    internal void ValidateOrdersStatusForParcelDelivery(FixedList<ShippingOrderItem> ordersForShipping) {

      foreach (var order in ordersForShipping) {
        if (order.Order.Status != OrderStatus.Shipping && 
            order.Order.Status != OrderStatus.Delivery &&
            order.Order.Status != OrderStatus.Closed) {
          Assertion.EnsureFailed("El estatus de los pedidos debe ser 'Envío' o 'Entrega'.");
        }
      }

    }


    internal void ValidateShippingByCustomer(FixedList<ShippingOrderItem> ordersForShipping) {

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


    internal void ValidateOrdersByCustomer(string shippingOrderUID, string orderUID) {

      var ordersForShipping = ShippingData.GetOrdersForShippingByShippingId(shippingOrderUID);
      var order = SalesOrder.Parse(orderUID);

      if (ordersForShipping.Select(x => x.Order.Customer.Id).FirstOrDefault() != order.Customer.Id) {
        Assertion.EnsureFailed($"El pedidos {order.OrderNumber} no pertenecen al mismo cliente.");
      }

    }


    internal ShippingActions GetActionsByShippingQueryType(
       ShippingStatus status, ShippingQueryType queryType) {

      ShippingActions shippingActions = new ShippingActions();

      if (queryType == ShippingQueryType.Shipping && status == ShippingStatus.EnCaptura) {

        shippingActions.CanEdit = true;
        shippingActions.CanDelete = true;
        shippingActions.CanCloseEdit = true;

      } else if (queryType == ShippingQueryType.Delivery && status == ShippingStatus.EnProceso) {

        shippingActions.CanPrintShippingLabel = true;
        shippingActions.CanPrintOrder = true;
        shippingActions.CanCloseShipping = true;

      }

      return shippingActions;
    }




    #endregion Private methods


  } // class ShippingHelper


} // namespace Empiria.Trade.Sales.ShippingAndHandling.Domain
