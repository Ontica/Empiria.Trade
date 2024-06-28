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


    internal FixedList<IInventoryReport> GeneratePurchasingReport(ReportQuery query) {

      var list = new FixedList<InventoryReportEntry>();

      return new FixedList<IInventoryReport>(list.Select(x => (IInventoryReport) x));
    }


    internal FixedList<IInventoryReport> GenerateStocksByProduct(ReportQuery query) {

      var product = ProductFields.Parse(query.ProductUID);

      var vendorProducts = ProductUseCases.GetVendorProductByProduct(product.ProductId);

      var helper = new ReportHelper(query);
      var stockByVendorProduct = helper.GetStockByVendorProduct(vendorProducts);

      FixedList<InventoryStockEntry> stockHeaders = helper.MapToStockHeaders(stockByVendorProduct);

      FixedList<InventoryStockEntry> stockLocations = helper.MapToStockLocations(stockByVendorProduct);
      return new FixedList<IInventoryReport>(stockHeaders.Select(x => (IInventoryReport) x));
    }


    internal FixedList<IInventoryReport> GenerateStocksByLocation(ReportQuery query) {

      var list = new FixedList<InventoryStockEntry>();

      return new FixedList<IInventoryReport>(list.Select(x => (IInventoryReport) x));
    }


    #endregion Public methods


    #region Private methods

    

    #endregion Private methods


  } // class InventoryReportsBuilder

} // namespace Empiria.Trade.Inventory.Domain
