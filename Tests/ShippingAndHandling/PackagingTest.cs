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
using Empiria.Trade.Packaging.UseCases;
using Empiria.Trade.Core;

namespace Empiria.Trade.Tests {


  /// <summary>Test cases for packaging.</summary>
  public class PackagingTest {


    #region Initialization

    public PackagingTest() {
      TestsCommonMethods.Authenticate();
    }

    #endregion Initialization


    #region Facts


    [Fact]
    public void GetPackagingTest() {

      var usecase = PackagingUseCases.UseCaseInteractor();
      string uid = "aea8c732-e518-498f-94a9-2476202c7561";
      Trade.Core.PackagingEntry sut = usecase.GetPackagingByUID(uid);

      Assert.NotNull(sut);

    }
    

      [Fact]
    public void GetPackingOrderItemTest() {

      var usecase = PackagingUseCases.UseCaseInteractor();
      string uid = "63e22d5b-0a2d-49a5-8aa8-64b97e46a283";
      PackagingItem sut = usecase.GetPackingOrderItemByUID(uid);

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
    public void CreatePackagingTest() {

      var usecase = PackagingUseCases.UseCaseInteractor();

      string orderUID = "c369fea9-955e-48db-b18f-263100db8e16";

      var packingItemFields = new PackagingEntryFields {
        OrderUID = "c369fea9-955e-48db-b18f-263100db8e16",
        PackageID = "CAJA LARGA PRUEBA 1",
        PackageTypeUID = "qwerty9e-7bbc-4169-8645-99274f785858"
      };

      ISalesOrderDto sut = usecase.CreatePackagingEntry(orderUID, packingItemFields);

      Assert.NotNull(sut);

    }


    [Fact]
    public void UpdatePackagingTest() {

      var usecase = PackagingUseCases.UseCaseInteractor();

      string orderUID = "c369fea9-955e-48db-b18f-263100db8e16";
      string packageForItemUID = "86b03027-aa8e-4753-bbcc-78b54e0cda97";

      var packingItemFields = new PackagingEntryFields {
        OrderUID = "c369fea9-955e-48db-b18f-263100db8e16",
        PackageID = "CAJA LARGA PRUEBA 1-1",
        PackageTypeUID = "qwerty9e-7bbc-4169-8645-99274f785858"
      };

      ISalesOrderDto sut = usecase.UpdatePackagingEntry(orderUID, packageForItemUID, packingItemFields);

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

      string orderUID = "0999e82c-44ad-450e-bd4e-b03ca058a9b7";
      string packingOrderUID = "54730874-5e41-476e-b340-d4dc213c2145";

      var missingItemFields = new MissingItemField {
        orderItemUID = "8bbb9a23-c4fd-4e61-abad-4cb921e5d290",
        //WarehouseUID = "2f6dfb0d-137b-4309-94ac-c5f7b8fbc9df",
        WarehouseBinUID = "Empty",
        Quantity = 1
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
