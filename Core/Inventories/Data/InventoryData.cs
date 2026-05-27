/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Trade Core                                 Component : Data Layer                              *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Data Service                            *
*  Type     : InventoryData                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data read methods for inventory.                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Data;

namespace Empiria.Trade.Core {


  /// <summary>Provides data read methods for inventory.</summary>
  public class InventoryData {

    static public FixedList<InventoryOrderItem> SearchMaxOrderItemPosition(InventoryOrder order) {

      var sql = $"SELECT TOP 1 * FROM OMS_Order_Items WHERE " +
                $"Order_Item_Status <> 'X' AND Order_Item_Order_Id = {order.Id} " +
                $"ORDER BY Order_Item_Position DESC";

      var op = DataOperation.Parse(sql);

      return DataReader.GetFixedList<InventoryOrderItem>(op);
    }


    internal static FixedList<InventoryEntry> GetInventoryEntriesByOrderItem(InventoryOrderItem orderItem) {

      var sql = $"SELECT * FROM OMS_Inventory_Entries " +
                $"WHERE Inv_Entry_Status != 'X' " +
                $"AND Inv_Entry_Order_Id = {orderItem.Order.Id} " +
                $"AND Inv_Entry_Order_Item_Id = {orderItem.Id} ";

      var op = DataOperation.Parse(sql);

      return DataReader.GetFixedList<InventoryEntry>(op);
    }


    static internal FixedList<SalesInventoryStock> GetInventoryStockByVendorProduct(
                                        int vendorProductId, string warehouseBinClauses) {

      if (warehouseBinClauses != string.Empty) {
        warehouseBinClauses = $"AND WarehouseBinId NOT IN ({warehouseBinClauses})";
      }

      string sql = $"SELECT * FROM vwSalesInventoryStock " +
                   $"WHERE VendorProductId = {vendorProductId} " +
                   $"{warehouseBinClauses} ";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<SalesInventoryStock>(dataOperation);

    }


    static internal FixedList<SalesInventoryStock> GetInventoryStockByClauses(string clauses) {

      string sql = $"SELECT * FROM vwSalesInventoryStock {clauses}";
      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<SalesInventoryStock>(dataOperation);
    }


    static internal string GetInventoryStockClauses(string vendorProducts, string warehouseBins) {
      var filters = new Filter();

      if (vendorProducts != string.Empty) {
        filters.AppendAnd($"VendorProductId IN ({vendorProducts})");
      }

      if (warehouseBins != string.Empty) {
        filters.AppendAnd($"WarehouseBinId IN ({warehouseBins})");
      }
      return filters.ToString().Length > 0 ? $"WHERE {filters}" : "";
    }


    internal static decimal GetProductPriceFromHistoricCost(string productName) {

      var productKey = productName.Split('-');

      var sql = $"SELECT top 1 Costo6  FROM Historic_Cost_Initial where Clave = '{productKey[0]}'" +
                $"  order by fecha desc ";

      var op = DataOperation.Parse(sql);

      return DataReader.GetScalar<decimal>(op);

    }


    internal static decimal GetProductPriceFromVirtualWarehouse(int productId) {

      var sql = $"SELECT TOP  1 Inv_Entry_Input_Cost FROM " +
                $"OMS_Inventory_Entries " +
                $"where Inv_Entry_Order_Id = -10 and Inv_Entry_Product_Id = {productId} " +
                $"order by Inv_Entry_Time ";

      var op = DataOperation.Parse(sql);

      return DataReader.GetScalar<decimal>(op);
    }


    internal static void WriteInventoryEntry(InventoryEntry entry) {

      var op = DataOperation.Parse("write_OMS_Inventory_Entry",
          entry.Id, entry.UID,
          entry.InventoryEntryTypeId,
          entry.Order.Id,
          entry.OrderItem.Id,
          entry.Product.Id,
          entry.Sku.Id,
          entry.Location.Id,
          entry.Observations,
          entry.Unit.Id,
          entry.InputQuantity,
          entry.InputCost, entry.OutputQuantity,
          entry.OutputCost, entry.CountingQuantity, entry.CountingCost,
          entry.EntryTime, entry.Tags, entry.ExtData,
          entry.Keywords, entry.Position, entry.PostedBy.Id,
          entry.PostingTime, (char) entry.Status);

      DataWriter.Execute(op);
    }

    #region Private methods


    #endregion Private methods


  } // class InventoryData

} // namespace Empiria.Trade.Core
