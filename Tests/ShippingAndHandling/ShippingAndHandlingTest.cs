/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Shipping and handling Management           Component : Test cases                              *
*  Assembly : Empiria.Trade.Shipping.dll                 Pattern   : Use cases tests                         *
*  Type     : ShippingAndHandlingTest                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Test cases for Shipping and handling.                                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using System.Threading.Tasks;
using Empiria.DataTypes;

using Xunit;

using Empiria.Tests;
using Empiria.Trade.ShippingAndHandling.UseCases;
using Empiria.Trade.ShippingAndHandling.Adapters;
using System.Collections.Generic;
using Empiria.Trade.Sales;
using Empiria.Trade.Orders;
using Empiria.Trade.ShippingAndHandling;

namespace Empiria.Trade.Tests {


  /// <summary>Test cases for Shipping and handling.</summary>
  public class ShippingAndHandlingTest {


    #region Initialization

    public ShippingAndHandlingTest() {
      //TestsCommonMethods.Authenticate();
    }

    #endregion Initialization


    #region Facts


    [Fact]
    public void GetPackingItemsByOrderTest() {

      var usecase = ShippingAndHandlingUseCases.UseCaseInteractor();
      string uid = //"f3bcb4ad-faaa-4afa-8a0c-8e2986c80065";
      "a769e40f-3fbd-45af-9022-11d482024a8f";

      IShippingAndHandling sut = usecase.GetPackingByOrder(uid);

      Assert.NotNull(sut);

    }


    [Fact]
    public void CreatePackingOrderTest() {

      var usecase = ShippingAndHandlingUseCases.UseCaseInteractor();
      
      string orderUID = "57335a81-56cf-477d-84e2-3193849210e1";

      var packingItemFields = new PackingItemFields {
        OrderUID = "57335a81-56cf-477d-84e2-3193849210e1",
        PackageID = "CAJA 1",
        PackageTypeUID = "0452a10b-0607-4d45-8614-385dda701b54"
      };
      
      IShippingAndHandling sut = usecase.CreatePackingItem(orderUID, packingItemFields);

      Assert.NotNull(sut);

    }


    [Fact]
    public void GetPackageTypesTest() {

      var usecase = ShippingAndHandlingUseCases.UseCaseInteractor();

      FixedList<INamedEntity> sut = usecase.GetPackageTypeList();

      Assert.NotNull(sut);

    }


    #endregion Facts



  } // class ShippingAndHandlingTest

} // namespace Empiria.Trade.Tests
