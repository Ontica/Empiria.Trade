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

    public FixedList<SalesOrder> GetOrders() {

      var orders = SalesOrderData.GetOrders(_fields);

      FixedList<SalesOrder> ordersByStatus = FilterOrdersByStatus(orders);

      FixedList<SalesOrder> returnedOrders = FilterOrdersByShippingMethod(ordersByStatus);

      GetItemsForOrders(returnedOrders);

      return returnedOrders;
    }

    
    public FixedList<SalesOrder> GetAuthorizationOrders() {
      //var orders = SalesOrderData.GetSalesOrdersToAuthorize(fields);

      FixedList<SalesOrder> orders = SalesOrderData.GetOrders(_fields);

      FixedList<SalesOrder> ordersByStatus = FilterOrdersByAuthorizationStatus(orders);
      
      FixedList<SalesOrder> returnedOrders = FilterOrdersByShippingMethod(ordersByStatus);

      foreach (var order in orders) {
        order.GetItemsAndOrderTotal();
      }

      return orders;
    }


    public FixedList<SalesOrder> GetOrdersToPacking(SearchOrderFields fields) {
      var orders = SalesOrderData.GetSalesOrdersToPacking(fields);
      foreach (var order in orders) {
        order.GetItemsAndOrderTotal();
      }

      return orders;
    }

    #endregion Public methods

    #region Private methods

    private FixedList<SalesOrder> FilterOrdersByAuthorizationStatus(FixedList<SalesOrder> orders) {

      if (_fields.Status == OrderStatus.Authorized) {

        return orders.FindAll(x => x.AuthorizationStatus == OrderStatus.Authorized.ToString());

      } else if (_fields.Status == OrderStatus.Pending) {

        return orders.FindAll(x => x.AuthorizationStatus == OrderStatus.Pending.ToString());

      } else if (_fields.Status == OrderStatus.Empty) {

        return orders.FindAll(x => x.AuthorizationStatus == OrderStatus.Pending.ToString() ||
                                 x.AuthorizationStatus == OrderStatus.Authorized.ToString());
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

        return orders.FindAll(x => x.OrderStatus == OrderStatus.Cancelled.ToString());

      } else if (_fields.Status == OrderStatus.Authorized) {

        return orders.FindAll(x => x.AuthorizationStatus == OrderStatus.Authorized.ToString());

      } else if (_fields.Status != OrderStatus.Empty) {

        return orders.FindAll(x => x.OrderStatus == _fields.Status.ToString());

      }

      return new FixedList<SalesOrder>(orders);
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


    internal FixedList<SalesOrder> GetOrdersByCustomer(int customerId) {
      var orders = SalesOrderData.GetSalesByCustomer(customerId);

      foreach (var order in orders) {

        order.GetItemsAndOrderTotal();
      }
      return orders;
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


    internal SalesOrder GetSalesOrder(string orderNumber) {
      var order = SalesOrderData.GetSalesOrder(orderNumber);

      order.GetItemsAndOrderTotal();

      return order;
    }


    #endregion Private methods

  } // class SalesOrderHelper

} // Empiria.Trade.Sales