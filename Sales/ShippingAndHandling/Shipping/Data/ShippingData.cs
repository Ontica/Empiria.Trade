/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : ShippingAndHandlingData Management         Component : Data Layer                              *
*  Assembly : Empiria.Trade.ShippingAndHandlingData.dll  Pattern   : Data Service                            *
*  Type     : PackagingData                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data read and write methods for shipping.                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.Data;
using Empiria.Trade.Core;
using Empiria.Trade.Core.Common;
using Empiria.Trade.Sales.ShippingAndHandling.Adapters;

namespace Empiria.Trade.Sales.ShippingAndHandling.Data {

  /// <summary>Provides data read and write methods for shipping.</summary>
  internal class ShippingData {


    #region Public methods


    static internal void DeleteOrderForShipping(string orderUID) {

      var orderId = SalesOrder.Parse(orderUID).Id;

      string sql = $"DELETE FROM OMS_Shipping_Order_Item " +
                   $"WHERE Order_Id = '{orderId}' ";

      var dataOperation = DataOperation.Parse(sql);

      DataWriter.Execute(dataOperation);

    }


    internal static void DeleteOrdersForShippingByShippingUID(string shippingOrderUID) {

      var shippingId = ShippingEntry.Parse(shippingOrderUID).Id;

      string sql = $"DELETE FROM OMS_Shipping_Order_Item " +
                   $"WHERE Shipping_Order_Id = {shippingId}";

      var dataOperation = DataOperation.Parse(sql);

      DataWriter.Execute(dataOperation);
    }


    static internal void DeleteShipping(string shippingOrderUID) {

      var shippingId = ShippingEntry.Parse(shippingOrderUID).Id;

      string sql = $"DELETE FROM OMS_Shipping_Order " +
                   $"WHERE Shipping_Order_Id = {shippingId}";

      var dataOperation = DataOperation.Parse(sql);

      DataWriter.Execute(dataOperation);
    }


    internal static void DeleteShippingPalletsByShippingUID(string shippingOrderUID) {
      var shippingId = ShippingEntry.Parse(shippingOrderUID).Id;

      string sql = $"DELETE FROM OMS_Shipping_Pallets " +
                   $"WHERE Shipping_Order_Id = {shippingId}";

      var dataOperation = DataOperation.Parse(sql);

      DataWriter.Execute(dataOperation);
    }


    static internal FixedList<ShippingOrderItem> GetShippingOrderItemsByOrdersID(string orderIdList) {

      if (orderIdList == string.Empty) {
        return new FixedList<ShippingOrderItem>();
      }

      string sql = $"SELECT * FROM OMS_Shipping_Order_Item where Order_Id IN ({orderIdList})";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<ShippingOrderItem>(dataOperation);
    }


    static internal FixedList<ShippingOrderItem> GetShippingOrderItems(string shippingOrderUID) {
      
      var shipping = ShippingEntry.Parse(shippingOrderUID);

      string sql = $"SELECT * FROM OMS_Shipping_Order_Item where Shipping_Order_Id IN ({shipping.Id})";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<ShippingOrderItem>(dataOperation);
    }


    internal static FixedList<ShippingOrderItem> GetShippingOrderItemsByOrdersUID(string[] OrderUIDs) {

      string orderIdList = GetOrderIdList(OrderUIDs);

      return GetShippingOrderItemsByOrdersID(orderIdList);
    }


    internal static FixedList<ShippingEntry> GetShippingOrdersByQuery(ShippingQuery query) {

      var clauses = "WHERE Shipping_Status != 'X' ";
      
      if (query.Keywords != string.Empty) {
        clauses += $"AND {SearchExpression.ParseAndLikeKeywords("Shipping_Order_Keywords", query.Keywords)} ";
      }

      if (query.ParcelSupplierUID != string.Empty) {
        var parcelSupplier = ParcelSupplier.Parse(query.ParcelSupplierUID);
        
        clauses += (clauses != "" ? "AND " : "WHERE ") + $"Parcel_Supplier_Id IN ({parcelSupplier.Id}) ";
      }

      if (query.QueryType == ShippingQueryType.Delivery && query.Status == ShippingStatus.Todos) {

        clauses += (clauses != "" ? "AND " : "WHERE ") + $"Shipping_Status IN ('E', 'C') ";
      }

      if (query.Status != ShippingStatus.Todos) {
        
        clauses += (clauses != "" ? "AND ": "WHERE ") + $"Shipping_Status IN ('{(char)query.Status}') ";
      }

      string sql = $"SELECT * FROM OMS_Shipping_Order {clauses}";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<ShippingEntry>(dataOperation);
    }


    static internal FixedList<ShippingEntry> GetShippingOrdersByUID(string shippingUID) {
      
      var clauses = string.Empty;

      if (shippingUID != string.Empty) {

        clauses = $"WHERE Shipping_Order_UID = '{shippingUID}'";
      }

      string sql = $"SELECT * FROM OMS_Shipping_Order {clauses}";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<ShippingEntry>(dataOperation);
    }


    internal static void WriteShipping(ShippingEntry shipping) {

      var op = DataOperation.Parse("write_OMS_Shipping_Order",
        shipping.ShippingOrderId, shipping.ShippingOrderUID, shipping.ParcelSupplier.Id,
         shipping.ShippingGuide, shipping.ParcelAmount,
        shipping.CustomerAmount, shipping.ShippingDate, shipping.DeliveryDate,
        shipping.Keywords, (char)shipping.ShippingMethod, (char) shipping.Status);

      DataWriter.Execute(op);
    }
    

    static internal void WriteShippingOrderItem(ShippingOrderItem shippingOrderItem) {
      
      var op = DataOperation.Parse("write_OMS_Shipping_Order_Item",
        shippingOrderItem.OrderForShippingId,
        shippingOrderItem.OrderForShippingUID,
        shippingOrderItem.ShippingOrder.Id,
        shippingOrderItem.SalesOrder.Id);

      DataWriter.Execute(op);
    }


    static internal void WriteShippingPackage(ShippingPackage shippingPackage) {

      var op = DataOperation.Parse("write_OMS_Shipping_Packages",
        shippingPackage.ShippingPackageId,
        shippingPackage.ShippingPackageUID,
        shippingPackage.ShippingPallet.ShippingPalletId,
        shippingPackage.OrderPacking.OrderPackingId,
        shippingPackage.Order.Id);

      DataWriter.Execute(op);
    }


    static internal void WriteShippingPallet(ShippingPallet shippingPallet) {
      var op = DataOperation.Parse("write_OMS_Shipping_Pallets",
        shippingPallet.ShippingPalletId,
        shippingPallet.ShippingPalletUID,
        shippingPallet.ShippingOrder.Id,
        shippingPallet.ShippingPalletName);

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

        int orderId = SalesOrder.Parse(uid).Id;
        orderIdList += $"{orderId},";
      }

      orderIdList = orderIdList.Remove(orderIdList.Length - 1, 1);

      return orderIdList;
    }


    static internal FixedList<ShippingPackage> GetShippingPackagesByPackageId(int packageId) {

      string sql = $"SELECT * FROM OMS_Shipping_Packages WHERE Order_Packing_Id IN ({packageId})";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<ShippingPackage>(dataOperation);

    }


    static internal FixedList<ShippingPackage> GetShippingPackagesByPallet(string palletUID) {

      var pallet = ShippingPallet.Parse(palletUID);

      string sql = $"SELECT * FROM OMS_Shipping_Packages WHERE Shipping_Pallet_Id IN ({pallet.Id})";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<ShippingPackage>(dataOperation);
    }


    static internal void DeleteShippingPackageById(int shippingPackageId) {

      string sql = $"DELETE FROM OMS_Shipping_Packages " +
                   $"WHERE Shipping_Package_Id = '{shippingPackageId}' ";

      var dataOperation = DataOperation.Parse(sql);

      DataWriter.Execute(dataOperation);
    }


    static internal FixedList<ShippingPallet> GetPalletByShippingOrder(string shippingUID) {

      var shipping = ShippingEntry.Parse(shippingUID);

      string sql = $"SELECT * FROM OMS_Shipping_Pallets WHERE Shipping_Order_Id IN ({shipping.Id})";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<ShippingPallet>(dataOperation);
    }


    internal static void DeleteShippingPackagesByPalletUID(string shippingPalletUID) {

      var shippingPalletId = ShippingPallet.Parse(shippingPalletUID).ShippingPalletId;

      string sql = $"DELETE FROM OMS_Shipping_Packages " +
                   $"WHERE Shipping_Pallet_Id = '{shippingPalletId}' ";

      var dataOperation = DataOperation.Parse(sql);

      DataWriter.Execute(dataOperation);
    }


    internal static void DeleteShippingPalletByUID(string shippingPalletUID) {

      var shippingPalletId = ShippingPallet.Parse(shippingPalletUID).ShippingPalletId;

      string sql = $"DELETE FROM OMS_Shipping_Pallets " +
                   $"WHERE Shipping_Pallet_Id = '{shippingPalletId}' ";

      var dataOperation = DataOperation.Parse(sql);

      DataWriter.Execute(dataOperation);
    }


    static internal void UpdateShippingStatus(string shippingOrderUID, ShippingStatus status) {

      var shippingId = ShippingEntry.Parse(shippingOrderUID).Id;

      string sql = $"UPDATE OMS_Shipping_Order SET Shipping_Status = '{(char) status}' " +
                   $"WHERE Shipping_Order_Id = {shippingId}";

      var dataOperation = DataOperation.Parse(sql);

      DataWriter.Execute(dataOperation);
    }


    #endregion Private methods

  } // class ShippingData

} // namespace Empiria.Trade.ShippingAndHandling.Data
