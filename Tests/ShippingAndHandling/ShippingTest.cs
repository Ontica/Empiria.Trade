﻿/* Empiria Trade *********************************************************************************************
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
    public void CreateShippingOrderTest() {

      var usecase = ShippingUseCases.UseCaseInteractor();

      ShippingDto sut = usecase.CreateShippingOrder(GetShippingFields());

      Assert.NotNull(sut);

    }


    [Fact]
    public void CreateShippingPalletTest() {

      var usecase = ShippingUseCases.UseCaseInteractor();
      
      string shippingUID = "31841ac9-168a-4b32-8b3c-65d1db9c7930";
      
      ShippingPalletFields fields = new ShippingPalletFields() {
        ShippingPalletName = "TARIMA 01",
        Packages = new string[] { 
          "768d0762-58a1-46c6-88e1-17103a78d76f",
          "3866728f-567e-4947-89eb-27c9da7bf817",
          "6bf9da6e-6aad-426c-a479-7a3fd4fec217"
        }
      };

      ShippingDto sut = usecase.CreateShippingPallet(shippingUID, fields);

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

      string shippingOrderUID = "68db22ab-891e-466e-80d1-e766fc370bcc";
      usecase.DeleteShipping(shippingOrderUID);

      Assert.True(true);

    }


    [Fact]
    public void GetParcelSupplierListTest() {

      var usecase = ShippingUseCases.UseCaseInteractor();

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
    public void ChangeOrdersForShippingStatusTest() {

      var usecase = ShippingUseCases.UseCaseInteractor();

      string shippingUID = "68db22ab-891e-466e-80d1-e766fc370bcc";

      ShippingDto sut = usecase.ChangeOrdersForShippingStatus(shippingUID);

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
          "0ca1ee24-bf52-4295-896e-c200eb1bfd04"
          //"f959a4df-af9b-4596-9ee8-4a3e2e94757f"
        }
      };

      ShippingDto sut = usecase.GetShippingOrderByQuery(query);

      Assert.NotNull(sut);

    }


    [Fact]
    public void GetShippingListTest() {

      var usecase = ShippingUseCases.UseCaseInteractor();

      ShippingQuery query = new ShippingQuery();

      FixedList<ShippingEntryDto> sut = usecase.GetShippingsList(query);

      Assert.NotNull(sut);
    }


    [Fact]
    public void GetShippingByUIDTest() {

      var usecase = ShippingUseCases.UseCaseInteractor();

      ShippingDto sut = usecase.GetShippingByUID("se31ce1f-f07a-4bfd-a454-3355ca64c2dr");

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

      string shippingUID = "68db22ab-891e-466e-80d1-e766fc370bcc";
      string orderUID = "f960c77a-1873-477b-a7af-75f99a6df41d";
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
        //"f960c77a-1873-477b-a7af-75f99a6df41d",
        //"f959a4df-af9b-4596-9ee8-4a3e2e94757f",
        "8a15068a-3bc2-4693-a415-a9e3410f63fc"
      };

      ShippingDataFields dataFields = new ShippingDataFields() {
        ShippingUID = "",
        ParcelSupplierUID = "8521a10b-0607-4d45-8614-385aba701b1r",
        ShippingGuide = "GUIA PAQUETEXPRESS 0001",
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
