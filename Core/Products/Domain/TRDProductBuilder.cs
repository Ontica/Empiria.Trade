/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Partitioned Type / Information Holder   *
*  Type     : Product                                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Generate data for TRDProducts.                                                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Empiria.Json;
using Empiria.Storage;
using Empiria.Trade.Products.Adapters;
using Empiria.Trade.Products.Data;
using Newtonsoft.Json;

namespace Empiria.Trade.Products.Domain {

  /// <summary>Generate data for TRDProducts.</summary>
  internal class TRDProductBuilder {

    #region Constructor

    public TRDProductBuilder() {
      
    }


    #endregion Constructor


    #region Public methods

    public object BuildProduct(Product product) {

      return product;
    }


    internal FixedList<Product> GetProductsForOrder(ProductQuery query) {

      FixedList<Product> products = TRDProductDataService.GetProductsForOrder(query);

      ValidateToGetPriceList(products, query);

      return products;
    }


    internal FixedList<Product> GetProductsList(ProductQuery query) {

      FixedList<Product> products = TRDProductDataService.GetProductsList(query.Keywords);

      ValidateToGetPriceList(products, query);

      return products;
    }


    internal VendorProduct GetStockAndAddToVendorProduct(VendorProduct vendorProduct) {

      var inventoryEntry = TRDProductDataService.GetInventoryEntry(vendorProduct.Id);

      if (inventoryEntry != null) {
        vendorProduct.InputQuantity = inventoryEntry.InputQuantity;
      }

      return vendorProduct;
    }

    #endregion Public methods


    #region Private methods


    private void ValidateToGetPriceList(FixedList<Product> products, ProductQuery query) {

      var helper = new TRDProductHelper(query);

      if (query.CustomerUID != "") {

        helper.GetProductsWithCustomerPrice(products);

      } else {

        helper.GetDefaultProductBasePrices(products);

      }

    }

    #endregion Private methods


  } // class TRDProductBuilder


} // Empiria.Trade.Products.Domain
