﻿/* Empiria Trade *********************************************************************************************
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


    static internal void DeleteOrderForShipping(string orderUID) {

      var orderId = SalesOrder.Parse(orderUID).Id;

      string sql = $"DELETE FROM TRDShippingOrderItems " +
                   $"WHERE OrderId = '{orderId}' ";

      var dataOperation = DataOperation.Parse(sql);

      DataWriter.Execute(dataOperation);

    }


    internal static void DeleteOrdersForShippingByShippingUID(string shippingOrderUID) {

      var shippingId = ShippingEntry.Parse(shippingOrderUID).Id;

      string sql = $"DELETE FROM TRDShippingOrderItems " +
                   $"WHERE ShippingOrderId = '{shippingId}' ";

      var dataOperation = DataOperation.Parse(sql);

      DataWriter.Execute(dataOperation);
    }


    static internal void DeleteShipping(string shippingOrderUID) {

      var shippingId = ShippingEntry.Parse(shippingOrderUID).Id;

      string sql = $"DELETE FROM TRDShipping " +
                   $"WHERE ShippingOrderId = '{shippingId}' ";

      var dataOperation = DataOperation.Parse(sql);

      DataWriter.Execute(dataOperation);
    }


    internal static void DeleteShippingPalletsByShippingUID(string shippingOrderUID) {
      var shippingId = ShippingEntry.Parse(shippingOrderUID).Id;

      string sql = $"DELETE FROM TRDShippingPallets " +
                   $"WHERE ShippingOrderId = '{shippingId}' ";

      var dataOperation = DataOperation.Parse(sql);

      DataWriter.Execute(dataOperation);
    }


    static internal FixedList<ShippingOrderItem> GetOrdersForShippingByOrderUID(string orderIdList) {

      if (orderIdList == string.Empty) {
        return new FixedList<ShippingOrderItem>();
      }

      string sql = $"SELECT * FROM TRDShippingOrderItems where OrderId IN ({orderIdList})";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<ShippingOrderItem>(dataOperation);
    }


    static internal FixedList<ShippingOrderItem> GetOrdersForShippingByShippingId(string shippingOrderUID) {
      
      var shippingId = ShippingEntry.Parse(shippingOrderUID).ShippingOrderId;

      string sql = $"SELECT * FROM TRDShippingOrderItems where ShippingOrderId IN ({shippingId})";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<ShippingOrderItem>(dataOperation);
    }


    internal static FixedList<ShippingOrderItem> GetOrdersForShippingByOrders(string[] OrderUIDs) {

      string orderIdList = GetOrderIdList(OrderUIDs);

      var shippingOrderItem = GetOrdersForShippingByOrderUID(orderIdList);

      //if (shippingOrderItem.Count == 0) {
      //  return new FixedList<ShippingOrderItem>();
      //}

      return shippingOrderItem;
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


    internal static void WriteShipping(ShippingEntry shipping) {

      var op = DataOperation.Parse("writeShipping",
        shipping.ShippingOrderId, shipping.ParcelSupplierId,
        shipping.ShippingUID, shipping.ShippingGuide, shipping.ParcelAmount,
        shipping.CustomerAmount, shipping.ShippingDate, shipping.DeliveryDate);

      DataWriter.Execute(op);
    }


    static internal void WriteShippingOrderItem(ShippingOrderItem shippingOrderItem) {
      var op = DataOperation.Parse("writeShippingOrderItems",
        shippingOrderItem.OrderForShippingId,
        shippingOrderItem.OrderForShippingUID,
        shippingOrderItem.ShippingOrder.ShippingOrderId,
        shippingOrderItem.Order.Id);

      DataWriter.Execute(op);
    }


    static internal void WriteShippingPallet(ShippingPallet shippingPallet) {
      var op = DataOperation.Parse("writeShippingPallet",
        shippingPallet.ShippingPalletId,
        shippingPallet.ShippingPalletUID,
        shippingPallet.ShippingPalletName,
        shippingPallet.ShippingOrder.ShippingOrderId);

      DataWriter.Execute(op);
    }


    static internal void WriteShippingPackage(ShippingPackage shippingPackage) {

      var op = DataOperation.Parse("writeShippingPackage",
        shippingPackage.ShippingPackageId,
        shippingPackage.ShippingPackageUID,
        shippingPackage.ShippingPallet.ShippingPalletId,
        shippingPackage.OrderPacking.OrderPackingId,
        shippingPackage.Order.Id);

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
