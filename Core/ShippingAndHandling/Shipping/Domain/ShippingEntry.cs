/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Shipping Management                        Component : Domain Layer                            *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Information Holder                      *
*  Type     : ShippingEntry                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a shipping entry.                                                                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;

namespace Empiria.Trade.ShippingAndHandling {

  /// <summary>Represents a shipping entry.</summary>
  internal class ShippingEntry {


    #region Constructor and parser

    public ShippingEntry() {

    }

    #endregion Constructor and parser


    #region Properties


    [DataField("ShippingId")]
    public int ShippingId {
      get; set;
    }


    [DataField("OrderId")]
    public int OrderId {
      get; set;
    }


    [DataField("ParcelSupplierId")]
    public int ParcelSupplierId {
      get; set;
    }


    [DataField("ShippingUID")]
    public string ShippingUID {
      get; set;
    }
    

    [DataField("ShippingGuide")]
    public string ShippingGuide {
      get; set;
    }


    [DataField("ParcelAmount")]
    public decimal ParcelAmount {
      get; set;
    }


    [DataField("CustomerAmount")]
    public decimal CustomerAmount {
      get; set;
    }


    [DataField("ShippingDate")]
    public DateTime ShippingDate {
      get; set;
    }


    [DataField("DeliveryDate")]
    public DateTime DeliveryDate {
      get; set;
    }


    #endregion Properties


  } // class ShippingEntry

} // namespace Empiria.Trade.ShippingAndHandling
