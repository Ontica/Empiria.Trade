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
using System.Collections.Generic;
using System.Linq;
using Empiria.Tests;
using Empiria.Trade.Core;
using Empiria.Trade.Sales.Adapters;
using Empiria.Trade.Sales.UseCases;
using Xunit;

namespace Empiria.Trade.Tests.Sales {

  /// <summary>Test cases for sales.   </summary>
  public class SalesTest {

    #region Initialization


    public SalesTest() {
      TestsCommonMethods.Authenticate();
    }

    #endregion Initialization

    [Fact]
    public void ShouldAuthorizeOrder() {

      using (var usecases = SalesOrderUseCases.UseCaseInteractor()) {
        
        var sut = usecases.AuthorizeSalesOrder("8ca76174-1a8c-4b30-975b-16a4eef46f67");

        Assert.NotNull(sut);
      }
    }


    [Fact]
    public void ShouldAuthroizeOrderSales() {
      var order = SalesOrder.Parse("f43c43e4-20e8-41b2-a2fd-a2f6d012b813");
      order.AuthorizeOrder();

      Assert.NotNull(order);
    }


    [Fact]
    public void GetSalesOrderByType() {

      var salesOrderUseCase = SalesOrderUseCases.UseCaseInteractor();

      var sut = salesOrderUseCase.GetSalesOrder("3d8db525-3c7d-422f-8c90-a6f35b46d52c", QueryType.SalesPacking);

      Assert.NotNull(sut);
    }


    [Fact]
    public void ShouldCreateNewOrder() {

      SalesOrderFields orderFields = GetSalesOrderFields();

      var useCase = SalesOrderUseCases.UseCaseInteractor();
      var sut = useCase.CreateSalesOrder(orderFields);

      Assert.NotNull(sut);
    }


    [Fact]
    public void ShouldUpdateOrder() {

      SalesOrderFields orderFields = GetSalesOrderFields();

      var useCase = SalesOrderUseCases.UseCaseInteractor();
      var sut = useCase.UpdateSalesOrder("b7a9fb1e-6f18-4956-8e93-33abff1d9c81", orderFields);

      Assert.NotNull(sut);
    }


    [Fact]
    public void ProcessSalesOrderTest() {

      var usecases = SalesOrderUseCases.UseCaseInteractor();

      SalesOrderFields fields = GetSalesOrderFields();

      var sut = usecases.ProcessSalesOrder(fields);

      Assert.NotNull(sut);
    }


    [Fact]
    public void ShouldGetOrderTest() {

      var fields = new SearchOrderFields {
        CustomerUID = "",
        QueryType = QueryType.SalesPacking,
        //Keywords = "",
        //FromDate = new DateTime(2026,9,5),
        //ToDate = new DateTime(2026, 9, 11),
        //Status = OrderStatus.ToSupply,
        //ShippingMethod = ShippingMethods.RutaLocal
      };

      var usecases = SalesOrderUseCases.UseCaseInteractor();

      SearchSalesOrderDto salesOrders = usecases.GetOrders(fields);

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
        Discount = 0,
        Discount2 = 0,
        Subtotal = 800,
        Notes = ""
      };

      var salesOrder = SalesOrder.Parse(130);

      var salesOrderItem = new SalesOrderItem(salesOrder, item);


      ////var vendorProduct = salesOrderItem.VendorProduct;

      var y = SalesOrderItemsMapper.Map(salesOrderItem);
      Assert.NotNull(y);
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
      var x = salesOrderUseCase.CancelCreditInOrder("68c0c501-89f7-4e04-ad08-e7e34f5cbb33", "cancel");

      Assert.NotNull(x);
    }

    [Fact]
    public void ShouldAppyCreditInOrder() {

      var salesOrderUseCase = SalesOrderUseCases.UseCaseInteractor();
      var sut = salesOrderUseCase.ApplySalesOrder("ac2f684b-c33a-4a99-9cc6-a5cc147ac298");

      Assert.NotNull(sut);
    }


    [Fact]
    public void ShouldGetOrderByOrderNumber() {

      var salesOrderUseCase = SalesOrderUseCases.UseCaseInteractor();
      var x = salesOrderUseCase.GetSalesOrder("P-TYN9NOZ37B");

      Assert.NotNull(x);
    }

    #region Helpers

    private SalesOrderFields GetSalesOrderFields() {

      return new SalesOrderFields {
        //UID = "b7a9fb1e-6f18-4956-8e93-33abff1d9c81",
        CustomerUID = "db100dac-92a3-4125-8de7-0cc072b49a72",
        CustomerContactUID = "b6f39137-9679-48ff-8fe7-bb18ec69bc5b",
        CustomerAddressUID = "11828272-485f-4b51-a61e-6050a321a8cc",
        SalesAgentUID = "32bdc986-2301-40f4-ba04-80b69d3e3a1f",
        SupplierUID = "4c0c43e4-8bdc-4b7d-b91e-3fb385441120",
        PaymentConditions = "Credito",
        OrderNumber = "P-56QFDP99FB",
        ShippingMethod = ShippingMethods.Paqueteria,
        //OrderTime = DateTime.Now,
        Status = OrderStatus.Captured,
        Notes = "",
        Items = GetSalesOrderItemFields()
      };
    }


    private FixedList<SalesOrderItemsFields> GetSalesOrderItemFields() {

      List<SalesOrderItemsFields> itemsFields = new List<SalesOrderItemsFields>();

      var fields = new SalesOrderItemsFields {
        OrderItemUID = "2acb4a1c-9663-445f-9e36-8a712d5cf9eb",
        VendorProductUID = "3a396d5d-7522-4663-9542-c1f7ec171a51",
        Quantity = 5,
        UnitPrice = 0.94434m,
        SalesPrice = 4.7217m,
        //DiscountPolicy = "",
        //Discount1 = 0,
        //Discount2 = 0,
        Subtotal = 4.7217m,
        Notes = "N/A"
      };

      itemsFields.Add(fields);

      var fields2 = new SalesOrderItemsFields {
        OrderItemUID = "0c9c4c0d-c73d-469e-8e15-f8568b13ae15",
        VendorProductUID = "43a90f2f-9126-4137-a115-8160d820cd97",
        Quantity = 4,
        UnitPrice = 14.77064m,
        SalesPrice = 59.08256m,
        DiscountPolicy = "",
        //Discount = 0,
        Discount2 = 12,
        Subtotal = 51.9926528m,
        Notes = "NOTAS DE PRODUCTO 2"
      };
      itemsFields.Add(fields2);

      return new FixedList<SalesOrderItemsFields>(itemsFields);
    }

    #endregion Helpers

  } // public class SalesTest

} // namespace Empiria.Trade.Tests.Sales
