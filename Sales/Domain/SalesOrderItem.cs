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
using Empiria.DataTypes;

namespace Empiria.Trade.Sales {

  /// <summary>Represents a Order item. </summary>
  public class SalesOrderItem : OrderItem {

    #region Constructors and parsers

    protected SalesOrderItem() {
      //no-op
    }

    public SalesOrderItem(SalesOrderItemsFields fields, int customerListPriceNumber) {
      LoadOrderItem(fields, customerListPriceNumber);
    }

    #endregion

    #region Public properties


    #endregion

    #region Public methods

    internal void LoadOrderItem(SalesOrderItemsFields fields, int priceListNumber) {
          
      this.OrderItemTypeId = 3;
      this.VendorProduct = fields.GetVendorProduct();
      this.ProductPriceId = GetProductPriceId(VendorProduct.Id, priceListNumber);
      this.PriceListNumber = priceListNumber;
      this.Quantity = fields.Quantity;
      this.BasePrice = GetProductPrice(VendorProduct.Id, priceListNumber);
      this.SalesPrice = (this.Quantity * this.BasePrice); 
      this.Discount = fields.AdditionalDiscount;
      this.Shipment = fields.Shipment;
      this.TaxesIVA = GetTaxesIva(); //calcular
      this.Total = (this.Quantity * this.BasePrice) + TaxesIVA ; //calcular
      this.Notes = String.IsNullOrEmpty(fields.Notes) ? String.Empty : fields.Notes;

    }


    public static void SaveSalesOrderItems(FixedList<SalesOrderItem> orderItems, int orderId) {
      foreach (SalesOrderItem orderItem in orderItems) {
        orderItem.OrderId = orderId;
        orderItem.Save();
      }

    }

    protected override void OnSave() {
      OrderId = this.OrderId;
      SalesOrderItemsData.Write(this);
    }

    private int GetProductPriceId(int vendorProductId, int customerPriceListNumber) {
     var productPriceRow =  SalesOrderItemsData.GetProductPrice(vendorProductId, customerPriceListNumber);

      return Convert.ToInt32(productPriceRow[0]);
    }

    private decimal GetProductPrice(int vendorProductId, int customerPriceListNumber) {
      var productPriceRow = SalesOrderItemsData.GetProductPrice(vendorProductId, customerPriceListNumber);

      return Convert.ToDecimal(productPriceRow[1]);
    }

    private decimal GetTaxesIva() {
      var subtotal = this.Quantity * this.BasePrice;
      return subtotal * 0.16m;
    }

    #endregion Public methods

  }  //namespace Empiria.Trade.Sales

} // public class OrderItem
