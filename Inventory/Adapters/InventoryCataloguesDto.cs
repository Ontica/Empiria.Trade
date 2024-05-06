/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Data Transfer Object                    *
*  Type     : InventoryCataloguesDto                     License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return inventory catalogues data.                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Inventory.Adapters {


  /// <summary>Output DTO used to return inventory catalogues data.</summary>
  public class InventoryCataloguesDto {

  } // class InventoryCataloguesDto


  public class InventoryProductDto {


    public string ProductCode {
      get; set;
    } = string.Empty;


    public string ProductDescription {
      get; set;
    } = string.Empty;


    public string Presentation {
      get; set;
    } = string.Empty;

  } // class InventoryProduct


  public class InventoryWarehouseBinDto {
    
    public string Rack {
      get; set;
    } = string.Empty;


    public string RackDescription {
      get; set;
    } = string.Empty;


    public int Position {
      get; set;
    }


    public int Level {
      get; set;
    }


  } // class InventoryWarehouseBin

} // namespace Empiria.Trade.Inventory.Adapters
