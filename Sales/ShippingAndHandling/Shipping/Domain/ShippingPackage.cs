/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Shipping Management                        Component : Domain Layer                            *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Information Holder                      *
*  Type     : ShippingPackage                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a shipping package in pallet entry.                                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.Trade.Core;
using Empiria.Trade.Sales.ShippingAndHandling.Data;

namespace Empiria.Trade.Sales {

  /// <summary>Represents a shipping package in pallet entry.</summary>
  public class ShippingPackage : BaseObject {


    #region Constructors and parsers

    public ShippingPackage() {
      //no-op
    }


    static public ShippingPackage Parse(int id) => ParseId<ShippingPackage>(id);

    static public ShippingPackage Parse(string uid) => ParseKey<ShippingPackage>(uid);

    static public ShippingPackage Empty => ParseEmpty<ShippingPackage>();


    internal ShippingPackage(string package, ShippingPallet pallet) {
      MapToShippingPackage(package, pallet);
    }


    #endregion Constructors and parsers

    #region Properties


    [DataField("Shipping_Package_Id")]
    public int ShippingPackageId {
      get; set;
    }


    [DataField("Shipping_Package_UID")]
    public string ShippingPackageUID {
      get; set;
    } = string.Empty;


    [DataField("Shipping_Pallet_Id")]
    public ShippingPallet ShippingPallet {
      get; set;
    }


    [DataField("Order_Packing_Id")]
    public PackagingEntry OrderPacking {
      get; set;
    }


    [DataField("Order_Id")]
    public SalesOrder Order {
      get; set;
    }


    public int TotalPackages {
      get; set;
    }


    public decimal TotalWeight {
      get; set;
    }


    public decimal TotalVolume {
      get; set;
    }


    #endregion Properties


    #region Private methods

    protected override void OnSave() {

      if (this.ShippingPackageId == 0) {

        this.ShippingPackageId = this.Id;
        this.ShippingPackageUID = this.UID;
      }
      ShippingData.WriteShippingPackage(this);
    }


    private void MapToShippingPackage(string package, ShippingPallet pallet) {
      
      this.ShippingPallet = pallet;
      this.OrderPacking = PackagingEntry.Parse(package);
      this.Order = SalesOrder.Parse(this.OrderPacking.SalesOrder.Id);

    }

    #endregion Private methods

  } // class ShippingPackage

} // namespace Empiria.Trade.Sales.ShippingAndHandling
