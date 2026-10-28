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
        
        var sut = usecases.SetSalesOrderToAuthorized("8ca76174-1a8c-4b30-975b-16a4eef46f67");

        Assert.NotNull(sut);
      }
    }


    [Fact]
    public void GetSalesOrderByUID() {

      var salesOrderUseCase = SalesOrderUseCases.UseCaseInteractor();

      var sut = salesOrderUseCase.GetSalesOrder("002cf56e-c48a-4008-82c7-e7224a460c25", QueryType.SalesPacking);

      Assert.NotNull(sut);
    }


    [Fact]
    public void GetSalesOrderForShipping() {

      var salesOrderUseCase = SalesOrderUseCases.UseCaseInteractor();

      SearchOrderFields fields = new SearchOrderFields();

      fields.QueryType = QueryType.Sales;
      fields.Status = OrderStatus.Shipping;
      fields.ShippingMethod = ShippingMethods.Paqueteria;
      fields.Keywords = "P-H7NPUF6L4D";

      var sut = salesOrderUseCase.GetSalesOrdersForShipping(fields);

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
      var sut = useCase.UpdateSalesOrder("a1832b0c-90da-43e3-9dca-53143d873a16", orderFields);

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
        QueryType = QueryType.Sales,
        //Keywords = "",
        //FromDate = new DateTime(2026,9,5),
        //ToDate = new DateTime(2026, 9, 11),
        Status = OrderStatus.Shipping,
        //ShippingMethod = ShippingMethods.RutaLocal
      };

      var usecases = SalesOrderUseCases.UseCaseInteractor();

      SearchSalesOrderDto salesOrders = usecases.SearchSalesOrders(fields);

      Assert.NotNull(salesOrders);
    }


    [Fact]
    public void ShouldCancelOrder() {

      var order = SalesOrder.Parse("ead9f502-e94c-4b3c-aa75-367482f9ba2c");


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

      var sut = useCase.SupplySalesOrder("a0194f91-9c1c-4405-8c1a-a0c284432533");
      
      Assert.NotNull(sut);
    }


    [Fact]
    public void ShouldDeauthorizeSalesOrder() {

      var salesOrderUseCase = SalesOrderUseCases.UseCaseInteractor();

      DeauthorizeFields fields = new DeauthorizeFields {
        Notes = "CANCELADO POR EL SISTEMA"
      };
      var x = salesOrderUseCase.SetCreditInOrderToCanceled("b1bdf729-c0e3-4476-95af-5bff62cdeb96", fields);

      Assert.NotNull(x);
    }

    [Fact]
    public void ShouldApplyCreditInOrder() {

      var salesOrderUseCase = SalesOrderUseCases.UseCaseInteractor();
      var sut = salesOrderUseCase.ApplySalesOrder("3dc85800-93d4-4121-a4ae-ff209edf7e53");

      Assert.NotNull(sut);
    }


    [Fact]
    public void ShouldGetOrderByOrderNumber() {

      var salesOrderUseCase = SalesOrderUseCases.UseCaseInteractor();
      var x = salesOrderUseCase.GetSalesOrderByNumber("P-TYN9NOZ37B");

      Assert.NotNull(x);
    }

    #region Helpers

    private SalesOrderFields GetSalesOrderFields() {

      return new SalesOrderFields {
        //UID = "fd7eefd6-9dcb-4020-8209-69b304f9085a",
        CustomerUID = "c74f0f44-39a4-4f8b-8e0a-7853909648b7",
        CustomerContactUID = "5dc570bd-8653-4ade-a869-0540fc391f49",
        CustomerAddressUID = "4886782b-fefa-48fd-8cf0-e4412dd94753",
        SalesAgentUID = "32bdc986-2301-40f4-ba04-80b69d3e3a1f",
        SupplierUID = "4d462287-9cee-400d-bd63-e2f31b171315",
        PaymentConditions = "Contado",
        OrderNumber = "P-9CYLF20XNF",
        ShippingMethod = ShippingMethods.Paqueteria,
        ParcelSupplierUID = "875153f9-464a-4479-9b3c-5e47d94d4499",
        //OrderTime = DateTime.Now,
        Status = OrderStatus.Captured,
        Notes = "",
        Items = GetSalesOrderItemFields()
      };
    }


    private FixedList<SalesOrderItemsFields> GetSalesOrderItemFields() {

      List<SalesOrderItemsFields> itemsFields = new List<SalesOrderItemsFields>();

      var fields = new SalesOrderItemsFields {
        //OrderItemUID = "",
        VendorProductUID = "440b32f0-6afc-4318-b14f-08aebe89167d",
        Quantity = 100,
        UnitPrice = 14.03313m,
        //DiscountPolicy = "",
        //Discount1 = 10,
        //Discount2 = 10,
        //Subtotal = 4.7217m,
        Notes = "N/AA"
      };

      itemsFields.Add(fields);

      //var fields2 = new SalesOrderItemsFields {
      //  OrderItemUID = "0c9c4c0d-c73d-469e-8e15-f8568b13ae15",
      //  VendorProductUID = "43a90f2f-9126-4137-a115-8160d820cd97",
      //  Quantity = 4,
      //  UnitPrice = 14.77064m,
      //  DiscountPolicy = "",
      //  //Discount = 0,
      //  Discount2 = 12,
      //  Subtotal = 51.9926528m,
      //  Notes = "NOTAS DE PRODUCTO 2"
      //};
      //itemsFields.Add(fields2);

      return new FixedList<SalesOrderItemsFields>(itemsFields);
    }

    #endregion Helpers

  } // public class SalesTest

} // namespace Empiria.Trade.Tests.Sales
