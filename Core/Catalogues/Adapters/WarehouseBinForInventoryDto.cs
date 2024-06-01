/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Core Management                            Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Data Transfer Object                    *
*  Type     : WarehouseBinForInventoryDto                License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return the entries of warehouse bin.                                        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Core.Catalogues.Adapters {

  /// <summary>Output DTO used to return the entries of warehouse bin.</summary>
  public class WarehouseBinForInventoryDto {


    public string UID {
      get; set;
    }


    public string Tag {
      get; set;
    }


    public string Name {
      get; set;
    }


    public string WarehouseName {
      get; set;
    }


    public int RackRow {
      get; set;
    }


    public int RackColumn {
      get; set;
    }


  } // class WarehouseBinForInventory

} // namespace Empiria.Trade.Core.Catalogues.Adapters
