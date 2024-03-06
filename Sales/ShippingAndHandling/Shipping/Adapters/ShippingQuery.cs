/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Shipping Management                        Component : Interface adapters                      *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Data Transfer Object                    *
*  Type     : ShippingQuery                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Query used to get order shipping entries.                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Orders;

namespace Empiria.Trade.Sales.ShippingAndHandling.Adapters {


  public enum ShippingStatus {
    Open,

    Close
    // EnCapturaDeDatos = 'Abierto',
    // EnProcesoDeEnvio = 'Cerrado',
    // Cerrado = 'Cerrado',
  }


  /// <summary>Query used to get order shipping entries.</summary>
  public class ShippingQuery {


    public string Keywords {
      get; set;
    } = string.Empty;


    public string ParcelSupplierUID {
      get;set;
    } = string.Empty;


    public ShippingStatus Status {
      get; set;
    } = ShippingStatus.Open;

  } // class ShippingQuery


  /// <summary>Query used to get order shipping entries for edit.</summary>
  public class ShippingFieldsQuery {


    public string[] Orders {
      get; set;
    } = new string[0];


  } // class ShippingFieldsQuery


} // namespace Empiria.Trade.Sales.ShippingAndHandling.Adapters
