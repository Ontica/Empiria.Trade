/* Empiria Trade *********************************************************************************************
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
using Empiria.Trade.Inventory.Domain;

namespace Empiria.Trade.Inventory.Data {


  /// <summary>Provides data read methods for inventory order.</summary>
  internal class InventoryOrderData {


    #region Public methods

    static internal void DeleteInventoryItemByOrderUID(string inventoryUID) {

      var inventoryId = InventoryOrderEntry.Parse(inventoryUID).Id;

      string sql = $"DELETE FROM TRDInventoryOrderItems " +
                   $"WHERE InventoryEntryId = '{inventoryId}' ";

      var dataOperation = DataOperation.Parse(sql);

      DataWriter.Execute(dataOperation);
    }


    static internal void DeleteInventoryItemByUID(string inventoryItemUID) {

      var itemId = InventoryOrderItem.Parse(inventoryItemUID).Id;

      string sql = $"DELETE FROM TRDInventoryOrderItems " +
                   $"WHERE InventoryItemId = '{itemId}' ";

      var dataOperation = DataOperation.Parse(sql);

      DataWriter.Execute(dataOperation);
    }


    static internal void DeleteInventoryOrderByUID(string inventoryUID) {

      var inventoryId = InventoryOrderEntry.Parse(inventoryUID).Id;

      string sql = $"DELETE FROM TRDInventoryOrders " +
                   $"WHERE InventoryEntryId = '{inventoryId}' ";

      var dataOperation = DataOperation.Parse(sql);

      DataWriter.Execute(dataOperation);
    }


    static internal FixedList<InventoryOrderItem> GetInventoryItemsByOrderUID(string inventoryUID) {

      var inventoryId = InventoryOrderEntry.Parse(inventoryUID).Id;

      string sql = $"SELECT * FROM TRDInventoryOrderItems WHERE InventoryEntryId IN ({inventoryId})";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<InventoryOrderItem>(dataOperation);
    }


    static internal FixedList<InventoryOrderEntry> GetInventoryOrderList() {

      string sql = $"SELECT * FROM TRDInventoryOrders ";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<InventoryOrderEntry>(dataOperation);

    }


    static internal FixedList<InventoryOrderEntry> GetInventoryOrderByUID(string inventoryUID) {

      string sql = $"SELECT * FROM TRDInventoryOrders WHERE InventoryEntryUID IN ('{inventoryUID}')";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<InventoryOrderEntry>(dataOperation);

    }


    internal static void WriteInventoryEntry(InventoryOrderEntry inventoryOrder) {

      var op = DataOperation.Parse("writeInventoryCountOrder",
          inventoryOrder.InventoryEntryId, inventoryOrder.InventoryEntryUID,
          inventoryOrder.InventoryAgentId, inventoryOrder.InventoryType,
          inventoryOrder.InventoryEntryName, inventoryOrder.Keywords,
          inventoryOrder.InventoryEntryDate);

      DataWriter.Execute(op);
    }


    internal static void WriteInventoryItem(InventoryOrderItem item) {

      var op = DataOperation.Parse("writeInventoryCountOrderItem",
          item.InventoryItemId, item.InventoryItemUID,
          item.InventoryEntry.InventoryEntryId,
          item.WarehouseBin.Id, item.VendorProduct.Id,
          item.Quantity, item.Comments);

      DataWriter.Execute(op);
    }

    #endregion Public methods


  } // class InventoryOrderData

} // namespace Empiria.Trade.Inventory.Data
