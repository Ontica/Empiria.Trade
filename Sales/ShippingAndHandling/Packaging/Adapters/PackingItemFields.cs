/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Packing Management                         Component : Interface adapters                      *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Data Transfer Object                    *
*  Type     : PackingItemFields                          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : DTO used to manage packing and handling fields.                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Sales.ShippingAndHandling.Adapters {


  /// <summary>DTO used to manage order packing fields.</summary>
  public class PackingItemFields {


    public string OrderUID {
      get; set;
    }


    public string PackageTypeUID {
      get;  set;
    }


    public string PackageID {
      get; set;
    }



  } // class PackingItemFields


  public class MissingItemField {


    public string orderItemUID {
      get; set;
    }


    public string WarehouseUID {
      get; set;
    }


    public string WarehouseBinUID {
      get; set;
    }


    public decimal Quantity {
      get; set;
    }


  } // class MissingItemField


} // namespace Empiria.Trade.ShippingAndHandling.Adapters
