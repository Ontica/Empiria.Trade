/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Shipping Management                        Component : Domain Layer                            *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Information Holder                      *
*  Type     : ShippingPackage                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a shipping package in pallet entry.                                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using Empiria.Trade.Orders;
using Empiria.Trade.Sales.ShippingAndHandling.Data;

namespace Empiria.Trade.Sales.ShippingAndHandling {

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


    [DataField("ShippingPackageId")]
    public int ShippingPackageId {
      get; set;
    }


    [DataField("ShippingPackageUID")]
    public string ShippingPackageUID {
      get; set;
    } = string.Empty;


    [DataField("ShippingPalletId")]
    public ShippingPallet ShippingPallet {
      get; set;
    }


    [DataField("OrderPackingId")]
    public PackageForItem OrderPacking {
      get; set;
    }


    [DataField("OrderId")]
    public Order Order {
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
      this.OrderPacking = PackageForItem.Parse(package);
      this.Order = Order.Parse(this.OrderPacking.OrderId);

    }

    #endregion Private methods

  } // class ShippingPackage

} // namespace Empiria.Trade.Sales.ShippingAndHandling
