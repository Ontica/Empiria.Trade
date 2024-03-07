/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Shipping Management                        Component : Interface adapters                      *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Data Transfer Object                    *
*  Type     : ShippingFields                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : DTO used to manage order shipping fields.                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Sales.ShippingAndHandling.Adapters {


  /// <summary>DTO used to manage order shipping fields.</summary>
  public class ShippingFields {


    public string[] Orders {
      get; set;
    }


    public ShippingDataFields ShippingData {
      get; set;
    }


  } // class ShippingFields


  public class ShippingDataFields {


    public string ShippingUID {
      get; set;
    }


    public string ParcelSupplierUID {
      get; set;
    }


    public string ShippingGuide {
      get; set;
    }


    public decimal ParcelAmount {
      get; set;
    }


    public decimal CustomerAmount {
      get; set;
    }


    public ShippingStatus Status {
      get; set;
    } = ShippingStatus.EnCaptura;

  }


  public class ShippingPalletFields {

    public string ShippingPalletName {
      get; set;
    }


    public string[] Packages {
      get; set;
    }

  }

} // namespace Empiria.Trade.Sales.ShippingAndHandling.Adapters
