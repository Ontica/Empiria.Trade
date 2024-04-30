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
using Empiria.Trade.Inventory.Adapters;
using Empiria.Trade.Inventory.Data;

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

      //CreateInventoryOrderItems(inventoryOrder, fields.ItemFields);

      return inventoryOrder;
    }


    internal void CreateInventoryOrderItem(string inventoryOrderUID, InventoryOrderItemFields fields) {

        var inventoryItem = new InventoryOrderItem(inventoryOrderUID, fields);
        inventoryItem.Save();
    }


    internal InventoryOrderEntry GetInventoryOrderByUID(string inventoryOrderUID) {

      var inventoryOrder = InventoryOrderEntry.Parse(inventoryOrderUID);

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


    internal InventoryOrderEntry UpdateInventoryCountOrder(string inventoryOrderUID,
      InventoryOrderFields fields) {

      return CreateInventoryOrder(fields, inventoryOrderUID);
    }




    #endregion Private methods


  } // class InventoryOrderBuilder

} // namespace Empiria.Trade.Inventory.Domain
