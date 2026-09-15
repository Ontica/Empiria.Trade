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
using Empiria.Trade.Products;


namespace Empiria.Trade.Core {

  /// <summary>Represents a sales order.</summary>
  public class SalesOrder : Order {

    #region Constructors and parsers

    public SalesOrder() {
      //no-op
    }

    protected SalesOrder(OrderType orderType) : base(orderType) {
      // Required by Empiria Framework for all partitioned types.
    }

    public SalesOrder(SalesOrderFields fields, OrderType orderType) : base(orderType) {
      Assertion.Require(fields, nameof(fields));
      
      if (IsNew) {
        OrderNo = "P-" + EmpiriaString.BuildRandomString(10).ToUpperInvariant();
        fields.OrderNumber = OrderNo;
        fields.Name = OrderNo;
        this.AuthorizationStatus = "Pending";
      }

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

    [DataField("ORDER_ID")]
    public int OrderId {
      get; protected set;
    }


    [DataField("Order_UID")]
    public string OrderUID {
      get; protected set;
    }


    public Party Customer {
      get; set;
    }


    public CustomerContact CustomerContact {
      get; protected set;
    }


    public CustomerAddress CustomerAddress {
      get; protected set;
    }


    public Party Supplier {
      get; set;
    }


    public Party SalesAgent {
      get; set;
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


    public TransactionActions Actions {
      get; private set;
    } = new TransactionActions();


    public int SalesAgentId {
      get {
        return ExtData.Get("salesAgentId", -1);
      }
      private set {
        ExtData.SetIfValue("salesAgentId", value);
      }
    }


    public int CustomerContactId {
      get {
        return ExtData.Get("customerContactId", -1);
      }
      private set {
        ExtData.SetIfValue("customerContactId", value);
      }
    }


    public int CustomerAddressId {
      get {
        return ExtData.Get("customerAddressId", -1);
      }
      private set {
        ExtData.SetIfValue("customerAddressId", value);
      }
    }


    public DateTime ReceptionTime {
      get {
        return ConditionsData.Get("receptionTime", DateTime.MaxValue);
      }
      private set {
        ConditionsData.SetIfValue("receptionTime", value);
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


    public string PedimentoImportacion {
      get {
        return ConditionsData.Get("pedimentoImportacion", string.Empty);
      }
      private set {
        ConditionsData.SetIfValue("pedimentoImportacion", value);
      }
    }


    public string CartaPorte {
      get {
        return ConditionsData.Get("cartaPorte", string.Empty);
      }
      private set {
        ConditionsData.SetIfValue("cartaPorte", value);
      }
    }


    public string ShippingMethod {
      get {
        return ConditionsData.Get("shippingMethod", string.Empty);
      }
      private set {
        ConditionsData.SetIfValue("shippingMethod", value);
      }
    }


    public string OrderStatus {
      get {
        return ExtData.Get("orderStatus", string.Empty);
      }
      private set {
        ExtData.SetIfValue("orderStatus", value);
      }
    }


    public string AuthorizationStatus {
      get {
        return ExtData.Get("authorizationStatus", string.Empty);
      }
      private set {
        ExtData.SetIfValue("authorizationStatus", value);
      }
    }

    #endregion

    #region Public methods

    //protected override void OnSave() {
    //  base.Save();
    //  SalesOrderItem.SaveSalesOrderItems(this.SalesOrderItems, this.Id);
    //}

    public void AddSalesOrderItem(SalesOrderItem item, int orderId) {
      Assertion.Require(item, nameof(item));

      item.Order = Order.Parse(orderId);
      item.SalesOrder = SalesOrder.Parse(orderId);
      item.UpdateItem();
      item.Save();
    }


    public void Apply() {
      this.OrderStatus = Core.OrderStatus.Applied.ToString();
      this.AuthorizationStatus = Core.OrderStatus.Authorized.ToString();

      //this.Activate();

      //SalesOrderData.Write(this);

      SetOrderValues();

      var actions = ActionsService.Load();
      actions.OnApply();
      this.Actions = actions.SetActions(this, QueryType.Sales);

      this.Activate();
      this.Save();
    }


    public void AuthorizeOrder() {
      this.AuthorizationStatus = Core.OrderStatus.Authorized.ToString();
      
      //TODO INVESTIGAR SI SE DEBE APLICAR
      this.OrderStatus = Core.OrderStatus.Applied.ToString();

      this.Authorization();

      //SalesOrderData.Write(this);

      SetOrderValues();

      var actions = ActionsService.Load();
      actions.OnAuthorize();
      this.Actions = actions.SetActions(this, QueryType.SalesAuthorization);

      this.Save();
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
      this.OrderStatus = Core.OrderStatus.Packing.ToString();
      this.AuthorizationStatus = Core.OrderStatus.ToSupply.ToString();

      //SalesOrderData.Write(this);

      SetOrderValues();

      var actions = ActionsService.Load();
      actions.OnAuthorize();
      this.Actions = actions.SetActions(this, QueryType.SalesAuthorization);

      this.Save();
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


    public void GetSalesOrderItems() {

      this.SalesOrderItems = SalesOrderItem.GetOrderItems(this.Id);
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
      
      this.Supplier = fields.GetSupplier();
      
      this.SalesAgent = fields.GetSalesAgent();
      this.SalesAgentId = fields.GetSalesAgent().Id;

      this.Customer = fields.GetCustomer();
      this.CustomerAddress = fields.GetCustomerAddress();
      this.CustomerAddressId = fields.GetCustomerAddress().Id;

      this.CustomerContact = fields.GetCustomerContact();
      this.CustomerContactId = fields.GetCustomerContact().Id;

      this.ScheduledTime = ExecutionServer.DateMaxValue;
      this.PaymentConditions = fields.PaymentConditions;
      //TODO GUARDAR EN EXT_DATA
      this.ShippingMethod = fields.ShippingMethod.ToString();
      this.ReceptionTime = ExecutionServer.DateMaxValue;
      this.PedimentoImportacion = string.Empty;
      this.CartaPorte = string.Empty;
      this.OrderStatus = fields.Status.ToString();

      this.SalesOrderItems = LoadSalesOrderItems(fields.Items, Customer);

      SetOrderTotals();

      var actions = ActionsService.Load();
      actions.OnCreate();
      this.Actions = actions.SetActions(this, QueryType.Sales);

      if (fields.CanUpdateOrder) {
        base.Update(fields);
      }
    }


    public void CalculateSalesOrder() {
      this.SetOrderValues();
    }

    public void SetOrderActions(QueryType queryType) {
      var actions = ActionsService.Load();

      this.Actions = actions.SetActions(this, queryType);
    }

    public void GetItemsAndOrderTotal() {
      this.SalesOrderItems = SalesOrderItem.GetOrderItems(this.OrderId);
      SetOrderTotals();
    }

    #endregion Public methods

    #region Helpers

    private void SetOrderValues() {
      GetItemsAndOrderTotal();
    }


    private FixedList<SalesOrderItem> LoadSalesOrderItems(FixedList<SalesOrderItemsFields> itemsFields, Party customer) {
      
      List<SalesOrderItem> orderItems = new List<SalesOrderItem>();

      foreach (SalesOrderItemsFields itemFields in itemsFields) {

        if (itemFields.OrderItemUID != string.Empty) {

          itemFields.UID = itemFields.OrderItemUID;

          var saleOrderItem = SalesOrderItem.Parse(itemFields.OrderItemUID);

          saleOrderItem.ProductUID = itemFields.VendorProductUID;

          saleOrderItem.Update(itemFields, customer);

          orderItems.Add(saleOrderItem);

        } else {

          var saleOrderItem = new SalesOrderItem(this, itemFields);
          orderItems.Add(saleOrderItem);
        }
      }

      return orderItems.ToFixedList();
    }


    private void SetOrderTotals() {

      this.OrderTotal = 0;
      this.ItemsTotal = 0;
      this.Tax = 0;
      this.ItemsCount = this.SalesOrderItems.Count;

      foreach (SalesOrderItem item in this.SalesOrderItems) {
        
        this.ItemsTotal += item.ItemSubtotal;
        this.Shipment += item.Shipment;
        this.Discount += item.Discount;
        this.Tax += item.TaxesIVA;
      }
      this.OrderTotal += this.ItemsTotal + this.Tax + this.Shipment - this.Discount;
    }


    public CustomerAddress GetCustomerAddress() {

      this.CustomerAddress = CustomerAddress.Parse(this.CustomerAddressId);
      return this.CustomerAddress;
    }


    public CustomerContact GetCustomerContact() {

      this.CustomerContact = CustomerContact.Parse(this.CustomerContactId);
      return this.CustomerContact;
    }

    #endregion Helpers

  }  //  class SalesOrder

}  // namespace Empiria.Trade.Sales
