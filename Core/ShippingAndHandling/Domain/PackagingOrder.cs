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

    static public PackagingOrder Parse(string uid) => ParseKey<PackagingOrder>(uid);

    static public PackagingOrder Empty => ParseEmpty<PackagingOrder>();


    public PackagingOrder(string orderUID, PackingOrderFields fields) {

      MapToPackagingOrder(orderUID, fields);

    }

    
    #endregion Constructor and parsers


    #region Properties


    [DataField("OrderPackagingId")]
    public string OrderPackagingId {
      get;
      internal set;
    }


    [DataField("OrderPackingUID")]
    public string OrderPackingUID {
      get;
      internal set;
    }


    [DataField("OrderId")]
    public int OrderId {
      get;
      internal set;
    }


    [DataField("PackageID")]
    public string PackageID {
      get;
      internal set;
    }


    [DataField("Size")]
    public string Size {
      get;
      internal set;
    }


    public Order Order {
      get;
      internal set;
    }


    #endregion Properties


    #region Private methods


    protected override void OnSave() {

      ShippingAndHandlingData.Write(this);

    }


    private void MapToPackagingOrder(string orderUID, PackingOrderFields fields) {

      this.Order = Order.Parse(orderUID); 
      this.OrderPackingUID = Guid.NewGuid().ToString();
      this.OrderId = Order.Id;
      this.PackageID = fields.PackageID;
      this.Size = fields.Size;

    }


    #endregion Private methods


  } // class PackagingOrder

} // namespace Empiria.Trade.ShippingAndHandling.Domain
