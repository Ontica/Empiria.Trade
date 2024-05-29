/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Partitioned Type / Information Holder   *
*  Type     : InventoryOrderBuilder                      License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Generate data for Inventory order.                                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Linq;
using System.Reflection;
using Empiria.Trade.Inventory.Adapters;
using Empiria.Trade.Inventory.Data;
using Empiria.Trade.Core.Inventories.Adapters;

namespace Empiria.Trade.Inventory.Domain {

  /// <summary>Generate data for Inventory order.</summary>
  internal class InventoryOrderBuilder {


    #region Constructors and parsers

    public InventoryOrderBuilder() {

    }


    #endregion Constructor and parsers


    #region Public methods

    internal InventoryOrderEntry CreateInventoryOrder(InventoryOrderFields fields,
      string inventoryOrderUID) {

      var inventoryOrder = new InventoryOrderEntry(fields, inventoryOrderUID);
      inventoryOrder.Save();

      return inventoryOrder;
    }


    internal void CreateInventoryOrderItem(string inventoryOrderUID, InventoryOrderItemFields fields) {

        var inventoryItem = new InventoryOrderItem(inventoryOrderUID, fields);
        inventoryItem.Save();
    }


    internal InventoryOrderEntry GetInventoryOrderByUID(string inventoryOrderUID) {

      var inventoryOrder = InventoryOrderData.GetInventoryOrderByUID(inventoryOrderUID);
      GetInventoryItemsForOrder(inventoryOrder);

      return inventoryOrder;
    }


    #endregion Public methods

    #region Private methods


    private void GetInventoryItemsForOrder(InventoryOrderEntry inventoryOrder) {

      FixedList<InventoryOrderItem> items =
        InventoryOrderData.GetInventoryItemsByOrderUID(inventoryOrder.InventoryOrderUID);

      if (items.Count > 0) {
        inventoryOrder.InventoryOrderItems = items;
      }
    }


    internal void UpdateInventoryCountOrder(string inventoryOrderUID,
      InventoryOrderFields fields) {

      CreateInventoryOrder(fields, inventoryOrderUID);
    }


    internal InventoryOrderActions GetActions(InventoryOrderEntry inventoryOrder) {
      
      if (inventoryOrder.Status == InventoryStatus.Cerrado ||
          inventoryOrder.InventoryOrderTypeId == 5) {
        
        return new InventoryOrderActions();
      }
      
      var actions = new InventoryOrderActions();
      actions.CanEdit = true;
      actions.CanEditItems = true;
      actions.CanDelete = true;
      if (inventoryOrder.InventoryOrderItems.Count > 0) {
        actions.CanClose = true;
      }
      //if (inventoryOrder.ResponsibleId == ExecutionServer.CurrentUserId ||
      //    inventoryOrder.PostedById == ExecutionServer.CurrentUserId) {

      //  actions.CanEdit = true;
      //  actions.CanDelete = true;

      //  if (inventoryOrder.InventoryOrderItems.Count > 0) {
      //    actions.CanClose = true;
      //  }
      //}

      //if (inventoryOrder.AssignedToId == ExecutionServer.CurrentUserId) {
      //  actions.CanEditItem = true;
      //}

      return actions;
    }


    internal InventoryOrderFields MapToInventoryOrderFields(InventoryItems inventoryItemData) {

      InventoryOrderFields fields = new InventoryOrderFields();

      fields.InventoryOrderTypeUID = "2ft8y5h4-db55-48b3-aa78-63132a8d5e7f"; // TODO referencia a tipo cuando se agregue a Types 
      fields.ResponsibleUID = "";
      fields.AssignedToUID = "";
      fields.Notes = "";
      fields.ReferenceId = inventoryItemData.OrderId;

      return fields;
    }


    internal InventoryOrderEntry CreateInventoryOrderBySale(InventoryItems inventoryItemsData) {

      InventoryOrderFields fields = MapToInventoryOrderFields(inventoryItemsData);

      var inventoryOrder = new InventoryOrderEntry(fields, "");
      inventoryOrder.Save();

      return inventoryOrder;
    }


    internal InventoryOrderItemFields MapToInventoryOrderItemFields(InventoryItems inventoryItem) {

      InventoryOrderItemFields fields = new InventoryOrderItemFields();
      fields.InventoryOrderTypeItemId = 5;
      fields.ItemReferenceId = inventoryItem.OrderItemId;
      fields.VendorProductUID = inventoryItem.VendorProductUID;
      fields.WarehouseBinUID = inventoryItem.WarehouseBinUID;
      fields.InProcessOutputQuantity = inventoryItem.Quantity;
      fields.Quantity = inventoryItem.Quantity; //TODO ESTE VALOR ES TEMPORAL PARA MOSTRAR EN PANTALLA
      return fields;
    }




    #endregion Private methods


  } // class InventoryOrderBuilder

} // namespace Empiria.Trade.Inventory.Domain
