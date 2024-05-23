/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Core Management                            Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Data Transfer Object                    *
*  Type     : InventoryItemsData                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return the entries of packaging for inventory.                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Core.Inventories.Adapters {


  /// <summary>Output DTO used to return the entries of packaging for inventory.</summary>
  public class InventoryItemsData {

    public int OrderId {
      get; set;
    }


    public int OrderItemId {
      get; set;
    }


    public string VendorProductUID {
      get; set;
    }


    public string WarehouseBinUID {
      get; set;
    }


    public string SupplierUID {
      get; set;
    }


    public decimal Quantity {
      get; set;
    }


  } // class InventoryItemsData

} // namespace Empiria.Trade.Core.Inventories.Adapters
