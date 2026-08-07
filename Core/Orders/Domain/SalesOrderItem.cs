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
using Empiria.Orders;

namespace Empiria.Trade.Core {

  /// <summary>Represents a Order item. </summary>
  public class SalesOrderItem : OrderItem {

    #region Constructors and parsers

    protected SalesOrderItem() {
      //no-op
    }

    public SalesOrderItem(SalesOrder salesOrder, SalesOrderItemsFields fields) {
      this.SalesOrder = salesOrder;
      LoadOrderItem(fields);
    }

    #endregion

    #region Public properties

    public SalesOrder SalesOrder {
      get; private set;
    }


    public decimal Subtotal_ {
      get; set;
    } = 0;


    public decimal Shipment {
      get; protected set;
    }


    public decimal TaxesIVA {
      get; protected set;
    }


    public int PriceListNumber {
      get; protected set;
    }


    public string DiscountPolicy {
      get; set;
    } = String.Empty;


    public decimal AdditionalDiscount {
      get; protected set;
    }

    public string Notes {
      get {
        return ExtData.Get("notes", string.Empty);
      }
      private set {
        ExtData.SetIfValue("notes", value);
      }
    }

    #endregion Public properties

    #region Public methods

    internal void LoadOrderItem(SalesOrderItemsFields fields) {

      FixedList<VendorPrices> prices = GetCustomerPriceList();

      //this.OrderItemTypeId = 1045;
      //this.Notes = String.IsNullOrEmpty(fields.Notes) ? String.Empty : fields.Notes;
      //this.VendorProduct = fields.GetVendorProduct();
      //this.PriceListNumber = GetPriceListNumber(prices);
      //this.ProductPriceId = GetProductPriceId(VendorProduct.Id);
      //this.Quantity = fields.Quantity;
      //this.BasePrice = GetProductPrice(VendorProduct.Id);
      //this.SalesPrice = GetSalesPrice();
      //this.DiscountPolicy = GetDiscount().ToString();
      //this.Discount = GetDiscount();
      //this.AdditionalDiscount = fields.Discount2;
      //this.Subtotal_ = GetSubtotal();
      //this.Shipment = 0;
      //this.TaxesIVA = GetTaxesIva(this.Subtotal_);
      //this.Total = GetTotal(this.Subtotal_);
      //this.ReceivedQty = 0;
      //this.ScheduledTime = ExecutionServer.DateMaxValue;
      //this.ReceptionTime = ExecutionServer.DateMaxValue;
      //this.Reviewed = string.Empty;
    }


    public static void SaveSalesOrderItems(FixedList<SalesOrderItem> orderItems, int orderId) {
      
      foreach (SalesOrderItem orderItem in orderItems) {
        orderItem.SalesOrder = SalesOrder.Parse(orderId);
        orderItem.Save();
      }

    }


    protected override void OnSave() {
      SalesOrderItemsData.Write(this);
    }


    public static FixedList<SalesOrderItem> GetOrderItems(int orderId) {
     
      var orderItems = SalesOrderItemsData.GetOrderItems(orderId);

      foreach (SalesOrderItem orderItem in orderItems) {
        orderItem.DiscountPolicy = "10"; 
        orderItem.Subtotal_ = CalculeSubtotal(orderItem);
      }

      return orderItems;
    }

    #endregion Public methods

    #region Private methods

    private int GetProductPriceId(int vendorProductId) {
     var productPriceRow =  SalesOrderItemsData.GetProductPrice(vendorProductId, this.PriceListNumber);

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
        additionalDiscount += (this.Subtotal_ * discount.Discount) / 100;
        this.Subtotal_ = Subtotal_ - ((this.Subtotal_ * discount.Discount) / 100);
        this.Notes += $"Tiene un descuento de: {discount.Discount} % por {discount.Description}";
      }

      return additionalDiscount;
    
    }

    private int GetPriceListNumber(FixedList<VendorPrices> vendorPrices) {
      //var vendorPrice = vendorPrices.Find(r => r.VendorId == this.VendorProduct.Vendor.Id);

      return new VendorPrices().PriceListId;
    }

    private decimal GetSalesPrice() {
      return (this.Quantity * this.UnitPrice);
    }

    private decimal GetSubtotal() {
      var subTotal = this.GetSalesPrice() - ((this.GetSalesPrice() * this.Discount) / 100);
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
