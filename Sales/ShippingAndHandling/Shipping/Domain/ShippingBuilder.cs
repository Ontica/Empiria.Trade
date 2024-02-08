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


    internal ShippingEntry CreateOrUpdateShipping(ShippingFields fields) {

      GetOrdersForShippingByOrders(fields.Orders);

      ShippingEntry shipping = CreateOrUpdateShipping(fields.ShippingData);

      CreateOrUpdateOrderForShipping(shipping.ShippingUID, fields.Orders);

      return GetShippingByOrders(fields.Orders);
    }


    internal ShippingEntry UpdateShippingOrder(ShippingFields fields) {

      GetOrdersForShippingByOrders(fields.Orders);

      CreateOrUpdateShipping(fields.ShippingData);

      return GetShippingByOrders(fields.Orders);
    }


    internal ShippingEntry GetShippingByOrders(string[] orders) {

      var helper = new ShippingHelper();

      FixedList<ShippingOrderItem> orderForShippingList = GetOrdersForShippingByOrders(orders);

      ShippingEntry shippingEntry = helper.GetShippingWithOrders(orderForShippingList);

      return shippingEntry;

    }


    internal FixedList<ShippingOrderItem> GetOrdersForShippingByOrders(string[] orders) {

      var helper = new ShippingHelper();

      FixedList<ShippingOrderItem> orderForShippingList = helper.GetOrdersForShippingByOrders(orders);

      helper.ShippingDataValidations(orderForShippingList);

      return orderForShippingList;

    }


    internal ShippingEntry GetShippingWithOrders(FixedList<ShippingOrderItem> orderForShippingList) {

      var helper = new ShippingHelper();

      ShippingEntry shippingEntry = helper.GetShippingWithOrders(orderForShippingList);

      return shippingEntry;

    }


    internal FixedList<ShippingEntry> GetShippingList(ShippingQuery query) {

      var helper = new ShippingHelper();

      FixedList<ShippingEntry> shippingList = ShippingData.GetShippingOrders("");

      shippingList = shippingList.Where(x => x.ShippingOrderId > 0).ToList().ToFixedList();

      helper.GetShippingOrderItemByEntry(shippingList);

      return shippingList;
    }


    internal ShippingEntry GetShippingByOrderUID(string orderUID) {

      string orderId = Order.Parse(orderUID).Id.ToString();
      var orderForShippingList = ShippingData.GetOrdersForShippingByOrderUID(orderId);

      var helper = new ShippingHelper();

      ShippingEntry shipping = helper.GetShippingWithOrders(orderForShippingList);

      return shipping;
    }


    internal ShippingEntry GetShippingByUID(string shippingOrderUID) {

      var shippingOrderId = ShippingEntry.Parse(shippingOrderUID).ShippingOrderId;

      var ordersForShipping = ShippingData.GetOrdersForShippingByShippingId(shippingOrderId);

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


    internal void CreateOrUpdateOrderForShipping(string shippingOrderUID, string[] orders) {

      var shipping = ShippingEntry.Parse(shippingOrderUID);

      foreach (var order in orders) {

        var shippingOrder = new ShippingOrderItem(order, shipping);
        shippingOrder.Save();

      }
    }


    internal ShippingEntry GetShippingEntry(string[] orders) {

      FixedList<ShippingOrderItem> ordersForShipping =
                                   ShippingData.GetOrdersForShippingByOrders(orders);

      FixedList<ShippingOrderItem> ordersList;

      if (ordersForShipping.Count > 0) {

        ordersList = GetAllOrdersForShipping(orders, ordersForShipping);

      } else {

        ordersList = GetOrdersWithoutShipping(orders);

      }

      var helper = new ShippingHelper();
      helper.GetShippingOrderItemMeasurementUnits(ordersList.ToFixedList());

      return helper.GetShippingWithOrders(ordersList);
    }


    private FixedList<ShippingOrderItem> GetAllOrdersForShipping(string[] orders,
                                          FixedList<ShippingOrderItem> ordersForShipping) {

      var helper = new ShippingHelper();

      var ordersList = ShippingData.GetOrdersForShippingByShippingId(
              ordersForShipping[0].ShippingOrder.ShippingOrderId);

      foreach (var order in orders) {
        
        var orderForShipping = ordersList.FirstOrDefault(x => x.Order.UID == order);
        
        if (orderForShipping == null) {
          Assertion.EnsureFailed("Uno o más pedidos no pertenecen al mismo envío!");
        }
      }

      return ordersList;
    }


    private FixedList<ShippingOrderItem> GetOrdersWithoutShipping(string[] orders) {

      var ordersList = new List<ShippingOrderItem>();

      foreach (var orderUID in orders) {

        var existShippingOrderItem = ordersList.FirstOrDefault(x => x.Order.UID == orderUID);

        if (existShippingOrderItem == null) {

          var order = SalesOrder.Parse(orderUID);
          var orderItem = new ShippingOrderItem();

          orderItem.ShippingOrderItemId = -1;
          orderItem.ShippingOrderItemUID = "";
          orderItem.ShippingOrder = ShippingEntry.Parse(-1);
          orderItem.Order = order;

          ordersList.Add(orderItem);

        }
      }

      return ordersList.ToFixedList();
    }


    private FixedList<ShippingOrderItem> GetOrdersForShippingByUID(string shippingOrderUID) {

      var shippingId = ShippingEntry.Parse(shippingOrderUID).ShippingOrderId;

      return ShippingData.GetOrdersForShippingByShippingId(shippingId);
    }


    internal string[] GetOrdersToChangeStatus(string shippingOrderUID) {

      FixedList<ShippingOrderItem> ordersForShipping = GetOrdersForShippingByUID(shippingOrderUID);

      List<string> orderList = new List<string>(); 
      
      foreach (var order in ordersForShipping) {
        orderList.Add(order.Order.UID);
      }
      
      return orderList.ToArray();
    }



    #endregion Private methods

  } // class ShippingBuilder
}
