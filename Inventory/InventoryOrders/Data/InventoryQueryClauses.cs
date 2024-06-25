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
using Empiria.Trade.Inventory.Adapters;

namespace Empiria.Trade.Inventory.Data {


  /// <summary>Provides query data clauses for inventory order.</summary>
  internal class InventoryQueryClauses {


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
