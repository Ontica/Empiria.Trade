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
      get; set;
    } = new NamedEntityDto("", "");


    public string InventoryOrderNo {
      get;
      internal set;
    }


    public NamedEntityDto ExternalObjectReference {
      get;
      internal set;
    } = new NamedEntityDto("", "");


    public NamedEntityDto Responsible {
      get;
      internal set;
    } = new NamedEntityDto("", "");


    public NamedEntityDto AssignedTo {
      get;
      internal set;
    } = new NamedEntityDto("", "");


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
      get;
      internal set;
    } = new NamedEntityDto("", "");


    public InventoryStatus Status {
      get; set;
    }


    public FixedList<InventoryOrderItemDto> Items {
      get; set;
    } = new FixedList<InventoryOrderItemDto>();

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


    public string InventoryOrderTypeName {
      get; set;
    } = string.Empty;


    public string InventoryOrderNo {
      get;
      internal set;
    }


    public string ExternalObjectReferenceName {
      get;
      internal set;
    } = string.Empty;


    public string ResponsibleName {
      get;
      internal set;
    } = string.Empty;


    public string AssignedToName {
      get;
      internal set;
    } = string.Empty;


    public string Notes {
      get; set;
    }


    public DateTime ClosingTime {
      get; set;
    }


    public DateTime PostingTime {
      get; set;
    }


    public string PostedByName {
      get;
      internal set;
    } = string.Empty;


    public InventoryStatus InventoryStatus {
      get; set;
    }


  } // class InventoryOrderDescriptorDto
} // namespace Empiria.Trade.Inventory.Adapters
