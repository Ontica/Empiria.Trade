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
using Empiria.Trade.Inventory.Adapters;

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
      string uid = "aea8c732-e518-498f-94a9-2476202c7561";
      PackageForItem sut = usecase.GetPackagingByUID(uid);

      Assert.NotNull(sut);

    }
    

      [Fact]
    public void GetPackingOrderItemTest() {

      var usecase = PackagingUseCases.UseCaseInteractor();
      string uid = "f0e6e2fc-5466-4d05-8ee9-738c31857283";
      PackingOrderItem sut = usecase.GetPackingOrderItemByUID(uid);

      Assert.NotNull(sut);

    }


    [Fact]
    public void GetPackagedDataTest() {

      var usecase = PackagingUseCases.UseCaseInteractor();
      string uid = "e1513326-ffa6-4a3d-af32-6e9d41316606";

      PackagedData sut = usecase.GetPackagedData(uid);

      Assert.NotNull(sut);

    }


    [Fact]
    public void GetPackagingForOrderTest() {

      var usecase = PackagingUseCases.UseCaseInteractor();
      string orderUid = "13489f86-1d3d-4e93-a59c-f2c555298f5d";//"e1513326-ffa6-4a3d-af32-6e9d41316606";

      PackingDto sut = usecase.GetPackagingForOrder(orderUid);
      
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
    public void UpdateInventoryOrderForPickingTest() {

      var usecase = PackagingUseCases.UseCaseInteractor();

      string orderUID = "542e49eb-e7bb-459b-991f-d1856a615fc0";
      var fields = new InventoryOrderFields() {
        InventoryOrderTypeUID = "2ft8y5h4-db55-48b3-aa78-63132a8d5e7f",
        ResponsibleUID = "f3c61569-25f7-4296-a48d-f01735e27062",
        AssignedToUID = "5c351378-9423-498c-bf4f-c7cb4dae5523",
        Notes = "NOTAS XYZ"
      };

      ISalesOrderDto sut = usecase.UpdateInventoryOrderForPicking(orderUID, fields);

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

      string orderUID = "542e49eb-e7bb-459b-991f-d1856a615fc0";
      string packingOrderUID = "1f8d3bb4-a172-4c17-b46e-568dee824b2f";

      var missingItemFields = new MissingItemField {
        orderItemUID = "b7db3feb-ae09-4358-b0ce-56cb09448ff8",
        //WarehouseUID = "2f6dfb0d-137b-4309-94ac-c5f7b8fbc9df",
        WarehouseBinUID = "30b307d4-8e0b-4185-a1d2-ab13e3d47fac",
        Quantity = 5
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
