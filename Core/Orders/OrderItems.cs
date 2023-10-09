/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Order Management                           Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Order.dll                    Pattern   : Partitioned Type / Information Holder   *
*  Type     : OrderItems                                 License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a OrderItem.                                                                        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.StateEnums;

using Empiria.Trade.Core.Domain;

namespace Empiria.Trade {

  /// <summary> Represents a OrderItem </summary>
  abstract public class OrderItems : BaseObject {

    #region Constructors and parsers

    protected OrderItems() {
      // no-op
    }

    static public OrderItems Parse(int id) {
      return BaseObject.ParseId<OrderItems>(id);
    }

    static public OrderItems Parse(string uid) {
      return BaseObject.ParseKey<OrderItems>(uid);
    }

    #endregion Constructors and parsers

    #region Public properties


    [DataField("OrderId")]
    public int OrderId {
      get;
      protected set;
    }

    [DataField("ProductId")]
    public int Product {
      get;
      protected set;
    }

    [DataField("PresentationId")]
    public int PresentationId {
      get;
      protected set;
    }

    [DataField("VendorId")]
    public Party Vendor {
      get;
      protected set;
    }

    [DataField("Quantity")]
    public int Quantity {
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

    [DataField("OrderItemNotes")]
    public string Notes {
      get;
      protected set;
    }

    [DataField("OrderItemStatus")]
    public EntityStatus Status {
      get;
      protected set;
    }

    #endregion Public properties
  } // class OrderItems 

} // namespace Empiria.Trade.Core.Orders
