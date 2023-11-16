/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Packaging Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Partitioned Type / Information Holder   *
*  Type     : PackagingOrder                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a Packaging order.                                                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Orders;
using Empiria.Trade.ShippingAndHandling.Adapters;
using Empiria.Trade.ShippingAndHandling.Data;

namespace Empiria.Trade.ShippingAndHandling {


  /// <summary>Represents a Packaging order.</summary>
  public class PackagingOrder : BaseObject {


    #region Constructor and parsers


    public PackagingOrder() {
      //no-op
    }

    static public PackagingOrder Parse(int id) => ParseId<PackagingOrder>(id);

    static public PackagingOrder Parse(int id, bool reload) => ParseId<PackagingOrder>(id, reload);

    //static public PackagingOrder Parse(string uid) => ParseKey<PackagingOrder>(uid);

    static public PackagingOrder Empty => ParseEmpty<PackagingOrder>();


    public PackagingOrder(PackingFields fields) {

      MapToPackagingOrder(fields);

    }

    
    #endregion Constructor and parsers


    #region Properties


    [DataField("OrderPackagingId")]
    public string OrderPackagingId {
      get;
      internal set;
    }


    [DataField("OrderId")]
    public int OrderId {
      get;
      internal set;
    }


    [DataField("OrderItemId")]
    public int OrderItemId {
      get;
      internal set;
    }


    [DataField("PackageQuantity")]
    public decimal PackageQuantity {
      get;
      internal set;
    }


    [DataField("PackageID")]
    public string PackageID {
      get;
      internal set;
    }


    public Order Order {
      get;
      internal set;
    }


    public OrderItem OrderItem {
      get;
      internal set;
    }

    #endregion Properties


    #region Private methods


    protected override void OnSave() {

      ShippingAndHandlingData.Write(this);

    }


    private void MapToPackagingOrder(PackingFields fields) {

      this.Order = Order.Parse(fields.OrderUID);
      this.OrderItem = OrderItem.Parse(fields.OrderItemUID);
      this.PackageQuantity = fields.PackageQuantity;
      this.PackageID = fields.PackageID;
      this.OrderId = Order.Id;
      this.OrderItemId = OrderItem.Id;

    }


    #endregion Private methods


  } // class PackagingOrder

} // namespace Empiria.Trade.ShippingAndHandling.Domain
