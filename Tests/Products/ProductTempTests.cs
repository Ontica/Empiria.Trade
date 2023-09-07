using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Empiria.Trade.Products.Adapters;
using Empiria.Trade.Products.UseCases;
using Xunit;

namespace Empiria.Trade.Tests.Products {
  
  /// <summary></summary>
  public class ProductTempTests {

    #region Initialization

    public ProductTempTests() {
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


    [Fact]
    public void GetDataCountFromDbTest() {

      var service = new TradeDataSchemaManager.Services.Services(true);
      var dt = service.GetDataCountFromDb();

      Assert.NotNull(dt);

    }

    #endregion Facts

  }
}
