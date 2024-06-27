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
  public enum InventoryReportType {

    InventoryStockReport,

    InventoryReport

  }


  /// <summary>Query used to get inventory reports.</summary>
  public class InventoryReportQuery {


    public InventoryReportType InventoryReportType {
      get; set;
    } = InventoryReportType.InventoryStockReport;


  }

}
