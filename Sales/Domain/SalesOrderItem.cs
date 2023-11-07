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

    #region Public properties


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
      this.SalesPrice = (this.Quantity * this.BasePrice);
      this.Discount = SalesDiscount.Apply(VendorProduct, Order);
      this.AdditionalDiscount = fields.AdditionalDiscount;
      this.Shipment = fields.Shipment;
      this.TaxesIVA = GetTaxesIva();
      this.Total = (this.Quantity * this.BasePrice) + TaxesIVA;
      this.Notes = String.IsNullOrEmpty(fields.Notes) ? String.Empty : fields.Notes;
    }

    private int GetPriceListNumber(FixedList<VendorPrices> vendorPrices) {
      var vendorPrice = vendorPrices.Find(r => r.VendorId == this.VendorProduct.Vendor.Id);

      return vendorPrice.PriceListId;
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
      var subtotal = this.Quantity * this.BasePrice;
      return subtotal * 0.16m;
    }


    public FixedList<VendorPrices> GetCustomerPriceList() {
      var pricesList = CustomerPrices.GetVendorPrices(this.Order.Customer.Id);

      return pricesList;
    }

    #endregion Public methods

  }  //namespace Empiria.Trade.Sales

} // public class OrderItem
