/* Empiria Trade Reporting ***********************************************************************************
*                                                                                                            *
*  Module   : Shipping Management                        Component : Interface adapters                      *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Data Transfer Object                    *
*  Type     : ShippingLabelDto                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return shipping label.                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Reporting.WebApi.Client.Adapters {


  /// <summary>Output DTO used to return shipping label.</summary>
  public class ShippingLabelDto {


    public string OrderUID {
      get; set;
    } = string.Empty;


    public string ProductCode {
      get; set;
    } = string.Empty;


    public string ProductPresentation {
      get; set;
    } = string.Empty;


    public string Description {
      get; set;
    } = string.Empty;


    public string Ubication {
      get; set;
    } = string.Empty;


    public string Comments {
      get; set;
    } = string.Empty;


    public decimal Quantity {
      get; set;
    }


  } // class ShippingLabelDto

} // namespace Empiria.Trade.Reporting.WebApi.Client.Adapters
