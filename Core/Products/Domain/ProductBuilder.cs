/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Partitioned Type / Information Holder   *
*  Type     : Product                                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Generate data for Products.                                                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System.Linq;
using Empiria.Trade.Products.Adapters;
using Empiria.Trade.Products.Data;

namespace Empiria.Trade.Products.Domain {

  /// <summary>Generate data for roducts.</summary>
  internal class ProductBuilder {

    #region Constructors and parsers

    private readonly ProductQuery query;


    public ProductBuilder(ProductQuery _query) {
      query = _query;
    }

    #endregion Constructors and parsers


    #region Public methods V2

    internal FixedList<ProductEntry> GetProducts() {

      var helper = new ProductHelper(query);

      return helper.BaseProducts.OrderBy(x=>x.InternalCode.Length)
                                .ThenBy(x => x.InternalCode)
                                .ToFixedList();
    }


    #endregion Public methods V2

    #region Public methods

    internal FixedList<ProductEntry> GetProductsForOrder() {

      FixedList<ProductEntry> products = ProductDataService.GetProductsForOrder(query);

      var helper = new ProductHelper(query);

      FixedList<ProductEntry> productsByStock = helper.GetProductsByStock(products);

      ValidateToGetPriceList(productsByStock);

      FixedList<ProductEntry> productsByCode = helper.GetProductsByCode(productsByStock);

      FixedList<ProductEntry> orderedProducts = helper.GetProductsOrderBy(productsByCode);

      return orderedProducts;
    }


    internal FixedList<ProductEntry> GetProductsForPurchaseOrder() {

      FixedList<ProductEntry> products = ProductDataService.GetProductsList(query.Keywords);

      var helper = new ProductHelper(query);

      FixedList<ProductEntry> productsByStock = helper.GetProductsByStock(products);

      FixedList<ProductEntry> productsByCode = helper.GetProductsByCodeForPurchaseOrder(productsByStock);

      FixedList<ProductEntry> orderedProducts = helper.GetProductsOrderBy(productsByCode);

      return orderedProducts;
    }


    internal FixedList<ProductEntry> GetProductsList() {

      FixedList<ProductEntry> products = ProductDataService.GetProductsList(query.Keywords);

      var helper = new ProductHelper(query);

      FixedList<ProductEntry> productsByStock = helper.GetProductsByStock(products);

      ValidateToGetPriceList(productsByStock);

      FixedList<ProductEntry> productsByCode = helper.GetProductsByCode(productsByStock);

      FixedList<ProductEntry> orderedProducts = helper.GetProductsOrderBy(productsByCode);

      return orderedProducts;
    }

    #endregion Public methods


    #region Private methods


    private void ValidateToGetPriceList(FixedList<ProductEntry> products) {

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
