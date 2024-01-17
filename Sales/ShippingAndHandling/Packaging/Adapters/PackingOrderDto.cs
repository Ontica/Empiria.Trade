/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Packing Management                         Component : Interface adapters                      *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Data Transfer Object                    *
*  Type     : PackingOrderDto                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : DTO used to manage order packing output.                                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Orders;

namespace Empiria.Trade.Sales.ShippingAndHandling.Adapters {


  /// <summary>Interface used to manage shipping and handling fields.</summary>
  public interface IShippingAndHandling {


  } // interface IShippingAndHandling


  /// <summary>DTO used to manage order packing output.</summary>
  public class PackingOrderDto : IShippingAndHandling {

    public string UID {
      get; set;
    }


    public Order Order {
      get; set;
    }


    public string PackageID {
      get; set;
    }


  } // class PackingOrderDto


} // namespace Empiria.Trade.ShippingAndHandling.Adapters
