/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Data Transfer Object                    *
*  Type     : PurchaseOrderQuery                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Query used to get purchase orders.                                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.StateEnums;
using Empiria.Trade.Orders;

namespace Empiria.Trade.Procurement.Adapters {
  
  
  /// <summary></summary>
  public class PurchaseOrderQuery {


    public string ProviderUID {
      get; set;
    } = string.Empty;


    public string Keywords {
      get; set;
    } = string.Empty;


    public EntityStatus Status {
      get; set;
    } = EntityStatus.All;



  } // class PurchaseOrderQuery

} // namespace Empiria.Trade.Inventory.PurchaseOrders.Adapters
