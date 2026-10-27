/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Shipping and handling Management           Component : Test cases                              *
*  Assembly : Empiria.Trade.Shipping.dll                 Pattern   : Use cases tests                         *
*  Type     : ShippingTest                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Test cases for shipping.                                                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using System.Threading.Tasks;
using Empiria.DataTypes;

using Xunit;

using Empiria.Tests;
using Empiria.Trade.Sales.ShippingAndHandling.UseCases;
using Empiria.Trade.Sales.ShippingAndHandling.Adapters;
using Empiria.Trade.Sales.ShippingAndHandling;
using Empiria.Trade.Core.Catalogues;
using System.Collections.Generic;
using Empiria.Services;
using Empiria.Trade.Sales;

namespace Empiria.Trade.Tests {


  /// <summary>Test cases for shipping.</summary>
  public class ShippingTest {


    #region Initialization

    public ShippingTest() {
      TestsCommonMethods.Authenticate();
    }

    #endregion Initialization


    #region Facts


    [Fact]
    public void GetShippingBillingTest() {

      string shippingUID = "2d4e170f-8bb4-4ecf-9a4d-3609e542d25b";
      string orderUID = "959427b0-b5e0-4942-81c5-3b4725bccc83";

      var usecase = BillingUseCases.UseCaseInteractor();

      var sut = usecase.GetShippingBilling(shippingUID, orderUID);

      Assert.NotNull(sut);

    }


    [Fact]
    public void CreateShippingOrderTest() {

      var usecase = ShippingUseCases.UseCaseInteractor();

      ShippingDto sut = usecase.CreateShippingOrder(GetShippingFields());

      Assert.NotNull(sut);
    }


    [Fact]
    public void CreateShippingPalletTest() {

      var usecase = ShippingUseCases.UseCaseInteractor();

      string shippingUID = "c448a856-af33-4b06-976b-a3e0c67f17af";

      ShippingPalletFields fields = new ShippingPalletFields() {
        ShippingPalletName = "TARIMA X-001-1",
        Packages = new string[] {
          "6310a6d9-9ed6-4c34-bba2-87848cf32787",
          "1672717e-8606-4a2c-b69d-52f8835798ba"
        }
      };

      ShippingDto sut = usecase.CreateShippingPallet(shippingUID, fields);

      Assert.NotNull(sut);

    }


    [Fact]
    public void UpdateShippingPalletTest() {

      var usecase = ShippingUseCases.UseCaseInteractor();

      string shippingUID = "c448a856-af33-4b06-976b-a3e0c67f17af";
      string shippingPalletUID = "69d507d8-72e2-49aa-9d1f-39bd4d0e7d30";

      ShippingPalletFields fields = new ShippingPalletFields() {
        ShippingPalletName = "TARIMA X-001-1",
        Packages = new string[] {
          "6310a6d9-9ed6-4c34-bba2-87848cf32787",
          //"1672717e-8606-4a2c-b69d-52f8835798ba"
        }
      };

      ShippingDto sut = usecase.UpdateShippingPallet(shippingUID, shippingPalletUID, fields);

      Assert.NotNull(sut);
    }


    [Fact]
    public void DeleteOrderForShippingTest() {

      var usecase = ShippingUseCases.UseCaseInteractor();

      string shippingOrderUID = "c448a856-af33-4b06-976b-a3e0c67f17af";
      string orderUID = "7bbee1de-4af0-42c0-994f-90ccaf71fa54";

      ShippingDto sut = usecase.DeleteOrderForShipping(shippingOrderUID, orderUID);

      Assert.NotNull(sut);
    }


    [Fact]
    public void DeleteShippingOrderTest() {

      var usecase = ShippingUseCases.UseCaseInteractor();

      string shippingOrderUID = "c448a856-af33-4b06-976b-a3e0c67f17af";
      usecase.DeleteShippingOrder(shippingOrderUID);

      Assert.True(true);
    }


    [Fact]
    public void DeleteShippingPalletByShippingUIDTest() {

      var usecase = ShippingUseCases.UseCaseInteractor();

      string shippingOrderUID = "c448a856-af33-4b06-976b-a3e0c67f17af";
      string shippingPalletUID = "69d507d8-72e2-49aa-9d1f-39bd4d0e7d30";

      usecase.DeleteShippingPalletByUID(shippingOrderUID, shippingPalletUID);

      Assert.True(true);
    }


    [Fact]
    public void GetParcelSupplierListTest() {

      var usecase = CataloguesUseCases.UseCaseInteractor();

      FixedList<INamedEntity> sut = usecase.GetParcelSupplierList();

      Assert.NotNull(sut);
    }


    [Fact]
    public void GetShippingOrderByUIDTest() {

      var usecase = ShippingUseCases.UseCaseInteractor();
     
      ShippingEntry sut = ShippingEntry.Parse(1);

      //ShippingPackage sut = ShippingPackage.Parse(1);

      Assert.NotNull(sut);
    }


    [Fact]
    public void GetShippingOrderByIDTest() {

      var usecase = ShippingUseCases.UseCaseInteractor();
      Financial.MoneyAccount sut = Financial.MoneyAccount.Parse(-1);
      //ShippingEntry sut = ShippingEntry.Parse(1);
      //ShippingOrderItem sut = ShippingOrderItem.Parse(-1);

      Assert.NotNull(sut);
    }


    [Fact]
    public void GetShippingLabelsTest() {

      var usecase = ShippingLabelUseCases.UseCaseInteractor();
      string shippingUID = "6a52e473-e81e-4ea9-9c01-b3bacf903662";

      FixedList<ShippingLabel> sut = usecase.GetShippingLabels(shippingUID);
      Assert.NotNull(sut);
    }


    [Fact]
    public void SetShippingOrderToDeliveryTest() {

      var usecase = ShippingUseCases.UseCaseInteractor();
      string shippingUID = "c448a856-af33-4b06-976b-a3e0c67f17af";

      ShippingDto sut = usecase.SetShippingOrderToDelivery(shippingUID);
      Assert.NotNull(sut);
    }


    [Fact]
    public void SetShippingOrderToClosedTest() {

      var usecase = DeliveryUseCase.UseCaseInteractor();
      string shippingUID = "c448a856-af33-4b06-976b-a3e0c67f17af";

      ShippingDto sut = usecase.SetShippingOrderToClosed(shippingUID);
      Assert.NotNull(sut);
    }


    [Fact]
    public void GetShippingByOrderUIDTest() {

      var usecase = ShippingUseCases.UseCaseInteractor();

      string orderUID = "0ca1ee24-bf52-4295-896e-c200eb1bfd04";

      ShippingEntryDto sut = usecase.GetShippingByOrderUID(orderUID);

      Assert.NotNull(sut);

    }


    [Fact]
    public void GetShippingByOrderUIDListTest() {

      var usecase = ShippingUseCases.UseCaseInteractor();

      ShippingFieldsQuery query = new ShippingFieldsQuery {
        Orders = new[] {
                    "251dc352-f05e-4656-ad0e-46c60a035cdd",
                    "7bbee1de-4af0-42c0-994f-90ccaf71fa54"
                }
      };

      ShippingDto sut = usecase.GetShippingOrderByQuery(query);

      Assert.NotNull(sut);
    }


    [Fact]
    public void GetShippingListTest() {

      var usecase = ShippingUseCases.UseCaseInteractor();

      ShippingQuery query = new ShippingQuery();
      //query.Keywords = "GUIA PRUEBA 1";
      //query.ParcelSupplierUID = "875153f9-464a-4479-9b3c-5e47d94d4499";
      //query.Status = ShippingStatus.EnCaptura;
      query.QueryType = ShippingQueryType.Delivery;
      FixedList<ShippingEntryDto> sut = usecase.GetShippingsList(query);

      Assert.NotNull(sut);
    }


    [Fact]
    public void GetShippingByUIDTest() {

      var usecases = ShippingUseCases.UseCaseInteractor();

      ShippingDto sut = usecases.GetShippingOrder(
        "ffffggr4-d9da-400d-86a9-93851da77777", ShippingQueryType.Shipping);

      Assert.NotNull(sut);
    }


    [Fact]
    public void GetOrdersForShippingByUIDTest() {

      var usecase = ShippingUseCases.UseCaseInteractor();

      string orderForShippingUID = "47f1f9cc-6948-4dc5-ba9e-c92060448902";

      ShippingOrderItem sut = usecase.GetOrdersForShippingByUID(orderForShippingUID);

      Assert.NotNull(sut);

    }


    [Fact]
    public void UpdateShippingOrderTest() {

      var usecase = ShippingUseCases.UseCaseInteractor();

      string shippingUID = "c448a856-af33-4b06-976b-a3e0c67f17af";
      ShippingDto sut = usecase.UpdateShippingOrder(shippingUID, GetShippingFields());

      Assert.NotNull(sut);
    }


    [Fact]
    public void CreateOrdersForShippingTest() {

      var usecase = ShippingUseCases.UseCaseInteractor();

      string shippingUID = "31841ac9-168a-4b32-8b3c-65d1db9c7930";
      string orderUID = "8a15068a-3bc2-4693-a415-a9e3410f63fc";
      ShippingDto sut = usecase.CreateOrderForShipping(shippingUID, orderUID);

      Assert.NotNull(sut);

    }


    [Fact]
    public void GetShippingPackageByUIDTest() {

      var usecase = ShippingUseCases.UseCaseInteractor();

      string shippingPackageUID = "jf2053f7-d1ab-4584-9f9b-3526c3594krt";
      ShippingPackage sut = usecase.GetShippingPackageByUID(shippingPackageUID);

      Assert.NotNull(sut);

    }


    [Fact]
    public void GetShippingPalletByUIDTest() {

      var usecase = ShippingUseCases.UseCaseInteractor();

      string shippingPalletUID = "24563f9f-48f7-466e-8a61-7bf4a6ekrlf5";
      ShippingPallet sut = usecase.GetShippingPalletByUID(shippingPalletUID);

      Assert.NotNull(sut);

    }



    #endregion Facts


    #region Private methods

    private ShippingFields GetShippingFields() {
      string[] orders = new string[] {
        "251dc352-f05e-4656-ad0e-46c60a035cdd",
        "7bbee1de-4af0-42c0-994f-90ccaf71fa54"
      };

      ShippingDataFields dataFields = new ShippingDataFields() {
        ShippingUID = "c448a856-af33-4b06-976b-a3e0c67f17af",
        ParcelSupplierUID = "875153f9-464a-4479-9b3c-5e47d94d4499",
        ShippingGuide = "GUIA ABCD02",
        ParcelAmount = 200,
        CustomerAmount = 50
      };

      ShippingFields fields = new ShippingFields() {
        Orders = orders,
        ShippingData = dataFields
      };

      return fields;
    }


    #endregion Private methods


  } // class ShippingTest

} // namespace Empiria.Trade.Tests
