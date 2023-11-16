/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Packing Management                         Component : Interface adapters                      *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Data Transfer Object                    *
*  Type     : PackingOrderFields                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : DTO used to manage shipping and handling fields.                                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.ShippingAndHandling.Adapters {


  /// <summary>DTO used to manage order shipping fields.</summary>
  public class PackingOrderFields {


    public FixedList<PackingFields> PackingFields {
      get; set;
    } = new FixedList<PackingFields>();


  } // class PackingOrderFields


  public class PackingFields {


    public string OrderUID {
      get; set;
    }


    public string OrderItemUID {
      get; set;
    }


    public decimal PackageQuantity {
      get; set;
    }


    public string PackageID {
      get; set;
    }


  } // class PackingFields


} // namespace Empiria.Trade.ShippingAndHandling.Adapters
