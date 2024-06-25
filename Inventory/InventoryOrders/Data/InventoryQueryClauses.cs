/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Trade Core                                 Component : Data Layer                              *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Data Service                            *
*  Type     : InventoryQueryClauses                      License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides query data clauses for inventory order.                                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Core;
using Empiria.Trade.Inventory.Adapters;

namespace Empiria.Trade.Inventory.Data {


  /// <summary>Provides query data clauses for inventory order.</summary>
  internal class InventoryQueryClauses {


    internal InventoryQueryClauses() {

    }


    internal InventoryQueryClauses(string inventoryOrderUID = "",
                                   int inventoryOrderTypeId = 0, int referenceId = 0) {

      if (inventoryOrderUID != string.Empty) {
        this.InventoryOrderId = InventoryOrderEntry.Parse(inventoryOrderUID).Id;
      }
      this.InventoryOrderTypeId = inventoryOrderTypeId;
      this.ReferenceId = referenceId;
    }


    internal InventoryQueryClauses(InventoryOrderQuery query) {
      GetClausesForInventoryOrder(query);
    }

    internal int InventoryOrderId {
      get; set;
    }


    internal int InventoryOrderTypeId {
      get; set;
    }


    internal int AssignedToId {
      get; set;
    }


    internal int ReferenceId {
      get; set;
    }


    internal string Keywords {
      get; set;
    } = string.Empty;


    internal InventoryStatus InventoryOrderStatus {
      get; set;
    } = InventoryStatus.Todos;


    #region Public methods


    private void GetClausesForInventoryOrder(InventoryOrderQuery query) {

      if (query.InventoryOrderTypeUID != string.Empty) {
        this.InventoryOrderTypeId = InventoryOrderType.Parse(query.InventoryOrderTypeUID).Id;
      }
      if (query.AssignedToUID != string.Empty) {
        this.AssignedToId = Party.Parse(query.AssignedToUID).Id;
      }
      if (query.Keywords != string.Empty) {
        this.Keywords = query.Keywords;
      }
      if (query.Status != InventoryStatus.Todos) {
        this.InventoryOrderStatus = query.Status;
      }
    }


    #endregion Public methods


  } // class InventoryQueryClauses


  internal class InventoryItemQueryClauses {


    internal int InventoryOrderId {
      get; set;
    }


    internal int InventoryOrderItemId {
      get; set;
    }


    internal int ItemReferenceId {
      get; set;
    }


    internal int VendorProductId {
      get; set;
    }


    internal int WarehouseBinId {
      get; set;
    }


  }


} // namespace Empiria.Trade.Inventory.Data
