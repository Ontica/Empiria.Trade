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
    public void GetPurchaseOrderTest() {

      var usecase = PurchaseOrderUseCases.UseCaseInteractor();

      string purchaseOrderUID = "d80a0d16-c28b-44f9-8862-056aa31d9b7d";
      var sut = usecase.GetPurchaseOrder(purchaseOrderUID);
      Assert.NotNull(sut);
    }


    [Fact]
    public void GetPurchaseOrderItemTest() {

      var usecase = PurchaseOrderUseCases.UseCaseInteractor();

      string purchaseOrderItemUID = "lokijue7-ba78-4efb-9753-18eb86poiuyt";
      var sut = usecase.GetPurchaseOrderItem(purchaseOrderItemUID);
      Assert.NotNull(sut);
    }


    [Fact]
    public void GetPurchaseOrderListTest() {

      var usecase = PurchaseOrderUseCases.UseCaseInteractor();

      PurchaseOrderQuery query = new PurchaseOrderQuery { };

      FixedList<PurchaseOrderEntry> sut = usecase.GetPurchaseOrderList(query);
      Assert.NotNull(sut);
    }


    [Fact]
    public void GetPurchaseOrderDataTest() {

      var usecase = PurchaseOrderUseCases.UseCaseInteractor();

      PurchaseOrderQuery query = new PurchaseOrderQuery {
        SupplierUID = "adb85aa4-0de4-4f99-ab9f-ed9dd07a113a",
        Keywords = "c a000000001",
        Status = Orders.OrderStatus.Empty
      };

      PurchaseOrderDataDto sut = usecase.GetPurchaseOrderDescriptor(query);
      Assert.NotNull(sut);
    }


    #endregion Facts


  } // class PurchaseOrderTests

} // namespace Empiria.Trade.Tests.Inventory
