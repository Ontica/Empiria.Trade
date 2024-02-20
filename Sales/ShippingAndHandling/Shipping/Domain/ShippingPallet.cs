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
using Empiria.Trade.Orders;
using Empiria.Trade.Sales.ShippingAndHandling.Adapters;

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


    [DataField("OrderPackingId")]
    public PackageForItem OrderPackingId {
      get; set;
    }


    [DataField("ShippingOrderId")]
    public ShippingEntry ShippingOrder {
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





    #endregion Private methods

  } // class ShippingPallet

} //namespace Empiria.Trade.Sales.ShippingAndHandling
