/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Data Transfer Object                    *
*  Type     : PurchaseOrderDataDto                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return purchase descriptor data.                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Core.Common;
using Empiria.Trade.Orders;

namespace Empiria.Trade.Procurement.Adapters {


  /// <summary>Output DTO used to return purchase descriptor data.</summary>
  public class PurchaseOrderDataDto {


    public PurchaseOrderQuery Query {
      get; set;
    } = new PurchaseOrderQuery();


    public FixedList<DataTableColumn> Columns {
      get; set;
    } = new FixedList<DataTableColumn>();


    public FixedList<PurchaseOrderDescriptorDto> Entries {
      get; set;
    } = new FixedList<PurchaseOrderDescriptorDto>();


  } // class PurchaseOrderDataDto


  public class PurchaseOrderDescriptorDto {


    public string OrderNo {
      get; internal set;
    }


    public string Supplier {
      get; internal set;
    }


    public string Customer {
      get; internal set;
    }


    public DateTime OrderTime {
      get; internal set;
    }


    public DateTime ScheduledTime {
      get; internal set;
    }


    public OrderStatus OrderStatus {
      get; internal set;
    }


    public decimal OrderTotal {
      get;
      internal set;
    }


  } // class PurchaseOrderDescriptorDto

} // namespace Empiria.Trade.Inventory.PurchaseOrders.Adapters
