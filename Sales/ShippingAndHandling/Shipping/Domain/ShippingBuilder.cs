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


    internal ShippingEntry GetShippingByOrders(string[] orders) {
      
      var query = new ShippingQuery() { Orders = orders };
      var helper = new ShippingHelper();

      FixedList<ShippingOrderItem> orderForShippingList = helper.GetShippingOrderItemList(orders);

      helper.ShippingDataValidations(orderForShippingList);

      ShippingEntry shippingEntry = helper.GetShippingEntry(orderForShippingList);

      return shippingEntry;

    }

    
    internal ShippingEntry GetShippingByOrderUID(string orderUID) {

      string orderId = Order.Parse(orderUID).Id.ToString();
      var shippingOrderItemList = ShippingData.GetShippingOrderItemByOrderUID(orderId);

      if (shippingOrderItemList.Count == 0) {
        return new ShippingEntry();
      }

      return shippingOrderItemList.FirstOrDefault().ShippingOrder;
    }


    internal ShippingEntry CreateOrUpdateShipping(ShippingFields fields) {

      var helper = new ShippingHelper();

      FixedList<ShippingOrderItem> orderForShippingList = helper.GetShippingOrderItemList(fields.Orders);

      helper.ShippingDataValidations(orderForShippingList);

      ShippingEntry shippingOrder = CreateOrUpdateShippingOrder(fields.ShippingData);

      FixedList<ShippingOrderItem> shippingOrderItem = CreateOrUpdateShippingOrderItem(
                                                        shippingOrder.UID, fields.Orders);

      ShippingEntry shippingOrderMerged = MergeShippingOrderWithItems(shippingOrder, shippingOrderItem);

      return shippingOrderMerged;
    }

    
    #endregion Public methods


    #region Private methods


    private ShippingEntry CreateOrUpdateShippingOrder(ShippingDataFields shippingData) {

      var shippingOrder = new ShippingEntry(shippingData);

      shippingOrder.Save();

      return shippingOrder;
    }


    private FixedList<ShippingOrderItem> CreateOrUpdateShippingOrderItem(
                                          string shippingOrderUID, string[] orders) {

      var shipping = ShippingEntry.Parse(shippingOrderUID);
      var shippingItems = new List<ShippingOrderItem>();

      foreach (var order in orders) {
        
        var shippingOrder = new ShippingOrderItem(order, shipping);

        shippingOrder.Save();

        shippingItems.Add(shippingOrder);

      }

      return shippingItems.ToFixedList();
    }


    private ShippingEntry MergeShippingOrderWithItems(ShippingEntry shippingOrder,
                                                      FixedList<ShippingOrderItem> shippingOrderItem) {

      shippingOrder.OrdersForShipping = shippingOrderItem;
      
      return shippingOrder;
    }


    #endregion Private methods

  } // class ShippingBuilder
}
