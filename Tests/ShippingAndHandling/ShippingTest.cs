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
    public void DeleteOrderForShippingTest() {

      var usecase = ShippingUseCases.UseCaseInteractor();

      string shippingOrderUID = "050590d4-3dd9-4331-8bf8-d7764c371850";
      string orderUID = "8a15068a-3bc2-4693-a415-a9e3410f63fc";
      ShippingDto sut = usecase.DeleteOrderForShipping(shippingOrderUID, orderUID);

      Assert.NotNull(sut);

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

      string orderUID = "f959a4df-af9b-4596-9ee8-4a3e2e94757f";

      ShippingEntryDto sut = usecase.GetShippingByOrderUID(orderUID);

      Assert.NotNull(sut);

    }


    [Fact]
    public void GetShippingByOrderUIDListTest() {

      var usecase = ShippingUseCases.UseCaseInteractor();

      ShippingFieldsQuery query = new ShippingFieldsQuery {
        Orders = new[] {
          "8a15068a-3bc2-4693-a415-a9e3410f63fc"
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
