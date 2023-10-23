/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Core Management                            Component : Test cases                              *
*  Assembly : Empiria.Trade.Test.dll                     Pattern   : Use cases tests                         *
*  Type     : SalesTests                                 License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Test cases for sales.                                                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;


using Xunit;


using Empiria.Trade.Sales.Adapters;
using Empiria.Trade.Sales;

using Empiria.StateEnums;
using System.Collections.Generic;

namespace Empiria.Trade.Tests.Sales {

  /// <summary>Test cases for sales.   </summary>
  public class SalesTest {

    [Fact]
    public void ShouldGetOrderTest() {

      var fields = new SearchOrderFields {
        ToDate = Convert.ToDateTime("01-01-2001"),
        FromDate = DateTime.Now,
        Status = Orders.OrderStatus.Captured
      };


      var salesOrders = SalesOrder.GetOrders(fields);
     

      Assert.NotNull(salesOrders);
    }

    [Fact]
    public void ShouldCreateOrderTest() {

      var item = new SalesOrderItemsFields {
        OrderItemUID = "",
        VendorProductUID = "f3f1fbc8-453b-4d5f-afe6-6b13e221c61f",
        Quantity = 3,
        BasePrice = 100,
        SpecialPrice = 96,
        SalesPrice = 393.30m,
        AdditionalDiscount = 0,
        AdditionalDiscountToApply = 0,
        Shipment = 339,
        Taxes = 19,
        Total = 800,
        Notes = "",
      };

      var salesOrderItem = new SalesOrderItem(item, 5);

      salesOrderItem.Save();

      //var vendorProduct = salesOrderItem.VendorProduct;

      var x = SalesOrderItemsMapper.Map(salesOrderItem);
      Assert.NotNull(x);
    }

    [Fact]
    public void ShouldCrateNewOrder() {
      //var x = new Trade.Core.Adapters.PartyContactsDto();
      //x.UID = 1;


      var item = new SalesOrderItemsFields {
        OrderItemUID = "",
        VendorProductUID = "f3f1fbc8-453b-4d5f-afe6-6b13e221c61f",
        Quantity = 3,
        BasePrice = 100,
        SpecialPrice = 96,
        SalesPrice = 393.30m,
        AdditionalDiscount = 0,
        AdditionalDiscountToApply = 0,
        Shipment = 339,
        Taxes = 19,
        Total = 800,
        Notes = "",
      };

      var item1 = new SalesOrderItemsFields {
        OrderItemUID = "",
        VendorProductUID = "e5f2f34b-65b1-43c9-a36b-027bdee92de9",
        Quantity = 2,
        BasePrice = 100,
        SpecialPrice = 96,
        SalesPrice = 393.30m,
        AdditionalDiscount = 0,
        AdditionalDiscountToApply = 0,
        Shipment = 339,
        Taxes = 19,
        Total = 800,
        Notes = "",
      };
      List<SalesOrderItemsFields> items = new List<SalesOrderItemsFields>();
      items.Add(item);
      items.Add(item1);

      var order = new SalesOrderFields {
        UID = "",
        OrderNumber = "",
        OrderTime = DateTime.Now,
        Status = Orders.OrderStatus.Captured,
        CustomerUID = "3fc1bdbe-8dd5-42f6-aa8c-62e902d687e7",

        CustomerContactUID = "",
        SupplierUID = "211e9e92-c56e-4ed3-b42f-e916211b92ce",
        SalesAgentUID = "8b1d6d37-8d6c-4983-a3a0-42ed6b867bbe",
        PaymentCondition = "1 Mes",
        ShippingMethod = "Red Paq",
        Items = items.ToFixedList()
      };

      var salesOrder = new SalesOrder(order);

     var x = salesOrder.GetCustomerPriceListNumber();


      Assert.NotEqual(100, x);

      salesOrder.Save();
      var y = SalesOrderMapper.Map(salesOrder);

      Assert.NotNull(y);
    }



  } // public class SalesTest

} // namespace Empiria.Trade.Tests.Sales
