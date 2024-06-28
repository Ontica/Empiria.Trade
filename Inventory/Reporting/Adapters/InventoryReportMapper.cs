/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Mapper class                            *
*  Type     : InventoryReportMapper                      License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Methods used to map inventory reports.                                                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Core.Common;
using System.Collections.Generic;
using System.Collections;
using Empiria.Trade.Inventory.Domain;

namespace Empiria.Trade.Inventory.Adapters {


  /// <summary>Methods used to map inventory reports.</summary>
  static internal class InventoryReportMapper {


    #region Public methods


    static public InventoryReportDataDto Map(
      FixedList<IInventoryReport> list, ReportQuery query) {

      return new InventoryReportDataDto {
        Query = query,
        Columns = ReportColumnBuilder.GetColumns(query),
        Entries = MapList(query, list)
      };
    }


    #endregion Public methods


    #region Private methods


    static private FixedList<DataTableColumn> GetColumns() {

      List<DataTableColumn> columns = new List<DataTableColumn>();

      columns.Add(new DataTableColumn("property", "name", "text"));
      columns.Add(new DataTableColumn("property", "name", "text-link"));
      columns.Add(new DataTableColumn("property", "name", "date"));
      columns.Add(new DataTableColumn("property", "name", "text-tag"));

      return columns.ToFixedList();
    }


    private static InventoryReportDescriptorDto MapStocksByLocation(InventoryReportEntry x) {
      var dto = new InventoryReportDescriptorDto();

      return dto;
    }


    private static InventoryStockDescriptorDto MapStocksByProduct(InventoryStockEntry x) {
      var dto = new InventoryStockDescriptorDto();

      return dto;
    }


    static private FixedList<IInventoryReportDto> MapList(
      ReportQuery query, FixedList<IInventoryReport> list) {

      switch (query.ReportType) {

        case ReportType.StocksByProduct:

          var inventoryStock = list.Select((x) => MapStocksByProduct((InventoryStockEntry) x));
          return new FixedList<IInventoryReportDto>(inventoryStock);

        case ReportType.StocksByLocation:

          var inventoryReport = list.Select((x) => MapStocksByLocation((InventoryReportEntry) x));
          return new FixedList<IInventoryReportDto>(inventoryReport);

        default:
          throw Assertion.EnsureNoReachThisCode(
                $"Unhandled inventory report type {query.ReportType}.");
      }

    }


    #endregion Private methods


  } // class InventoryReportMapper

} // namespace Empiria.Trade.Inventory.Adapters
