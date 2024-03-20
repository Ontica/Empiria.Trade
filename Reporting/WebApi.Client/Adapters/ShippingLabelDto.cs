﻿/* Empiria Trade Reporting ***********************************************************************************
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


    public string orderUID {
      get; set;
    } = string.Empty;


    public string productCode {
      get; set;
    } = string.Empty;


    public string productPresentation {
      get; set;
    } = string.Empty;


    public string description {
      get; set;
    } = string.Empty;


    public string ubication {
      get; set;
    } = string.Empty;


    public string comments {
      get; set;
    } = string.Empty;


    public decimal quantity {
      get; set;
    }


  } // class ShippingLabelDto

} // namespace Empiria.Trade.Reporting.WebApi.Client.Adapters
