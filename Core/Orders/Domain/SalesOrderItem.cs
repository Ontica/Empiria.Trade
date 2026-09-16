/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Order Management                           Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Order.dll                    Pattern   : Partitioned Type / Information Holder   *
*  Type     : Order Item                                 License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a Order item.                                                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Linq;
using Empiria.Orders;
using Empiria.Products;
using Empiria.Trade.Products;

namespace Empiria.Trade.Core {

  /// <summary>Represents a Order item. </summary>
  public class SalesOrderItem : OrderItem {

    #region Constructors and parsers

    protected SalesOrderItem() {
      //no-op
    }

    protected SalesOrderItem(OrderItemType orderType) : base(orderType) {
      // Required by Empiria Framework for all partitioned types.
    }

    static public new SalesOrderItem Parse(int id) => ParseId<SalesOrderItem>(id);

    static public new SalesOrderItem Parse(string uid) => ParseKey<SalesOrderItem>(uid);

    static public SalesOrderItem Empty => ParseEmpty<SalesOrderItem>();

    public SalesOrderItem(SalesOrder salesOrder, SalesOrderItemsFields fields) {

      this.ProductUID = fields.VendorProductUID;
      this.SalesOrder = salesOrder;

      Update(fields, salesOrder.Beneficiary);
    }

    #endregion

    #region Public properties

    [DataField("ORDER_ITEM_ID")]
    public int OrderItemId {
      get; protected set;
    }


    [DataField("ORDER_ITEM_UID")]
    public string OrderItemUID {
      get; protected set;
    }


    public SalesOrder SalesOrder {
      get; set;
    }


    public string ProductUID {
      get; set;
    } = string.Empty;


    public ProductEntry ProductEntry {
      get {
        return ProductEntry.ParseUID(this.ProductUID == string.Empty ? this.Product.UID : this.ProductUID);
      }
    }


    public int PriceListNumber {
      get; protected set;
    } = 3;


    public decimal ProductPrice {
      get; private set;
    }


    public decimal ItemQuantity {
      get; private set;
    }


    public decimal BasePrice {
      get; private set;
    }


    public decimal ItemUnitPrice {
      get; private set;
    }


    public decimal SalesPrice {
      get; private set;
    }


    public decimal ItemDiscount {
      get; private set;
    }


    public decimal ItemSubtotal {
      get; set;
    }


    public decimal Shipment {
      get; protected set;
    }


    public decimal TaxesIVA {
      get; protected set;
    }


    public decimal ItemTotal {
      get; private set;
    }


    public string DiscountPolicy {
      get; set;
    } = string.Empty;


    //public decimal AdditionalDiscount {
    //  get; protected set;
    //}


    public decimal AdditionalDiscount {
      get {
        return ExtData.Get<decimal>("additionalDiscount", 0);
      }
      private set {
        ExtData.SetIfValue("additionalDiscount", value);
      }
    }


    public string Notes {
      get {
        return ExtData.Get("notes", string.Empty);
      }
      private set {
        ExtData.SetIfValue("notes", value);
      }
    }


    public SalesOrderItemsFields ItemFields {
      get; set;
    } = new SalesOrderItemsFields();

    #endregion Public properties

    #region Public methods

    internal void UpdateItem() {

      var product = ProductEntry.Parse(ItemFields.VendorProductUID);

      ItemFields.ProductUID = product.UID;
      ItemFields.ProductUnitUID = product.BaseUnit.UID;
      ItemFields.Discount = ItemFields.Discount1;
      //ItemFields.LocationUID = TODO OBTENER LOCALIZACION DE PRODUCTO ASIGNADO A ORDERITEM
      base.Update(ItemFields);
    }


    internal void Update(SalesOrderItemsFields fields, Parties.Party customer) {

      var productPrice = ProductEntry.GetProductPrice(customer.ExtendedData.Get<int>("ListaPrecios", 0));
      decimal unitPrice = productPrice.Price;

      if (fields.UnitPrice > productPrice.Price) {
        unitPrice = fields.UnitPrice;
      }

      //this.OrderItemTypeId = 1045;
      this.OrderItemUID = fields.OrderItemUID;
      this.Notes = String.IsNullOrEmpty(fields.Notes) ? string.Empty : fields.Notes;
      //this.PriceListNumber = GetPriceListNumber(prices);
      this.ProductPrice = productPrice.Price;
      this.ItemQuantity = fields.Quantity;
      this.BasePrice = unitPrice;
      this.ItemUnitPrice = unitPrice;
      this.SalesPrice = GetSalesPrice();
      //this.DiscountPolicy = GetDiscount().ToString();
      this.ItemDiscount = fields.Discount1; //GetDiscount();
      this.AdditionalDiscount = fields.Discount2;
      this.ItemSubtotal = GetSubtotal(fields.Discount1);
      this.Shipment = 0;
      this.TaxesIVA = this.ItemSubtotal * 0.16M;
      this.ItemTotal = this.ItemSubtotal + this.TaxesIVA + this.Shipment;
      //this.ScheduledTime = ExecutionServer.DateMaxValue;
      //this.ReceptionTime = ExecutionServer.DateMaxValue;
      //this.Reviewed = string.Empty;
      this.ItemFields = fields;
    }


    public static void SaveSalesOrderItems(FixedList<SalesOrderItem> orderItems, int orderId) {

      foreach (SalesOrderItem orderItem in orderItems) {
        orderItem.SalesOrder = SalesOrder.Parse(orderId);
        orderItem.Save();
      }

    }


    //protected override void OnSave() {
    //  SalesOrderItemsData.Write(this);
    //}


    public static FixedList<SalesOrderItem> GetOrderItems(int orderId) {

      var orderItems = SalesOrderItemsData.GetOrderItems(orderId);

      foreach (SalesOrderItem orderItem in orderItems) {
        orderItem.ItemQuantity = orderItem.Quantity;
        orderItem.ItemUnitPrice = orderItem.UnitPrice;
        //orderItem.ProductPrice = orderItem.UnitPrice;
        orderItem.SalesPrice = orderItem.UnitPrice;
        orderItem.ItemDiscount = orderItem.Discount;

        orderItem.DiscountPolicy = string.Empty;
        orderItem.ItemSubtotal = CalculeSubtotal(orderItem);

        orderItem.TaxesIVA = orderItem.ItemSubtotal * 0.16M;
      }

      return orderItems;
    }

    #endregion Public methods

    #region Private methods

    private int GetProductPriceId(int vendorProductId) {
      var productPriceRow = SalesOrderItemsData.GetProductPrice(vendorProductId, this.PriceListNumber);

      return Convert.ToInt32(productPriceRow[0]);
    }

    private decimal GetProductPrice(int vendorProductId) {
      var productPriceRow = SalesOrderItemsData.GetProductPrice(vendorProductId, this.PriceListNumber);

      return Convert.ToDecimal(productPriceRow[1]);
    }

    private FixedList<VendorPrices> GetCustomerPriceList() {
      var pricesList = CustomerPrices.GetVendorPrices(this.SalesOrder.Customer.Id);

      return pricesList;
    }

    private decimal GetDiscount() {
      return SalesDiscountData.GetCustomerDiscount(this.SalesOrder.Customer.Id);
    }

    private decimal GetAdditionalDiscount() {

      decimal additionalDiscount = 0;

      //var discounts = SalesDiscount.GetDiscountByVendor(this.VendorProduct, this.Order.OrderTime);
      var discounts = new FixedList<SalesDiscount>();

      foreach (SalesDiscount discount in discounts) {
        additionalDiscount += (this.ItemSubtotal * discount.Discount) / 100;
        this.ItemSubtotal = ItemSubtotal - ((this.ItemSubtotal * discount.Discount) / 100);
        this.Notes += $"Tiene un descuento de: {discount.Discount} % por {discount.Description}";
      }

      return additionalDiscount;

    }

    private int GetPriceListNumber(FixedList<VendorPrices> vendorPrices) {
      //var vendorPrice = vendorPrices.Find(r => r.VendorId == this.VendorProduct.Vendor.Id);

      return new VendorPrices().PriceListId;
    }

    private decimal GetSalesPrice() {
      return (this.ItemQuantity * this.ItemUnitPrice);
    }

    private decimal GetSubtotal(decimal discount) {
      var subTotal = this.GetSalesPrice() - ((this.GetSalesPrice() * discount) / 100); // this.Discount
      subTotal = subTotal - ((subTotal * AdditionalDiscount) / 100);

      return subTotal;
    }

    static private decimal CalculeSubtotal(SalesOrderItem orderItem) {
      var subTotal = orderItem.GetSalesPrice() - ((orderItem.GetSalesPrice() * orderItem.Discount) / 100);
      subTotal = subTotal - ((subTotal * orderItem.AdditionalDiscount) / 100);

      return subTotal;
    }

    #endregion Private methods

  }  //namespace Empiria.Trade.Sales

} // public class OrderItem
