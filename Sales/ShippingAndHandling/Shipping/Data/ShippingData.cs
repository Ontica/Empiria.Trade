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


    static internal void DeleteShippingOrderItem(string orderUID) {

      var orderId = SalesOrder.Parse(orderUID).Id;

      string sql = $"DELETE FROM TRDShippingOrderItems " +
                   $"WHERE OrderId = '{orderId}' ";

      var dataOperation = DataOperation.Parse(sql);

      DataWriter.Execute(dataOperation);

    }


    internal FixedList<SimpleObjectData> GetParcelSupplierList() {

      string sql = "SELECT * FROM SimpleObjects WHERE ObjectStatus = 'A' AND ObjectTypeId = 1063";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<SimpleObjectData>(dataOperation);
    }


    static internal FixedList<ShippingEntry> GetShippingOrders(string shippingUID) {

      var clauses = string.Empty;

      if (shippingUID != string.Empty) {

        var shippingId = ShippingEntry.Parse(shippingUID).ShippingOrderId;
        clauses = $"WHERE ShippingOrderId IN ({shippingId})";
      }

      string sql = $"SELECT * FROM TRDShipping {clauses}";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<ShippingEntry>(dataOperation);
    }


    static internal FixedList<ShippingOrderItem> GetOrdersForShippingByOrderUID(string orderIdList) {

      if (orderIdList == string.Empty) {
        return new FixedList<ShippingOrderItem>();
      }

      string sql = $"SELECT * FROM TRDShippingOrderItems where OrderId IN ({orderIdList})";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<ShippingOrderItem>(dataOperation);
    }


    static internal FixedList<ShippingOrderItem> GetShippingOrderItemByShippingOrderUID(int shippingId) {

      string sql = $"SELECT * FROM TRDShippingOrderItems where ShippingOrderId IN ({shippingId})";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<ShippingOrderItem>(dataOperation);
    }


    internal static FixedList<ShippingOrderItem> GetShippingOrderItemList(string[] OrderUIDs) {

      string orderIdList = GetOrderIdList(OrderUIDs);

      var shippingOrderItem = GetOrdersForShippingByOrderUID(orderIdList);

      if (shippingOrderItem.Count == 0) {
        return new FixedList<ShippingOrderItem>();
      }

      return shippingOrderItem;
    }


    internal static void WriteShipping(ShippingEntry shipping) {

      var op = DataOperation.Parse("writeShipping",
        shipping.ShippingOrderId, shipping.ParcelSupplierId,
        shipping.ShippingUID, shipping.ShippingGuide, shipping.ParcelAmount,
        shipping.CustomerAmount, shipping.ShippingDate, shipping.DeliveryDate);

      DataWriter.Execute(op);
    }


    static internal void WriteShippingOrderItem(ShippingOrderItem shippingOrderItem) {
      var op = DataOperation.Parse("writeShippingOrderItems",
        shippingOrderItem.ShippingOrderItemId,
        shippingOrderItem.ShippingOrderItemUID,
        shippingOrderItem.ShippingOrder.ShippingOrderId,
        shippingOrderItem.Order.Id);

      DataWriter.Execute(op);
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
