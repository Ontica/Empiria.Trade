﻿/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Shipping Management                        Component : Interface adapters                      *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Data Transfer Object                    *
*  Type     : ShippingQuery                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Query used to get order shipping entries.                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Sales.ShippingAndHandling.Adapters {


  /// <summary>Query used to get order shipping entries.</summary>
  public class ShippingQuery {

    public string[] OrderUIDs {
      get; set;
    } = new string[0];

  } // class ShippingQuery


} // namespace Empiria.Trade.Sales.ShippingAndHandling.Adapters
