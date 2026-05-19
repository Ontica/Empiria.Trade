/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Information Holder                      *
*  Type     : InventoryUtility                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents an inventory utility.                                                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using System.Linq;

using Empiria.Trade.Core;

using Empiria.StateEnums;
using Empiria.Trade.Inventory.Adapters;

namespace Empiria.Trade.Inventory {

  /// <summary>Represents an inventory utility.</summary>
  internal class InventoryUtility {

    #region Fields

    static internal readonly string INVENTORY_MANAGER = "inventory-manager";

    #endregion Fields

    #region Public methods

    static internal InventoryOrderActions GetActions(InventoryOrder order) {

      bool existClosedEntries = ValuateIfInventoryEntriesClosed(
                                  order.GetItems<Core.InventoryOrderItem>());

      InventoryOrderActions actions = new InventoryOrderActions {
        CanEdit = order.Status == EntityStatus.Pending || order.Status == EntityStatus.Active,
        CanEditItems = (order.Status == EntityStatus.Pending || order.Status == EntityStatus.Active) && order.InventoryType.ItemsRequired == true,
        CanDelete = order.Status == EntityStatus.Pending || order.Status == EntityStatus.Active,
        CanClose = order.Status == EntityStatus.Pending || order.Status == EntityStatus.Active,
        CanEditEntries = (order.Status == EntityStatus.Pending || order.Status == EntityStatus.Active || existClosedEntries) && order.InventoryType.EntriesRequired == true,
        DisplayCountStatus = true,
        HasCountVariance = GetPermission(),
      };

      return actions;
    }

    
    static internal bool GetPermission() {
      return ExecutionServer.CurrentPrincipal.IsInRole(INVENTORY_MANAGER);

    }


    static internal void EnsureIsValidToClose(FixedList<Core.InventoryOrderItem> orderItems) {
      foreach (var item in orderItems) {

        var entries = InventoryEntry.GetListFor(item);

        var entriesQuantity = entries.Sum(x => x.InputQuantity);

        Assertion.Require(item.Quantity == entriesQuantity, "Faltan productos por asignar.");
      }
    }


    static internal InventoryOrder GetInventoryOrder(string orderUID) {

      InventoryOrder inventoryOrder = InventoryOrder.Parse(orderUID);

      FixedList<Core.InventoryOrderItem> items = inventoryOrder.GetItems<Core.InventoryOrderItem>();

      GetInventoryEntriesByItem(items);

      return inventoryOrder;
    }


    #endregion Public methods

    #region Private methods

    static private void GetInventoryEntriesByItem(FixedList<Core.InventoryOrderItem> items) {

      foreach (var item in items) {

        item.Entries = InventoryEntry.GetListFor(item);
      }
    }


    static private bool ValuateIfInventoryEntriesClosed(FixedList<Core.InventoryOrderItem> items) {

      if (items.Count == 0) {
        return false;
      }
      var countClosedEntries = items.SelectFlat(x => x.Entries.FindAll(y =>
                                                  y.Status == Core.InventoryStatus.Cerrado));

      if (countClosedEntries.Count > 0) {
        return true;
      }

      return false;
    }


    #endregion Private methods

  } // class InventoryUtility

} // namespace Empiria.Inventory
