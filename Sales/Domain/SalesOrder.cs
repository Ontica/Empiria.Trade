/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Sales Order Management                     Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Sales.dll                    Pattern   : Partitioned Type / Information Holder   *
*  Type     : SalesOrder                                 License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a sales order.                                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using Empiria.Services;
using System.Security.Cryptography;
using Empiria.Trade.Core;
using Empiria.Trade.Orders;
using Empiria.Trade.Sales.Adapters;
using Empiria.Trade.Sales.Data;
using Empiria.Trade.ShippingAndHandling.UseCases;
using Empiria.Trade.ShippingAndHandling;

namespace Empiria.Trade.Sales {

  /// <summary>Represents a sales order.</summary>
  public class SalesOrder : Order {

    #region Constructors and parsers

    public SalesOrder() {
      //no-op
    }

    public SalesOrder(SalesOrderFields fields) {
      Update(fields);
    }

    static public new SalesOrder Parse(int id) {
      return BaseObject.ParseId<SalesOrder>(id);
    }

    static public new SalesOrder Parse(string uid) {
      return BaseObject.ParseKey<SalesOrder>(uid);
    }

    

    #endregion Constructors and parsers

    #region Public properties

    public FixedList<SalesOrderItem> SalesOrderItems {
      get; private set;
    } = new FixedList<SalesOrderItem>();

    public int ItemsCount {
      get; private set;
    } = 0;


    public decimal ItemsTotal {
      get; private set;
    } = 0m;

    public decimal Shipment {
      get; private set;
    } = 0m;

    public decimal Discount {
      get; private set;
    } = 0m;

    public decimal Taxes {
      get; private set;
    } = 0m;

    public decimal OrderTotal {
      get; private set;
    } = 0m;

    public string PriceList {
      get; private set;
    }

    public OrderActions Actions {
      get;
      protected set;
    }

    public decimal TotalDebt {
      get; private set;
    } = 0;

    public decimal CreditLimit {
      get; private set;
    } = 0;

    public FixedList<CreditTransaction> CreditTransactions {
      get; private set;
    }
    
    public decimal Weight {
      get; set;
    }

    public int TotalPackages {
      get; set;
    }


    #endregion

    #region Public methods

    protected override void OnSave() {
      if (IsNew) {
        OrderNumber = "P-" + EmpiriaString.BuildRandomString(10).ToUpperInvariant();
        OrderTime = DateTime.Now;
        Status = OrderStatus.Captured;
 
      }

      SalesOrderData.Write(this);
      SalesOrderItem.SaveSalesOrderItems(this.SalesOrderItems, this.Id);

    }

    public void Apply() {
      Status = OrderStatus.Applied;
      AuthorizationStatus = OrderAuthorizationStatus.Pending;

      SalesOrderData.Write(this);
      this.SalesOrderItems = SalesOrderItem.GetOrderItems(this.Id);
      this.Actions = OrderActions.GetApplyActions();
      this.Actions.CanAuthorize = false;
      SetOrderTotals();
    }

    internal void Update(SalesOrderFields fields) {

      this.OrderTypeId = 1025;
      this.OrderTime = fields.OrderTime;
      this.Customer = fields.GetCustomer();
      this.Supplier = fields.GetSupplier();
      this.SalesAgent = fields.GetSalesAgent();
      this.Notes = fields.Notes;
      this.Status = fields.Status;
      this.ShippingMethod = fields.ShippingMethod;
      this.PaymentCondition = fields.PaymentCondition;
      this.PriceList = GetPriceList();
      this.SalesOrderItems = LoadSalesOrderItems(fields.Items);

      SetOrderTotals();

      this.Actions = OrderActions.GetCaptureActions();
    }

    public void Cancel() {

      Status = OrderStatus.Cancelled;

      SalesOrderData.Write(this);
      SalesOrderItemsData.CancelOrderItems(this.Id);
      this.SalesOrderItems = SalesOrderItem.GetOrderItems(this.Id);

      SetOrderTotals();
    }

    public void Modify(SalesOrderFields fields) {
      SalesOrderItemsData.CancelOrderItems(this.Id);
      Update(fields);

      Save();
    }

    public void Authorize() {
      AuthorizationStatus = OrderAuthorizationStatus.Authorized;
      this.AuthorizationTime = DateTime.Now;
      this.AuthorizatedById = ExecutionServer.CurrentUserId;

      this.Status = OrderStatus.Packing;
      AuthorizationStatus = OrderAuthorizationStatus.ToSupply;

      this.Actions = OrderActions.GetAuthorizedActions();

      SalesOrderData.Write(this);
      this.SalesOrderItems = SalesOrderItem.GetOrderItems(this.Id);

      SetOrderTotals();
    }

    public void Supply() {
      this.Status = OrderStatus.CarrierSelector;
      
      AuthorizationStatus = OrderAuthorizationStatus.CarrierSelctor;

      SalesOrderData.Write(this);
      this.SalesOrderItems = SalesOrderItem.GetOrderItems(this.Id);
      this.Actions = OrderActions.GetPackingActions();
      SetOrderTotals();

    }

    static public FixedList<SalesOrder> GetOrders(SearchOrderFields fields) {          
      var orders = SalesOrderData.GetSalesOrders(fields);

      return GetOrderItems(orders, "Pedidos");
    }

    static public FixedList<SalesOrder> GetOrdersToAuthorize(SearchOrderFields fields) {
      var orders = SalesOrderData.GetSalesOrdersToAuthorize(fields);
      SetCustomerCreditInfo(orders);
     
      return GetOrderItems(orders ,"Autorizacion");
    }

    internal static FixedList<SalesOrder> GetOrdersToPacking(SearchOrderFields fields) {
      var orders = SalesOrderData.GetSalesOrdersToPacking(fields);
      GetWeightTotalPackageByOrder(orders);
      return GetOrderItems(orders,"Surtido");
    }

    private static void GetWeightTotalPackageByOrder(FixedList<SalesOrder> orders) {
      foreach (var order in orders) {
        var usecasePackage = PackagingUseCases.UseCaseInteractor();
        PackagedData packageInfo = usecasePackage.GetPackagedData(order.UID);
        order.Weight = packageInfo.Weight;
        order.TotalPackages = packageInfo.Count;
      }
    }

    #endregion Public methods

    #region Helpers

    private FixedList<SalesOrderItem> LoadSalesOrderItems(FixedList<SalesOrderItemsFields> orderItemsFields) {
      List<SalesOrderItem> orderItems = new List<SalesOrderItem>();

      foreach (SalesOrderItemsFields itemFields in orderItemsFields) {
        var saleOrderItem = new SalesOrderItem(this, itemFields);

        orderItems.Add(saleOrderItem);
      }

      return orderItems.ToFixedList();
    }


    private void SetOrderTotals() {
      this.OrderTotal = 0;
      this.ItemsTotal = 0;
      this.Taxes = 0;

      foreach (SalesOrderItem item in this.SalesOrderItems) {
        this.ItemsCount++;
        this.ItemsTotal += item.SubTotal;
        this.Shipment += item.Shipment;
        this.Discount += item.Discount;
        this.Taxes += item.TaxesIVA;
        this.OrderTotal += item.Total;
      }

    }

    private static void SetOrderTotals(SalesOrder order) {

      order.OrderTotal = 0;
      order.ItemsTotal = 0;
      order.Taxes = 0;

      foreach (SalesOrderItem item in order.SalesOrderItems) {
        order.ItemsCount++;
        order.ItemsTotal += item.SubTotal;
        order.Shipment += item.Shipment;
        order.Discount += item.Discount;
        order.Taxes += item.TaxesIVA;
        order.OrderTotal += item.Total;
      }

    }


    static private FixedList<SalesOrder> GetOrderItems(FixedList<SalesOrder> orders, string queryType) {
      List<SalesOrder> salesOrders = new List<SalesOrder>();

      foreach (var order in orders) {
        order.SalesOrderItems = SalesOrderItem.GetOrderItems(order.Id);
        SetOrderTotals(order);
        salesOrders.Add(order);
        SetAuthorizedActions(order, queryType);
        order.CreditTransactions = GetCreditTransactions(order.Customer.Id);
      }

      return salesOrders.ToFixedList<SalesOrder>();
    }

    private static void SetAuthorizedActions(SalesOrder order, string queryType) {
      switch (order.Status) {
        case OrderStatus.Captured: order.Actions = OrderActions.GetCaptureActions(); break;
        case OrderStatus.Applied: {
          order.Actions = OrderActions.GetApplyActions();
          if (queryType == "Pedidos") {
            order.Actions.CanAuthorize = false;
          }
          if (queryType == "Autorizacion") {
            order.Actions.CanAuthorize = true;
          }

        }
        break;
        case OrderStatus.Authorized: order.Actions = OrderActions.GetAuthorizedActions(); break;
        case OrderStatus.Packing: order.Actions = OrderActions.GetPackingActions(); break;
        case OrderStatus.Cancelled: order.Actions = OrderActions.GetCancellActions(); break;
        case OrderStatus.CarrierSelector:order.Actions = OrderActions.GetSelectCarrierActions();break;
      }

    }

    private string GetPriceList() {
      var pricesList = CustomerPrices.GetVendorPrices(this.Customer.Id);

      var vendorPrice = pricesList.Find(r => r.VendorId == this.Supplier.Id);

      return vendorPrice.PriceListId.ToString();
    }

    static private void SetCustomerCreditInfo(FixedList<SalesOrder> orders) {
      foreach (SalesOrder order in orders) {
        order.TotalDebt = CrediLineData.GetCreditDebt(order.Customer.Id);
        order.CreditLimit = CrediLineData.GetCreditLimit(order.Customer.Id);
      }

    }

    static public FixedList<CreditTransaction> GetCreditTransactions(int customerId) {
      var creditLineId = Empiria.Trade.Sales.Data.CrediLineData.GetCreditLineId(customerId);
      return CreditTransaction.GetCreditTransactions(creditLineId);
    }

    static public SalesOrder GetSalesOrder(string orderUID) {
      var order = Parse(orderUID);
      order.SalesOrderItems = SalesOrderItem.GetOrderItems(order.Id);
      SetOrderTotals(order);
      
      SetAuthorizedActions(order, "");
      order.CreditTransactions = GetCreditTransactions(order.Customer.Id);
      order.TotalDebt = CrediLineData.GetCreditDebt(order.Customer.Id);
      order.CreditLimit = CrediLineData.GetCreditLimit(order.Customer.Id);
      var usecasePackage = PackagingUseCases.UseCaseInteractor();
      PackagedData packageInfo = usecasePackage.GetPackagedData(order.UID);
      order.Weight = packageInfo.Weight;
      order.TotalPackages = packageInfo.Count;

      return order;
    }



    #endregion Helpers

  }  //  class SalesOrder

}  // namespace Empiria.Trade.Sales
