/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Order Management                           Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Partitioned Type / Information Holder   *
*  Type     : OrderItem                                  License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents an order item.                                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.StateEnums;

using Empiria.Trade.Products;

namespace Empiria.Trade.Orders {

  /// <summary> Represents an order item.</summary>
  abstract public class OrderItem : BaseObject {

    #region Constructors and parsers

    protected OrderItem() {
      // no-op
    }

    static public OrderItem Empty => BaseObject.ParseEmpty<OrderItem>();

    static public OrderItem Parse(int id) {
      return BaseObject.ParseId<OrderItem>(id);
    }

    static public OrderItem Parse(string uid) {
      return BaseObject.ParseKey<OrderItem>(uid);
    }

    #endregion Constructors and parsers

    #region Public properties


    [DataField("OrderId")]
    public Order Order {
      get;
      protected set;
    } 


    [DataField("OrderItemTypeId")]
    public int OrderItemTypeId {
      get;
      protected set;
    }


    [DataField("VendorProductId")]
    public VendorProduct VendorProduct {
      get;
      protected set;
    }


    [DataField("Quantity")]
    public decimal Quantity {
      get;
      protected set;
    }


    [DataField("ReceivedQty")]
    public decimal ReceivedQty {
      get; protected set;
    }


    [DataField("ProductPriceId")]
    public int ProductPriceId {
      get;
      protected set;
    }


    [DataField("PriceListNumber")]
    public int PriceListNumber {
      get;
      protected set;
    }


    [DataField("BasePrice")]
    public decimal BasePrice {
      get;
      protected set;
    }


    [DataField("SalesPrice")]
    public decimal SalesPrice {
      get;
      protected set;
    }


    [DataField("Discount")]
    public decimal Discount {
      get;
      protected set;
    }


    [DataField("AdditionalDiscount")]
    public decimal AdditionalDiscount {
      get;
      protected set;
    }


    [DataField("Shipment")]
    public decimal Shipment {
      get;
      protected set;
    }


    [DataField("TaxesIVA")]
    public decimal TaxesIVA {
      get;
      protected set;
    }


    [DataField("TaxesIEPS")]
    public decimal TaxesIEPS {
      get;
      protected set;
    } = 0;


    [DataField("Total")]
    public decimal Total {
      get;
      protected set;
    }


    [DataField("OrderItemNotes", Default ="")]
    public string Notes {
      get;
      protected set;
    } = String.Empty;


    [DataField("ScheduledTime")]
    public DateTime ScheduledTime {
      get;
      protected set;
    }


    [DataField("ReceptionTime")]
    public DateTime ReceptionTime {
      get;
      protected set;
    }


    [DataField("Reviewed")]
    public string Reviewed {
      get;
      protected set;
    }


    [DataField("OrderItemStatus", Default = EntityStatus.Active)]
    public EntityStatus Status {
      get;
      protected set;
    }

    #endregion Public properties

  } // class OrderItems

} //  namespace Empiria.Trade.Orders
