/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Sales Order Management                     Component : Use cases Layer                         *
*  Assembly : Empiria.Trade.Orders.dll                   Pattern   : Use case interactor class               *
*  Type     : SalesOrderUseCases                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases used to management Products.                                                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Services;
using Empiria.Trade.Core;
using Empiria.Trade.Core.Catalogues;
using Empiria.Trade.Core.Inventories.Adapters;
using Empiria.Trade.Financial.Adapters;
using Empiria.Trade.Financial.UseCases;
using Empiria.Trade.Inventory.UseCases;
using Empiria.Trade.Orders;
using Empiria.Trade.Products;
using Empiria.Trade.Sales.Adapters;

using Empiria.Trade.Sales.Data;
using Empiria.Trade.Sales.ShippingAndHandling;
using Empiria.Trade.Sales.ShippingAndHandling.Data;

namespace Empiria.Trade.Sales.UseCases {

  /// <summary>Use cases used to management Orders.</summary>
   public class SalesOrderUseCases : UseCase {

    #region Constructors and parsers

    public SalesOrderUseCases() {
      // no-op
    }

    static public SalesOrderUseCases UseCaseInteractor() {
      return UseCase.CreateInstance<SalesOrderUseCases>();
    }

    #endregion Constructors and parsers

    #region Use cases

    public ISalesOrderDto ProcessSalesOrder(SalesOrderFields fields) {
      Assertion.Require(fields, "fields");

      SalesOrder order;

      ValidateOrderItemsExistence(fields.Items);

      if (fields.UID.Length != 0) {
        order = SalesOrder.Parse(fields.UID);
        order.Update(fields);
      } else {
        order = new SalesOrder(fields);
      }

      return SalesOrderMapper.Map(order);
    }


    public ISalesOrderDto CreateSalesOrder(SalesOrderFields fields) {
      Assertion.Require(fields, "fields");

      ValidateShippingMethod(fields);
      ValidateOrderItemsExistence(fields.Items);

      var order = new SalesOrder(fields);

      order.Save();

      return SalesOrderMapper.Map(order); 
    }

    public ISalesOrderDto DeliverySalesOrderWithMap(string orderUID) {

      Assertion.Require(orderUID, "orderUID");

      var order = SalesOrder.Parse(orderUID);

      order.Deliver();
          
      return SalesOrderMapper.Map(order);
    }

    public void DeliverySalesOrder(string orderUID) {

      Assertion.Require(orderUID, "orderUID");

      var order = SalesOrder.Parse(orderUID);

      order.Deliver();
    }

    public void CloseSalesOrder(string orderUID) {

      Assertion.Require(orderUID, "orderUID");

      var order = SalesOrder.Parse(orderUID);

      order.Close();
    }

    public SearchSalesOrderDto GetOrders(SearchOrderFields fields) {
      Assertion.Require(fields, "fields");

      var helper = new SalesOrderHelper();

      switch (fields.QueryType) {

        case QueryType.Sales: {
          var salesOrdersList = helper.GetOrders(fields);
          return SearchSealesOrderMapper.Map(fields, salesOrdersList);
        }
        case QueryType.SalesAuthorization: {
          FixedList<SalesOrder> salesOrders = helper.GetOrdersToAuthorize(fields);
          return SearchSealesOrderMapper.Map(fields, salesOrders);
        }
        case QueryType.SalesPacking: {
          FixedList<SalesOrder> salesOrders = helper.GetOrdersToPacking(fields);
          return SearchSealesOrderMapper.Map(fields, salesOrders);
        }

        default: {
          throw Assertion.EnsureNoReachThisCode($"It is invalid queryType:{fields.QueryType}");
        }

      } 

    }

    public FixedList<ISalesOrderDto> GetOrdersForShipping(SearchOrderFields fields) {
      var helper = new SalesOrderHelper();
      var salesOrdersList = helper.GetOrders(fields);

      return SearchSealesOrderMapper.MapEntries(fields, salesOrdersList);
    }

    public ISalesOrderDto CancelSalesOrder(string orderUID) {
      Assertion.Require(orderUID, "orderUID");

      var order = SalesOrder.Parse(orderUID);
      order.Cancel();

      return SalesOrderMapper.Map(order);
    }

    public void ChangeOrdersToDeliveryStatus(string [] ordersUID) {
      Assertion.Require(ordersUID, "ordersUID");

      foreach(var orderUID in ordersUID) {
        DeliverySalesOrder(orderUID);
      }     
    
    }

    public void ChangeOrdersToCloseStatus(string[] ordersUID) {
      Assertion.Require(ordersUID, "ordersUID");

      foreach (var orderUID in ordersUID) {
        CloseSalesOrder(orderUID);
      }

    }

    public ISalesOrderDto ApplySalesOrder(string orderUID) {
      Assertion.Require(orderUID, "orderUID");
           
      var order = SalesOrder.Parse(orderUID);
      order.GetOrderTotal();

      switch (order.PaymentCondition) {
        case "Contado": order.AuthorizePayment(); break;
        case "Credito": SetCreditOrder(order); break;
      }
         
      return SalesOrderMapper.Map(order);
    }
       

    public ISalesOrderDto GetSalesOrder(string orderUID, QueryType queryType) {
      var order = SalesOrder.Parse(orderUID);

      order.CalculateSalesOrder();
      order.SetOrderActions(queryType);

      return SalesOrderMapper.Map(order);
    }

    public ISalesOrderDto UpdateSalesOrder(SalesOrderFields fields) {
      Assertion.Require(fields, "fields");
                  
      if (fields.Status != Orders.OrderStatus.Captured) {
        Assertion.RequireFail($"It is only possible to update orders in the Captured status your order status is:{fields.Status}");
      }

      ValidateOrderItemsExistence(fields.Items);

      var order = SalesOrder.Parse(fields.UID);

      SalesOrderItemsData.CancelOrderItems(order.Id);

      order.Update(fields);
      order.Save();
                       
      return SalesOrderMapper.Map(order);
    }

    public FixedList<NamedEntityDto> GetStatusList() {
      return SalesOrderStatusService.GetStatusList();
    }

    public ISalesOrderDto AuthorizeSalesOrder(string orderUID) {

      Assertion.Require(orderUID, "orderUID");

      var order = SalesOrder.Parse(orderUID);

      if (order.Status != Orders.OrderStatus.Applied) {
        Assertion.RequireFail($"It is only possible to Authorize orders in the Applied status, your order status is: {order.Status}");
      }

      order.Authorize();

      AddCredit(order);

      return SalesOrderMapper.Map(order);
    }
      

    public ISalesOrderDto SupplySalesOrder(string orderUID) {
      Assertion.Require(orderUID, "orderUID");

      var order = SalesOrder.Parse(orderUID);

      if (order.Status != Orders.OrderStatus.Packing) {
        Assertion.RequireFail($"It is only possible to Supply orders in the Packing status, your order status is: {order.Status}");
      }

      order.Supply();

      SalesOrderHelper helper = new SalesOrderHelper();
      helper.CreateInventoryOrderBySale(order.Id, order.Supplier.UID);
      
      return SalesOrderMapper.Map(order);
    }

    public FixedList<NamedEntityDto> GetAuthorizationStatusList() {
      return SalesOrderStatusService.GetAuthorizationStatusList();
    }

    public FixedList<NamedEntityDto> GetPackingStatusList() {
      return SalesOrderStatusService.GetPackingStatusList();
    }


    #endregion Use cases

    #region Private methods

    private void AddCredit(SalesOrder order) {
      var creditFields = new CreditTrasnactionFields() {
        CustomerId = order.Customer.Id,
        TransactionTime = DateTime.Now,
        CreditAmount = order.OrderTotal,
        PayableOrderId = order.Id,
        ExtData = order.OrderNumber
      };
      var moneyAccountUseCase = MoneyAccountUseCases.UseCaseInteractor();

      moneyAccountUseCase.AddCreditTransaction(creditFields);
    }

    private void SetCreditOrder(SalesOrder order) {
      var debitTotal = GetCustomerTotalDebt(order.Customer.Id) + order.OrderTotal;
      if (debitTotal > GetCusomerCreditLimit(order.Customer.Id)) {
        order.Apply();
      } else {
        AddCredit(order);
        order.Authorize();
      }
    }

    private void ValidateShippingMethod(SalesOrderFields fields) {
      if ((fields.ShippingMethod != Orders.ShippingMethods.Ocurre) && (fields.customerAddressUID == String.Empty)) {
        throw Assertion.EnsureNoReachThisCode($"It is customer address is mandatory.");
      }
    }

    private void ValidateOrderItemsExistence(FixedList<SalesOrderItemsFields> items) {
      foreach (var item in items) {
        var vendor = VendorProduct.Parse(item.VendorProductUID);
        var productExistence = GetItemExistence(vendor.Id);

        if (productExistence < item.Quantity) {
          throw Assertion.EnsureNoReachThisCode($"No hay existencia suficiente. del producto {vendor.ProductFields.ProductCode} {vendor.ProductFields.ProductName}");
        }
      }

    }

    private decimal GetItemExistence(int vendorProductId) {

      var usecase = CataloguesUseCases.UseCaseInteractor();
      FixedList<SalesInventoryStock> sut = usecase.GetInventoryStockByVendorProduct(vendorProductId);

      return sut[0].Stock;
    }

    public ISalesOrderDto CancelCreditInOrder(string orderUID, string notes) {

      var order = SalesOrder.Parse(orderUID);

      var moneyAccountUseCase = MoneyAccountUseCases.UseCaseInteractor();
      moneyAccountUseCase.CancelTransaction(order.Id, notes);

      order.Deauthorize();
      

     return SalesOrderMapper.Map(order);
    }

    static private decimal GetCusomerCreditLimit(int customerId) {
      var moneyAccountUseCases = MoneyAccountUseCases.UseCaseInteractor();

      return moneyAccountUseCases.GetMoneyAccountCreditLimit(customerId);
    }

    static private decimal GetCustomerTotalDebt(int customerId) {

      var moneyAccountUseCase = MoneyAccountUseCases.UseCaseInteractor();

      return moneyAccountUseCase.GetMoneyAccountTotalDebt(customerId);
    }

    #endregion Private methods

  } // class SalesOrderUseCases

} //namespace Empiria.Trade.Sales.UseCases



