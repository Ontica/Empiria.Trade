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

    public SalesOrderItem(int orderId, SalesOrderItemsFields fields) {
      LoadOrderItem(orderId, fields);
    }


    #endregion

    #region Public properties


    #endregion

    #region Public methods

    internal void LoadOrderItem(int orderId, SalesOrderItemsFields fields) {
      this.OrderId = orderId;      
      this.OrderItemTypeId = 3; 
      this.Product = Products.Product.Parse(fields.ProductUID);
      this.ProductPriceId = fields.ProductPriceId;
      this.PriceListNumber = fields.PriceListNumber;
      this.Vendor = Party.Parse(fields.VendorUID);
      this.Quantity = fields.Quantity;
      this.BasePrice = fields.BasePrice;
      this.SalesPrice = fields.SalesPrice;
      this.Discount = fields.AdditionalDiscount;
      this.Shipment = fields.Shipment;
      this.TaxesIVA = fields.Taxes;
      this.Total = fields.Total;
      this.Notes = fields.Notes;
      this.Status = fields.Status;
    }

    protected override void OnSave() {
      SalesOrderItemsData.Write(this);
    }


    #endregion Public methods

  }  //namespace Empiria.Trade.Sales

} // public class OrderItem
