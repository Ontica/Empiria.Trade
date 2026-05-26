/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Partitioned Type / Information Holder   *
*  Type     : Product                                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Generate data for Products.                                                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
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

    internal FixedList<Product> GetProducts() {

      var helper = new ProductHelper(query);

      return helper.BaseProducts;
    }


    #endregion Public methods V2

    #region Public methods

    public object BuildProduct(Product product) {

      return product;
    }


    internal FixedList<Product> GetProductsForOrder() {

      FixedList<Product> products = ProductDataService.GetProductsForOrder(query);

      var helper = new ProductHelper(query);

      FixedList<Product> productsByStock = helper.GetProductsByStock(products);

      ValidateToGetPriceList(productsByStock);

      FixedList<Product> productsByCode = helper.GetProductsByCode(productsByStock);

      FixedList<Product> orderedProducts = helper.GetProductsOrderBy(productsByCode);

      return orderedProducts;
    }


    internal FixedList<Product> GetProductsForPurchaseOrder() {

      FixedList<Product> products = ProductDataService.GetProductsList(query.Keywords);

      var helper = new ProductHelper(query);

      FixedList<Product> productsByStock = helper.GetProductsByStock(products);

      FixedList<Product> productsByCode = helper.GetProductsByCodeForPurchaseOrder(productsByStock);

      FixedList<Product> orderedProducts = helper.GetProductsOrderBy(productsByCode);

      return orderedProducts;
    }


    internal FixedList<Product> GetProductsList() {

      FixedList<Product> products = ProductDataService.GetProductsList(query.Keywords);

      var helper = new ProductHelper(query);

      FixedList<Product> productsByStock = helper.GetProductsByStock(products);

      ValidateToGetPriceList(productsByStock);

      FixedList<Product> productsByCode = helper.GetProductsByCode(productsByStock);

      FixedList<Product> orderedProducts = helper.GetProductsOrderBy(productsByCode);

      return orderedProducts;
    }

    #endregion Public methods


    #region Private methods


    private void ValidateToGetPriceList(FixedList<Product> products) {

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
