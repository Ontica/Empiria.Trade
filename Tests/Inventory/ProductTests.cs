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
using Empiria.Trade.Inventory.Products.UseCases;
using Empiria.Trade.Inventory.Products.Adapters;
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
    public async Task ShouldGetProductDtoTest() {

      var usecase = ProductsUseCases.UseCaseInteractor();
      ProductQuery query = new ProductQuery {
        Keywords = "TORNILLO  ARADO GRADO"
      };
      ProductDto sut = await usecase.BuildProducts(query).ConfigureAwait(false);
        
      Assert.NotNull(sut);
      Assert.NotEmpty(sut.ProductList);
      
    }


    [Fact]
    public async Task ShouldGetProductListTest() {

      var usecase = ProductsUseCases.UseCaseInteractor();
      ProductQuery query = new ProductQuery {
        Keywords = "TA58X412"
      };

      FixedList<IProductEntryDto> sut = await usecase.BuildProductsList(query).ConfigureAwait(false);

      Assert.NotNull(sut);
      Assert.NotEmpty(sut);

    }


    #endregion Facts


  } // class ProductTests

} // namespace Empiria.Trade.Tests.Products
