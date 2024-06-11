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
using Empiria.Trade.Financial.UseCases;
using TradeDataSchemaManager.Services;

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
        //Status = OrderStatus.Authorized,
        // ShippingMethod = ShippingMethods.Paqueteria
      };

      var usecases = SalesOrderUseCases.UseCaseInteractor();

        SearchSalesOrderDto salesOrders = usecases.GetOrders(fields);
       
      //var salesOrdersHelper = new SalesOrderHelper();

      //var salesOrders = salesOrdersHelper.GetOrders(fields);

      //var x = SearchSealesOrderMapper.MapBaseSalesOrderPackingList(salesOrders);


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
        SupplierUID = "45ff9d31-cb77-4fd5-9dea-7bcbc4cbe292",
        SalesAgentUID = "8b1d6d37-8d6c-4983-a3a0-42ed6b867bbe",
        PaymentCondition = "1 Mes",
        ShippingMethod = ShippingMethods.Paqueteria,
        Items = items.ToFixedList()
      };

        var salesOrder = new SalesOrder(order);
      salesOrder.Save();
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

     var order = SalesOrder.Parse("2cc43be3-b153-42e1-8ede-06ecb6bc7b1b");
      order.CalculateSalesOrder();
      
      order.SetOrderActions(QueryType.SalesPacking);


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
    public void ShouldDeliverSalesOrder() {
      var order = SalesOrder.Parse("f3bcb4ad-faaa-4afa-8a0c-8e2986c80065");
      order.Deliver();

      var orderDto = SalesOrderMapper.Map(order);

      Assert.NotNull(orderDto);
    }


    [Fact]
    public void ShouldSupplySalesOrder() {

      var useCase = SalesOrderUseCases.UseCaseInteractor();
      string orderUID = "999b56ab-7e63-4bed-bdff-666de817cb86";
      var sut = useCase.SupplySalesOrder(orderUID);
      Assert.NotNull(sut);
    }


    [Fact]
    public void ShouldCancelCredintInOrder() {

      var salesOrderUseCase = SalesOrderUseCases.UseCaseInteractor();
      var x =  salesOrderUseCase.CancelCreditInOrder("68c0c501-89f7-4e04-ad08-e7e34f5cbb33", "cancel");

      Assert.NotNull(x);
    }

    [Fact]
    public void ShouldAppyCredintInOrder() {

      var salesOrderUseCase = SalesOrderUseCases.UseCaseInteractor();
      var x = salesOrderUseCase.ApplySalesOrder("88f479bb-4c95-41c8-86d6-51f8df9cf833");

      Assert.NotNull(x);
    }


    [Fact]
    public void ShouldGetOrderByOrderNumber() {

      var salesOrderUseCase = SalesOrderUseCases.UseCaseInteractor();
      var x = salesOrderUseCase.GetSalesOrder("P-1CEDWUQUQ9");

      Assert.NotNull(x);
    }

  } // public class SalesTest

} // namespace Empiria.Trade.Tests.Sales
