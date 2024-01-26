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
      
      string orderUID = "f3bcb4ad-faaa-4afa-8a0c-8e2986c80065";

      ShippingFields fields = new ShippingFields() {
        OrderUID = orderUID,
        ParcelSupplierUID = "8521a10b-0607-4d45-8614-385aba701b1r",
        ShippingGuide ="9876543210",
        ParcelAmount = 200,
        CustomerAmount= 100
      };

      ShippingEntryDto sut = usecase.CreateShippingOrder(orderUID, fields);

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

      //string orderUID = "f3bcb4ad-faaa-4afa-8a0c-8e2986c80065";
      ShippingQuery query = new ShippingQuery {
        OrderUIDs = new[] { "a99e6f92-4c07-4516-a7d2-17bf9629ede7", "0ca1ee24-bf52-4295-896e-c200eb1bfd04", "f3bcb4ad-faaa-4afa-8a0c-8e2986c80065" }
      };

      ShippingDto sut = usecase.GetCompleteShippingByOrderUIDList(query);

      Assert.NotNull(sut);

    }


    #endregion Facts



  } // class ShippingTest

} // namespace Empiria.Trade.Tests
