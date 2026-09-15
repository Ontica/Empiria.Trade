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
using System.Linq;
using DocumentFormat.OpenXml.Bibliography;
using Empiria.Orders;
using Empiria.Parties;
using Empiria.Services;
using Empiria.StateEnums;
using Empiria.Trade.Core;
using Empiria.Trade.Core.Catalogues;
using Empiria.Trade.Core.UsesCases;
using Empiria.Trade.Financial;
using Empiria.Trade.Financial.Adapters;
using Empiria.Trade.Financial.UseCases;
using Empiria.Trade.Products;
using Empiria.Trade.Sales.Adapters;

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

    public ISalesOrderDto GetSalesOrder(string orderUID, QueryType queryType) {

      var order = SalesOrder.Parse(orderUID);
      order.GetSalesOrderItems();

      order.Customer = Party.Parse(order.Beneficiary.Id);
      order.GetCustomerContact();
      order.GetCustomerAddress();

      order.Supplier = Party.Parse(order.Provider.Id);
      order.SalesAgent = Party.Parse(order.SalesAgentId);

      order.CalculateSalesOrder();
      order.SetOrderActions(queryType);

      return SalesOrderMapper.Map(order);
    }


    public ISalesOrderDto ProcessSalesOrder(SalesOrderFields fields) {
      Assertion.Require(fields, "fields");

      if (fields.PaymentConditions == string.Empty) {
        fields.PaymentConditions = "Contado";
      }
      
      ValuateSalesOrder(fields);

      SalesOrder order = InitializeSalesOrder(fields);

      return SalesOrderMapper.Map(order);
    }


    public ISalesOrderDto CreateSalesOrder(SalesOrderFields fields) {
      Assertion.Require(fields, "fields");

      ValuateSalesOrder(fields);

      var orderType = OrderType.SalesOrder;

      fields.CanUpdateOrder = true;
      fields.Status = OrderStatus.Captured;

      fields.MapToOrderFields(orderType);

      var order = new SalesOrder(fields, orderType);

      order.Save();

      foreach (var item in order.SalesOrderItems) {

        order.AddSalesOrderItem(item, order.Id);

      }

      return SalesOrderMapper.Map(order);
    }


    public ISalesOrderDto UpdateSalesOrder(string orderUID, SalesOrderFields fields) {
      Assertion.Require(fields, "fields");

      if (fields.Status != OrderStatus.Captured) { // OrderStatus.Captured
        Assertion.RequireFail($"It is only possible to update orders in the Captured status, " +
                              $"your order status is:{fields.Status}");
      }

      ValidateCustomerAddress(fields.CustomerUID, fields.CustomerAddressUID);
      ValidateOrderItemsExistence(fields.Items);

      var order = SalesOrder.Parse(orderUID);

      fields.CanUpdateOrder = true;
      fields.OrderNumber = order.OrderNo;
      fields.MapToOrderFields(order.OrderType);

      //SalesOrderItemsData.CancelOrderItems(order.Id);

      order.Update(fields);
      order.Save();

      foreach (var item in order.SalesOrderItems) {

        order.AddSalesOrderItem(item, order.Id);
      }

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


    public SearchSalesOrderDto GetOrdersV2(SearchOrderFields fields) {
      Assertion.Require(fields, "fields");

      //fields.EnsureIsValidSearch();

      var helper = new SalesOrderHelper(fields);

      switch (fields.QueryType) {

        case QueryType.Sales: {
          var salesOrdersList = helper.GetOrders();
          return SearchSealesOrderMapper.Map(fields, salesOrdersList);
        }
        case QueryType.SalesAuthorization: {
          FixedList<SalesOrder> salesOrders = helper.GetAuthorizationOrders();
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


    public SearchSalesOrderDto GetOrders(SearchOrderFields fields) {
      Assertion.Require(fields, "fields");

      var helper = new SalesOrderHelper(fields);

      switch (fields.QueryType) {

        case QueryType.Sales: {
          var salesOrdersList = helper.GetOrders();
          return SearchSealesOrderMapper.Map(fields, salesOrdersList);
        }
        case QueryType.SalesAuthorization: {
          FixedList<SalesOrder> salesOrders = helper.GetAuthorizationOrders();
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


    public FixedList<ISalesOrderDto> GetOrdersByCustomerUID(string customerUID) {
      var helper = new SalesOrderHelper();
      var customer = Parties.Party.Parse(customerUID);

      FixedList<SalesOrder> salesOrders = helper.GetOrdersByCustomer(customer.Id);

      return SearchSealesOrderMapper.MapBaseSalesOrders(salesOrders);
    }


    public FixedList<ISalesOrderDto> GetOrdersForShipping(SearchOrderFields fields) {
      var helper = new SalesOrderHelper(fields);

      var salesOrdersList = helper.GetOrders();

      return SearchSealesOrderMapper.MapEntries(fields, salesOrdersList);
    }


    public ISalesOrderDto CancelSalesOrder(string orderUID) {
      Assertion.Require(orderUID, "orderUID");

      var order = SalesOrder.Parse(orderUID);
      order.Cancel();

      return SalesOrderMapper.Map(order);
    }


    public void ChangeOrdersToDeliveryStatus(string[] ordersUID) {
      Assertion.Require(ordersUID, "ordersUID");

      foreach (var orderUID in ordersUID) {
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
      
      order.Customer = order.Beneficiary;
      order.GetCustomerContact();
      order.GetCustomerAddress();
      order.Supplier = Party.Parse(order.Provider.Id);
      order.SalesAgent = Party.Parse(order.SalesAgentId);

      order.GetItemsAndOrderTotal();

      switch (order.PaymentConditions) {
        case "Contado":
          order.AuthorizePayment();
          break;
        case "Crédito":
        case "Credito":
          SetCreditOrder(order);
          break;
      }

      SalesOrderHelper helper = new SalesOrderHelper();
      //helper.CreateInventoryOrderBySale(order.SalesOrderItems);

      return SalesOrderMapper.Map(order);
    }


    public ISalesOrderDto GetSalesOrder(string orderNumber) {
      SalesOrderHelper helper = new SalesOrderHelper();

      var order = helper.GetSalesOrder(orderNumber);

      return SalesOrderMapper.Map(order);
    }


    public FixedList<NamedEntityDto> GetStatusList() {
      return SalesOrderStatusService.GetStatusList();
    }


    public ISalesOrderDto AuthorizeSalesOrder(string orderUID) {

      Assertion.Require(orderUID, "orderUID");

      var order = SalesOrder.Parse(orderUID);
      var orderStatus = EnumExtensions.GetOrderStatusEnum(order.OrderStatus);

      if (orderStatus != OrderStatus.Applied) { // OrderStatus.Applied
        Assertion.RequireFail($"It is only possible to Authorize orders in the Applied status, " +
                              $"your order status is: {order.Status}");
      }

      order.AuthorizeOrder();

      AddCredit(order);

      order.Save();

      return SalesOrderMapper.Map(order);
    }


    public ISalesOrderDto SupplySalesOrder(string orderUID) {

      Assertion.Require(orderUID, "orderUID");

      var order = SalesOrder.Parse(orderUID);

      if (order.Status != EntityStatus.OnReview) { // OrderStatus.Packing
        Assertion.RequireFail($"It is only possible to Supply orders in the Packing status, " +
                              $"your order status is: {order.Status}");
      }

      order.Supply();

      return SalesOrderMapper.Map(order);
    }


    public FixedList<NamedEntityDto> GetAuthorizationStatusList() {
      return SalesOrderStatusService.GetAuthorizationStatusList();
    }


    public FixedList<NamedEntityDto> GetPackingStatusList() {
      return SalesOrderStatusService.GetPackingStatusList();
    }


    static internal decimal GetItemExistence(int productId) {

      var usecase = CataloguesUseCases.UseCaseInteractor();

      FixedList<SalesInventoryStock> inventoryStock =
        CataloguesUseCases.GetInventoryStockByVendorProduct(productId, "");

      return inventoryStock.Sum(x => x.Stock);
    }

    #endregion Use cases

    #region Private methods

    private void AddCredit(SalesOrder order) {

      var creditFields = new CreditTrasnactionFields() {
        CustomerId = order.Customer.Id,
        TransactionTime = DateTime.Now,
        CreditAmount = order.OrderTotal,
        PayableOrderId = order.Id,
        ExtData = order.OrderNo
      };

      var moneyAccountUseCase = MoneyAccountUseCases.UseCaseInteractor();

      moneyAccountUseCase.AddCreditTransaction(creditFields);
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

      //return moneyAccountUseCase.GetMoneyAccountTotalDebt(customerId);
      return moneyAccountUseCase.GetMoneyAccountTotalDebits(customerId);
    }


    private SalesOrder InitializeSalesOrder(SalesOrderFields fields) {
      SalesOrder order;

      var orderType = OrderType.SalesOrder;
      
      fields.MapToOrderFields(orderType);

      if (fields.UID.Length > 0) {
        order = SalesOrder.Parse(fields.UID);
        order.Update(fields);
      } else {
        order = new SalesOrder(fields, orderType);
      }
      return order;
    }


    private void SetCreditOrder(SalesOrder order) {

      var customerCreditLimit = order.Beneficiary.ExtendedData.Get<decimal>("LimiteCredito", 0);
      
      var customerDebit = GetCustomerTotalDebt(order.Beneficiary.Id);

      var orderTotal = order.OrderTotal;

      var debitTotal = customerDebit + orderTotal;

      //if (debitTotal > GetCusomerCreditLimit(order.Beneficiary.Id)) {
      if (debitTotal <= customerCreditLimit) {
        //SI DEUDA ES MENOR O IGUAL A LIMITE, SE APLICA
        order.Apply();
      } else {

        AddCredit(order);

        order.AuthorizeOrder();
      }
    }


    private void ValidateShippingMethod(SalesOrderFields fields) {

      if ((fields.ShippingMethod != ShippingMethods.Ocurre) && (fields.CustomerAddressUID == string.Empty)) {
        throw Assertion.EnsureNoReachThisCode($"La dirección del cliente es obligatoria.");
      }
    }


    private void ValidateOrderItemsExistence(FixedList<SalesOrderItemsFields> itemsFields) {

      foreach (var fields in itemsFields) {

        var product = ProductEntry.Parse(fields.VendorProductUID);
        var productExistence = GetItemExistence(product.Id);

        fields.ProductStock = productExistence;

        if (productExistence < fields.Quantity) {

          Assertion.EnsureNoReachThisCode($"No hay existencia suficiente del producto " +
            $"{product.InternalCode} {product.Name}");
        }
      }

    }


    private void ValidateCustomerAddress(string customerUID, string customerAddressUID) {

      var usescase = CustomerUseCases.UseCaseInteractor();
      var addresses = usescase.GetCustomerAddress(customerUID);

      if (usescase.GetCustomerAddress(customerUID).Contains(x => x.UID == customerAddressUID) == false) {
        Assertion.EnsureNoReachThisCode($"El Cliente no tiene registrada la dirección seleccionada");
      }
    }


    private void ValuateSalesOrder(SalesOrderFields fields) {

      ValidateCustomerAddress(fields.CustomerUID, fields.CustomerAddressUID);
      ValidateShippingMethod(fields);
      ValidateOrderItemsExistence(fields.Items);
    }

    #endregion Private methods

  } // class SalesOrderUseCases

} //namespace Empiria.Trade.Sales.UseCases



  