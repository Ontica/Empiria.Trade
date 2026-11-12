/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Partitioned Type / Information Holder   *
*  Type     : InventoryOrderHelper                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Helper methods to build reports.                                                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Core.Catalogues;
using Empiria.Trade.Core;
using System.Collections.Generic;
using Empiria.Trade.Inventory.Adapters;
using Empiria.Trade.Products;
using System.Linq;
using System.Collections;

namespace Empiria.Trade.Inventory.Domain {


  /// <summary>Helper methods to build reports.</summary>
  internal class ReportHelper {


    #region Constructors and parsers


    private ReportQuery query;


    public ReportHelper(ReportQuery _query) {
      this.query = _query;
    }


    #endregion Constructors and parsers

    #region Public methods

    
    internal FixedList<Core.InventoryStockEntry> GetStockByVendorProduct(
      FixedList<VendorProduct> vendorProducts) {

      var list = new List<Core.InventoryStockEntry>();

      foreach (var vendorProduct in vendorProducts) {

        var stockByVendorProduct = CataloguesUseCases.GetInventoryStockByClauses(
          vendorProduct.VendorProductId, 0);

        list.AddRange(stockByVendorProduct);
      }

      return list.ToFixedList();
    }


    internal FixedList<InventoryStock> GetHeadersByProduct(
      FixedList<Core.InventoryStockEntry> stockByVendorProduct) {

      var list = new List<InventoryStock>();

      foreach (var stock in stockByVendorProduct) {
        var entry = new InventoryStock();

        var exist = list
          .Where(x => x.VendorProduct.VendorProductId == stock.VendorProduct.VendorProductId)
          .FirstOrDefault();
        if (exist == null) {

          entry.ItemType = ReportItemType.Group;
          entry.VendorProduct = stock.VendorProduct;
          entry.WarehouseBin = stock.WarehouseBin;
          entry.Stock = stockByVendorProduct
            .Where(x => x.VendorProduct.VendorProductId == stock.VendorProduct.VendorProductId)
            .Sum(x => x.Stock);
          entry.RealStock = stockByVendorProduct
            .Where(x => x.VendorProduct.VendorProductId == stock.VendorProduct.VendorProductId)
            .Sum(x => x.RealStock);
          entry.StockInProcess = stockByVendorProduct
            .Where(x => x.VendorProduct.VendorProductId == stock.VendorProduct.VendorProductId)
            .Sum(x => x.StockInProcess);

          list.Add(entry);
        }
      }

      return list.OrderBy(x => x.VendorProduct.ProductFields.ProductCode)
                 .ThenBy(x => x.VendorProduct.ProductPresentation.Id).ToFixedList();
    }


    internal FixedList<InventoryStock> GetStockLocationsByProduct(
      FixedList<Core.InventoryStockEntry> stockByVendorProduct, FixedList<InventoryStock> stockHeaders) {

      var list = new List<InventoryStock>();

      foreach (var header in stockHeaders) {
        
        var entries = stockByVendorProduct.Where(
              x => x.VendorProduct.VendorProductUID == header.VendorProduct.VendorProductUID)
          .ToList();

        list.Add(header);
        list.AddRange(MapToEntriesByProduct(entries));
      }
      return list.ToFixedList();
    }


    internal FixedList<InventoryStock> GetStockByProductForLocations(
      FixedList<Core.InventoryStockEntry> stockByVendorProduct, FixedList<InventoryStock> stockHeaders) {

      var list = new List<InventoryStock>();

      foreach (var header in stockHeaders) {

        var entries = stockByVendorProduct.Where(
              x => x.WarehouseBin.Id == header.WarehouseBin.Id)
          .ToList();

        list.Add(header);
        list.AddRange(MapToEntriesByLocation(entries));
      }
      return list.ToFixedList();
    }


    private IEnumerable<InventoryStock> MapToEntriesByLocation(List<Core.InventoryStockEntry> salesEntries) {

      var returnedList = new List<InventoryStock>();
      foreach (var stock in salesEntries) {

        var entry = new InventoryStock();
        entry.ItemType = ReportItemType.Entry;
        entry.VendorProduct = stock.VendorProduct;
        entry.WarehouseBin = stock.WarehouseBin;
        entry.Stock = stock.Stock;
        entry.RealStock = stock.RealStock;
        entry.StockInProcess = stock.StockInProcess;
        returnedList.Add(entry);
      }
      return returnedList.OrderBy(x => x.VendorProduct.Id)
                         .ThenBy(x => x.VendorProduct.ProductPresentation.Id).ToList();
    }


    private IEnumerable<InventoryStock> MapToEntriesByProduct(List<Core.InventoryStockEntry> salesEntries) {

      var returnedList = new List<InventoryStock>();
      foreach (var stock in salesEntries) {
        
        var entry = new InventoryStock();
        entry.ItemType = ReportItemType.Entry;
        entry.VendorProduct = stock.VendorProduct;
        entry.WarehouseBin = stock.WarehouseBin;
        entry.Stock = stock.Stock;
        entry.RealStock = stock.RealStock;
        entry.StockInProcess = stock.StockInProcess;
        returnedList.Add(entry);
      }
      return returnedList;
    }


    internal FixedList<InventoryStock> GetHeadersByLocation(
      FixedList<Core.InventoryStockEntry> stockByVendorProduct) {

      var list = new List<InventoryStock>();

      foreach (var stock in stockByVendorProduct) {
        var entry = new InventoryStock();

        var exist = list
          .Where(x => x.WarehouseBin.Id == stock.WarehouseBin.Id)
          .FirstOrDefault();
        if (exist == null) {

          entry.ItemType = ReportItemType.Group;
          entry.VendorProduct = stock.VendorProduct;
          entry.WarehouseBin = stock.WarehouseBin;
          entry.Stock = stockByVendorProduct
            .Where(x => x.WarehouseBin.Id == stock.WarehouseBin.Id)
            .Sum(x => x.Stock);
          entry.RealStock = stockByVendorProduct
            .Where(x => x.WarehouseBin.Id == stock.WarehouseBin.Id)
            .Sum(x => x.RealStock);
          entry.StockInProcess = stockByVendorProduct
            .Where(x => x.WarehouseBin.Id == stock.WarehouseBin.Id)
            .Sum(x => x.StockInProcess);

          list.Add(entry);
        }
      }

      return list.OrderBy(x => x.WarehouseBin.Id)
                 .ThenBy(x => x.VendorProduct.ProductPresentation.Id).ToFixedList();
    }


    #endregion Public methods

  } // class ReportHelper

} // namespace Empiria.Trade.Inventory.Domain
