/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Test cases                              *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Use cases tests                         *
*  Type     : ProductTests                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Test cases for products.                                                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System.Threading.Tasks;
using Xunit;

using Empiria.Trade.Products;
using Empiria.Trade.Products.Adapters;
using Empiria.Trade.Products.UseCases;
using Empiria.Trade.Sales.Adapters;
using Empiria.Trade.Sales.UseCases;

namespace Empiria.Trade.Tests.Core {

  /// <summary>Test cases for products.</summary>
  public class ProductTests {

    #region Initialization

    public ProductTests() {
      //TestsCommonMethods.Authenticate();
    }

    #endregion Initialization


    #region Facts

    [Fact]
    public void GetProductForSearcherTest() {

      var usecase = ProductUseCases.UseCaseInteractor();
      ProductQuery query = new ProductQuery {
        Keywords = "TMG12X4", //TG5G516X3 TCC12X1
        OnStock = true
      };

      FixedList<ProductForSearchingDto> sut = usecase.GetProductsForSearcher(query);

      Assert.NotNull(sut);
      Assert.NotEmpty(sut);
    }


    [Fact]
    public void GetProductsForPurchaseOrderTest() {

      var usecase = ProductUseCases.UseCaseInteractor();
      ProductQuery query = new ProductQuery {
        Keywords = "TMG12X4", //TG5G516X3 TCC12X1
        OnStock = false,
        SupplierUID = "b8b6d1ce-ffd0-47fd-92ef-3db32fa44ed5"
      };

      FixedList<ProductForSearchingDto> sut = usecase.GetProductsForPurchaseOrder(query);

      Assert.NotNull(sut);
      Assert.NotEmpty(sut);
    }


    [Fact]
    public async Task GetProductListV1Test() {

      var usecase = ProductUseCases.UseCaseInteractor();
      ProductQuery query = new ProductQuery {
        Keywords = "PPBTA14X34-3500",
        OnStock= false
      };

      FixedList<IProductEntryDto> sut = await usecase.GetProductsListV1(query).ConfigureAwait(false);

      Assert.NotNull(sut);
      Assert.NotEmpty(sut);
    }


    [Fact]
    public async Task GetProductsForOrderTest() {

      var usecase = ProductForOrderUseCases.UseCaseInteractor();

      ProductOrderQuery query = new ProductOrderQuery {
        Keywords = "PPBTA14X34-3500",
        OnStock = true,
        Order = {
          CustomerUID = "b251959f-2b7b-46af-bdf6-09171a6182ee",
          SalesAgentUID = "",
          SupplierUID = "",

        }
      };

      FixedList<IProductEntryDto> sut = await usecase.GetProductsForOrder(query).ConfigureAwait(false);

      Assert.NotNull(sut);
      Assert.NotEmpty(sut);
    }


    [Fact]
    public void GetProductPresentationTest() {

      var usecase = ProductUseCases.UseCaseInteractor();

      string uid = "6012ea18-82d2-4e0e-9fe2-f81a1d076b94";

      ProductPresentation sut = usecase.GetProductPresentation(uid);

      Assert.NotNull(sut);
    }


    [Fact]
    public void GetProductGroupTest() {

      var usecase = ProductUseCases.UseCaseInteractor();

      string uid = "382dd00c-5be5-43b3-aeca-5d5addb72fb2";

      ProductGroup sut = usecase.GetProductGroup(uid);

      Assert.NotNull(sut);
    }


    [Fact]
    public void GetProductSubgroupTest() {

      var usecase = ProductUseCases.UseCaseInteractor();

      string uid = "UID-SUBGROUP-0000-001";

      ProductSubgroup sut = usecase.GetProductSubgroup(uid);

      Assert.NotNull(sut);
    }


    #endregion Facts


  } // class ProductTests

} // namespace Empiria.Trade.Tests.Products
