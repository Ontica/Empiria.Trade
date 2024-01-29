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
    public void GetParcelSupplierListTest() {

      var usecase = ShippingUseCases.UseCaseInteractor();

      FixedList<INamedEntity> sut = usecase.GetParcelSupplierList();

      Assert.NotNull(sut);

    }


    [Fact]
    public void CreateShippingOrderTest() {

      var usecase = ShippingUseCases.UseCaseInteractor();

      ShippingDto sut = usecase.CreateShippingOrder(GetShippingFields());

      Assert.NotNull(sut);

    }

    
    [Fact]
    public void GetShippingTest() {

      var usecase = ShippingUseCases.UseCaseInteractor();

      string shippingUID = "b1c7eb56-d9d7-45cc-b7af-5335e0496bc7";

      ShippingEntry sut = usecase.GetShippingByUID(shippingUID);

      Assert.NotNull(sut);

    }


    [Fact]
    public void GetShippingByOrderUIDTest() {

      var usecase = ShippingUseCases.UseCaseInteractor();

      string orderUID = "f3bcb4ad-faaa-4afa-8a0c-8e2986c80065";

      ShippingEntryDto sut = usecase.GetShippingByOrderUID(orderUID);

      Assert.NotNull(sut);

    }


    [Fact]
    public void GetShippingByOrderUIDListTest() {

      var usecase = ShippingUseCases.UseCaseInteractor();

      ShippingQuery query = new ShippingQuery {
        Orders = new[] {
          //"dc9aa2bb-1082-43b9-afd5-fdaae4e7deeb" //vacio
          //"a99e6f92-4c07-4516-a7d2-17bf9629ede7", //ligados1
          //"0ca1ee24-bf52-4295-896e-c200eb1bfd04", //ligados1
          "9096d6cd-1ba7-42fe-9c0a-55fd37ecadc1", //ligados2
          "f959a4df-af9b-4596-9ee8-4a3e2e94757f"  //ligados2
        }
      };

      ShippingDto sut = usecase.GetShippingOrderByQuery(query);

      Assert.NotNull(sut);

    }


    #endregion Facts


    #region Private methods

    private ShippingFields GetShippingFields() {
      string[] orders = new string[] { };

      ShippingDataFields dataFields = new ShippingDataFields() {
        ShippingUID = "",
        ParcelSupplierUID = "8521a10b-0607-4d45-8614-385aba701b1r",
        ShippingGuide = "GUIA 00003",
        ParcelAmount = 200,
        CustomerAmount = 100
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
