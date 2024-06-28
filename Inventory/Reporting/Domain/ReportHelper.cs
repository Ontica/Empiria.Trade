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

    internal FixedList<SalesInventoryStock> GetStockByVendorProduct(FixedList<VendorProduct> vendorProducts) {

      var list = new List<SalesInventoryStock>();
      foreach (var vendorProduct in vendorProducts) {

        var stockByVendorProduct = CataloguesUseCases.GetInventoryStockByClauses(
          vendorProduct.VendorProductId, 0);

        list.AddRange(stockByVendorProduct);
      }

      return list.ToFixedList();
    }


    internal FixedList<InventoryStockEntry> MapToStockHeaders(
      FixedList<SalesInventoryStock> stockByVendorProduct) {

      var list = new List<InventoryStockEntry>();

      foreach (var stock in stockByVendorProduct) {
        var entry = new InventoryStockEntry();

        var exist = list
          .Where(x => x.VendorProduct.VendorProductId == stock.VendorProduct.VendorProductId)
          .FirstOrDefault();
        if (exist == null) {

          entry.ItemType = ReportItemType.Header;
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

      return list.OrderBy(x => x.VendorProduct.ProductPresentation.Id).ToFixedList();
    }


    internal FixedList<InventoryStockEntry> MapToStockLocations(
      FixedList<SalesInventoryStock> stockByVendorProduct) {

      return new FixedList<InventoryStockEntry>();
    }

    #endregion Public methods

  } // class ReportHelper

} // namespace Empiria.Trade.Inventory.Domain
