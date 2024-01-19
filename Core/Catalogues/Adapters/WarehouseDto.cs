/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Core Management                            Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Data Transfer Object                    *
*  Type     : WarehouseDto                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return the entries of warehouse.                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Core.Catalogues {

  /// <summary>Output DTO used to return the entries of warehouse.</summary>
  public class WarehouseDto {


    public string UID {
      get; set;
    }


    public string Code {
      get; set;
    }


    public string Name {
      get; set;
    }


    public decimal Stock {
      get; set;
    }

  } // class WarehouseDto


  public class WarehouseBinDto {


    public string UID {
      get; set;
    }


    public string OrderItemUID {
      get; set;
    }


    public string Name {
      get; set;
    }


    public string WarehouseName {
      get; set;
    }


    public decimal Stock {
      get; set;
    }

  } // class WarehouseBinDto


} // namespace Empiria.Trade.Core.Catalogues
