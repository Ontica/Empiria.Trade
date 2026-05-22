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
          Notes = "TEST 27-04-2026",
        };

        PurchaseOrderDto sut = usecase.CreatePurchaseOrder(fields);

        Assert.NotNull(sut);
      }
    }


    [Fact]
    public void CreatePurchaseOrderItemTest() {

      var usecase = PurchaseOrderUseCases.UseCaseInteractor();

      string purchaseOrderUID = "68131d68-f199-46eb-9846-aced7d7d5e38";

      var fields = new PurchaseOrderItemFields {
        Product = "TTRC8X34-200-S",
        VendorProductUID = "6b8a8254-0e41-42d9-88ed-4392977efb65",
        Quantity = 100,
        Price = 100,
      };

      PurchaseOrderDto sut = usecase.CreatePurchaseOrderItem(purchaseOrderUID, fields);

      Assert.NotNull(sut);
    }


    [Fact]
    public void DeletePurchaseOrder() {

      var usecase = PurchaseOrderUseCases.UseCaseInteractor();

      usecase.DeletePurchaseOrder("68131d68-f199-46eb-9846-aced7d7d5e38");

      Assert.True(true);
    }


    [Fact]
    public void DeletePurchaseOrderItem() {

      var usecase = PurchaseOrderUseCases.UseCaseInteractor();
      var orderUID = "68131d68-f199-46eb-9846-aced7d7d5e38";
      var orderItemUID = "1fb8ac8b-bd36-4bce-b391-7b2202031682";

      PurchaseOrderDto sut = usecase.DeletePurchaseOrderItem(orderUID, orderItemUID);

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

      PurchaseOrdersDataDto sut = usecase.GetPurchaseOrderDescriptor(query);
      Assert.NotNull(sut);
    }


    [Fact]
    public void UpdatePurchaseOrderItemTest() {

      var usecase = PurchaseOrderUseCases.UseCaseInteractor();

      string purchaseOrderUID = "68131d68-f199-46eb-9846-aced7d7d5e38";
      string purchaseOrderItemUID = "912a20ca-0a5b-4249-a4f0-ce00da1591e2";

      PurchaseOrderItemFields fields = GetPurchaseOrderItemFields();

      PurchaseOrderDto sut = usecase.UpdatePurchaseOrderItem(purchaseOrderUID, purchaseOrderItemUID, fields);

      Assert.NotNull(sut);
    }


    [Fact]
    public void UpdatePurchaseOrderTest() {

      var usecase = PurchaseOrderUseCases.UseCaseInteractor();

      PurchaseOrderFields fields = GetPurchaseOrderFields();
      PurchaseOrderDto sut = usecase.UpdatePurchaseOrder("68131d68-f199-46eb-9846-aced7d7d5e38", fields);

      Assert.NotNull(sut);
    }


    #endregion Facts


    #region Helpers


    private PurchaseOrderFields GetPurchaseOrderFields() {

      var fields = new PurchaseOrderFields {
        SupplierUID = "0d06cb65-4122-41c0-af87-1b7f8d1609da", // *
        Notes = "TEST edicion", // *
        PaymentConditions = "Credito",
        ShippingMethod = "Paqueteria",
        ScheduledTime = new DateTime(2026, 8, 30)
      };

      return fields;
    }


    private PurchaseOrderItemFields GetPurchaseOrderItemFields() {

      return new PurchaseOrderItemFields {
        Product = "ASF24-20",
        VendorProductUID = "be585bd8-bdf7-4669-9a04-5f52a1bcbfde",
        Quantity = 1,
        Price = 1,
        Weight = 1,
        Description = "UPDATE ITEM TEST 222"
      };
    }

    #endregion Helpers


  } // class PurchaseOrderTests

} // namespace Empiria.Trade.Tests.Inventory
