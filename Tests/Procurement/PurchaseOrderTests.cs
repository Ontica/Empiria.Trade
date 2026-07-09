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
using Empiria.Trade.Core;
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
    public void ClosePurchaseOrderTest() {

      var usecase = PurchaseOrderUseCases.UseCaseInteractor();

      string purchaseOrderUID = "8bdc8346-d7ed-44ba-9463-261f56525a4f";

      PurchaseOrderDto sut = usecase.ClosePurchaseOrder(purchaseOrderUID);

      Assert.NotNull(sut);
    }


    [Fact]
    public void CreatePurchaseOrderTest() {

      using (var usecase = PurchaseOrderUseCases.UseCaseInteractor()) {

        var fields = new PurchaseOrderFields {
          SupplierUID = "b8b6d1ce-ffd0-47fd-92ef-3db32fa44ed5",
          Notes = "TEST 29-06-2026",
          CurrencyUID = "A7F54FC5-4626-4B14-9E44-96A054FBDE27",
          ExchangeRate = 19.505050M
        };

        PurchaseOrderDto sut = usecase.CreatePurchaseOrder(fields);

        Assert.NotNull(sut);
      }
    }


    [Fact]
    public void CreatePurchaseOrderItemTest() {

      var usecase = PurchaseOrderUseCases.UseCaseInteractor();

      string purchaseOrderUID = "8bdc8346-d7ed-44ba-9463-261f56525a4f";

      var fields = new PurchaseOrderItemFields {
        Product = "TMG12X4-25",
        VendorProductUID = "6ee8b450-d723-44b9-aa47-dd043236d8aa",
        Quantity = 10,
        ProductUnitUID = "BJG2T8Q6-4E73-412B-84C7-4F97OPERMV52"
        //Price = 100,
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

      string purchaseOrderUID = "62322440-38c3-46d4-9593-b31c98455389";
      PurchaseOrderDto sut = usecase.GetPurchaseOrderDto(purchaseOrderUID);
      Assert.NotNull(sut);
    }


    [Fact]
    public void SearchPurchaseOrderDataTest() {

      var usecase = PurchaseOrderUseCases.UseCaseInteractor();

      PurchaseOrderQuery query = new PurchaseOrderQuery {
        SupplierUID = "",
        Keywords = "OC-V3GT1VO1",
        Status = ""
      };

      PurchaseOrdersDataDto sut = usecase.GetPurchaseOrderDescriptor(query);
      Assert.NotNull(sut);
    }


    [Fact]
    public void UpdatePurchaseOrderItemTest() {

      var usecase = PurchaseOrderUseCases.UseCaseInteractor();

      string purchaseOrderUID = "09660694-82a9-41a3-9384-6e56f18d813f";
      string purchaseOrderItemUID = "9dbef995-6970-4c29-996d-49e78eaa717a";

      PurchaseOrderItemFields fields = GetPurchaseOrderItemFields();

      PurchaseOrderDto sut = usecase.UpdatePurchaseOrderItem(purchaseOrderUID, purchaseOrderItemUID, fields);

      Assert.NotNull(sut);
    }


    [Fact]
    public void UpdatePurchaseOrderTest() {

      var usecase = PurchaseOrderUseCases.UseCaseInteractor();

      PurchaseOrderFields fields = GetPurchaseOrderFields();
      PurchaseOrderDto sut = usecase.UpdatePurchaseOrder("af9a7ae8-0696-4884-96e2-74abb2b0aa0a", fields);

      Assert.NotNull(sut);
    }


    #endregion Facts


    #region Helpers


    private PurchaseOrderFields GetPurchaseOrderFields() {

      var fields = new PurchaseOrderFields {
        SupplierUID = "f1da81d7-4a26-4af9-b5e4-7a1df5855c6c", // *
        Notes = "TEST edicion", // *
        PaymentConditions = "Credito",
        ShippingMethod = "Paqueteria",
        ScheduledTime = new DateTime(2026, 8, 30),
        CurrencyUID = "A7F54FC5-4626-4B14-9E44-96A054FBDE27",
        ExchangeRate = 18.50M
      };

      return fields;
    }


    private PurchaseOrderItemFields GetPurchaseOrderItemFields() {

      return new PurchaseOrderItemFields {
        Product = "TCCT12X112",
        VendorProductUID = "6b8a8254-0e41-42d9-88ed-4392977efb65",
        Quantity = 1,
        Price = 1,
        Weight = 1,
        Description = "UPDATE ITEM 2JUN2026"
      };
    }

    #endregion Helpers


  } // class PurchaseOrderTests

} // namespace Empiria.Trade.Tests.Inventory
