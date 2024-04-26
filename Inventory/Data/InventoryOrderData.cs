﻿/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Trade Core                                 Component : Data Layer                              *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Data Service                            *
*  Type     : InventoryData                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data read methods for inventory order.                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Data;
using Empiria.Trade.Core;
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


    static internal FixedList<InventoryOrderItem> GetInventoryItemsByOrderUID(string inventoryOrderUID) {

      var inventoryId = InventoryOrderEntry.Parse(inventoryOrderUID).Id;

      string sql = $"SELECT * FROM TRDInventoryOrderItems WHERE InventoryOrderId IN ({inventoryId})";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<InventoryOrderItem>(dataOperation);
    }


    static internal FixedList<InventoryOrderEntry> GetInventoryOrderList(InventoryOrderQuery query) {

      string clauses = GetClausesFromQuery(query);
      string sql = $"SELECT * FROM TRDInventoryOrders WHERE InventoryOrderId > 0 {clauses}";
      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<InventoryOrderEntry>(dataOperation);
    }


    static internal FixedList<InventoryOrderEntry> GetInventoryOrderByUID(string inventoryOrderUID) {

      string sql = $"SELECT * FROM TRDInventoryOrders WHERE InventoryEntryUID IN ('{inventoryOrderUID}')";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<InventoryOrderEntry>(dataOperation);

    }


    internal static void WriteInventoryEntry(InventoryOrderEntry entry) {

      var op = DataOperation.Parse("writeInventoryCountOrder",
          entry.InventoryOrderId, entry.InventoryOrderUID,
          entry.InventoryOrderTypeId, entry.InventoryOrderNo,
          entry.ExternalObjectReferenceId, entry.ResponsibleId,
          entry.AssignedToId, entry.Notes,
          entry.InventoryOrderExtData, entry.Keywords,
          entry.ClosingTime, entry.PostingTime,
          entry.PostedById, (char) entry.Status);

      DataWriter.Execute(op);
    }


    internal static void WriteInventoryItem(InventoryOrderItem item) {

      var op = DataOperation.Parse("writeInventoryCountOrderItem",
          item.InventoryOrderItemId, item.InventoryOrderItemUID,
          item.InventoryOrder.InventoryOrderId, item.ExternalObjectItemReferenceId,
          item.ItemNotes, item.VendorProduct.Id,
          item.WarehouseBin.Id, item.Quantity,
          item.InputQuantity, item.OutputQuantity,
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

    #endregion Private methods

  } // class InventoryOrderData

} // namespace Empiria.Trade.Inventory.Data
