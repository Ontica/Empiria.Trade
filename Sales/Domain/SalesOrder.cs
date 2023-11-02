﻿/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Order Management                           Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Order.dll                    Pattern   : Partitioned Type / Information Holder   *
*  Type     : Order                                      License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a Order.                                                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using Empiria.Trade.Orders;
using Empiria.Trade.Sales.Adapters;
using Empiria.Trade.Sales.Data;

using Newtonsoft.Json;
using Empiria.Trade.Core;

namespace Empiria.Trade.Sales {

  /// <summary>Represent Order</summary>
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
       

    #endregion


    #region Public methods

    protected override void OnSave() {
      if (IsNew) {
        OrderNumber = "P-" + EmpiriaString.BuildRandomString(10);
        OrderTime = DateTime.Now;
        Status = OrderStatus.Captured;
      }
      SalesOrderData.Write(this);
      SalesOrderItem.SaveSalesOrderItems(this.SalesOrderItems, this.Id);
      
    }

    public void Apply() {
      Status = OrderStatus.Applied;

      SalesOrderData.Write(this);
      this.SalesOrderItems = SalesOrderItem.GetOrderItems(this.Id);

      GetOrderTotals();
    }



    public static FixedList<SalesOrder> GetOrders(SearchOrderFields fields) {
      var orders = SalesOrderData.GetSalesOrders(fields);
      List<SalesOrder> salesOrders = new List<SalesOrder>();

      foreach (var order in orders) {
        order.SalesOrderItems = SalesOrderItem.GetOrderItems(order.Id);
        GetOrderTotals(order);
        salesOrders.Add(order);
      }

      return salesOrders.ToFixedList<SalesOrder>();
    }

    public FixedList<VendorPrices> GetCustomerPriceList() {
     var pricesList = CustomerPrices.GetVendorPrices(this.Customer.Id);

      return pricesList;
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
      this.SalesOrderItems =  LoadSalesOrderItems(fields.Items);
      GetOrderTotals();

    }

    public void Cancel() {

      Status = OrderStatus.Cancelled;
      
      SalesOrderData.Write(this);
      SalesOrderItemsData.CancelOrderItems(this.Id);
      this.SalesOrderItems = SalesOrderItem.GetOrderItems(this.Id);

      GetOrderTotals();
    }

    public void Modify(SalesOrderFields fields) {      
      SalesOrderItemsData.CancelOrderItems(this.Id);
      Update(fields);

      Save();
    }

    #endregion Public methods

    #region Private methods

    private FixedList<SalesOrderItem> LoadSalesOrderItems(FixedList<SalesOrderItemsFields> orderItemsFields) {
      List<SalesOrderItem> orderItems = new List<SalesOrderItem>();

     var priceLists = GetCustomerPriceList();
      
      foreach (SalesOrderItemsFields itemFields in orderItemsFields) {
        var saleOrderItem = new SalesOrderItem(itemFields, priceLists);
        orderItems.Add(saleOrderItem);
      }
     
      return orderItems.ToFixedList<SalesOrderItem>();
    }

    private void GetOrderTotals() {

      InitializeValues();

      foreach (SalesOrderItem item in this.SalesOrderItems) {
        this.ItemsCount++;
        this.ItemsTotal += item.SalesPrice;
        this.Shipment += item.Shipment;
        this.Discount += item.Discount;
        this.Taxes += item.TaxesIVA;
        this.OrderTotal += item.Total;
      }

    }

    private static void GetOrderTotals(SalesOrder  order) {

      foreach (SalesOrderItem item in order.SalesOrderItems) {
        order.ItemsCount++;
        order.ItemsTotal += item.SalesPrice;
        order.Shipment += item.Shipment;
        order.Discount += item.Discount;
        order.Taxes += item.TaxesIVA;
        order.OrderTotal += item.Total;
      }

    }

    private  void InitializeValues() {
      this.ItemsCount = 0;
      this.ItemsTotal = 0;
      this.Shipment = 0;
      this.Discount = 0;
      this.Taxes = 0;
      this.OrderTotal = 0;
    }

    #endregion

  }  //  class SalesOrder



  }  // namespace Empiria.Trade.Sales
