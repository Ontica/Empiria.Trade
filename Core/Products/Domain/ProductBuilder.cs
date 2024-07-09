/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Partitioned Type / Information Holder   *
*  Type     : Product                                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Generate data for Products.                                                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Empiria.Json;
using Empiria.Storage;
using Empiria.Trade.Core.Catalogues;
using Empiria.Trade.Products.Adapters;
using Empiria.Trade.Products.Data;
using Newtonsoft.Json;

namespace Empiria.Trade.Products.Domain {

  /// <summary>Generate data for roducts.</summary>
  internal class ProductBuilder {

    #region Constructors and parsers

    public ProductBuilder() {
      
    }


    #endregion Constructors and parsers


    #region Public methods

    public object BuildProduct(Product product) {

      return product;
    }


    internal FixedList<Product> GetProductsForOrder(ProductQuery query) {

      FixedList<Product> products = ProductDataService.GetProductsForOrder(query);

      var helper = new ProductHelper(query);

      FixedList<Product> productsByStock = helper.GetProductsByStock(products);
      
      ValidateToGetPriceList(productsByStock, query);

      FixedList<Product> productsByCode = helper.GetProductsByCode(productsByStock);

      FixedList<Product> orderedProducts = helper.GetProductsOrderBy(productsByCode);

      return orderedProducts;
    }


    internal FixedList<Product> GetProductsForPurchaseOrder(ProductQuery query) {

      FixedList<Product> products = ProductDataService.GetProductsList(query.Keywords);

      var helper = new ProductHelper(query);

      FixedList<Product> productsByStock = helper.GetProductsByStock(products);

      FixedList<Product> productsByCode = helper.GetProductsByCodeForPurchaseOrder(productsByStock);

      FixedList<Product> orderedProducts = helper.GetProductsOrderBy(productsByCode);

      return orderedProducts;
    }
    

    internal FixedList<Product> GetProductsList(ProductQuery query) {

      FixedList<Product> products = ProductDataService.GetProductsList(query.Keywords);

      var helper = new ProductHelper(query);

      FixedList<Product> productsByStock = helper.GetProductsByStock(products);

      ValidateToGetPriceList(productsByStock, query);

      FixedList<Product> productsByCode = helper.GetProductsByCode(productsByStock);

      FixedList<Product> orderedProducts = helper.GetProductsOrderBy(productsByCode);

      return orderedProducts;
    }


    internal VendorProduct GetStockAndAddToVendorProduct(VendorProduct vendorProduct) {

      var inventoryEntry = CataloguesData.GetInventoryEntry(vendorProduct.Id);

      if (inventoryEntry != null) {
        vendorProduct.InputQuantity = inventoryEntry.InputQuantity;
      }

      return vendorProduct;
    }

    #endregion Public methods


    #region Private methods


    private void ValidateToGetPriceList(FixedList<Product> products, ProductQuery query) {

      var helper = new ProductHelper(query);

      if (query.CustomerUID != "") {

        helper.GetProductsWithCustomerPrice(products);

      } else {

        helper.GetDefaultProductBasePrices(products);

      }

    }

    #endregion Private methods


  } // class ProductBuilder


} // Empiria.Trade.Products.Domain
