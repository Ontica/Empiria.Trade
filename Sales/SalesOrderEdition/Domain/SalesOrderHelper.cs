/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Sales Order Management                     Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Sales.dll                    Pattern   : Partitioned Type / Information Holder   *
*  Type     : SalesOrderHeleperv                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  :  Helper methods to Seles Order.                                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System.Collections.Generic;

using Empiria.Trade.Core.Inventories.Adapters;
using Empiria.Trade.Core;
using Empiria.Trade.Core.Catalogues;
using System.Linq;
using Empiria.Parties;
using System;

namespace Empiria.Trade.Sales {

  /// <summary>Helper methods to Seles Order. </summary>
  public class SalesOrderHelper {

    private SearchOrderFields _fields;

    public SalesOrderHelper() {

    }

    public SalesOrderHelper(SearchOrderFields fields) {
      _fields = fields;
    }

    #region Public methods

    public FixedList<SalesOrder> GetAuthorizationOrders() {
      //var orders = SalesOrderData.GetSalesOrdersToAuthorize(fields);

      FixedList<SalesOrder> orders = SalesOrderData.GetOrders(_fields);

      FixedList<SalesOrder> ordersByStatus = FilterOrdersForAuthorizationStatus(orders);

      FixedList<SalesOrder> returnedOrders = FilterOrdersByShippingMethod(ordersByStatus);

      GetItemsForOrders(returnedOrders);

      return returnedOrders;
    }


    public FixedList<SalesOrder> GetOrders() {

      var orders = SalesOrderData.GetOrders(_fields);

      FixedList<SalesOrder> ordersByStatus = FilterOrdersByStatus(orders);

      FixedList<SalesOrder> returnedOrders = FilterOrdersByShippingMethod(ordersByStatus);

      GetItemsForOrders(returnedOrders);

      return returnedOrders;
    }


    internal FixedList<SalesOrder> GetOrdersByCustomer(int customerId) {

      var orders = SalesOrderData.GetSalesByCustomer(customerId);

      GetItemsForOrders(orders);

      return orders;
    }


    public FixedList<SalesOrder> GetOrdersToPacking() {

      //var _orders = SalesOrderData.GetSalesOrdersToPacking(_fields);

      FixedList<SalesOrder> orders = SalesOrderData.GetOrders(_fields);

      FixedList<SalesOrder> ordersByStatus = FilterOrdersForPackingStatus(orders);

      FixedList<SalesOrder> returnedOrders = FilterOrdersByShippingMethod(ordersByStatus);

      GetItemsForOrders(returnedOrders);

      return returnedOrders;
    }


    internal SalesOrder GetSalesOrder(string orderNumber) {
      var order = SalesOrderData.GetSalesOrder(orderNumber);

      order.GetItemsAndOrderTotal();

      return order;
    }

    #endregion Public methods

    #region Private methods

    private FixedList<SalesOrder> FilterOrdersForAuthorizationStatus(FixedList<SalesOrder> orders) {

      if (_fields.Status == OrderStatus.Authorized) {

        return orders.FindAll(x => x.SalesOrderProcessStatus == OrderStatus.Authorized.ToString());

      } else if (_fields.Status == OrderStatus.Applied) {

        return orders.FindAll(x => x.SalesOrderProcessStatus == OrderStatus.Applied.ToString());

      } else if (_fields.Status == OrderStatus.Empty) {

        return orders.FindAll(x => x.SalesOrderProcessStatus == OrderStatus.Applied.ToString() ||
                                 x.SalesOrderProcessStatus == OrderStatus.Authorized.ToString());
      }

      return new FixedList<SalesOrder>();
    }


    private FixedList<SalesOrder> FilterOrdersByShippingMethod(FixedList<SalesOrder> orders) {

      if (_fields.ShippingMethod != ShippingMethods.None) {

        return orders.Where(x => x.ShippingMethod == _fields.ShippingMethod.ToString()).ToFixedList();
      }

      return orders;
    }


    private FixedList<SalesOrder> FilterOrdersByStatus(FixedList<SalesOrder> orders) {

      if (_fields.Status == OrderStatus.Cancelled) {

        return orders.FindAll(x => x.SalesOrderProcessStatus == OrderStatus.Cancelled.ToString());

      } else if (_fields.Status == OrderStatus.Authorized) {

        return orders.FindAll(x => x.SalesOrderProcessStatus == OrderStatus.Authorized.ToString());

      } else if (_fields.Status != OrderStatus.Empty) {

        return orders.FindAll(x => x.SalesOrderProcessStatus == _fields.Status.ToString());

      }

      return new FixedList<SalesOrder>(orders);
    }


    private FixedList<SalesOrder> FilterOrdersForPackingStatus(FixedList<SalesOrder> orders) {

      if (_fields.Status == OrderStatus.Suppled) {

        //status = "  AND ((OrderStatus = 'S') or (OrderStatus = 'D') or (OrderStatus = 'F')) ";
        return orders.FindAll(x => x.SalesOrderProcessStatus == OrderStatus.Shipping.ToString() ||
                                   x.SalesOrderProcessStatus == OrderStatus.Delivery.ToString() ||
                                   x.SalesOrderProcessStatus == OrderStatus.Closed.ToString());

      } else if (_fields.Status == OrderStatus.ToSupply || _fields.Status == OrderStatus.Packing) {

        //status = " AND (OrderStatus = 'P') ";
        return orders.FindAll(x => x.SalesOrderProcessStatus == OrderStatus.Packing.ToString() ||
                                   x.SalesOrderProcessStatus == OrderStatus.Authorized.ToString());

      } else if (_fields.Status == OrderStatus.InProgress) {

        //status = " AND (OrderAuthorizationStatus = 'U') ";
        return orders.FindAll(x => x.SalesOrderProcessStatus == OrderStatus.InProgress.ToString());

      } else if (_fields.Status == OrderStatus.Empty) {

        //status = "  AND ((OrderStatus = 'P') or (OrderStatus = 'S') or (OrderStatus = 'D') or (OrderStatus = 'F')) ";
        return orders.FindAll(x => x.SalesOrderProcessStatus == OrderStatus.Authorized.ToString() ||
                                   x.SalesOrderProcessStatus == OrderStatus.Packing.ToString() ||
                                   x.SalesOrderProcessStatus == OrderStatus.Shipping.ToString() ||
                                   x.SalesOrderProcessStatus == OrderStatus.Delivery.ToString() ||
                                   x.SalesOrderProcessStatus == OrderStatus.Closed.ToString());
      }

      return new FixedList<SalesOrder>();
    }


    private FixedList<InventoryItems> GetDataForInventoryOutput(FixedList<SalesOrderItem> salesOrderItems) {

      var dataForInventoryList = new List<InventoryItems>();
      foreach (var item in salesOrderItems) {

        var data = new InventoryItems();
        data.OrderId = item.SalesOrder.Id;
        data.OrderItemId = item.Id;
        //data.VendorProductUID = item.VendorProduct.VendorProductUID;
        data.Quantity = item.Quantity;
        data.WarehouseBinUID = WarehouseBin.Parse(-1).WarehouseBinUID;

        dataForInventoryList.Add(data);
      }

      return dataForInventoryList.ToFixedList();
    }


    private void GetItemsForOrders(FixedList<SalesOrder> orders) {

      foreach (var order in orders) {
        //TODO MODIFICAR ESTE CODIGO
        order.Customer = Party.Parse(order.Beneficiary.Id);
        order.Supplier = Party.Parse(order.Provider.Id);
        order.SalesAgent = Party.Parse(order.Responsible.Id);

        order.GetItemsAndOrderTotal();
      }
    }

    

    #endregion Private methods

  } // class SalesOrderHelper

} // Empiria.Trade.Sales