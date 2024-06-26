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

namespace Empiria.Trade.Tests {


  /// <summary>Test cases for shipping.</summary>
  public class ShippingTest {


    #region Initialization

    public ShippingTest() {
      //TestsCommonMethods.Authenticate();
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

      string shippingUID = "78111577-fb09-428d-ac69-9f1062441f33";

      ShippingPalletFields fields = new ShippingPalletFields() {
        ShippingPalletName = "TARIMA 02",
        Packages = new string[] {
          "768d0762-58a1-46c6-88e1-17103a78d76f",
          //"3866728f-567e-4947-89eb-27c9da7bf817"
          "6bf9da6e-6aad-426c-a479-7a3fd4fec217"
        }
      };

      ShippingDto sut = usecase.CreateShippingPallet(shippingUID, fields);

      Assert.NotNull(sut);

    }


    [Fact]
    public void UpdateShippingPalletTest() {

      var usecase = ShippingUseCases.UseCaseInteractor();

      string shippingUID = "78111577-fb09-428d-ac69-9f1062441f33";
      string shippingPalletUID = "71339233-ef14-45c6-a0a7-598a2383a894";

      ShippingPalletFields fields = new ShippingPalletFields() {
        ShippingPalletName = "TARIMA 01",
        Packages = new string[] {
          "768d0762-58a1-46c6-88e1-17103a78d76f",
          //"3866728f-567e-4947-89eb-27c9da7bf817"
          "6bf9da6e-6aad-426c-a479-7a3fd4fec217"
        }
      };

      ShippingDto sut = usecase.UpdateShippingPallet(shippingUID, shippingPalletUID, fields);

      Assert.NotNull(sut);

    }


    [Fact]
    public void DeleteOrderForShippingTest() {

      var usecase = ShippingUseCases.UseCaseInteractor();

      string shippingOrderUID = "68db22ab-891e-466e-80d1-e766fc370bcc";
      string orderUID = "8a15068a-3bc2-4693-a415-a9e3410f63fc";
      ShippingDto sut = usecase.DeleteOrderForShipping(shippingOrderUID, orderUID);

      Assert.NotNull(sut);

    }


    [Fact]
    public void DeleteShippingTest() {

      var usecase = ShippingUseCases.UseCaseInteractor();

      string shippingOrderUID = "31841ac9-168a-4b32-8b3c-65d1db9c7930";
      usecase.DeleteShipping(shippingOrderUID);

      Assert.True(true);

    }


    [Fact]
    public void DeleteShippingPalletByShippingUIDTest() {

      var usecase = ShippingUseCases.UseCaseInteractor();

      string shippingOrderUID = "31841ac9-168a-4b32-8b3c-65d1db9c7930";
      string shippingPalletUID = "7e18a808-16ea-4544-9116-213a0d649ac6";

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
    public void GetShippingTest() {

      var usecase = ShippingUseCases.UseCaseInteractor();
      string shippingUID = "9ed68735-af5c-4e49-85dc-4a177c82b273";

      ShippingEntry sut = usecase.GetShippingByShippingUID(shippingUID);
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
    public void ChangeOrdersForShippingStatusTest() {

      var usecase = ShippingUseCases.UseCaseInteractor();
      string shippingUID = "5fcc47c4-ba69-4c7c-bbc3-324bcd25418e";

      ShippingDto sut = usecase.ChangeOrdersForShippingStatus(shippingUID);
      Assert.NotNull(sut);
    }


    [Fact]
    public void UpdateShippingStatusTest() {

      var usecase = DeliveryUseCase.UseCaseInteractor();
      string shippingUID = "71200bf7-5a31-4ea2-b125-e5c638187736";

      ShippingDto sut = usecase.UpdateShippingStatus(shippingUID);
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
                    "d435344d-dc53-4554-bfe5-61db87c4af91"
                }
      };

      ShippingDto sut = usecase.GetShippingOrderByQuery(query);

      Assert.NotNull(sut);
    }


    [Fact]
    public void GetShippingListTest() {

      var usecase = ShippingUseCases.UseCaseInteractor();

      ShippingQuery query = new ShippingQuery();
      query.Keywords = "";
      query.ParcelSupplierUID = "";
      query.Status = ShippingStatus.Todos;
      FixedList<ShippingEntryDto> sut = usecase.GetShippingsList(query);

      Assert.NotNull(sut);
    }


    [Fact]
    public void GetShippingByUIDTest() {

      var usecase = ShippingUseCases.UseCaseInteractor();

      ShippingDto sut = usecase.GetShippingByUID(
        "d087a5a7-038e-4135-904f-8f746fc4fd58", ShippingQueryType.Shipping);

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

      string shippingUID = "050590d4-3dd9-4331-8bf8-d7764c371850";
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
        "d0389b37-bea7-400e-904d-e2056bafb723"
      };

      ShippingDataFields dataFields = new ShippingDataFields() {
        ShippingUID = "",
        ParcelSupplierUID = "g5se58ab-75d0-4e8f-bed4-2305e5er2t55",
        ShippingGuide = "GUIA CASTORSITO ABCD01",
        ParcelAmount = 1,
        CustomerAmount = 1
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
