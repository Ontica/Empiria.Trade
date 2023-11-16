﻿/* Empiria Trade *********************************************************************************************
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
using Empiria.Trade.Sales.UseCases;

using Empiria.Trade.Core;
using Xunit.Abstractions;
using Empiria.Trade.Orders;
using System.Linq;

namespace Empiria.Trade.Tests.Sales {

  /// <summary>Test cases for sales.   </summary>
  public class SalesTest {

    [Fact]
    public void ShouldGetOrderTest() {

      var fields = new SearchOrderFields {
       
        FromDate = Convert.ToDateTime("2023/01/10"),
        ToDate = Convert.ToDateTime("2023/11/28"),
       
      };


      var salesOrders = SalesOrder.GetOrders(fields);

      //var x = SalesOrderMapper.Map(salesOrders);


      Assert.NotNull(salesOrders);
    }

    [Fact]
    public void ShouldCreateOrderTest() {

      var item = new SalesOrderItemsFields {
            OrderItemUID = "",
            VendorProductUID = "56214e92-2cb0-49fd-943e-0a1c5df51a10",
            Quantity = 1,
            UnitPrice = 100,
            SalesPrice = 0,
            DiscountPolicy = "",
            Discount1 = 0,
            Discount2 = 0,
            Subtotal = 800,
            Notes =  ""
      };

      var salesOrder = SalesOrder.Parse(130);

      var salesOrderItem = new SalesOrderItem(salesOrder, item);

      //salesOrderItem.Save();

      ////var vendorProduct = salesOrderItem.VendorProduct;

      var y = SalesOrderItemsMapper.Map(salesOrderItem);
      Assert.NotNull(y);
    }

    //[Fact]
    //public void ShouldCrateNewOrder() {

    //  var item = new SalesOrderItemsFields {
    //    OrderItemUID = "",
    //    VendorProductUID = "023de292-90e6-47ab-91a2-331f551885df",
    //    Quantity = 10,
    //    BasePrice = 100,
    //    SpecialPrice = 96,
    //    SalesPrice = 393.30m,
    //    AdditionalDiscount = 10,
    //    AdditionalDiscountToApply = 0,
    //    Shipment = 339,
    //    Taxes = 19,
    //    Total = 800,
    //    Notes = "",
    //  };

    //  var item1 = new SalesOrderItemsFields {
    //    OrderItemUID = "",
    //    VendorProductUID = "f3f1fbc8-453b-4d5f-afe6-6b13e221c61f",
    //    Quantity = 5,
    //    BasePrice = 100,
    //    SpecialPrice = 96,
    //    SalesPrice = 393.30m,
    //    AdditionalDiscount = 5,
    //    AdditionalDiscountToApply = 0,
    //    Shipment = 339,
    //    Taxes = 19,
    //    Total = 800,
    //    Notes = "",
    //  };
    //  List<SalesOrderItemsFields> items = new List<SalesOrderItemsFields>();
    //  items.Add(item);
    //  items.Add(item1);

    //  var order = new SalesOrderFields {
    //    //UID = "217ac442-5409-44b9-ae4d-22f9f104c5fe",
    //    //OrderNumber = "P-EY2DDRfrr2",
    //    OrderTime = DateTime.Now,
    //    Status = Orders.OrderStatus.Captured,
    //    CustomerUID = "a8753de5-1b65-41ce-8c38-9c0dd0152048",

    //    CustomerContactUID = "",
    //    SupplierUID = "211e9e92-c56e-4ed3-b42f-e916211b92ce",
    //    SalesAgentUID = "8b1d6d37-8d6c-4983-a3a0-42ed6b867bbe",
    //    PaymentCondition = "1 Mes",
    //    ShippingMethod = "Red Paq",
    //    Items = items.ToFixedList()
    //  };

    //  var salesOrder = new SalesOrder(order);


    //  Assert.NotNull(salesOrder);

    //}

    [Fact]
    public void ShouldCancelOrder() {

      var order = SalesOrder.Parse("ead9f502-e94c-4b3c-aa75-367482f9ba2c");
      //order.Cancel();

      var orderDto = SalesOrderMapper.Map(order);

      Assert.NotNull(orderDto);
    }

    [Fact]
    public void ShouldCancelOrderItem() {

      var item = SalesOrderItem.Parse("d4ceaf5f-4b50-4320-86e4-03a2ef128982");
      Assert.NotNull(item);
      
       
    }

    [Fact]
    public void ShouldGetVendorPrices() {
      var prices = CustomerPrices.GetVendorPrices(100);

      Assert.NotNull(prices);
    }

    [Fact]
    public void ShouldGetOrderStatusList() {
      var orderStatusList = Enum.GetNames(typeof(OrderStatus)).ToList();

      Assert.NotNull(orderStatusList);
    }

    [Fact]
    public void ShouldAuthroizeOrderSales() {
      var order = SalesOrder.Parse("97eb241a-6bb8-4306-a4dd-5efc210058b7");
      order.Authorize();

      Assert.NotNull(order);
    }


  } // public class SalesTest

} // namespace Empiria.Trade.Tests.Sales
