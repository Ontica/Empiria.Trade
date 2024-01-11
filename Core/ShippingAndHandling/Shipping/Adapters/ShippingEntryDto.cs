/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Shipping Management                        Component : Interface adapters                      *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Data Transfer Object                    *
*  Type     : ShippingEntryDto                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return the entries of shipping.                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.ShippingAndHandling.Adapters {

  /// <summary>Output DTO used to return the entries of shipping.</summary>
  public class ShippingEntryDto {

    
    public INamedEntity ParcelSupplier {
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


  } // class ShippingEntryDto


  public class ShippingFields {


    public string OrderUID {
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

  }

} // namespace Empiria.Trade.ShippingAndHandling.Adapters
