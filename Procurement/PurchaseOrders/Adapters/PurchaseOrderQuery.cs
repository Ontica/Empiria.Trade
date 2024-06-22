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
using Empiria.Trade.Orders;

namespace Empiria.Trade.Procurement.Adapters {
  
  
  /// <summary></summary>
  public class PurchaseOrderQuery {


    public string SupplierUID {
      get; set;
    } = string.Empty;


    public string Keywords {
      get; set;
    } = string.Empty;


    public OrderStatus Status {
      get; set;
    } = OrderStatus.Empty;



  } // class PurchaseOrderQuery

} // namespace Empiria.Trade.Inventory.PurchaseOrders.Adapters
