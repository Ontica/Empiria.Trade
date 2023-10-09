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
    public async Task ShouldGetProductListTest() {

      var usecase = ProductsUseCases.UseCaseInteractor();
      ProductQuery query = new ProductQuery {
        Keywords = "TORNILLO  ARADO GRADO"
      };

      FixedList<IProductEntryDto> sut = await usecase.BuildProductsList(query).ConfigureAwait(false);

      Assert.NotNull(sut);
      Assert.NotEmpty(sut);

    }


    #endregion Facts


  } // class ProductTests

} // namespace Empiria.Trade.Tests.Products
