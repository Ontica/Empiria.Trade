/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Test cases                              *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Use cases tests                         *
*  Type     : ProductTests                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Test cases for products.                                                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Xunit;

using Empiria.Tests;
using Empiria.Trade.Products.UseCases;
using Empiria.Trade.Products.Adapters;
using System.Threading.Tasks;

namespace Empiria.Trade.Tests.Products {

  /// <summary>Test cases for products.</summary>
  public class ProductTests {

    #region Initialization

    public ProductTests() {
      //TestsCommonMethods.Authenticate();
    }

    #endregion Initialization


    #region Facts


    [Fact]
    public async Task ShouldGetProductList() {

      var usecase = ProductsUseCases.UseCaseInteractor();
      string clauses = "tornillo galvanizado";
      ProductDto sut = await usecase.BuildProducts(clauses).ConfigureAwait(false);
        
      Assert.NotNull(sut);
      Assert.NotEmpty(sut.ProductList);
      
    }


    #endregion Facts


  } // class ProductTests

} // namespace Empiria.Trade.Tests.Products
