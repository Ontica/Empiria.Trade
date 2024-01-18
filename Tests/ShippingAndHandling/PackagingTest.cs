/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Shipping and handling Management           Component : Test cases                              *
*  Assembly : Empiria.Trade.Shipping.dll                 Pattern   : Use cases tests                         *
*  Type     : PackagingTest                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Test cases for packaging.                                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using System.Threading.Tasks;
using Empiria.DataTypes;

using Xunit;

using Empiria.Tests;
using Empiria.Trade.Sales.ShippingAndHandling;
using Empiria.Trade.Sales.ShippingAndHandling.UseCases;
using Empiria.Trade.Sales.ShippingAndHandling.Adapters;
using Empiria.Trade.Sales.Adapters;

namespace Empiria.Trade.Tests {


  /// <summary>Test cases for packaging.</summary>
  public class PackagingTest {


    #region Initialization

    public PackagingTest() {
      //TestsCommonMethods.Authenticate();
    }

    #endregion Initialization


    #region Facts


    [Fact]
    public void GetPackagingTest() {

      var usecase = PackagingUseCases.UseCaseInteractor();
      string uid = "9af1e772-1847-4fbe-a897-d3f0d137efaa";
      PackageForItem sut = usecase.GetPackagingByUID(uid);

      Assert.NotNull(sut);

    }


    [Fact]
    public void GetGetPackagedDataTest() {

      var usecase = PackagingUseCases.UseCaseInteractor();
      string uid = "e1513326-ffa6-4a3d-af32-6e9d41316606";

      PackagedData sut = usecase.GetPackagedData(uid);

      Assert.NotNull(sut);

    }



    [Fact]
    public void GetPackagingForOrderTest() {

      var usecase = PackagingUseCases.UseCaseInteractor();
      string uid = "7d035bba-e4c9-40e3-92f4-772389806fb2";//"e1513326-ffa6-4a3d-af32-6e9d41316606";

      IShippingAndHandling sut = usecase.GetPackagingForOrder(uid);

      Assert.NotNull(sut);

    }


    [Fact]
    public void CreatePackageForItemTest() {

      var usecase = PackagingUseCases.UseCaseInteractor();

      string orderUID = "c75a25fc-92e6-493e-aefb-fc24a312898a";

      var packingItemFields = new PackingItemFields {
        OrderUID = "c75a25fc-92e6-493e-aefb-fc24a312898a",
        PackageID = "Caja 40001",
        PackageTypeUID = "0452a10b-0607-4d45-8614-385dda701b54"
      };

      ISalesOrderDto sut = usecase.CreatePackageForItem(orderUID, packingItemFields);

      Assert.NotNull(sut);

    }


    [Fact]
    public void UpdatePackageForItemTest() {

      var usecase = PackagingUseCases.UseCaseInteractor();

      string orderUID = "c75a25fc-92e6-493e-aefb-fc24a312898a";
      string packageForItemUID = "789bc9f2-1304-488e-b573-d2da58f04515";
      var packingItemFields = new PackingItemFields {
        OrderUID = "c75a25fc-92e6-493e-aefb-fc24a312898a",
        PackageID = "Caja 00001",
        PackageTypeUID = "0452a10b-0607-4d45-8614-385dda701b54"
      };

      ISalesOrderDto sut = usecase.UpdatePackageForItem(orderUID, packageForItemUID, packingItemFields);

      Assert.NotNull(sut);

    }


    [Fact]
    public void DeletePackageForItemTest() {

      var usecase = PackagingUseCases.UseCaseInteractor();

      string orderUID = "e1513326-ffa6-4a3d-af32-6e9d41316606";
      string packageForItemUID = "41c4cfe0-18d4-4242-ad4e-b7ca3dc9c287";

      ISalesOrderDto sut = usecase.DeletePackageForItem(orderUID, packageForItemUID);

      Assert.NotNull(sut);

    }


    [Fact]
    public void CreatePackingOrderItemFieldsTest() {

      var usecase = PackagingUseCases.UseCaseInteractor();

      string orderUID = "c75a25fc-92e6-493e-aefb-fc24a312898a";
      string packingOrderUID = "943a2830-6a04-4e1e-89ac-ce22b88fb884";

      var missingItemFields = new MissingItemField {
        orderItemUID = "5adf0776-0528-4ccb-b89f-7618c3266674",
        WarehouseUID = "2f6dfb0d-137b-4309-94ac-c5f7b8fbc9df",
        WarehouseBinUID = "be68b023-202d-4bf3-b0b6-3726ed5be44f",
        Quantity = 2
      };

      ISalesOrderDto sut = usecase.CreatePackingOrderItemFields(
                                  orderUID, packingOrderUID, missingItemFields);

      Assert.NotNull(sut);

    }


    [Fact]
    public void DeletePackingOrderItemFieldsTest() {

      var usecase = PackagingUseCases.UseCaseInteractor();

      string orderUID = "c75a25fc-92e6-493e-aefb-fc24a312898a";
      string packingItemUID = "789bc9f2-1304-488e-b573-d2da58f04515";
      string packingItemEntryUID = "f67be6b1-2a47-46c5-9d68-a49b8382165f";

      ISalesOrderDto sut = usecase.DeletePackingOrderItem(
                                  orderUID, packingItemUID, packingItemEntryUID);

      Assert.NotNull(sut);

    }


    [Fact]
    public void GetPackageTypesTest() {

      var usecase = PackagingUseCases.UseCaseInteractor();

      FixedList<INamedEntity> sut = usecase.GetPackageTypeList();

      Assert.NotNull(sut);

    }


    #endregion Facts



  } // class PackagingTest

} // namespace Empiria.Trade.Tests
