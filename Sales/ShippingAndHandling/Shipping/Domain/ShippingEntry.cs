/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Shipping Management                        Component : Domain Layer                            *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Information Holder                      *
*  Type     : ShippingEntry                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a shipping entry.                                                                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using Empiria.Trade.Core.Common;
using Empiria.Trade.Orders;
using Empiria.Trade.Sales.ShippingAndHandling.Data;
using Empiria.Trade.Sales.ShippingAndHandling.Domain;
using Empiria.Trade.Sales.ShippingAndHandling.Adapters;

namespace Empiria.Trade.Sales.ShippingAndHandling {

  /// <summary>Represents a shipping entry.</summary>
  public class ShippingEntry : BaseObject {


    #region Constructor and parser

    public ShippingEntry() {
      //no-op
    }


    static public ShippingEntry Parse(int id) => ParseId<ShippingEntry>(id);

    static public ShippingEntry Parse(int id, bool reload) => ParseId<ShippingEntry>(id, reload);

    static public ShippingEntry Parse(string uid) => ParseKey<ShippingEntry>(uid);

    static public ShippingEntry Empty => ParseEmpty<ShippingEntry>();


    public ShippingEntry(string orderUID, ShippingFields fields) {
      MapToShippingEntry(orderUID, fields);
    }


    #endregion Constructor and parser


    #region Properties


    [DataField("ShippingOrderId")]
    public int ShippingOrderId {
      get; set;
    }


    [DataField("OrderId")]
    public int OrderId {
      get; set;
    }


    [DataField("ParcelSupplierId")]
    public int ParcelSupplierId {
      get; set;
    }


    [DataField("ShippingUID")]
    public string ShippingUID {
      get; set;
    }
    

    [DataField("ShippingGuide")]
    public string ShippingGuide {
      get; set;
    }


    [DataField("ParcelAmount")]
    public decimal ParcelAmount {
      get; set;
    }


    [DataField("CustomerAmount")]
    public decimal CustomerAmount {
      get; set;
    }


    [DataField("ShippingDate")]
    public DateTime ShippingDate {
      get; set;
    }


    [DataField("DeliveryDate")]
    public DateTime DeliveryDate {
      get; set;
    }


    public FixedList<ShippingOrderItem> OrderForShipping {
      get ; set;
    } = new FixedList<ShippingOrderItem>();


    #endregion Properties


    #region Private methods

    protected override void OnSave() {

      if (this.ShippingOrderId == 0) {

        this.ShippingOrderId = this.Id;
        this.ShippingUID = this.UID;
      }
      ShippingData.WriteShipping(this);
    }


    private void MapToShippingEntry(string orderUID, ShippingFields fields) {

      var builder = new ShippingBuilder();
      var shipping = builder.GetShippingByOrderUID(orderUID);

      if (shipping.ShippingOrderId > 0) {
        this.ShippingOrderId = shipping.ShippingOrderId;
        this.ShippingUID = shipping.ShippingUID;
      }

      this.OrderId = Order.Parse(orderUID).Id;
      this.ParcelSupplierId = SimpleObjectData.Parse(fields.ParcelSupplierUID).Id;
      this.ShippingGuide = fields.ShippingGuide;
      this.ParcelAmount = fields.ParcelAmount;
      this.CustomerAmount = fields.CustomerAmount;
      this.ShippingDate = DateTime.Now;
      this.DeliveryDate = DateTime.Now;
    }


    #endregion Private methods

  } // class ShippingEntry

  
} // namespace Empiria.Trade.ShippingAndHandling
