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


    static private IInventoryReportDto MapPurchasingReport(InventoryReportEntry x) {
      var dto = new InventoryStockDescriptorDto();

      return dto;
    }


    static private IInventoryReportDto MapStocksByLocation(InventoryStockEntry x) {
      var dto = new InventoryStockDescriptorDto();

      return dto;
    }


    static private IInventoryReportDto MapStocksByProduct(InventoryStockEntry x) {
      var dto = new InventoryStockDescriptorDto();

      dto.VendorProductUID = x.VendorProduct.VendorProductUID;
      dto.Name = x.VendorProduct.ProductFields.ProductName;
      dto.Code = x.VendorProduct.ProductFields.ProductCode;
      dto.Presentation = x.VendorProduct.ProductPresentation.PresentationName;
      dto.WarehouseBinTag = x.WarehouseBin.Tag;
      dto.WarehouseName = x.WarehouseBin.Warehouse.Name;
      dto.Rack = x.WarehouseBin.Name;
      dto.Stock = x.Stock;
      dto.RealStock = x.RealStock;
      dto.StockInProcess = x.StockInProcess;
      dto.ItemType = x.ItemType;
      return dto;
    }


    static private FixedList<IInventoryReportDto> MapList(
      ReportQuery query, FixedList<IInventoryReport> list) {

      switch (query.ReportType) {

        case ReportType.StocksByProduct:

          var stocksByProduct = list.Select((x) => MapStocksByProduct((InventoryStockEntry) x));
          return new FixedList<IInventoryReportDto>(stocksByProduct);

        case ReportType.StocksByLocation:

          var stocksByLocation = list.Select((x) => MapStocksByLocation((InventoryStockEntry) x));
          return new FixedList<IInventoryReportDto>(stocksByLocation);

        case ReportType.PurchasingReport:

          var purchasingReport = list.Select((x) => MapPurchasingReport((InventoryReportEntry) x));
          return new FixedList<IInventoryReportDto>(purchasingReport);

        default:
          throw Assertion.EnsureNoReachThisCode(
                $"Unhandled inventory report type {query.ReportType}.");
      }

    }


    #endregion Private methods


  } // class InventoryReportMapper

} // namespace Empiria.Trade.Inventory.Adapters
