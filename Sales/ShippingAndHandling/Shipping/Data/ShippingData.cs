/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : ShippingAndHandlingData Management         Component : Data Layer                              *
*  Assembly : Empiria.Trade.ShippingAndHandlingData.dll  Pattern   : Data Service                            *
*  Type     : PackagingData                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data read and write methods for shipping.                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections;
using System.Collections.Generic;
using Empiria.Data;
using Empiria.Trade.Core.Common;
using Empiria.Trade.Orders;
using Empiria.Trade.Sales.ShippingAndHandling.Domain;

namespace Empiria.Trade.Sales.ShippingAndHandling.Data {

  /// <summary>Provides data read and write methods for shipping.</summary>
  internal class ShippingData {


    #region Public methods


    internal FixedList<SimpleObjectData> GetParcelSupplierList() {

      string sql = "SELECT * FROM SimpleObjects WHERE ObjectStatus = 'A' AND ObjectTypeId = 1063";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<SimpleObjectData>(dataOperation);
    }


    internal static FixedList<ShippingOrderItem> GetShippingOrderItemList(string[] OrderUIDs) {

      string orderIdList = GetOrderIdList(OrderUIDs);

      var shippingOrderItem = GetShippingOrderItemByOrderUID(orderIdList);
      
      if (shippingOrderItem.Count == 0) {
        return new FixedList<ShippingOrderItem>();
      }

      return shippingOrderItem;
    }


    internal static FixedList<ShippingOrderItem> GetShippingOrderItemByOrderUID(string orderUIDs) {

      if (orderUIDs == string.Empty) {
        return new FixedList<ShippingOrderItem>();
      }

      string sql = $"SELECT * FROM TRDShippingOrderItems where OrderId IN ({orderUIDs})";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<ShippingOrderItem>(dataOperation);
    }


    internal static void WriteShipping(ShippingEntry shipping) {

      var op = DataOperation.Parse("writeShipping",
        shipping.ShippingOrderId, shipping.ParcelSupplierId,
        shipping.ShippingUID, shipping.ShippingGuide, shipping.ParcelAmount,
        shipping.CustomerAmount, shipping.ShippingDate, shipping.DeliveryDate);

      DataWriter.Execute(op);
    }


    static internal ShippingEntry GetShippingByOrderUIDList(Adapters.ShippingQuery query) {
      return new ShippingEntry();
    }


    #endregion Public methods


    #region Private methods

    static private string GetOrderIdList(string[] orderUIDs) {

      if (orderUIDs.Length == 0) {
        return string.Empty;
      }

      string orderIdList = "";
      
      foreach (var uid in orderUIDs) {
        
        int orderId = Order.Parse(uid).Id;
        orderIdList += $"{orderId},";
      }
      
      orderIdList = orderIdList.Remove(orderIdList.Length - 1, 1);

      return orderIdList;
    }

    #endregion Private methods

  } // class ShippingData

} // namespace Empiria.Trade.ShippingAndHandling.Data
