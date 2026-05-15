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
using Empiria.Inventory.Adapters;
using Empiria.StateEnums;

namespace Empiria.Trade.Inventory.Adapters {

  /// <summary>Output DTO used to return inventory type.</summary>
  public class InventoryTypeDto {

    public string UID {
      get; internal set;
    }

    public string Name {
      get; internal set;
    }

    public InventoryTypeRulesDto Rules {
      get; internal set;
    }

  } // class InventoryType


  /// <summary>Output DTO used to return inventory display rules.</summary>
  public class InventoryTypeRulesDto {

    public Boolean EntriesRequired {
      get; internal set;
    }


    public Boolean ItemsRequired {
      get; internal set;
    }

  } //  class InventoryTypeRules


  /// <summary>Output DTO used to return inventory data.</summary>
  public class InventoryOrderDto {

    public string UID {
      get; internal set;
    }


    public NamedEntityDto OrderType {
      get; internal set;
    }


    public string OrderNo {
      get; internal set;
    }


    public InventoryTypeDto InventoryType {
      get; internal set;
    }


    public NamedEntityDto Warehouse {
      get; internal set;
    }


    public NamedEntityDto Responsible {
      get; internal set;
    }


    public NamedEntityDto RequestedBy {
      get; internal set;
    }


    public string Description {
      get; internal set;
    }


    public DateTime ClosingTime {
      get; internal set;
    }


    public DateTime PostingTime {
      get; internal set;
    }


    public NamedEntityDto PostedBy {
      get; internal set;
    }


    public NamedEntityDto Status {
      get;
      internal set;
    }


    public FixedList<InventoryOrderItemDto> Items {
      get; set;
    } = new FixedList<InventoryOrderItemDto>();


    public InventoryOrderActions Actions {
      get; set;
    } = new InventoryOrderActions();


  } // class InventoryOrderDto


  public class InventoryOrderItemDto {

    public string UID {
      get; internal set;
    } = string.Empty;


    public string ProductName {
      get; internal set;
    } = string.Empty;


    public string Description {
      get; internal set;
    } = string.Empty;


    public NamedEntityDto ProductUnit {
      get; internal set;
    }


    public decimal Quantity {
      get; internal set;
    }


    public string Location {
      get; internal set;
    }


    public decimal AssignedQuantity {
      get; internal set;
    }


    public NamedEntityDto PostedBy {
      get; internal set;
    }


    public DateTime PostingTime {
      get; internal set;
    }


    public EntityStatus Status {
      get; internal set;
    }


    public FixedList<InventoryEntryDto> Entries {
      get; internal set;
    }

  }


  /// <summary>Output DTO used to return inventory descriptor data.</summary>
  public class InventoryOrderDescriptorDto : IInventoryOrderDto {


    public string UID {
      get; set;
    }


    public string OrderTypeName {
      get; set;
    }


    public string OrderNo {
      get; set;
    }


    public string InventoryTypeName {
      get; set;
    }


    public string WarehouseName {
      get; set;
    }


    public string ResponsibleName {
      get; set;
    }


    public string RequestedByName {
      get; set;
    }


    public string Description {
      get; set;
    }


    public string DocumentNo {
      get; set;
    }


    public string PostedByName {
      get; set;
    }


    public DateTime PostingTime {
      get; set;
    }


    public string Status {
      get; set;
    }


    public string StakeholderName {
      get;
      internal set;
    }


  } // class InventoryOrderDescriptorDto

} // namespace Empiria.Trade.Inventory.Adapters
