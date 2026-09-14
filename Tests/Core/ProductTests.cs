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
using Empiria.Trade.Core;
using Empiria.Trade.Products.Data;
using System.Collections.Generic;

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
    public void GetProductPriceTest() {

      FixedList<ProductPrices> sut = ProductDataService.GetProductPrices(-27883);
      
      Assert.NotNull(sut);
      Assert.NotEmpty(sut);
    }


    [Fact]
    public void GetProductPriceTypeTest() {

      FixedList<ProductPriceType> sut = ProductPriceType.GetList();
      
      Assert.NotNull(sut);
      Assert.NotEmpty(sut);
    }


    [Fact]
    public async Task GetProductsForOrderTest() {

      var usecase = ProductForOrderUseCases.UseCaseInteractor();

      var items = new List<SalesOrderItemsFields>();

      var item = new SalesOrderItemsFields {
        Quantity = 5000,
        SalesPrice = 2156.15m,
        Subtotal = 2156.15m,
        UnitPrice = 0.43123m,
        VendorProductUID = "4fc7db76-f004-4280-9ad7-b42b4a8e6924"
      };

      items.Add(item);

      ProductOrderQuery query = new ProductOrderQuery {
        Keywords = "TG5G14X1",
        OnStock = true,
        Order = {
          CustomerUID = "c74f0f44-39a4-4f8b-8e0a-7853909648b7",
          CustomerAddressUID = "asas1212-caa4-460e-95cd-de7e11122233",
          CustomerContactUID = "68449009-0f03-401b-a3af-dd2b97724bf2",
          //PaymentConditions = "Credito",
          SalesAgentUID = "9f63cc87-f7e9-4664-81fe-d176c06e81ec",
          OrderNumber = "P-2XKOROX2UK",
          ShippingMethod = ShippingMethods.RutaLocal,
          SupplierUID = "4d462287-9cee-400d-bd63-e2f31b171315",
          //OrderTime = new System.DateTime(2026, 08, 12),
          Status = OrderStatus.Captured,
          Items = items.ToFixedList()
        }
      };

      FixedList<ProductForSearchingDto> sut = await usecase.GetProductsForOrder(query).ConfigureAwait(false);

      Assert.NotNull(sut);
      Assert.NotEmpty(sut);
    }


    [Fact]
    public void GetProductForSearcherTest() {

      var usecase = ProductUseCases.UseCaseInteractor();
      ProductQuery query = new ProductQuery {
        Keywords = "THMEF10X70-100", //TG5G516X3 TCC12X1
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
        Keywords = "TMG12X1-35", //TG5G516X3 TCC12X1
        OnStock = false, 
        SupplierUID = ""
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
