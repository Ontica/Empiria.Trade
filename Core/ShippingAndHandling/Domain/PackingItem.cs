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
  public class PackingItem : BaseObject {


    #region Constructor and parsers


    public PackingItem() {
      //no-op
    }

    static public PackingItem Parse(int id) => ParseId<PackingItem>(id);

    static public PackingItem Parse(int id, bool reload) => ParseId<PackingItem>(id, reload);

    static public PackingItem Parse(string uid) => ParseKey<PackingItem>(uid);

    static public PackingItem Empty => ParseEmpty<PackingItem>();


    public PackingItem(string orderUID, PackingItemFields orderFields) {

      MapToPackagingOrder(orderUID, orderFields);

    }

    
    #endregion Constructor and parsers


    #region Properties


    [DataField("OrderPackingId")]
    public int OrderPackingId {
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


    [DataField("PackageTypeId")]
    public int PackageTypeId {
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


    public PackageType PackageType {
      get;
      internal set;
    }


    #endregion Properties


    #region Private methods


    protected override void OnSave() {

      ShippingAndHandlingData.Write(this);

    }


    private void MapToPackagingOrder(string orderUID, PackingItemFields orderFields) {

      this.Order = Order.Parse(orderUID);
      this.PackageType = PackageType.Parse(orderFields.PackageTypeUID);
      this.OrderPackingUID = Guid.NewGuid().ToString();
      this.OrderId = Order.Id;
      this.PackageTypeId = PackageType.PackageTypeId;
      this.PackageID = orderFields.PackageID;

    }


    #endregion Private methods


  } // class PackagingOrder

} // namespace Empiria.Trade.ShippingAndHandling.Domain
