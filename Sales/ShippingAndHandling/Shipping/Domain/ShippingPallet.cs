/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Shipping Management                        Component : Domain Layer                            *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Information Holder                      *
*  Type     : ShippingPallet                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a shipping pallet entry.                                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.Trade.Sales.ShippingAndHandling.Adapters;
using Empiria.Trade.Sales.ShippingAndHandling.Data;

namespace Empiria.Trade.Sales {

  /// <summary>Represents a shipping pallet entry.</summary>
  public class ShippingPallet : BaseObject {


    #region Constructors and parsers

    public ShippingPallet() {
      //no-op
    }


    static public ShippingPallet Parse(int id) => ParseId<ShippingPallet>(id);

    static public ShippingPallet Parse(string uid) => ParseKey<ShippingPallet>(uid);

    static public ShippingPallet Empty => ParseEmpty<ShippingPallet>();


    public ShippingPallet(string shippingUID, ShippingPalletFields fields) {
      
      Update(shippingUID, fields);
    }


    #endregion Constructors and parsers

    #region Properties


    [DataField("Shipping_Pallet_Id")]
    public int ShippingPalletId {
      get; set;
    }


    [DataField("Shipping_Pallet_UID")]
    public string ShippingPalletUID {
      get; set;
    } = string.Empty;


    [DataField("Shipping_Order_Id")]
    public ShippingEntry ShippingOrder {
      get; set;
    }


    [DataField("Shipping_Pallet_Name")]
    public string ShippingPalletName {
      get; set;
    } = string.Empty;


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

      if (IsNew) {
        this.ShippingPalletId = this.Id;
        this.ShippingPalletUID = this.UID;
      }
      ShippingData.WriteShippingPallet(this);
    }


    public void Update(string shippingUID, ShippingPalletFields fields) {

      this.ShippingOrder = ShippingEntry.Parse(shippingUID);
      this.ShippingPalletName = fields.ShippingPalletName;
    }


    #endregion Private methods

  } // class ShippingPallet

} //namespace Empiria.Trade.Sales.ShippingAndHandling
