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

using Empiria.Financial;
using Empiria.Orders;
using Empiria.Parties;


namespace Empiria.Trade.Core {

  /// <summary>Represents a sales order.</summary>
  public class SalesOrder : Order {

    #region Constructors and parsers

    public SalesOrder() {
      //no-op
    }

    public SalesOrder(SalesOrderFields fields, OrderType orderType) : base(orderType) {
      Assertion.Require(fields, nameof(fields));

      if (IsNew) {
        OrderNo = "P-" + EmpiriaString.BuildRandomString(10).ToUpperInvariant();
      }

      Update(fields);
    }

    public SalesOrder(SalesOrderFields fields) {
      Update(fields);
    }

    static public new SalesOrder Parse(int id) => ParseId<SalesOrder>(id);

    static public new SalesOrder Parse(string uid) => ParseKey<SalesOrder>(uid);

    static public new SalesOrder Empty => ParseEmpty<SalesOrder>();

    public override FixedList<IPayableEntity> GetPayableEntities() {

      return new FixedList<IPayableEntity>();
    }

    #endregion Constructors and parsers

    #region Public properties

    public Party Customer {
      get; protected set;
    }


    public CustomerContact CustomerContact {
      get; protected set;
    }


    public CustomerAddress CustomerAddress {
      get; protected set;
    }


    public Party Supplier {
      get; protected set;
    }


    public Party SalesAgent {
      get; protected set;
    }


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


    public decimal Tax {
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


    public string ShippingMethod {
      get {
        return ConditionsData.Get("shippingMethod", string.Empty);
      }
      private set {
        ConditionsData.SetIfValue("shippingMethod", value);
      }
    }


    public DateTime ScheduledTime {
      get {
        return ConditionsData.Get("scheduledTime", DateTime.MaxValue);
      }
      private set {
        ConditionsData.SetIfValue("scheduledTime", value);
      }
    }

    #endregion

    #region Public methods

    protected override void OnSave() {

      SalesOrderData.Write(this);
      SalesOrderItem.SaveSalesOrderItems(this.SalesOrderItems, this.Id);
    }

    public void Apply() {
      //Status = OrderStatus.Applied;
      //AuthorizationStatus = OrderAuthorizationStatus.Pending;

      SalesOrderData.Write(this);

      SetOrderValues();

      var actions = ActionsService.Load();
      actions.OnApply();
      this.Actions = actions.SetActions(this, QueryType.Sales);

      // TODO VERIFICAR ACCIONES

      this.Activate();
      this.Save();
    }


    public void Authorize() {
      //AuthorizationStatus = OrderAuthorizationStatus.Authorized;
      //this.AuthorizationTime = DateTime.Now;
      //this.AuthorizatedById = ExecutionServer.CurrentUserId;

      //this.Status = OrderStatus.Packing;
      ////AuthorizationStatus = OrderAuthorizationStatus.ToSupply;


      SalesOrderData.Write(this);

      SetOrderValues();

      var actions = ActionsService.Load();
      actions.OnAuthorize();
      this.Actions = actions.SetActions(this, QueryType.SalesAuthorization);
    }

    public void Deauthorize() {
      //Status = OrderStatus.Applied;
      //AuthorizationStatus = OrderAuthorizationStatus.Pending;

      SalesOrderData.Write(this);

      SetOrderValues();

      var actions = ActionsService.Load();
      actions.OnApply();
      this.Actions = actions.SetActions(this, QueryType.SalesAuthorization);
    }

    public void AuthorizePayment() {
      //this.Status = OrderStatus.Packing;
      //AuthorizationStatus = OrderAuthorizationStatus.ToSupply;

      SalesOrderData.Write(this);

      SetOrderValues();

      var actions = ActionsService.Load();
      actions.OnAuthorize();
      this.Actions = actions.SetActions(this, QueryType.SalesAuthorization);
    }



    public void Cancel() {
      //Status = OrderStatus.Cancelled;

      SalesOrderData.Write(this);
      SalesOrderItemsData.CancelOrderItems(this.Id);
      this.SalesOrderItems = SalesOrderItem.GetOrderItems(this.Id);

      SetOrderTotals();

      var actions = ActionsService.Load();
      this.Actions = actions.SetActions(this, QueryType.Sales);
    }

    public void Close() {
      //this.Status = OrderStatus.Closed;

      //AuthorizationStatus = OrderAuthorizationStatus.Empty;

      SalesOrderData.Write(this);
      SetOrderValues();
    }

    public void Deliver() {
      //this.Status = OrderStatus.Delivery;

      //AuthorizationStatus = OrderAuthorizationStatus.Suppled;

      SalesOrderData.Write(this);
      SetOrderValues();
    }


    public void Supply() {
      //this.Status = OrderStatus.Shipping;

      //AuthorizationStatus = OrderAuthorizationStatus.Suppled;

      SalesOrderData.Write(this);
      SetOrderValues();

      var actions = ActionsService.Load();
      actions.OnSuppy();
      this.Actions = actions.SetActions(this, QueryType.SalesPacking);
    }

    
    public void Update(SalesOrderFields fields) {

      //this.Customer = fields.GetCustomer();
      this.Supplier = fields.GetSupplier();
      this.SalesAgent = fields.GetSalesAgent();
      this.CustomerAddress = fields.GetCustomerAddress();
      this.CustomerContact = fields.GetCustomerContact();
      //this.ShippingMethod = fields.ShippingMethod;
      this.PriceList = GetPriceList();
      this.SalesOrderItems = LoadSalesOrderItems(fields.Items);
      this.ScheduledTime = ExecutionServer.DateMaxValue;
      //this.ReceptionTime = ExecutionServer.DateMaxValue;
      //this.PedimentoImportacion = string.Empty;
      //this.CartaPorte = string.Empty;

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
      this.Tax = 0;
      this.ItemsCount = this.SalesOrderItems.Count;

      foreach (SalesOrderItem item in this.SalesOrderItems) {
        this.ItemsTotal += item.Subtotal_;
        this.Shipment += item.Shipment;
        this.Discount += item.Discount;
        this.Tax += item.TaxesIVA;
        this.OrderTotal += item.Subtotal;
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
