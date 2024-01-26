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


    internal ShippingEntry CreateShippingForOrder() {

      var data = new ShippingData();

      throw new NotImplementedException();
    }


    internal ShippingEntry GetCompleteShippingByOrderUIDList(ShippingQuery query) {

      var helper = new ShippingHelper();

      FixedList<ShippingOrderItem> orderForShippingList = helper.GetOrderForShippingList(query.OrderUIDs);

      //TODO VALIDAR QUE TODAS LAS ORDENES SEAN DEL MISMO CLIENTE, MISMO SHIPPING,

      helper.ShippingDataValidations(orderForShippingList);

      ShippingEntry shippingEntry = helper.GetShippingEntry(orderForShippingList);

      return new ShippingEntry();

    }

    
    internal ShippingEntry GetShippingByOrderUID(string orderUID) {

      string orderId = Order.Parse(orderUID).Id.ToString();
      var shippingOrderItemList = ShippingData.GetShippingOrderItemByOrderUID(orderId);

      if (shippingOrderItemList.Count == 0) {
        return new ShippingEntry();
      }

      //ShippingEntry.Parse(shippingOrderItemList.FirstOrDefault().ShippingOrderId);
      return shippingOrderItemList.FirstOrDefault().ShippingOrder;
    }


    #endregion Public methods


    #region Private methods


    #endregion Private methods

  } // class ShippingBuilder
}
