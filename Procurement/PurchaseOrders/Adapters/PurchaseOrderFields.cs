﻿/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Data Transfer Object                    *
*  Type     : PurchaseOrderFields                        License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : DTO used to manage purchase order fields.                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Orders;

namespace Empiria.Trade.Procurement.Adapters {


  /// <summary>DTO used to manage purchase order fields.</summary>
  public class PurchaseOrderFields {


    public int SupplierUID {
      get; internal set;
    }


    public int CustomerUID {
      get; internal set;
    }


    public int CustomerAddressUID {
      get; internal set;
    }


    public int CustomerContactUID {
      get;
      internal set;
    }


    public int SalesAgentUID {
      get; internal set;
    }


    public string Notes {
      get; internal set;
    }


    //public OrderAuthorizationStatus OrderAuthorizationStatus {
    //  get; internal set;
    //} = OrderAuthorizationStatus.Empty;


    public DateTime ScheduledTime {
      get;
      internal set;
    } = DateTime.MaxValue;


    public DateTime ReceptionTime {
      get;
      internal set;
    } = DateTime.MaxValue;


  } // class PurchaseOrderFields


  /// <summary>DTO used to manage purchase order item fields.</summary>
  public class PurchaseOrderItemFields {





  } // class PurchaseOrderItemFields


} // namespace Empiria.Trade.Inventory.PurchaseOrders.Adapters
