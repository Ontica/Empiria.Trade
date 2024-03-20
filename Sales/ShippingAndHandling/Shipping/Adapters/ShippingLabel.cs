/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Shipping Management                        Component : Interface adapters                      *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Data Transfer Object                    *
*  Type     : ShippingLabel                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return shipping label.                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Sales.ShippingAndHandling.Adapters {

  /// <summary>Output DTO used to return shipping label.</summary>
  public class ShippingLabel {


    public string OrderUID {
      get; set;
    }


    public string ProductCode {
      get; set;
    }


    public string ProductPresentation {
      get; set;
    }


    public string Description {
      get; set;
    }


    public string Comments {
      get; set;
    }


    public decimal Quantity {
      get; set;
    }


    public string Ubication {
      get; set;
    }
    
  } // class ShippingLabel

} // namespace Empiria.Trade.Sales.ShippingAndHandling.Adapters
