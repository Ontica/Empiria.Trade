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
    public void CreatePackingOrderTest() {

      var usecase = ShippingAndHandlingUseCases.UseCaseInteractor();

      PackingOrderFields packingOrder = GetFields();

      FixedList<IShippingAndHandling> sut = usecase.CreatePackingOrder(packingOrder);

      Assert.NotNull(sut);

    }


    private PackingOrderFields GetFields() {

      var packagingOrder = new PackingOrderFields();
      var fieldsList = new List<PackingFields>();
      var orderUid = "57335a81-56cf-477d-84e2-3193849210e1";

      var field1 = new PackingFields {
        OrderUID = orderUid,
        OrderItemUID = "85836afb-b4ee-4552-803e-0ee40bd096f9",
        PackageQuantity = 3,
        PackageID = "CAJA 1"
      };
      fieldsList.Add(field1);

      var field2 = new PackingFields {
        OrderUID = orderUid,
        OrderItemUID = "93bd32f9-feb2-46bb-9b49-c2587f9fe995",
        PackageQuantity = 3,
        PackageID = "CAJA 2"
      };
      fieldsList.Add(field2);

      var field3 = new PackingFields {
        OrderUID = orderUid,
        OrderItemUID = "bc1fba5d-4a05-47bd-b185-3104ddafa9a2",
        PackageQuantity = 3,
        PackageID = "CAJA 3"
      };
      fieldsList.Add(field3);

      packagingOrder.PackingFields = fieldsList.ToFixedList();

      return packagingOrder;
    }


    #endregion Facts



  } // class ShippingAndHandlingTest

} // namespace Empiria.Trade.Tests
