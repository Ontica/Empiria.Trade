/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Data Transfer Object                    *
*  Type     : InventoryOrderDescriptorDto                License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return inventory descriptor data.                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Inventory.Adapters {

  /// <summary>Output DTO used to return inventory descriptor data.</summary>
  public class InventoryOrderDescriptorDto {


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
