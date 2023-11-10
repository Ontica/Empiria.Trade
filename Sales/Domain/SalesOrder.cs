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
using System.Linq;
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

    static public FixedList<SalesOrder> GetOrders(SearchOrderFields fields) {
      var orders = SalesOrderData.GetSalesOrders(fields);
      List<SalesOrder> salesOrders = new List<SalesOrder>();

      foreach (var order in orders) {
        order.SalesOrderItems = SalesOrderItem.GetOrderItems(order.Id);
        SetOrderTotals(order);
        salesOrders.Add(order);
      }

      return salesOrders.ToFixedList<SalesOrder>();
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
        OrderNumber = "P-" + EmpiriaString.BuildRandomString(10).ToUpperInvariant();
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
      this.SalesOrderItems = LoadSalesOrderItems(fields.Items);

      SetOrderTotals();
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

      foreach (SalesOrderItem item in this.SalesOrderItems) {
        this.ItemsCount++;
        this.ItemsTotal += item.SalesPrice;
        this.Shipment += item.Shipment;
        this.Discount += item.Discount;
        this.Taxes += item.TaxesIVA;
        this.OrderTotal += item.Total;
      }

    }

    private static void SetOrderTotals(SalesOrder order) {

      foreach (SalesOrderItem item in order.SalesOrderItems) {
        order.ItemsCount++;
        order.ItemsTotal += item.SalesPrice;
        order.Shipment += item.Shipment;
        order.Discount += item.Discount;
        order.Taxes += item.TaxesIVA;
        order.OrderTotal += item.Total;
      }

    }

    internal static FixedList<string> GetStatusList() {
      var orderStatusList = Enum.GetNames(typeof(OrderStatus)).ToList();

      return orderStatusList.ToFixedList();
    }

    public void Authorize() {
     AuthorizationStatus = AutorizationStatus.Authorized;
     this.AuthorizationTime = DateTime.Now;
     this.AuthorizatedById = 2;

     SalesOrderData.Write(this);
     this.SalesOrderItems = SalesOrderItem.GetOrderItems(this.Id);

     SetOrderTotals();
    }

    #endregion Helpers

  }  //  class SalesOrder

}  // namespace Empiria.Trade.Sales
