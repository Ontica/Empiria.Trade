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
        QueryType = QueryType.SalesPacking,
        Keywords = "",
        FromDate = Convert.ToDateTime("2023/01/10"),
        ToDate = Convert.ToDateTime("2024/12/28"),

      };


      var salesOrdersHelper = new SalesOrderHelper();

      var salesOrders = salesOrdersHelper.GetOrdersToPacking(fields);

      var x = SearchSealesOrderMapper.MapBaseSalesOrderPackingList(salesOrders);


      Assert.NotNull(x);
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

     
      ////var vendorProduct = salesOrderItem.VendorProduct;

      var y = SalesOrderItemsMapper.Map(salesOrderItem);
      Assert.NotNull(y);
    }

    [Fact]
    public void ShouldCrateNewOrder() {
    
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
      Notes = ""
    };

    
      List<SalesOrderItemsFields> items = new List<SalesOrderItemsFields>();
     items.Add(item);
     

      var order = new SalesOrderFields {
        //UID = "217ac442-5409-44b9-ae4d-22f9f104c5fe",
        //OrderNumber = "P-EY2DDRfrr2",
        OrderTime = DateTime.Now,
        Status = Orders.OrderStatus.Captured,
        CustomerUID = "97c3b0b7-6f7d-4700-a329-9825a60440c1",

        CustomerContactUID = "",
        SupplierUID = "211e9e92-c56e-4ed3-b42f-e916211b92ce",
        SalesAgentUID = "8b1d6d37-8d6c-4983-a3a0-42ed6b867bbe",
        PaymentCondition = "1 Mes",
        ShippingMethod = "Red Paq",
        Items = items.ToFixedList()
      };

        var salesOrder = new SalesOrder(order);
      
      var map = SalesOrderMapper.Map(salesOrder);
        Assert.NotNull(map);

      }

  

      [Fact]
    public void ShouldCancelOrder() {

      var order = SalesOrder.Parse("ead9f502-e94c-4b3c-aa75-367482f9ba2c");
     

      var orderDto = SalesOrderMapper.Map(order);

      Assert.NotNull(orderDto);
    }

    [Fact]
    public void ShouldGetOrder() {

     var order = SalesOrder.Parse("044cbcfa-ebbe-4745-a27b-7783c4cff64b");
     

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
      var order = SalesOrder.Parse("f43c43e4-20e8-41b2-a2fd-a2f6d012b813");
      order.Authorize();

      Assert.NotNull(order);
    }

    [Fact]
    public void ShouldGetCreditTransactions() {
      var order = SalesOrder.Parse("044cbcfa-ebbe-4745-a27b-7783c4cff64b");
      var creditTransactions = order.GetCustomerCreditTransactions();

      Assert.NotNull(creditTransactions);
    }

    [Fact]
    public void ShouldDeliverSalesOrder() {
      var order = SalesOrder.Parse("f3bcb4ad-faaa-4afa-8a0c-8e2986c80065");
      order.Deliver();

      var orderDto = SalesOrderMapper.Map(order);

      Assert.NotNull(orderDto);
    }
    


  } // public class SalesTest

} // namespace Empiria.Trade.Tests.Sales
