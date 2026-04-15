/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory and handling Management          Component : Test cases                              *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Use cases tests                         *
*  Type     : PurchaseOrderTests                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Test cases for purchase order.                                                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.StateEnums;
using Empiria.Tests;
using Empiria.Trade.Orders;
using Empiria.Trade.Procurement;
using Empiria.Trade.Procurement.Adapters;
using Empiria.Trade.Procurement.UseCases;
using Xunit;

namespace Empiria.Trade.Tests.Procurement {


  /// <summary>Test cases for purchase order.</summary>
  public class PurchaseOrderTests {


    #region Initialization

    public PurchaseOrderTests() {
      TestsCommonMethods.Authenticate();
    }

    #endregion Initialization


    #region Facts

    [Fact]
    public void CreatePurchaseOrderTest() {

      using (var usecase = PurchaseOrderUseCases.UseCaseInteractor()) {

        var fields = new PurchaseOrderFields {
          SupplierUID = "0d06cb65-4122-41c0-af87-1b7f8d1609da",
          Notes = "TEST",
        };

        PurchaseOrderDto sut = usecase.CreatePurchaseOrderV2(fields);

        Assert.NotNull(sut);
      }
    }


    [Fact]
    public void CreatePurchaseOrderItemTest() {

      var usecase = PurchaseOrderUseCases.UseCaseInteractor();

      string purchaseOrderUID = "68131d68-f199-46eb-9846-aced7d7d5e38";
      
      var fields = new PurchaseOrderItemFields {
        Product = "TMT516X34-1700",
        Quantity = 1
      };

      PurchaseOrderDto sut = usecase.CreatePurchaseOrderItemV2(purchaseOrderUID, fields);

      Assert.NotNull(sut);
    }


    [Fact]
    public void GetPurchaseOrderTest() {

      var usecase = PurchaseOrderUseCases.UseCaseInteractor();

      string purchaseOrderUID = "68131d68-f199-46eb-9846-aced7d7d5e38";
      PurchaseOrderDto sut = usecase.GetPurchaseOrderDto(purchaseOrderUID);
      Assert.NotNull(sut);
    }


    [Fact]
    public void SearchPurchaseOrderDataTest() {

      var usecase = PurchaseOrderUseCases.UseCaseInteractor();

      PurchaseOrderQuery query = new PurchaseOrderQuery {
        SupplierUID = "0d06cb65-4122-41c0-af87-1b7f8d1609da",
        Keywords = "",
        Status = EntityStatus.All
      };

      PurchaseOrdersDataDto sut = usecase.GetPurchaseOrderDescriptorV2(query);
      Assert.NotNull(sut);
    }


    [Fact]
    public void CreatePurchaseOrderEntryTest() {

      var usecase = PurchaseOrderUseCases.UseCaseInteractor();

      PurchaseOrderFields fields = GetPurchaseOrderFields();
      PurchaseOrderDto sut = usecase.CreatePurchaseOrder(fields);

      Assert.NotNull(sut);
    }


    [Fact]
    public void UpdatePurchaseOrderItemTest() {

      var usecase = PurchaseOrderUseCases.UseCaseInteractor();

      string purchaseOrderUID = "fb1e2de6-e39e-4189-893b-ac9b74adb232";
      string purchaseOrderItemUID = "iykijue7-ba78-4efb-9753-18eb86poiurt";
      PurchaseOrderItemFields fields = GetPurchaseOrderItemFields();
      PurchaseOrderDto sut = usecase.UpdatePurchaseOrderItem(purchaseOrderUID, purchaseOrderItemUID, fields);

      Assert.NotNull(sut);
    }


    private PurchaseOrderItemFields GetPurchaseOrderItemFields() {

      var fields = new PurchaseOrderItemFields {
        VendorProductUID = "22c278bb-2e61-4b37-b3a7-76f40950fc49",
        Quantity = 90,
        Price = 2,
        Weight = 1,
        Notes = "X"
      };
      return fields;
    }


    [Fact]
    public void DeletePurchaseOrder() {

      var usecase = PurchaseOrderUseCases.UseCaseInteractor();

      usecase.DeletePurchaseOrder("fb1e2de6-e39e-4189-893b-ac9b74adb232");

      Assert.True(true);
    }


    


    [Fact]
    public void GetPurchaseOrderItemTest() {

      var usecase = PurchaseOrderUseCases.UseCaseInteractor();

      string purchaseOrderItemUID = "iykijue7-ba78-4efb-9753-18eb86poiurt";
      var sut = usecase.GetPurchaseOrderItem(purchaseOrderItemUID);
      Assert.NotNull(sut);
    }


    [Fact]
    public void GetPurchaseOrderDataTest() {

      var usecase = PurchaseOrderUseCases.UseCaseInteractor();

      PurchaseOrderQuery query = new PurchaseOrderQuery {
        SupplierUID = "",
        Keywords = "OC-4JKBI1W5BY",
        Status = EntityStatus.All
      };

      PurchaseOrdersDataDto sut = usecase.GetPurchaseOrderDescriptor(query);
      Assert.NotNull(sut);
    }


    [Fact]
    public void UpdateInventoryOrderTest() {

      var usecase = PurchaseOrderUseCases.UseCaseInteractor();

      PurchaseOrderFields fields = GetPurchaseOrderFields();
      PurchaseOrderDto sut = usecase.UpdatePurchaseOrder("fb1e2de6-e39e-4189-893b-ac9b74adb232", fields);

      Assert.NotNull(sut);
    }


    #endregion Facts


    #region Helpers


    private PurchaseOrderFields GetPurchaseOrderFields() {

      var fields = new PurchaseOrderFields {
        SupplierUID = "0d06cb65-4122-41c0-af87-1b7f8d1609da", // *
        Notes = "TEST", // *
      };

      return fields;
    }

    #endregion Helpers


  } // class PurchaseOrderTests

} // namespace Empiria.Trade.Tests.Inventory
