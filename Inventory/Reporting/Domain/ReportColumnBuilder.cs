/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Partitioned Type / Information Holder   *
*  Type     : ReportColumnBuilder                        License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Generate columns for reports.                                                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using Empiria.Trade.Core.Common;
using Empiria.Trade.Inventory.Adapters;

namespace Empiria.Trade.Inventory.Domain {


  /// <summary>Generate columns for reports.</summary>
  internal class ReportColumnBuilder {


    #region Public methods


    static internal FixedList<DataTableColumn> GetColumns(ReportQuery query) {

      switch (query.ReportType) {
        case ReportType.StocksByProduct:

          return GetColumnsStocksByProduct();

        case ReportType.StocksByLocation:

          return GetColumnsStocksByProduct();
          //return GetColumnsStocksByLocation();

        default:
          throw Assertion.EnsureNoReachThisCode(
                $"Unhandled inventory report type {query.ReportType}.");
      }


    }


    #endregion Public methods


    #region Private methods


    private static FixedList<DataTableColumn> GetColumnsStocksByLocation() {

      List<DataTableColumn> columns = new List<DataTableColumn>();

      columns.Add(new DataTableColumn("property", "name", "text"));
      columns.Add(new DataTableColumn("property", "name", "text-link"));
      columns.Add(new DataTableColumn("property", "name", "date"));
      columns.Add(new DataTableColumn("property", "name", "text-tag"));

      return columns.ToFixedList();
    }


    private static FixedList<DataTableColumn> GetColumnsStocksByProduct() {

      List<DataTableColumn> columns = new List<DataTableColumn>();
      columns.Add(new DataTableColumn("code", "Código", "text-link"));
      columns.Add(new DataTableColumn("name", "Nombre", "text"));
      columns.Add(new DataTableColumn("presentation", "Presentación", "text"));
      columns.Add(new DataTableColumn("tag", "Localización", "text-nowrap"));
      columns.Add(new DataTableColumn("stock", "Stock disponible", "decimal"));
      columns.Add(new DataTableColumn("realStock", "Stock real", "decimal"));
      columns.Add(new DataTableColumn("stockInProcess", "Stock entrante", "decimal"));
      //columns.Add(new DataTableColumn("property", "name", "date"));
      
      return columns.ToFixedList();
    }


    #endregion Private methods


  } // class ReportColumnBuilder

} // namespace Empiria.Trade.Inventory.Domain
