/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Use cases Layer                         *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Use case interactor class               *
*  Type     : InventoryReportsUseCases                   License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases used to build Inventory reports.                                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Services;
using Empiria.Trade.Inventory.Adapters;
using Empiria.Trade.Inventory.Domain;
using Empiria.Trade.Inventory.UseCases;

namespace Empiria.Trade.Inventory.UseCases {


  /// <summary>Use cases used to build Inventory reports.</summary>
  public class InventoryReportsUseCases : UseCase {


    #region Constructors and parsers

    public InventoryReportsUseCases() {
      // no-op
    }

    static public InventoryReportsUseCases UseCaseInteractor() {
      return CreateInstance<InventoryReportsUseCases>();
    }


    #endregion Constructors and parsers


    #region Public methods


    public InventoryReportDataDto BuildInventoryReport(ReportQuery query) {

      var builder = new InventoryReportsBuilder();
      
      switch (query.ReportType) {
        case ReportType.StocksByProduct:

          return InventoryReportMapper.Map(builder.GenerateStocksByProduct(query), query);

        case ReportType.StocksByLocation:

          return InventoryReportMapper.Map(builder.GenerateStocksByLocation(query), query);

        case ReportType.PurchasingReport:

          return InventoryReportMapper.Map(builder.GeneratePurchasingReport(query), query);

        default:
          throw Assertion.EnsureNoReachThisCode(
                $"Unhandled inventory report type {query.ReportType}.");
      }
    }


    #endregion Public methods


  } // class InventoryReportsUseCases

} // namespace Empiria.Trade.Inventory.Reporting.UseCases
