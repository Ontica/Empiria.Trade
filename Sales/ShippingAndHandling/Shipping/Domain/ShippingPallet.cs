/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Shipping Management                        Component : Domain Layer                            *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Information Holder                      *
*  Type     : ShippingPallet                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a shipping pallet entry.                                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using Empiria.Trade.Core.Common;
using Empiria.Trade.Orders;
using Empiria.Trade.Sales.ShippingAndHandling.Adapters;
using Empiria.Trade.Sales.ShippingAndHandling.Data;

namespace Empiria.Trade.Sales.ShippingAndHandling {

  /// <summary>Represents a shipping pallet entry.</summary>
  public class ShippingPallet : BaseObject {


    #region Constructors and parsers

    public ShippingPallet() {
      //no-op
    }


    static public ShippingPallet Parse(int id) => ParseId<ShippingPallet>(id);

    static public ShippingPallet Parse(string uid) => ParseKey<ShippingPallet>(uid);

    static public ShippingPallet Empty => ParseEmpty<ShippingPallet>();


    public ShippingPallet(string shippingUID, ShippingPalletFields fields, string shippingPalletUID) {
      MapToShippingPallet(shippingUID, fields, shippingPalletUID);
    }


    #endregion Constructors and parsers

    #region Properties


    [DataField("ShippingPalletId")]
    public int ShippingPalletId {
      get; set;
    }


    [DataField("ShippingPalletUID")]
    public string ShippingPalletUID {
      get; set;
    } = string.Empty;


    [DataField("ShippingPalletName")]
    public string ShippingPalletName {
      get; set;
    } = string.Empty;


    [DataField("ShippingOrderId")]
    public ShippingEntry ShippingOrder {
      get; set;
    }


    public string[] ShippingPackages {
      get; set;
    } = new string[] { };


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

      if (this.ShippingPalletId == 0) {
        this.ShippingPalletId = this.Id;
        this.ShippingPalletUID = this.UID;
      }
      ShippingData.WriteShippingPallet(this);
    }


    private void MapToShippingPallet(string shippingUID,
                  ShippingPalletFields fields, string shippingPalletUID) {

      var pallet = shippingPalletUID != string.Empty ? Parse(shippingPalletUID) : null;

      if (shippingPalletUID != string.Empty && !pallet.Equals(null)) {
        this.ShippingPalletId = pallet.Id;
      }
      
      this.ShippingOrder = ShippingEntry.Parse(shippingUID);
      this.ShippingPalletName = fields.ShippingPalletName;
    }


    #endregion Private methods

  } // class ShippingPallet

} //namespace Empiria.Trade.Sales.ShippingAndHandling
