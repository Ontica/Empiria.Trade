/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Use cases Layer                         *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Use case interactor class               *
*  Type     : ReportGeneratorUseCases                    License   : Please read LICENSE.txt file            *
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
  public class ReportGeneratorUseCases : UseCase {


    #region Constructors and parsers

    public ReportGeneratorUseCases() {
      // no-op
    }

    static public ReportGeneratorUseCases UseCaseInteractor() {
      return CreateInstance<ReportGeneratorUseCases>();
    }


    #endregion Constructors and parsers


    #region Public methods


    public ReportDataDto BuildReport(ReportQuery query) {

      var builder = new ReportBuilder(query);
      
      switch (query.ReportType) {
        case ReportType.StocksByProduct:
          
          if (query.Products.Length == 0) {
            Assertion.EnsureFailed($"Por favor especifique mínimo un producto.");
          }
          
          return ReportMapper.Map(builder.GenerateStocksByProduct(), query);

        case ReportType.StocksByLocation:

          if (query.WarehouseBins.Length == 0) {
            Assertion.EnsureFailed($"Por favor especifique mínimo una ubicación.");
          }
          
          return ReportMapper.Map(builder.GenerateStocksByLocation(), query);

        case ReportType.PurchasingReport:

          return ReportMapper.Map(builder.GeneratePurchasingReport(), query);

        default:
          throw Assertion.EnsureNoReachThisCode(
                $"Unhandled report type {query.ReportType}.");
      }
    }


    #endregion Public methods


  } // class ReportGeneratorUseCases

} // namespace Empiria.Trade.Inventory.Reporting.UseCases
