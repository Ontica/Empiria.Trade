﻿/* Empiria Trade *********************************************************************************************
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

      this.CreditTransactions = GetCreditTransactions(this.Customer.Id);
      this.TotalDebt = CrediLineData.GetCreditDebt(this.Customer.Id);
      this.CreditLimit = CrediLineData.GetCreditLimit(this.Customer.Id);
      if (this.UID != "") {
        var usecasePackage = PackagingUseCases.UseCaseInteractor();
        PackagedData packageInfo = usecasePackage.GetPackagedData(this.UID);
        this.Weight = packageInfo.Weight;
        this.TotalPackages = packageInfo.Count;
      } else {
        this.Weight = 0;
        this.TotalPackages = 0;
      }

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

    public void SetCustomerCreditInfos() {
      this.TotalDebt = CrediLineData.GetCreditDebt(this.Customer.Id);
      this.CreditLimit = CrediLineData.GetCreditLimit(this.Customer.Id);
    }
     

      public void GetWeightTotalPackageByOrder() {
      var usecasePackage = PackagingUseCases.UseCaseInteractor();
      PackagedData packageInfo = usecasePackage.GetPackagedData(this.UID);

      this.Weight = packageInfo.Weight;
      this.TotalPackages = packageInfo.Count;
    }

    public void CalculateSalesOrder(string queryType) {

      this.SalesOrderItems = SalesOrderItem.GetOrderItems(this.Id);
      SetOrderTotals();

      this.SetAuthorizedActions(queryType);
      this.CreditTransactions = this.GetCustomerCreditTransactions();
      this.TotalDebt = CrediLineData.GetCreditDebt(this.Customer.Id);
      this.CreditLimit = CrediLineData.GetCreditLimit(this.Customer.Id);

      var usecasePackage = PackagingUseCases.UseCaseInteractor();
      PackagedData packageInfo = usecasePackage.GetPackagedData(this.UID);
      this.Weight = packageInfo.Weight;
      this.TotalPackages = packageInfo.Count;
    }

    public FixedList<CreditTransaction> GetCustomerCreditTransactions() {
      var creditLineId = Empiria.Trade.Sales.Data.CrediLineData.GetCreditLineId(this.Customer.Id);
      return CreditTransaction.GetCreditTransactions(creditLineId);
    }

    public void GetOrderTotal() {
      this.SalesOrderItems = SalesOrderItem.GetOrderItems(this.Id);
      SetOrderTotals();
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

  

    private string GetPriceList() {
      var pricesList = CustomerPrices.GetVendorPrices(this.Customer.Id);

      var vendorPrice = pricesList.Find(r => r.VendorId == this.Supplier.Id);

      return vendorPrice.PriceListId.ToString();
    }

   

    static public FixedList<CreditTransaction> GetCreditTransactions(int customerId) {
      var creditLineId = Empiria.Trade.Sales.Data.CrediLineData.GetCreditLineId(customerId);
      return CreditTransaction.GetCreditTransactions(creditLineId);
    }

    internal void SetAuthorizedActions(string queryType) {
      switch (this.Status) {
        case OrderStatus.Captured:
          this.Actions = OrderActions.GetCaptureActions();
          break;
        case OrderStatus.Applied: {
          this.Actions = OrderActions.GetApplyActions();
          if (queryType == "SalesOrdersAuthorization") {
            this.Actions.CanAuthorize = true;
          } else {
            this.Actions.CanAuthorize = false;
          }

        }
        break;
        case OrderStatus.Authorized:
          this.Actions = OrderActions.GetAuthorizedActions();
          break;
        case OrderStatus.Packing:
          this.Actions = OrderActions.GetPackingActions();
          break;
        case OrderStatus.Cancelled:
          this.Actions = OrderActions.GetCancellActions();
          break;
        case OrderStatus.CarrierSelector:
          this.Actions = OrderActions.GetSelectCarrierActions();
          break;
      }

    }



    #endregion Helpers

  }  //  class SalesOrder

}  // namespace Empiria.Trade.Sales
