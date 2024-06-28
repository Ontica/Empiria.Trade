/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Data Transfer Object                    *
*  Type     : InventoryReportQuery                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Query used to get inventory reports.                                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Inventory.Adapters {


  /// <summary>Enum with inventory report types </summary>
  public enum ReportType {

    StocksByProduct,

    StocksByLocation

  }


  /// <summary>Query used to get inventory reports.</summary>
  public class ReportQuery {


    public ReportType ReportType {
      get; set;
    } = ReportType.StocksByProduct;


    public string ProductUID {
      get; set;
    } = string.Empty;


    public string WarehouseBinUID {
      get; set;
    } = string.Empty;


  }

}
