﻿/* Empiria Trade *********************************************************************************************
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

    public SalesOrderItem(SalesOrderItemsFields fields) {
      LoadOrderItem(fields);
    }


    #endregion

    #region Public properties


    #endregion

    #region Public methods

    internal void LoadOrderItem(SalesOrderItemsFields fields) {
          
      this.OrderItemTypeId = 3;
      this.ProductId = 4;
      this.ProductPriceId = 3;
      this.PriceListNumber = 5;
      this.VendorId = 3;// Party.Parse(fields.Vendor.VendorUID);
      this.Quantity = fields.Quantity;
      this.BasePrice = fields.BasePrice;
      this.SalesPrice = fields.SalesPrice;
      this.Discount = fields.AdditionalDiscount;
      this.Shipment = fields.Shipment;
      this.TaxesIVA = fields.Taxes;
      this.Total = fields.Total;
      this.Notes = fields.Notes;
      
    }

    protected override void OnSave() {
      SalesOrderItemsData.Write(this);
    }


    #endregion Public methods

  }  //namespace Empiria.Trade.Sales

} // public class OrderItem
