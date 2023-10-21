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
      this.Quantity = fields.Quantity; //mandan 
      this.BasePrice = GetProductPrice(VendorProduct.Id, priceListNumber);
      this.SalesPrice = (this.Quantity * this.BasePrice); //mandan
      this.Discount = fields.AdditionalDiscount;
      this.Shipment = fields.Shipment;
      this.TaxesIVA = GetTaxesIva(); //calcular
      this.Total = (this.Quantity * this.BasePrice) + TaxesIVA ; //calcular
      this.Notes = fields.Notes;
      
    }

    
    protected override void OnSave() {
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

  public class ProductPrice {

    public int ProductPriceId {
      get; set;
    }

    public decimal PriceList {
      get; set;
    }
  }

} // public class OrderItem
