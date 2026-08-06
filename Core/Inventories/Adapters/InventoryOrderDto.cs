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
using Empiria.StateEnums;

namespace Empiria.Trade.Core {

  public interface IInventoryOrderDto {

  }


  /// <summary>Output DTO used to return inventory entry data.</summary>
  public class InventoryEntryDto {

    public string UID {
      get;  set;
    }


    public string Product {
      get;  set;
    }


    public string Location {
      get;  set;
    }


    public decimal Quantity {
      get;  set;
    }


    public NamedEntityDto PostedBy {
      get;  set;
    }


    public DateTime PostingTime {
      get;  set;
    }

  } // class InventoryEntryDto


  /// <summary>Output DTO used to return inventory type.</summary>
  public class InventoryTypeDto {

    public string UID {
      get;  set;
    }

    public string Name {
      get;  set;
    }

    public InventoryTypeRulesDto Rules {
      get;  set;
    }

  } // class InventoryType


  /// <summary>Output DTO used to return inventory display rules.</summary>
  public class InventoryTypeRulesDto {

    public Boolean EntriesRequired {
      get;  set;
    }


    public Boolean ItemsRequired {
      get;  set;
    }

  } //  class InventoryTypeRules


  /// <summary>Output DTO used to return inventory data.</summary>
  public class InventoryOrderDto {

    public string UID {
      get;  set;
    }


    public NamedEntityDto OrderType {
      get;  set;
    }


    public string OrderNo {
      get;  set;
    }


    public InventoryTypeDto InventoryType {
      get;  set;
    }


    public NamedEntityDto Warehouse {
      get;  set;
    }


    public NamedEntityDto Responsible {
      get;  set;
    }


    public NamedEntityDto RequestedBy {
      get;  set;
    }


    public string Description {
      get;  set;
    }


    public DateTime ClosingTime {
      get;  set;
    }


    public DateTime PostingTime {
      get;  set;
    }


    public NamedEntityDto PostedBy {
      get;  set;
    }


    public NamedEntityDto Status {
      get;
       set;
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
      get;  set;
    } = string.Empty;


    public string ProductName {
      get;  set;
    } = string.Empty;


    public string Description {
      get;  set;
    } = string.Empty;


    public NamedEntityDto ProductUnit {
      get;  set;
    }


    public decimal Quantity {
      get;  set;
    }


    public string Location {
      get;  set;
    }


    public decimal AssignedQuantity {
      get;  set;
    }


    public NamedEntityDto PostedBy {
      get;  set;
    }


    public DateTime PostingTime {
      get;  set;
    }


    public EntityStatus Status {
      get;  set;
    }


    public FixedList<InventoryEntryDto> Entries {
      get;  set;
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
       set;
    }


  } // class InventoryOrderDescriptorDto

} // namespace Empiria.Trade.Inventory.Adapters
