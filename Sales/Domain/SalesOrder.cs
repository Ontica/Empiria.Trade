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
using System.Text.RegularExpressions;
using System.Threading;
using Empiria.Trade.Core;
using Empiria.Trade.Orders;
using Empiria.Trade.Sales.Adapters;
using Empiria.Trade.Sales.Data;
using Newtonsoft.Json.Linq;

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
      this.Actions = GetApplyActions();
      
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

      this.Actions = GetCaptureActions();
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
      this.Actions = GetAuthorizedActions();

      SalesOrderData.Write(this);
      this.SalesOrderItems = SalesOrderItem.GetOrderItems(this.Id);

      SetOrderTotals();
    }

    static public FixedList<SalesOrder> GetOrders(SearchOrderFields fields) {          
      var orders = SalesOrderData.GetSalesOrders(fields);

      return GetOrderItems(orders);
    }

    static public FixedList<SalesOrder> GetOrdersToAuthorize(SearchOrderFields fields) {
      var orders = SalesOrderData.GetSalesOrdersToAuthorize(fields);

      return GetOrderItems(orders);
    }

    internal static FixedList<SalesOrder> GetOrdersToPacking(SearchOrderFields fields) {
      var orders = SalesOrderData.GetSalesOrdersToPacking(fields);

      return GetOrderItems(orders);
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
        this.ItemsTotal += item.SubTotal;
        this.Shipment += item.Shipment;
        this.Discount += item.Discount;
        this.Taxes += item.TaxesIVA;
        this.OrderTotal += item.Total;
      }

    }

    private static void SetOrderTotals(SalesOrder order) {

      order.OrderTotal = 0;

      foreach (SalesOrderItem item in order.SalesOrderItems) {
        order.ItemsCount++;
        order.ItemsTotal += item.SubTotal;
        order.Shipment += item.Shipment;
        order.Discount += item.Discount;
        order.Taxes += item.TaxesIVA;
        order.OrderTotal += item.Total;
      }

    }

    internal static FixedList<NamedEntityDto> GetStatusList() {

     var captured = new NamedEntityDto("Captured", "Capturada");
     var applied = new NamedEntityDto("Applied", "Aplicada");
     var authorized = new NamedEntityDto("Authorized", "Autorizada");
     var packing = new NamedEntityDto("Packing", "Surtiendose");
     var carrierSelector = new NamedEntityDto("CarrierSelector", "Seleccion de paqueteria");
     var shipping = new NamedEntityDto("Shipping", "Envio");
     var delivery = new NamedEntityDto("Delivery", "Entrega");
     var closed = new NamedEntityDto("Closed", "Cerrada");
     var cancelled = new NamedEntityDto("Cancelled", "Cancelada");

      List<NamedEntityDto> orderSalesStatus = new List<NamedEntityDto>();
      orderSalesStatus.Add(captured);
      orderSalesStatus.Add(applied);
      orderSalesStatus.Add(authorized);
      orderSalesStatus.Add(packing);
      orderSalesStatus.Add(carrierSelector);
      orderSalesStatus.Add(shipping);
      orderSalesStatus.Add(delivery);
      orderSalesStatus.Add(closed);
      orderSalesStatus.Add(cancelled);

      return orderSalesStatus.ToFixedList<NamedEntityDto>();
    }


    static private FixedList<SalesOrder> GetOrderItems(FixedList<SalesOrder> orders) {
      List<SalesOrder> salesOrders = new List<SalesOrder>();

      foreach (var order in orders) {
        order.SalesOrderItems = SalesOrderItem.GetOrderItems(order.Id);
        SetOrderTotals(order);
        salesOrders.Add(order);
        SetAuthorizedActions(order);
      }

      return salesOrders.ToFixedList<SalesOrder>();
    }

    

    internal static FixedList<NamedEntityDto> GetAuthorizationStatusList() {
      var authorized = new NamedEntityDto("authorized", "Autorizado");
      var pending = new NamedEntityDto("pending", "Por Autorizar");   

      List<NamedEntityDto> orderSalesStatus = new List<NamedEntityDto>();

      orderSalesStatus.Add(authorized);
      orderSalesStatus.Add(pending);
      

      return orderSalesStatus.ToFixedList<NamedEntityDto>();
    }

    internal static FixedList<NamedEntityDto> GetPackingStatusList() {
      var toSupply = new NamedEntityDto("ToSupply", "Por surtir");
      var inprogress = new NamedEntityDto("InProgress", "En proceso");
      var supplied = new NamedEntityDto("Suppled", "Surtido");
      List<NamedEntityDto> orderPackcingStatusList = new List<NamedEntityDto>();

      orderPackcingStatusList.Add(toSupply);
      orderPackcingStatusList.Add(inprogress);
      orderPackcingStatusList.Add(supplied);

      return orderPackcingStatusList.ToFixedList<NamedEntityDto>();
    }

    private static OrderActions GetCaptureActions() {
      OrderActions actions = new OrderActions();
      actions.CanApply = true;
      actions.CanAuthorize = false;
      actions.CanEdit = true;
      actions.CanSelectCarrier = false;
      actions.TransportPackaging = false;
      actions.CanShipping = false;
      actions.CanClose = false;

      return actions;
    }

    private static OrderActions GetApplyActions() {
      OrderActions actions = new OrderActions();

      actions.CanApply = false;
      actions.CanAuthorize = true;
      actions.CanEdit = false;
      actions.CanSelectCarrier = false;
      actions.TransportPackaging = false;
      actions.CanShipping = false;
      actions.CanClose = false;

      return actions;
    }

    private static  OrderActions GetAuthorizedActions() {
      OrderActions actions = new OrderActions();

      actions.CanApply = false;
      actions.CanAuthorize = false;
      actions.CanEdit = false;
      actions.TransportPackaging = true;
      actions.CanSelectCarrier = false;
      actions.CanShipping = false;
      actions.CanClose = false;

      return actions;
    }

    private static OrderActions GetPackingActions() {
      OrderActions actions = new OrderActions();

      actions.CanApply = false;
      actions.CanAuthorize = false;
      actions.CanEdit = false;
      actions.TransportPackaging = false;
      actions.CanSelectCarrier = true;
      actions.CanShipping = false;
      actions.CanClose = false;

      return actions;
    }

    private static OrderActions GetCancellActions() {
      OrderActions actions = new OrderActions();
      return actions;
    }

      private static void SetAuthorizedActions(SalesOrder order) {
      switch (order.Status) {
        case OrderStatus.Captured: order.Actions = GetCaptureActions(); break;
        case OrderStatus.Applied:  order.Actions = GetApplyActions(); break;
        case OrderStatus.Authorized: order.Actions = GetAuthorizedActions(); break;
        case OrderStatus.Packing: order.Actions = GetPackingActions(); break;
        case OrderStatus.Cancelled: order.Actions = GetCancellActions(); break;
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
