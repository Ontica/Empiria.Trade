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
using Empiria.Trade.Core;
using Empiria.Trade.Inventory;
using Empiria.Trade.Inventory.Adapters;
using Empiria.Trade.Inventory.Domain;

namespace Empiria.Trade.Inventory.Data {


  /// <summary>Provides data read methods for inventory order.</summary>
  internal class InventoryOrderData {


    #region Public methods

    static internal void DeleteInventoryItemByOrderUID(string inventoryOrderUID) {

      var inventoryId = InventoryOrderEntry.Parse(inventoryOrderUID).Id;

      string sql = $"DELETE FROM TRDInventoryOrderItems " +
                   $"WHERE InventoryOrderId = '{inventoryId}' ";

      var dataOperation = DataOperation.Parse(sql);

      DataWriter.Execute(dataOperation);
    }


    static internal void DeleteInventoryItemByUID(string inventoryOrderItemUID) {

      var itemId = InventoryOrderItem.Parse(inventoryOrderItemUID).Id;

      string sql = $"DELETE FROM TRDInventoryOrderItems " +
                   $"WHERE InventoryOrderItemId = '{itemId}' ";

      var dataOperation = DataOperation.Parse(sql);

      DataWriter.Execute(dataOperation);
    }


    static internal void DeleteInventoryOrderByUID(string inventoryOrderUID) {

      var inventoryId = InventoryOrderEntry.Parse(inventoryOrderUID).Id;

      string sql = $"DELETE FROM TRDInventoryOrders " +
                   $"WHERE InventoryOrderId = '{inventoryId}' ";

      var dataOperation = DataOperation.Parse(sql);

      DataWriter.Execute(dataOperation);
    }


    static internal FixedList<InventoryOrderItem> GetInventoryItemsByInventoryOrder(
      int inventoryOrderId) {

      string sql = $"SELECT * FROM TRDInventoryOrderItems WHERE " +
                   $"InventoryOrderId IN ({inventoryOrderId})";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<InventoryOrderItem>(dataOperation);
    }


    static internal FixedList<InventoryEntry> GetInventoryItemsBySalesOrderAndWhBin(
                                        int orderItemId, int warehouseBinId) {

      string sql = $"SELECT * FROM TRDInventoryOrderItems " +
                   $"WHERE ItemReferenceId = {orderItemId} AND WarehouseBinId = {warehouseBinId} ";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<InventoryEntry>(dataOperation);
    }


    static internal FixedList<InventoryOrderEntry> GetInventoryOrderList(InventoryOrderQuery query) {

      string clauses = GetClausesFromQuery(query);
      string sql = $"SELECT * FROM TRDInventoryOrders WHERE InventoryOrderId > 0 {clauses}";
      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<InventoryOrderEntry>(dataOperation);
    }


    static internal FixedList<InventoryOrderEntry> GetInventoryOrderBySalesOrder(
      int inventoryOrderTypeId, int referenceId) {

      string sql = $"SELECT * FROM TRDInventoryOrders " +
                   $"WHERE InventoryOrderTypeId = {inventoryOrderTypeId} " +
                   $"AND ReferenceId = {referenceId}";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<InventoryOrderEntry>(dataOperation);
    }


    static internal InventoryOrderEntry GetInventoryOrderByUID(string inventoryOrderUID) {

      string sql = $"SELECT * FROM TRDInventoryOrders WHERE InventoryOrderUID IN ('{inventoryOrderUID}')";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObject<InventoryOrderEntry>(dataOperation);
    }


    internal static void WriteInventoryEntry(InventoryOrderEntry entry) {

      var op = DataOperation.Parse("writeInventoryOrder",
          entry.InventoryOrderId, entry.InventoryOrderUID,
          entry.InventoryOrderTypeId, entry.InventoryOrderNo,
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

    static private string GetClausesFromQuery(InventoryOrderQuery query) {
      var orderType = string.Empty;

      if (query.InventoryOrderTypeUID != string.Empty) {
        var inventoryOrderTypeId = new InventoryOrderEntry()
          .GetInventoryOrderTypeId(query.InventoryOrderTypeUID); //TODO CREAR REGISTROS EN TYPES Y PARSEAR;

        orderType = $"InventoryOrderTypeId = {inventoryOrderTypeId}";
      }

      var filters = new Filter(orderType);

      if (query.AssignedToUID != string.Empty) {
        filters.AppendAnd($"AssignedToId = {Party.Parse(query.AssignedToUID).Id}");
      }

      if (query.Keywords != string.Empty) {
        filters.AppendAnd($"{SearchExpression.ParseAndLikeKeywords("InventoryOrderKeywords", query.Keywords)}");
      }

      if (query.Status != InventoryStatus.Todos) {
        filters.AppendAnd($"InventoryOrderStatus = '{(char) query.Status}'");
      }

      return filters.ToString().Length > 0 ? $"AND {filters}" : "";
    }


    static internal void CloseInventoryOrder(int inventoryOrderId, InventoryStatus status) {
      
      string sql = $"UPDATE TRDInventoryOrders SET " +
                   $"InventoryOrderStatus = '{(char) status}', " +
                   $"ClosingTime = '{ConvertDateTimeToString()}' " +
                   $"WHERE InventoryOrderId IN('{inventoryOrderId}')";

      var dataOperation = DataOperation.Parse(sql);

      DataWriter.Execute(dataOperation);
    }


    static internal void CloseInventoryOrderForSalesOrder(int inventoryOrderTypeId, int referenceId) {

      string sql = $"UPDATE TRDInventoryOrders SET " +
                   $"InventoryOrderStatus = '{(char) InventoryStatus.Cerrado}' " +
                   $",ClosingTime = '{ConvertDateTimeToString()}' " +
                   $"WHERE InventoryOrderTypeId = {inventoryOrderTypeId} " +
                   $"AND ReferenceId = {referenceId} ";

      var dataOperation = DataOperation.Parse(sql);

      DataWriter.Execute(dataOperation);
    }


    internal static void CloseInventoryOrderItemsForSales(int inventoryOrderId) {

      string sql = $"UPDATE TRDInventoryOrderItems SET " +
                   $"InventoryOrderItemStatus = '{(char) InventoryStatus.Cerrado}' " +
                   $",OutputQuantity = InProcessOutputQuantity " +
                   $",InProcessOutputQuantity = 0 " +
                   $",InventoryOrderItemNotes = 'APLICADO' " +
                   $",ClosingTime = '{ConvertDateTimeToString()}' " +
                   $"WHERE InventoryOrderId = {inventoryOrderId} ";

      var dataOperation = DataOperation.Parse(sql);

      DataWriter.Execute(dataOperation);
    }

    
    internal static void CloseInventoryItemForInventoryOrder(
      int inventoryOrderItemId, decimal quantityDifference) {

      string sql = $"UPDATE TRDInventoryOrderItems SET " +
                   $"InventoryOrderItemStatus = '{(char) InventoryStatus.Cerrado}' " +
                   $"{GetQuantityClauses(quantityDifference)} " +
                   $",ClosingTime = '{ConvertDateTimeToString()}' " +
                   $"WHERE InventoryOrderItemId = {inventoryOrderItemId} ";

      var dataOperation = DataOperation.Parse(sql);

      DataWriter.Execute(dataOperation);
    }


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
