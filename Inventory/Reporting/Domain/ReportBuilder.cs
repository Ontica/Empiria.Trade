/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Partitioned Type / Information Holder   *
*  Type     : ReportBuilder                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Generate data for reports.                                                                     *
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


  /// <summary>Generate data for reports.</summary>
  internal class ReportBuilder {


    #region Constructors and parsers

    ReportQuery query = new ReportQuery();

    public ReportBuilder(ReportQuery _query) {
      this.query = _query;
    }


    #endregion Constructors and parsers


    #region Public methods


    internal FixedList<IReportEntry> GeneratePurchasingReport() {

      var list = new FixedList<ReportEntry>();

      return new FixedList<IReportEntry>(list.Select(x => (IReportEntry) x));
    }


    internal FixedList<IReportEntry> GenerateStocksByProduct() {

      var stockByVendorProduct = GetStockByVendorProduct();
      var helper = new ReportHelper(query);

      FixedList<InventoryStockEntry> stockHeaders = helper.GetHeadersByProduct(stockByVendorProduct);

      FixedList<InventoryStockEntry> stockEntries =
        helper.GetStockLocationsByProduct(stockByVendorProduct, stockHeaders);

      return new FixedList<IReportEntry>(stockEntries.Select(x => (IReportEntry) x));
    }


    internal FixedList<IReportEntry> GenerateStocksByLocation() {

      var warehouseBins = WarehouseBin.ParseIdList(query.WarehouseBins).ToList();

      var stockByVendorProduct = CataloguesUseCases.GetStockByUIDList(
          new List<int>(), warehouseBins);

      var helper = new ReportHelper(query);

      FixedList<InventoryStockEntry> stockHeaders = helper.GetHeadersByLocation(stockByVendorProduct);

      FixedList<InventoryStockEntry> stockEntries =
        helper.GetStockByProductForLocations(stockByVendorProduct, stockHeaders);

      return new FixedList<IReportEntry>(stockEntries.Select(x => (IReportEntry) x));
    }


    #endregion Public methods


    #region Private methods


    private FixedList<SalesInventoryStock> GetStockByVendorProduct() {

      var products = SimpleObjects.ConcatIntsToString(
        ProductFields.ParseIdList(query.Products).ToList());

      var vendorProducts = ProductUseCases.GetVendorProductByProduct(products);

      var helper = new ReportHelper(query);
      return helper.GetStockByVendorProduct(vendorProducts);
    }


    #endregion Private methods


  } // class ReportBuilder

} // namespace Empiria.Trade.Inventory.Domain
