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
using Empiria.DataTypes;
using Empiria.Trade.Core.Catalogues;
using Empiria.Trade.Core.Common;
using Empiria.Trade.Orders;
using Empiria.Trade.Sales.ShippingAndHandling.Adapters;
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

      return shippingOrderItem;
    }


    internal static FixedList<ShippingEntry> GetShippingOrdersByQuery(ShippingQuery query) {

      var clauses = string.Empty;
      
      if (query.Keywords != string.Empty) {
        clauses += $"WHERE {SearchExpression.ParseAndLikeKeywords("ShippingKeywords", query.Keywords)} ";
      }

      if (query.ParcelSupplierUID != string.Empty) {
        var parcelSupplierId = SimpleObjectData.Parse(query.ParcelSupplierUID).Id;
        
        clauses += (clauses != "" ? "AND " : "WHERE ") + $"ParcelSupplierId IN ({parcelSupplierId}) ";
      }

      if (query.Status != ShippingStatus.Todos) {
        
        clauses += (clauses != "" ? "AND ": "WHERE ") + $"ShippingStatus IN ('{(char)query.Status}') ";
      }

      string sql = $"SELECT * FROM TRDShipping {clauses}";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<ShippingEntry>(dataOperation);
    }


    static internal FixedList<ShippingEntry> GetShippingOrdersByUID(string shippingUID) {
      
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
        shipping.CustomerAmount, shipping.ShippingDate, shipping.DeliveryDate,
        shipping.Keywords, shipping.Status);

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


    static internal FixedList<ShippingPackage> GetShippingPackagesByPackageId(int packageId) {

      string sql = $"SELECT * FROM TRDShippingPackages WHERE OrderPackingId IN ({packageId})";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<ShippingPackage>(dataOperation);

    }


    static internal FixedList<ShippingPackage> GetShippingPackagesByPalletUID(string palletUID) {

      var palletId = ShippingPallet.Parse(palletUID).Id;

      string sql = $"SELECT * FROM TRDShippingPackages WHERE ShippingPalletId IN ({palletId})";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<ShippingPackage>(dataOperation);

    }


    static internal void DeleteShippingPackageById(int shippingPackageId) {

      string sql = $"DELETE FROM TRDShippingPackages " +
                   $"WHERE ShippingPackageId = '{shippingPackageId}' ";

      var dataOperation = DataOperation.Parse(sql);

      DataWriter.Execute(dataOperation);
    }


    static internal FixedList<ShippingPallet> GetPalletByShippingUID(string shippingUID) {

      var shippingId = ShippingEntry.Parse(shippingUID).ShippingOrderId;

      string sql = $"SELECT * FROM TRDShippingPallets WHERE ShippingOrderId IN ({shippingId})";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<ShippingPallet>(dataOperation);

    }


    internal static void DeleteShippingPackageByPalletUID(string shippingPalletUID) {

      var shippingPalletId = ShippingPallet.Parse(shippingPalletUID).ShippingPalletId;

      string sql = $"DELETE FROM TRDShippingPackages " +
                   $"WHERE ShippingPalletId = '{shippingPalletId}' ";

      var dataOperation = DataOperation.Parse(sql);

      DataWriter.Execute(dataOperation);
    }


    internal static void DeleteShippingPalletByUID(string shippingPalletUID) {

      var shippingPalletId = ShippingPallet.Parse(shippingPalletUID).ShippingPalletId;

      string sql = $"DELETE FROM TRDShippingPallets " +
                   $"WHERE ShippingPalletId = '{shippingPalletId}' ";

      var dataOperation = DataOperation.Parse(sql);

      DataWriter.Execute(dataOperation);
    }


    #endregion Private methods

  } // class ShippingData

} // namespace Empiria.Trade.ShippingAndHandling.Data
