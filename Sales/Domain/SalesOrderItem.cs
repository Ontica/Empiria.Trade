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

using Empiria.Trade.Core.Domain;
using Empiria.Trade.Sales.Adapters;
using Empiria.Trade.Sales.Data;

namespace Empiria.Trade.Sales {

  /// <summary>Represents a Order item. </summary>
  public class SalesOrderItem : OrderItems {

    #region Constructors and parsers

    protected SalesOrderItem() {
      //no-op
    }

    public SalesOrderItem(int orderId, OrderItemsFields fields) {
      LoadOrderItem(orderId, fields);
    }


    #endregion

    #region Public properties


    #endregion

    #region Public methods

    internal void LoadOrderItem(int orderId, OrderItemsFields fields) {
      this.OrderId = orderId;
      this.Product = 3;
      this.PresentationId = 10;
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
      OrderItemsData.Write(this);
    }

    
    #endregion Public methods

  }  //namespace Empiria.Trade.Sales

} // public class OrderItem
