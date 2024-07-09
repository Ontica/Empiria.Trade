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
using System.Threading.Tasks;
using Empiria.DataTypes;

using Xunit;

using Empiria.Tests;
using Empiria.Trade.Products.UseCases;
using Empiria.Trade.Products.Adapters;
using Empiria.Trade.Products;
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
    public void GetProductTest() {

      var usecase = ProductUseCases.UseCaseInteractor();

      string uid = "78be58ab-75d0-4e8f-bed4-2305e70101c8";

      IProductEntryDto sut = usecase.GetProductByUID(uid);

      Assert.NotNull(sut);
    }


    [Fact]
    public async Task GetProductListTest() {

      var usecase = ProductUseCases.UseCaseInteractor();
      ProductQuery query = new ProductQuery {
        Keywords = "PPBTA14X34-3500",
        OnStock= true
      };

      FixedList<IProductEntryDto> sut = await usecase.GetProductsList(query).ConfigureAwait(false);

      Assert.NotNull(sut);
      Assert.NotEmpty(sut);
    }


    [Fact]
    public async Task GetProductListForPurchaseOrderTest() {

      var usecase = ProductUseCases.UseCaseInteractor();
      ProductQuery query = new ProductQuery {
        Keywords = "PPBTA14X34-3500",
        OnStock = true
      };

      FixedList<IProductEntryDto> sut = await usecase.GetProductsForPurchaseOrder(query).ConfigureAwait(false);

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
    public void GetVendorProductTest() {

      var usecase = ProductUseCases.UseCaseInteractor();

      string uid = "1d47e4e5-ff97-4197-8bd1-b49df2780c32";

      VendorProduct sut = usecase.GetVendorProduct(uid);

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
