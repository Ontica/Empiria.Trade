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

using Empiria.Trade.Core;
using Empiria.Trade.Sales.Adapters;
using Empiria.Trade.Sales.Data;

using Empiria.Trade.Orders;

namespace Empiria.Trade.Sales {

  /// <summary>Represents a Order item. </summary>
  public class SalesOrderItem : OrderItem {

    #region Constructors and parsers

    protected SalesOrderItem() {
      //no-op
    }

    public SalesOrderItem(SalesOrder salesOrder, SalesOrderItemsFields fields) {
      this.Order = salesOrder;
      LoadOrderItem(fields);
    }

    #endregion

    #region Private properties

    private decimal SubTotal {
      get; set;
    } = 0;

    #endregion

    #region Public methods

    internal void LoadOrderItem(SalesOrderItemsFields fields) {

      FixedList<VendorPrices> prices = GetCustomerPriceList();

      this.OrderItemTypeId = 1045;
      this.VendorProduct = fields.GetVendorProduct();
      this.PriceListNumber = GetPriceListNumber(prices);
      this.ProductPriceId = GetProductPriceId(VendorProduct.Id);
      this.Quantity = fields.Quantity;
      this.BasePrice = GetProductPrice(VendorProduct.Id);
      this.SalesPrice = GetSalesPrice();
      this.Discount = GetDiscount(fields.AdditionalDiscount);
      this.SubTotal = this.SalesPrice - Discount;
      this.AdditionalDiscount = GetAdditionalDiscount();
      this.Shipment = fields.Shipment;
      this.TaxesIVA = GetTaxesIva();
      this.Total = GetTotal();
      this.Notes = String.IsNullOrEmpty(fields.Notes) ? String.Empty : fields.Notes;
    }

   

    private int GetPriceListNumber(FixedList<VendorPrices> vendorPrices) {
      var vendorPrice = vendorPrices.Find(r => r.VendorId == this.VendorProduct.Vendor.Id);

      return vendorPrice.PriceListId;
    }

    private decimal GetSalesPrice() {
      return (this.Quantity * this.BasePrice); 
    }

    public static void SaveSalesOrderItems(FixedList<SalesOrderItem> orderItems, int orderId) {
      foreach (SalesOrderItem orderItem in orderItems) {
        orderItem.Order = Order.Parse(orderId);
        orderItem.Save();
      }

    }

    protected override void OnSave() {
      SalesOrderItemsData.Write(this);
    }

    public static FixedList<SalesOrderItem> GetOrderItems(int orderId) {
      return SalesOrderItemsData.GetOrderItems(orderId);
    }

    private int GetProductPriceId(int vendorProductId) {
     var productPriceRow =  SalesOrderItemsData.GetProductPrice(vendorProductId, this.PriceListNumber);

      return Convert.ToInt32(productPriceRow[0]);
    }

    private decimal GetProductPrice(int vendorProductId) {
      var productPriceRow = SalesOrderItemsData.GetProductPrice(vendorProductId, this.PriceListNumber);

      return Convert.ToDecimal(productPriceRow[1]);
    }

    private decimal GetTaxesIva() {
      return this.SubTotal * 0.16m;
    }

    private decimal GetTotal() {
      return this.SubTotal + this.TaxesIVA;
    }


    private FixedList<VendorPrices> GetCustomerPriceList() {
      var pricesList = CustomerPrices.GetVendorPrices(this.Order.Customer.Id);

      return pricesList;
    }

    private decimal GetDiscount(decimal discount) {
      return (this.SalesPrice * discount) / 100;
    }

    private decimal GetAdditionalDiscount() {
      
      decimal additionalDiscount = 0;

      var discounts = SalesDiscount.GetDiscountByVendor(this.VendorProduct, this.Order.OrderTime);

      foreach (SalesDiscount discount in discounts) {
        additionalDiscount += (this.SubTotal * discount.Discount) / 100;
        this.SubTotal = SubTotal - ((this.SubTotal * discount.Discount) / 100);

      }

      return additionalDiscount;
    
    }
    #endregion Public methods

  }  //namespace Empiria.Trade.Sales

} // public class OrderItem
