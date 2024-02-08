/* Empiria Trade *********************************************************************************************
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
using System.Runtime.Remoting.Messaging;
using Empiria.Trade.Core.Common;
using Empiria.Trade.Orders;
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


    internal ShippingEntry CreateOrUpdateShipping(ShippingFields fields) {

      GetOrdersForShipping(fields.Orders);

      CreateOrUpdateShipping(fields.ShippingData);

      return GetShippingByOrders(fields.Orders);
    }


    internal ShippingEntry GetShippingByOrders(string[] orders) {

      var helper = new ShippingHelper();

      FixedList<ShippingOrderItem> orderForShippingList = GetOrdersForShipping(orders);

      ShippingEntry shippingEntry = helper.GetShippingWithOrderItems(orderForShippingList);

      return shippingEntry;

    }


    internal FixedList<ShippingOrderItem> GetOrdersForShipping(string[] orders) {

      var helper = new ShippingHelper();

      FixedList<ShippingOrderItem> orderForShippingList = helper.GetShippingOrderItemList(orders);

      helper.ShippingDataValidations(orderForShippingList);

      return orderForShippingList;

    }


    internal ShippingEntry GetShippingWithOrders(FixedList<ShippingOrderItem> orderForShippingList) {

      var helper = new ShippingHelper();

      ShippingEntry shippingEntry = helper.GetShippingWithOrderItems(orderForShippingList);

      return shippingEntry;

    }


    internal FixedList<ShippingEntry> GetShippingList(ShippingQuery query) {

      var helper = new ShippingHelper();

      FixedList<ShippingEntry> shippingList = ShippingData.GetShippingOrders(query.Keywords);

      helper.GetShippingOrderItemByEntry(shippingList);

      return shippingList.Where(x => x.ShippingOrderId > 0).ToList().ToFixedList();
    }


    internal ShippingEntry GetShippingByOrderUID(string orderUID) {

      string orderId = Order.Parse(orderUID).Id.ToString();
      var orderForShippingList = ShippingData.GetShippingOrderItemByOrderUID(orderId);

      var helper = new ShippingHelper();

      ShippingEntry shipping = helper.GetShippingWithOrderItems(orderForShippingList);

      return shipping;
    }


    internal ShippingEntry GetShippingByUID(string shippingOrderUID) {
      
      var shippingOrderId = ShippingEntry.Parse(shippingOrderUID).ShippingOrderId;

      var ordersForShipping = ShippingData.GetShippingOrderItemByShippingOrderUID(shippingOrderId);

      List<string> orders = new List<string>();

      foreach (var order in ordersForShipping) {
        orders.Add(order.Order.UID);
      }

      return GetShippingByOrders(orders.ToArray());
    }


    #endregion Public methods


    #region Private methods


    private ShippingEntry CreateOrUpdateShipping(ShippingDataFields shippingData) {

      var shippingOrder = new ShippingEntry(shippingData);

      shippingOrder.Save();

      return shippingOrder;
    }


    internal void CreateOrUpdateOrderForShipping(
                                          string shippingOrderUID, string[] orders) {

      var shipping = ShippingEntry.Parse(shippingOrderUID);
      var shippingItems = new List<ShippingOrderItem>();

      foreach (var order in orders) {
        
        var shippingOrder = new ShippingOrderItem(order, shipping);
        shippingOrder.Save();
        shippingItems.Add(shippingOrder);

      }
    }


    private ShippingEntry MergeShippingOrderWithItems(ShippingEntry shippingOrder,
                                                      FixedList<ShippingOrderItem> shippingOrderItem) {

      shippingOrder.OrdersForShipping = shippingOrderItem;
      
      return shippingOrder;
    }


    #endregion Private methods

  } // class ShippingBuilder
}
