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


    internal void CreateOrUpdateOrdersForShipping(string shippingOrderUID, string[] orders) {

      var shipping = ShippingEntry.Parse(shippingOrderUID);

      foreach (var order in orders) {

        var shippingOrder = new ShippingOrderItem(order, shipping);
        shippingOrder.Save();

      }
    }

    internal ShippingEntry CreateShippingOrder(ShippingFields fields) {

      var helper = new ShippingHelper();

      helper.ValidationsToCreateShipping(helper.GetOrdersForShippingByOrders(fields.Orders));

      ShippingEntry shipping = CreateOrUpdateShipping(fields.ShippingData);

      CreateOrUpdateOrdersForShipping(shipping.ShippingUID, fields.Orders);

      //return GetShippingByOrders(fields.Orders);
      //return GetShippingEntry(fields.Orders);
      return shipping;
    }


    internal ShippingEntry GetShippingByOrders(string[] orders) {

      var helper = new ShippingHelper();

      FixedList<ShippingOrderItem> orderForShippingList = helper.GetOrdersForShippingByOrders(orders);

      helper.ShippingDataValidations(orderForShippingList);

      ShippingEntry shippingEntry = helper.GetShippingWithOrders(orderForShippingList);

      return shippingEntry;

    }


    internal ShippingEntry GetShippingByOrderUID(string orderUID) {

      string orderId = Order.Parse(orderUID).Id.ToString();
      var orderForShippingList = ShippingData.GetOrdersForShippingByOrderUID(orderId);

      var helper = new ShippingHelper();

      ShippingEntry shipping = helper.GetShippingWithOrders(orderForShippingList);

      return shipping;
    }


    internal ShippingEntry GetShippingByUID(string shippingOrderUID) {

      return GetShippingByOrders(GetOrdersUIDList(shippingOrderUID));
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

      return shippingList;
    }


    internal ShippingEntry UpdateShippingOrder(ShippingFields fields) {

      var helper = new ShippingHelper();

      helper.ValidationsToCreateShipping(helper.GetOrdersForShippingByOrders(fields.Orders));

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
