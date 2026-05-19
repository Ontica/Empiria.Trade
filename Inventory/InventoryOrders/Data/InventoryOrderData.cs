/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Trade Core                                 Component : Data Layer                              *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Data Service                            *
*  Type     : InventoryOrderData                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data read methods for inventory order.                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Net.NetworkInformation;
using Empiria.Data;
using Empiria.StateEnums;
using Empiria.Trade.Core;
using Empiria.Trade.Inventory;
using Empiria.Trade.Inventory.Adapters;
using Empiria.Trade.Inventory.Domain;
using Empiria.Trade.Products;

namespace Empiria.Trade.Inventory.Data {


  /// <summary>Provides data read methods for inventory order.</summary>
  internal class InventoryOrderData {

    #region Public methods V2

    internal static FixedList<InventoryOrder> SearchInventoryOrders(string filter, string sort) {

      var sql = "SELECT * FROM OMS_Orders";

      if (!string.IsNullOrWhiteSpace(filter)) {
        sql += $" WHERE {filter}";
      }
      if (!string.IsNullOrWhiteSpace(sort)) {
        sql += $" ORDER BY {sort}";
      }

      var op = DataOperation.Parse(sql);

      return DataReader.GetFixedList<InventoryOrder>(op);
    }


    internal static int VerifyProductAndLocationInOrder(int orderId, int productID, int locationID) {

      var sql = $"select count(*) from OMS_Order_Items where Order_Item_Order_Id = {orderId} " +
                $" and Order_Item_Product_Id = {productID} and Order_Item_Location_Id = {locationID} and Order_Item_Status <> 'X'";

      var op = DataOperation.Parse(sql);

      return DataReader.GetScalar<int>(op);
    }


    internal static FixedList<Core.InventoryOrderItem> SearchMaxOrderItemPosition(InventoryOrder order) {

      var sql = $"SELECT TOP 1 * FROM OMS_Order_Items WHERE " +
                $"Order_Item_Status <> 'X' AND Order_Item_Order_Id = {order.Id} " +
                $"ORDER BY Order_Item_Position DESC";

      var op = DataOperation.Parse(sql);

      return DataReader.GetFixedList<Core.InventoryOrderItem>(op);
    }


    internal static decimal GetProductPriceFromVirtualWarehouse(int productId) {

      var sql = $"SELECT TOP  1 Inv_Entry_Input_Cost FROM " +
                $"OMS_Inventory_Entries " +
                $"where Inv_Entry_Order_Id = -10 and Inv_Entry_Product_Id = {productId} " +
                $"order by Inv_Entry_Time ";

      var op = DataOperation.Parse(sql);

      return DataReader.GetScalar<decimal>(op);
    }


    internal static void DeleteEntry(int orderId, int orderItemId) {

      string sql = $"UPDATE OMS_Inventory_Entries " +
                   $"SET Inv_Entry_Status = 'X' " +
                   $"WHERE Inv_Entry_Order_Id = {orderId} AND " +
                   $"Inv_Entry_Order_Item_Id = {orderItemId}";

      var dataOperation = DataOperation.Parse(sql);

      DataWriter.Execute(dataOperation);
    }

    #endregion Public methods V2


    #region Public methods


    internal static void CloseInventoryOrderItemsForSales(int inventoryOrderId) {

      string sql = $"UPDATE TRDInventoryOrderItems SET " +
                   $"InventoryOrderItemStatus = '{(char) EntityStatus.Closed}' " +
                   $",OutputQuantity = InProcessOutputQuantity " +
                   $",InProcessOutputQuantity = 0 " +
                   $",InventoryOrderItemNotes = 'APLICADO' " +
                   $",ClosingTime = '{ConvertDateTimeToString()}' " +
                   $"WHERE InventoryOrderId = {inventoryOrderId} ";

      var dataOperation = DataOperation.Parse(sql);

      DataWriter.Execute(dataOperation);
    }


    static internal FixedList<InventoryOrderItem> GetInventoryItemsBySalesOrderAndWhBin(
                                        int orderItemId, int warehouseBinId, int vendorProductId) {
      
      string whBinClauses = string.Empty, vendorProductClauses = string.Empty;

      if (warehouseBinId > 0) {
        whBinClauses = $"AND WarehouseBinId = {warehouseBinId}";
      }

      if (vendorProductId > 0) {
        vendorProductClauses = $"AND VendorProductId = {vendorProductId}";
      }

      string sql = $"SELECT * FROM TRDInventoryOrderItems " +
                   $"WHERE ItemReferenceId = {orderItemId} " +
                   $"{whBinClauses} " +
                   $"{vendorProductClauses} ";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<InventoryOrderItem>(dataOperation);
    }


    internal static void WriteInventoryEntry(InventoryOrderEntry entry) {

      var op = DataOperation.Parse("writeInventoryOrder",
          entry.InventoryOrderId, entry.InventoryOrderUID,
          entry.InventoryOrderType.Id, entry.InventoryOrderNo,
          entry.ReferenceId, entry.ResponsibleId,
          entry.AssignedToId, entry.Notes,
          entry.InventoryOrderExtData, entry.Keywords,
          entry.ScheduledTime,
          entry.ClosingTime, entry.PostingTime,
          entry.PostedById, (char) entry.Status);

      DataWriter.Execute(op);
    }


    internal static void WriteInventoryItem(InventoryOrderItem item) {

      var op = DataOperation.Parse("writeInventoryOrderItem",
          item.InventoryOrderItemId, item.InventoryOrderItemUID,
          item.InventoryOrder.InventoryOrderId, item.InventoryOrderTypeItemId,
          item.ItemReferenceId, item.ItemNotes,
          item.VendorProduct.Id, item.WarehouseBin.Id,
          item.CountingQuantity,
          item.InProcessInputQuantity, item.InProcessOutputQuantity,
          item.InputQuantity, item.OutputQuantity,
          item.UnitId, item.InputCost, item.OutputCost,
          item.CurrencyId,
          item.ExtData, item.ClosingTime,
          item.PostingTime, item.PostedById,
          (char) item.Status);

      DataWriter.Execute(op);
    }

    #endregion Public methods


    #region Private methods


    static private string GetQuantityClauses(decimal quantityDifference) {

      if (quantityDifference < 0) {
        return $",OutputQuantity = {Math.Abs(quantityDifference)} ";

      } else if (quantityDifference > 0) {
        return $",InputQuantity = {quantityDifference} ";

      } else {
        return string.Empty;
      }
    }


    static private string ConvertDateTimeToString() {

      return $"{DateTime.Now.Year}/{DateTime.Now.Day}/{DateTime.Now.Month} " +
             $"{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}";
    }


    #endregion Private methods

  } // class InventoryOrderData

} // namespace Empiria.Trade.Inventory.Data
