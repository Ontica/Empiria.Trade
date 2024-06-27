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
  internal class InventoryReportsUseCases : UseCase {


    #region Constructors and parsers

    public InventoryReportsUseCases() {
      // no-op
    }

    static public InventoryReportsUseCases UseCaseInteractor() {
      return CreateInstance<InventoryReportsUseCases>();
    }


    #endregion Constructors and parsers


    #region Public methods


    public InventoryReportDataDto BuildInventoryReport(InventoryReportQuery query) {

      var builder = new InventoryReportsBuilder();

      switch (query.InventoryReportType) {
        case InventoryReportType.InventoryStockReport:

          var stock = builder.GenerateInventoryStock(query);
          return InventoryReportMapper.Map(stock, query);

        case InventoryReportType.InventoryReport:
          
          var report = builder.GenerateInventoryReport(query);
          return InventoryReportMapper.Map(report, query);

        default:
          throw Assertion.EnsureNoReachThisCode(
                $"Unhandled inventory report type {query.InventoryReportType}.");
      }

    }


    #endregion Public methods


  } // class InventoryReportsUseCases

} // namespace Empiria.Trade.Inventory.Reporting.UseCases
