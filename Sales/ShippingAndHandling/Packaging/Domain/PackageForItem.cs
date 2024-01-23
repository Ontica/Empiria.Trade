/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Packaging Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Partitioned Type / Information Holder   *
*  Type     : PackageForItem                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a Packaging order.                                                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Core.Catalogues;
using Empiria.Trade.Orders;
using Empiria.Trade.Sales.ShippingAndHandling.Adapters;
using Empiria.Trade.Sales.ShippingAndHandling.Data;

namespace Empiria.Trade.Sales.ShippingAndHandling {


  /// <summary>Represents a Packaging order.</summary>
  public class PackageForItem : BaseObject {


    #region Constructor and parsers


    public PackageForItem() {
      //no-op
    }

    static public PackageForItem Parse(int id) => ParseId<PackageForItem>(id);

    static public PackageForItem Parse(int id, bool reload) => ParseId<PackageForItem>(id, reload);

    static public PackageForItem Parse(string uid) => ParseKey<PackageForItem>(uid);

    static public PackageForItem Empty => ParseEmpty<PackageForItem>();


    public PackageForItem(string orderUID, PackingItemFields orderFields, string packageForItemUID) {

      MapToPackagingOrder(orderUID, orderFields, packageForItemUID);

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

      if (OrderPackingId == 0) {

        OrderPackingId = Id;
      }
      PackagingData.WritePacking(this);

    }


    private void MapToPackagingOrder(string orderUID, PackingItemFields orderFields, string packageForItemUID) {

      var packaging = Parse(packageForItemUID);

      if (packaging.Id > 0) {
        OrderPackingId = packaging.OrderPackingId;
        OrderPackingUID = packageForItemUID;
      }

      Order = Order.Parse(orderUID);
      PackageType = PackageType.Parse(orderFields.PackageTypeUID);

      OrderId = Order.Id;
      PackageTypeId = PackageType.PackageTypeId;
      PackageID = orderFields.PackageID;


    }


    #endregion Private methods


  } // class PackageForItem

} // namespace Empiria.Trade.ShippingAndHandling.Domain
