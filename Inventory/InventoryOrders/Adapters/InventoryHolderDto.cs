/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Data Transfer Object                    *
*  Type     : InventoryOrderDto                          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return inventory data.                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Inventory.Adapters {

  /// <summary>Output DTO used to return inventory data.</summary>
  public class InventoryHolderDto {

    public InventoryOrderDto Order {
      get; internal set;
    }


    public FixedList<InventoryOrderItemDto> Items {
      get; internal set;
    }


    public InventoryOrderActions Actions {
      get; internal set;
    }

  } // class InventoryHolderDto


  /// <summary>Output DTO used to return inventory order actions.</summary>
  public class InventoryOrderActions {

    public bool CanEdit {
      get; set;
    } = false;


    public bool CanEditItems {
      get; set;
    } = false;


    public bool CanEditEntries {
      get; set;
    } = false;


    public bool CanDelete {
      get; set;
    } = false;


    public bool CanClose {
      get; set;
    } = false;


    public bool CanOpen {
      get; set;
    } = false;


    public bool DisplayCountStatus {
      get; set;
    } = false;


    public bool HasCountVariance {
      get; set;
    } = false;

  } // class InventoryOrderActions

} // namespace Empiria.Trade.Inventory.Adapters
