/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Data Transfer Object                    *
*  Type     : PurchaseOrderQuery                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Query used to get purchase orders.                                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.StateEnums;

namespace Empiria.Trade.Core {
  
  
  /// <summary></summary>
  public class PurchaseOrderQuery {

    public string SupplierUID {
      get; set;
    } = string.Empty;


    public string Keywords {
      get; set;
    } = string.Empty;


    public string Status {
      get; set;
    }


    public EntityStatus Status_ {
      get {
        return GetEntityStatus(Status);
      }
    }


    private EntityStatus GetEntityStatus(string status) {

      switch (status) {
        case "Pending":
        case "InProgress":
          return EntityStatus.Pending;
        case "Active":
        case "Captured":
          return EntityStatus.Active;
        case "OnReview":
          return EntityStatus.OnReview;
        case "Suspended":
        case "Cancelled":
          return EntityStatus.Suspended;
        case "Discontinued":
          return EntityStatus.Discontinued;
        case "Deleted":
          return EntityStatus.Deleted;
        case "Closed":
          return EntityStatus.Closed;
        default:
          return EntityStatus.All;
      }
    }

  } // class PurchaseOrderQuery

} // namespace Empiria.Trade.Core
