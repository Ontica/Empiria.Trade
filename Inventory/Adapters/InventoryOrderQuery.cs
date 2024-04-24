/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Shipping Management                        Component : Interface adapters                      *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Data Transfer Object                    *
*  Type     : ShippingQuery                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Query used to get order inventory orders.                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Inventory.Adapters {


  /// <summary>Query used to get order inventory orders.</summary>
  public class InventoryOrderQuery {


    public string InventoryOrderTypeUID {
      get; set;
    } = string.Empty;


    //public string ExternalObjectReferenceUID {
    //    get; set;
    //} = string.Empty;


    public string AssignedToUID {
      get; set;
    } = string.Empty;


    public string Keywords {
      get; set;
    } = string.Empty;


    public InventoryStatus Status {
      get; set;
    } = InventoryStatus.Todos;


  }
}
