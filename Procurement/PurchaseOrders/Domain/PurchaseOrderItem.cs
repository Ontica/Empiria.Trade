/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Information Holder                      *
*  Type     : PurchaseOrderItem                          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a purchase order item.                                                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Diagnostics;
using Empiria.StateEnums;
using Empiria.Trade.Orders;
using Empiria.Trade.Procurement.Adapters;
using Empiria.Trade.Procurement.Data;

namespace Empiria.Trade.Procurement {


  /// <summary>Represents a purchase order item.</summary>
  public class PurchaseOrderItem : OrderItem {


    #region Constructors and parsers


    public PurchaseOrderItem() {
      // no-op
    }


    public PurchaseOrderItem(string orderUID, PurchaseOrderItemFields fields) {
      MapToPurchaseOrderItem(orderUID, fields);
    }


    static public new PurchaseOrderItem Parse(int id) {
      return BaseObject.ParseId<PurchaseOrderItem>(id);
    }


    static public new PurchaseOrderItem Parse(string uid) {
      return BaseObject.ParseKey<PurchaseOrderItem>(uid);
    }


    static public PurchaseOrderItem ParseEmpty() {
      return new PurchaseOrderItem();
    }


    #endregion Constructors and parsers


    #region Properties


    public int OrderItemId {
      get; internal set;
    }


    public string OrderItemUID {
      get; internal set;
    }


    public decimal SubTotal {
      get; internal set;
    }


    #endregion Properties


    #region Private methods


    protected override void OnSave() {

      if (IsNew) {
        OrderItemId = this.Id;
        OrderItemUID = this.UID;
        Status = EntityStatus.Active;
      }

      PurchaseOrderData.WritePurchaseOrderItem(this);
    }


    private void MapToPurchaseOrderItem(string orderUID, PurchaseOrderItemFields fields) {

      if (fields.UID != string.Empty) {
        this.OrderItemId = OrderItem.Parse(fields.UID).Id;
        this.OrderItemUID = fields.UID;
      }
      
      this.Order = Order.Parse(orderUID);
      this.OrderItemTypeId = 1031;
      this.VendorProduct = Products.VendorProduct.Parse(fields.VendorProductUID);
      this.Quantity = fields.Quantity;
      this.ReceivedQty = 0;
      this.ProductPriceId = -1;
      this.PriceListNumber = 1;
      this.BasePrice = fields.BasePrice;
      this.SalesPrice = fields.SalesPrice;
      this.Discount = fields.Discount;
      this.Shipment = 0;
      this.TaxesIVA = fields.Taxes;
      this.Total = fields.Total;
      this.Notes = fields.Notes;
      this.ScheduledTime = fields.ScheduledTime;
      this.ReceptionTime = fields.ReceptionTime;
      this.Reviewed = fields.Reviewed;
      
    }


    #endregion Private methods

  } // class PurchaseOrderItem

} // namespace Empiria.Trade.Procurement.PurchaseOrders.Domain
