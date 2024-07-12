﻿/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Mapper class                            *
*  Type     : ReportMapper                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Methods used to map inventory reports.                                                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Core.Common;
using System.Collections.Generic;
using System.Collections;
using Empiria.Trade.Inventory.Domain;
using Empiria.Trade.Core;

namespace Empiria.Trade.Inventory.Adapters {


  /// <summary>Methods used to map inventory reports.</summary>
  static internal class ReportMapper {


    #region Public methods


    static public ReportDataDto Map(
      FixedList<IReportEntry> list, ReportQuery query) {

      return new ReportDataDto {
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


    static private IReportDto MapPurchasingReport(ReportEntry x) {
      var dto = new InventoryStockDescriptorDto();

      return dto;
    }


    static private IReportDto MapStocksByLocation(InventoryStockEntry x) {
      var dto = new InventoryStockDescriptorDto();

      dto.UID = x.VendorProduct.VendorProductUID;
      //dto.WarehouseName = x.WarehouseBin.Warehouse.Name;
      //dto.Rack = x.WarehouseBin.Name;
      dto.Stock = x.Stock;
      dto.RealStock = x.RealStock;
      dto.StockInProcess = x.StockInProcess;
      dto.ItemType = x.ItemType;

      if (x.ItemType == ReportItemType.Group) {
        var warehouse = x.WarehouseBin.Warehouse.Id > 0 ? $"ALMACEN {x.WarehouseBin.Warehouse.Code}" : "";
        var tag = x.WarehouseBin.Id == -1 ? "PROCESO DE SALIDA" :
                  $"{x.WarehouseBin.Tag} ({x.WarehouseBin.Name})";
        dto.Code = x.WarehouseBin.Warehouse.Code;
        dto.Tag = tag;
        dto.Name = $"{warehouse} - {tag}";
      } else {

        dto.Code = x.VendorProduct.ProductFields.ProductCode;
        dto.Name = x.VendorProduct.ProductFields.ProductName;
        dto.Presentation = x.VendorProduct.ProductPresentation.PresentationName;
      }
      return dto;
    }


    static private IReportDto MapStocksByProduct(InventoryStockEntry x) {
      var dto = new InventoryStockDescriptorDto();

      dto.UID = x.VendorProduct.VendorProductUID;
      dto.Name = x.VendorProduct.ProductFields.ProductName;
      //dto.WarehouseName = x.WarehouseBin.Warehouse.Name;
      //dto.Rack = x.WarehouseBin.Name;
      dto.Stock = x.Stock;
      dto.RealStock = x.RealStock;
      dto.StockInProcess = x.StockInProcess;
      dto.ItemType = x.ItemType;
      if (x.ItemType == ReportItemType.Group) {
        dto.Code = x.VendorProduct.ProductFields.ProductCode;
        dto.Presentation = x.VendorProduct.ProductPresentation.PresentationName;
      } else {
        var tag = x.WarehouseBin.Id == -1 ? "PROCESO DE SALIDA" :
                  $"{x.WarehouseBin.Tag} ({x.WarehouseBin.Name})";
        dto.Tag = tag;
      }
      return dto;
    }


    static private FixedList<IReportDto> MapList(
      ReportQuery query, FixedList<IReportEntry> list) {

      switch (query.ReportType) {

        case ReportType.StocksByProduct:

          var stocksByProduct = list.Select((x) => MapStocksByProduct((InventoryStockEntry) x));
          return new FixedList<IReportDto>(stocksByProduct);

        case ReportType.StocksByLocation:

          var stocksByLocation = list.Select((x) => MapStocksByLocation((InventoryStockEntry) x));
          return new FixedList<IReportDto>(stocksByLocation);

        case ReportType.PurchasingReport:

          var purchasingReport = list.Select((x) => MapPurchasingReport((ReportEntry) x));
          return new FixedList<IReportDto>(purchasingReport);

        default:
          throw Assertion.EnsureNoReachThisCode(
                $"Unhandled inventory report type {query.ReportType}.");
      }

    }


    #endregion Private methods


  } // class ReportMapper

} // namespace Empiria.Trade.Inventory.Adapters
