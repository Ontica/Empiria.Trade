/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Partitioned Type / Information Holder   *
*  Type     : InventoryReportsBuilder                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Generate data for Inventory order.                                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using System.Linq;
using Empiria.Trade.Core;
using Empiria.Trade.Core.Catalogues;
using Empiria.Trade.Inventory.Adapters;
using Empiria.Trade.Products;
using Empiria.Trade.Products.UseCases;

namespace Empiria.Trade.Inventory.Domain {


  /// <summary></summary>
  internal class InventoryReportsBuilder {


    #region Public methods


    internal FixedList<IInventoryReport> GenerateStocksByProduct(ReportQuery query) {

      var product = ProductFields.Parse(query.ProductUID);

      var vendorProducts = ProductUseCases.GetVendorProductByProduct(product.ProductId);

      var stockByVendorProduct = GetStockByVendorProduct(vendorProducts);

      FixedList<InventoryStockEntry> list = MapToInventoryStockEntries(stockByVendorProduct);

      return new FixedList<IInventoryReport>(list.Select(x => (IInventoryReport) x));
    }


    internal FixedList<IInventoryReport> GenerateStocksByLocation(ReportQuery query) {

      var list = new FixedList<InventoryReportEntry>();

      return new FixedList<IInventoryReport>(list.Select(x => (IInventoryReport) x));
    }


    #endregion Public methods


    #region Private methods

    private FixedList<SalesInventoryStock> GetStockByVendorProduct(FixedList<VendorProduct> vendorProducts) {

      var list = new List<SalesInventoryStock>();
      foreach (var vendorProduct in vendorProducts) {

        var stockByVendorProduct = CataloguesUseCases.GetInventoryStockByClauses(
          vendorProduct.VendorProductId, 0);

        list.AddRange(stockByVendorProduct);
      }

      return list.ToFixedList();
    }


    private FixedList<InventoryStockEntry> MapToInventoryStockEntries(
      FixedList<SalesInventoryStock> stockByVendorProduct) {

      var list = new List<InventoryStockEntry>();

      foreach (var stock in stockByVendorProduct) {
        var entry = new InventoryStockEntry();

        var exist = list
          .Where(x => x.VendorProduct.VendorProductId == stock.VendorProduct.VendorProductId)
          .FirstOrDefault();
        if (exist == null) {

          entry.VendorProduct = stock.VendorProduct;
          entry.WarehouseBin = stock.WarehouseBin;
          entry.Stock = stockByVendorProduct
            .Where(x => x.VendorProduct.VendorProductId == stock.VendorProduct.VendorProductId)
            .Sum(x => x.Stock);
          entry.RealStock = stockByVendorProduct
            .Where(x => x.VendorProduct.VendorProductId == stock.VendorProduct.VendorProductId)
            .Sum(x => x.RealStock);

          list.Add(entry);
        }
      }

      return list.ToFixedList();
    }

    #endregion Private methods


  } // class InventoryReportsBuilder

} // namespace Empiria.Trade.Inventory.Domain
