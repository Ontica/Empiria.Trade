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
    public void GetPackagingTest() {

      var usecase = ShippingAndHandlingUseCases.UseCaseInteractor();
      string uid = "72e920b8-60cd-4d26-be2f-4a91a540d1f1";
      PackageForItem sut = usecase.GetPackagingByUID(uid);

      Assert.NotNull(sut);

    }


    [Fact]
    public void GetGetPackagedDataTest() {

      var usecase = ShippingAndHandlingUseCases.UseCaseInteractor();
      string uid = "e1513326-ffa6-4a3d-af32-6e9d41316606";

      PackagedData sut = usecase.GetPackagedData(uid);

      Assert.NotNull(sut);

    }



    [Fact]
    public void GetPackagingForOrderTest() {

      var usecase = ShippingAndHandlingUseCases.UseCaseInteractor();
      string uid = "7d035bba-e4c9-40e3-92f4-772389806fb2";//"e1513326-ffa6-4a3d-af32-6e9d41316606";

      IShippingAndHandling sut = usecase.GetPackagingForOrder(uid);

      Assert.NotNull(sut);

    }


    [Fact]
    public void CreatePackageForItemTest() {

      var usecase = ShippingAndHandlingUseCases.UseCaseInteractor();

      string orderUID = "c75a25fc-92e6-493e-aefb-fc24a312898a";

      var packingItemFields = new PackingItemFields {
        OrderUID = "c75a25fc-92e6-493e-aefb-fc24a312898a",
        PackageID = "Caja 1",
        PackageTypeUID = "0452a10b-0607-4d45-8614-385dda701b54"
      };

      IShippingAndHandling sut = usecase.CreatePackageForItem(orderUID, packingItemFields);

      Assert.NotNull(sut);

    }


    [Fact]
    public void UpdatePackageForItemTest() {

      var usecase = ShippingAndHandlingUseCases.UseCaseInteractor();

      string orderUID = "c75a25fc-92e6-493e-aefb-fc24a312898a";
      string packageForItemUID = "789bc9f2-1304-488e-b573-d2da58f04515";
      var packingItemFields = new PackingItemFields {
        OrderUID = "c75a25fc-92e6-493e-aefb-fc24a312898a",
        PackageID = "Caja 1",
        PackageTypeUID = "0452a10b-0607-4d45-8614-385dda701b54"
      };

      IShippingAndHandling sut = usecase.UpdatePackageForItem(orderUID, packageForItemUID, packingItemFields);

      Assert.NotNull(sut);

    }


    [Fact]
    public void DeletePackageForItemTest() {

      var usecase = ShippingAndHandlingUseCases.UseCaseInteractor();

      string orderUID = "e1513326-ffa6-4a3d-af32-6e9d41316606";
      string packageForItemUID = "41c4cfe0-18d4-4242-ad4e-b7ca3dc9c287";

      IShippingAndHandling sut = usecase.DeletePackageForItem(orderUID, packageForItemUID);

      Assert.NotNull(sut);

    }


    [Fact]
    public void CreatePackingOrderItemFieldsTest() {

      var usecase = ShippingAndHandlingUseCases.UseCaseInteractor();

      string orderUID = "c75a25fc-92e6-493e-aefb-fc24a312898a";
      string packingOrderUID = "fbade95a-3b01-468b-b524-7283248c20ee";

      var missingItemFields = new MissingItemField {
        orderItemUID = "5adf0776-0528-4ccb-b89f-7618c3266674",
        WarehouseUID = "f0061eb4-833c-44bf-8893-adcb88281d06",
        WarehouseBinUID = "22d33c45-c41f-426c-92f4-453fdc0abc1b",
        Quantity = 10
      };

      IShippingAndHandling sut = usecase.CreatePackingOrderItemFields(
                                  orderUID, packingOrderUID, missingItemFields);

      Assert.NotNull(sut);

    }


    [Fact]
    public void DeletePackingOrderItemFieldsTest() {

      var usecase = ShippingAndHandlingUseCases.UseCaseInteractor();

      string orderUID = "c75a25fc-92e6-493e-aefb-fc24a312898a";
      string packingItemUID = "789bc9f2-1304-488e-b573-d2da58f04515";
      string packingItemEntryUID = "f67be6b1-2a47-46c5-9d68-a49b8382165f";

      IShippingAndHandling sut = usecase.DeletePackingOrderItem(
                                  orderUID, packingItemUID, packingItemEntryUID);

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
