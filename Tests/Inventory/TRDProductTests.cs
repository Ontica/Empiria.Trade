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

namespace Empiria.Trade.Tests {

  /// <summary>Test cases for products.</summary>
  public class TRDProductTests {

    #region Initialization

    public TRDProductTests() {
      //TestsCommonMethods.Authenticate();
    }

    #endregion Initialization


    #region Facts



    [Fact]
    public void GetProductTest() {

      var usecase = TRDProductUseCases.UseCaseInteractor();

      string uid = "78be58ab-75d0-4e8f-bed4-2305e70101c8";

      IProductEntryDto sut = usecase.GetTRDProduct(uid);

      Assert.NotNull(sut);

    }


    [Fact]
    public async Task GetProductListTest() {

      var usecase = TRDProductUseCases.UseCaseInteractor();
      ProductQuery query = new ProductQuery {
        Keywords = "TM1X2"
      };

      FixedList<IProductEntryDto> sut = await usecase.GetProductsList(query).ConfigureAwait(false);

      Assert.NotNull(sut);
      Assert.NotEmpty(sut);

    }


    [Fact]
    public async Task GetProductsForOrderTest() {

      var usecase = ProductForOrderUseCases.UseCaseInteractor();

      ProductOrderQuery query = new ProductOrderQuery {
        Keywords = "TA58X412",
        Order = { 
          CustomerUID = "7ed4164a-24b0-4728-910b-eb26f0684a12",
          SalesAgentUID = "",
          SupplierUID = ""
        }
      };

      FixedList<IProductEntryDto> sut = await usecase.GetProductsForOrder(query).ConfigureAwait(false);

      Assert.NotNull(sut);
      Assert.NotEmpty(sut);

    }


    [Fact]
    public void GetInventoryEntryTest() {

      var usecase = TRDProductUseCases.UseCaseInteractor();

      string uid = "eed89084-4b16-4a89-bcf8-2a26c82448cc";

      InventoryEntry sut = usecase.GetInventoryEntry(uid);

      Assert.NotNull(sut);

    }


    [Fact]
    public void GetProductPresentationTest() {

      var usecase = TRDProductUseCases.UseCaseInteractor();

      string uid = "6012ea18-82d2-4e0e-9fe2-f81a1d076b94";

      ProductPresentation sut = usecase.GetProductPresentation(uid);

      Assert.NotNull(sut);

    }


    [Fact]
    public void GetVendorProductTest() {

      var usecase = TRDProductUseCases.UseCaseInteractor();

      string uid = "aa2396b5-9acf-43bd-870a-112e9ed61351";

      VendorProduct sut = usecase.GetVendorProduct(uid);

      Assert.NotNull(sut);

    }


    [Fact]
    public void GetProductGroupTest() {

      var usecase = TRDProductUseCases.UseCaseInteractor();

      string uid = "382dd00c-5be5-43b3-aeca-5d5addb72fb2";

      ProductGroup sut = usecase.GetProductGroup(uid);

      Assert.NotNull(sut);

    }


    [Fact]
    public void GetProductSubgroupTest() {

      var usecase = TRDProductUseCases.UseCaseInteractor();

      string uid = "UID-SUBGROUP-0000-001";

      ProductSubgroup sut = usecase.GetProductSubgroup(uid);

      Assert.NotNull(sut);

    }


    [Fact]
    public void GetWarehouseTest() {

      var usecase = TRDProductUseCases.UseCaseInteractor();

      string uid = "2f6dfb0d-137b-4309-94ac-c5f7b8fbc9df";

      Warehouse sut = usecase.GetWarehouse(uid);

      Assert.NotNull(sut);

    }


    [Fact]
    public void GetWarehouseBinTest() {

      var usecase = TRDProductUseCases.UseCaseInteractor();

      string uid = "22d33c45-c41f-426c-92f4-453fdc0ccc8a";

      WarehouseBin sut = usecase.GetWarehouseBin(uid);

      Assert.NotNull(sut);

    }


    [Fact]
    public async Task ShouldUpdateUID() {

      var usecase = TRDProductUseCases.UseCaseInteractor();

      var query = new TableQuery {
        TableName = "TRDProductPrice",
        IdName = "ProductPriceId",
        UidName = "ProductPriceUID"
      };

      string sut = await usecase.UpdateGUID(query).ConfigureAwait(false);

      Assert.NotEmpty(sut);

    }


    #endregion Facts


  } // class ProductTests

} // namespace Empiria.Trade.Tests.Products
