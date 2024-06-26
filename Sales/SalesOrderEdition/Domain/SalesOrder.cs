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

using Empiria.Trade.Core;
using Empiria.Trade.Orders;
using Empiria.Trade.Sales.Adapters;
using Empiria.Trade.Sales.Data;


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
   
    public TransactionActions Actions {
      get; private set;
    } = new TransactionActions();

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
                   
      SetOrderValues();

      var actions = ActionsService.Load();
      actions.OnApply();
      this.Actions = actions.SetActions(this, QueryType.Sales);
    }


    public void Authorize() {
      AuthorizationStatus = OrderAuthorizationStatus.Authorized;
      this.AuthorizationTime = DateTime.Now;
      this.AuthorizatedById = ExecutionServer.CurrentUserId;

      this.Status = OrderStatus.Packing;
      //AuthorizationStatus = OrderAuthorizationStatus.ToSupply;
           

      SalesOrderData.Write(this);

      SetOrderValues();

      var actions = ActionsService.Load();
      actions.OnAuthorize();
      this.Actions = actions.SetActions(this, QueryType.SalesAuthorization);
    }

    public void Deauthorize() {
      Status = OrderStatus.Applied;
      AuthorizationStatus = OrderAuthorizationStatus.Pending;

      SalesOrderData.Write(this);

      SetOrderValues();

      var actions = ActionsService.Load();
      actions.OnApply();
      this.Actions = actions.SetActions(this, QueryType.SalesAuthorization);
    }

    public void AuthorizePayment() {
      this.Status = OrderStatus.Packing;
      AuthorizationStatus = OrderAuthorizationStatus.ToSupply;

      SalesOrderData.Write(this);

      SetOrderValues();

      var actions = ActionsService.Load();
      actions.OnAuthorize();
      this.Actions = actions.SetActions(this, QueryType.SalesAuthorization);
    }



    public void Cancel() {
      Status = OrderStatus.Cancelled;

      SalesOrderData.Write(this);
      SalesOrderItemsData.CancelOrderItems(this.Id);
      this.SalesOrderItems = SalesOrderItem.GetOrderItems(this.Id);

      SetOrderTotals();

      var actions = ActionsService.Load();
      this.Actions = actions.SetActions(this, QueryType.Sales);
    }

    public void Close() {
      this.Status = OrderStatus.Closed;

      AuthorizationStatus = OrderAuthorizationStatus.Empty;

      SalesOrderData.Write(this);
      SetOrderValues();
    }

    public void Deliver() {
      this.Status = OrderStatus.Delivery;

      AuthorizationStatus = OrderAuthorizationStatus.Suppled;

      SalesOrderData.Write(this);
      SetOrderValues();
    }
       

    public void Supply() {
      this.Status = OrderStatus.Shipping;

      AuthorizationStatus = OrderAuthorizationStatus.Suppled;

      SalesOrderData.Write(this);
      SetOrderValues();

      var actions = ActionsService.Load();
      actions.OnSuppy();
      this.Actions = actions.SetActions(this, QueryType.SalesPacking);
    }

    internal void Update(SalesOrderFields fields) {

      this.OrderTypeId = 1025;
      this.OrderTime = fields.OrderTime;
      this.Customer = fields.GetCustomer();
      this.Supplier = fields.GetSupplier();
      this.SalesAgent = fields.GetSalesAgent();
      this.CustomerAddress = fields.GetCustomerAddress();
      this.CustomerContact = fields.GetCustomerContact();
      this.Notes = fields.Notes;
      this.Status = fields.Status;
      this.ShippingMethod = fields.ShippingMethod;
      this.PaymentCondition = fields.PaymentCondition;
      this.PriceList = GetPriceList();
      this.SalesOrderItems = LoadSalesOrderItems(fields.Items);
      this.ScheduledTime = ExecutionServer.DateMaxValue;
      this.ReceptionTime = ExecutionServer.DateMaxValue;
      this.PedimentoImportacion = string.Empty;
      this.CartaPorte = string.Empty;


      SetOrderTotals();

      var actions = ActionsService.Load();
      actions.OnCreate();
      this.Actions = actions.SetActions(this, QueryType.Sales);
    }
      

    public void CalculateSalesOrder() {
      this.SetOrderValues();
    }

    public void SetOrderActions(QueryType queryType) {
      var actions = ActionsService.Load();

      this.Actions = actions.SetActions(this, queryType);
    }
       
    public void GetOrderTotal() {
      this.SalesOrderItems = SalesOrderItem.GetOrderItems(this.Id);
      SetOrderTotals();
    }

    #endregion Public methods

    #region Helpers

    private void SetOrderValues() {
      GetOrderTotal();
    }

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
      this.ItemsCount = this.SalesOrderItems.Count;

      foreach (SalesOrderItem item in this.SalesOrderItems) {
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
    

    #endregion Helpers

  }  //  class SalesOrder

}  // namespace Empiria.Trade.Sales
