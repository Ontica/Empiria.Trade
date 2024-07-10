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
      //TestsCommonMethods.Authenticate();
    }

    #endregion Initialization


    #region Facts



    [Fact]
    public void CreatePurchaseOrderTest() {

      var usecase = PurchaseOrderUseCases.UseCaseInteractor();

      PurchaseOrderFields fields = GetPurchaseOrderFields();
      PurchaseOrderDto sut = usecase.CreatePurchaseOrder(fields);

      Assert.NotNull(sut);
    }


    [Fact]
    public void CreatePurchaseOrderItemTest() {

      var usecase = PurchaseOrderUseCases.UseCaseInteractor();

      string purchaseOrderUID = "fb1e2de6-e39e-4189-893b-ac9b74adb232";
      PurchaseOrderItemFields fields = GetPurchaseOrderItemFields();
      PurchaseOrderDto sut = usecase.CreatePurchaseOrderItem(purchaseOrderUID, fields);

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
    public void GetPurchaseOrderTest() {

      var usecase = PurchaseOrderUseCases.UseCaseInteractor();

      string purchaseOrderUID = "fb1e2de6-e39e-4189-893b-ac9b74adb232";
      var sut = usecase.GetPurchaseOrder(purchaseOrderUID);
      Assert.NotNull(sut);
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
        Status = Orders.OrderStatus.Captured
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
        SupplierUID = "1db9f53f-caf1-4e37-9626-ae68a89b42a5",
        Notes = "OC 27 EDITADO No. 2",
        PaymentCondition = "Credito",
        ShippingMethod = ShippingMethods.Paqueteria
      };

      return fields;
    }

    #endregion Helpers


  } // class PurchaseOrderTests

} // namespace Empiria.Trade.Tests.Inventory
