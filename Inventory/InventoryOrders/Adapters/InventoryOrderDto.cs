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
  public class InventoryOrderDto {


    public string UID {
      get; set;
    }


    public NamedEntityDto InventoryOrderType {
      get; internal set;
    }


    public string InventoryOrderNo {
      get; internal set;
    } = string.Empty;


    public NamedEntityDto ExternalObjectReference {
      get; internal set;
    }


    public NamedEntityDto Responsible {
      get; internal set;
    }


    public NamedEntityDto AssignedTo {
      get; internal set;
    }


    public string Notes {
      get; set;
    }


    public DateTime ClosingTime {
      get; set;
    }


    public DateTime PostingTime {
      get; set;
    }


    public NamedEntityDto PostedBy {
      get; internal set;
    }


    public InventoryStatus Status {
      get; set;
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
      get; set;
    }


    public InventoryProductDto Product {
      get; set;
    } = new InventoryProductDto();


    public InventoryWarehouseBinDto WarehouseBin {
      get; set;
    } = new InventoryWarehouseBinDto();


    public decimal Quantity {
      get; set;
    }


    public string Notes {
      get; set;
    }


    //public string ExternalObjectItemReferenceUID {
    //  get; set;
    //}


    //public decimal InputQuantity {
    //  get; set;
    //}


    //public decimal OutputQuantity {
    //  get; set;
    //}


    //public InventoryStatus Status {
    //  get; set;
    //}

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


  public class InventoryOrderActions {


    public bool CanEdit {
      get; set;
    } = false;


    public bool CanEditItems {
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


  } // class InventoryOrderActions

} // namespace Empiria.Trade.Inventory.Adapters
