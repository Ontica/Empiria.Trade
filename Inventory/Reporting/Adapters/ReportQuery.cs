/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Data Transfer Object                    *
*  Type     : ReportQuery                                License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Query used to get reports.                                                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Inventory.Adapters {


  /// <summary>Enum with report types.</summary>
  public enum ReportType {

    StocksByProduct,

    StocksByLocation,

    PurchasingReport

  } // enum ReportType


  /// <summary>Query used to get reports.</summary>
  public class ReportQuery {


    public ReportType ReportType {
      get; set;
    } = ReportType.StocksByProduct;


    public string[] Products {
      get; set;
    } = new string[0];


    public string[] WarehouseBins {
      get; set;
    } = new string[0];


    public string Keywords {
      get; set;
    } = string.Empty;


    public string ProductUID {
      get; set;
    } = string.Empty;


    public string WarehouseBinUID {
      get; set;
    } = string.Empty;


  } // class ReportQuery

} // namespace Empiria.Trade.Inventory.Adapters
