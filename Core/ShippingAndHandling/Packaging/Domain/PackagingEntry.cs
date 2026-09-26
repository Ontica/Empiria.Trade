/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Packaging Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Partitioned Type / Information Holder   *
*  Type     : PackagingEntry                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a Packaging order.                                                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.Orders;
using Empiria.Trade.Core.Catalogues;

namespace Empiria.Trade.Core {


  /// <summary>Represents a Packaging order.</summary>
  public class PackagingEntry : BaseObject {


    #region Constructor and parsers

    public PackagingEntry() {
      //no-op
    }

    static public PackagingEntry Parse(int id) => ParseId<PackagingEntry>(id);

    static public PackagingEntry Parse(string uid) => ParseKey<PackagingEntry>(uid);

    static public PackagingEntry Empty => ParseEmpty<PackagingEntry>();

    public PackagingEntry(string orderUID, PackingItemFields orderFields, string packageForItemUID) {

      MapToPackagingOrder(orderUID, orderFields, packageForItemUID);

    }

    #endregion Constructor and parsers


    #region Properties

    [DataField("Order_Packing_Id")]
    public int OrderPackingId {
      get;
      internal set;
    }


    [DataField("Order_Packing_UID")]
    public string OrderPackingUID {
      get;
      internal set;
    }


    [DataField("Order_Id")]
    public int OrderId {
      get;
      internal set;
    }


    [DataField("Package_Type_Id")]
    public int PackageTypeId {
      get;
      internal set;
    }


    [DataField("Package_ID")]
    public string PackageID {
      get;
      internal set;
    }


    public SalesOrder Order {
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

      Order = SalesOrder.Parse(orderUID);
      PackageType = PackageType.Parse(orderFields.PackageTypeUID);

      OrderId = Order.Id;
      PackageTypeId = PackageType.PackageTypeId;
      PackageID = orderFields.PackageID;
    }

    #endregion Private methods


  } // class PackageForItem

} // namespace Empiria.Trade.ShippingAndHandling.Domain
