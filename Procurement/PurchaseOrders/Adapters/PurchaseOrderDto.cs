/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Data Transfer Object                    *
*  Type     : PurchaseOrderDto                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return purchase order data.                                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Core;
using Empiria.Trade.Orders;

namespace Empiria.Trade.Procurement.Adapters {

  /// <summary>Output DTO used to return purchase order data.</summary>
  public class PurchaseOrderDto : IOrderDto {

    public string UID {
      get; internal set;
    } = string.Empty;


    public string OrderNumber {
      get; internal set;
    } = string.Empty;


    public NamedEntityDto Supplier {
      get; internal set;
    } = NamedEntityDto.Empty;


    public NamedEntityDto Status {
      get; internal set;
    } = NamedEntityDto.Empty;


    public string Notes {
      get; internal set;
    } = string.Empty;


    public PaymentCondition PaymentConditions {
      get; set;
    } = PaymentCondition.None;


    public ShippingMethods ShippingMethod {
      get; internal set;
    } = ShippingMethods.None;


    public DateTime OrderTime {
      get; set;
    } = DateTime.MaxValue;


    public DateTime ScheduledTime {
      get; set;
    } = DateTime.MaxValue;


    public DateTime ReceptionTime {
      get; set;
    } = DateTime.MaxValue;


    public DateTime PostingTime {
      get; set;
    }


    public FixedList<PurchaseOrderItemDto> Items {
      get; set;
    } = new FixedList<PurchaseOrderItemDto>();


    public PurchaseOrderTotal Totals {
      get; set;
    } = new PurchaseOrderTotal();


    public PurchaseOrderActions Actions {
      get; set;
    } = new PurchaseOrderActions();
    
  } // class PurchaseOrderDto


  public class PurchaseOrderItemDto : IOrderItemDto {

    public string UID {
      get; internal set;
    } = string.Empty;


    public string VendorProductUID {
      get; internal set;
    }


    public string ProductCode {
      get; internal set;
    } = string.Empty;


    public string ProductName {
      get; internal set;
    } = string.Empty;


    public string PresentationName {
      get; internal set;
    }


    public string Description {
      get; internal set;
    }


    public decimal PackingSmallBag {
      get; internal set;
    }


    public decimal PackagingSize {
      get; internal set;
    }


    public decimal Weight {
      get; internal set;
    }


    public object ProductAttrs {
      get; internal set;
    }


    public decimal Quantity {
      get; internal set;
    }


    public decimal ReceivedQuantity {
      get; internal set;
    }


    public decimal TotalUnits {
      get; internal set;
    }


    public decimal Price {
      get; internal set;
    }


    public decimal Total {
      get; internal set;
    }


    public string Notes {
      get; internal set;
    } = string.Empty;
    
  } // class PurchaseOrderItemDto


  public class PurchaseOrderTotal {

    public decimal ItemsTotal {
      get;
      internal set;
    }


    public decimal ShipmentTotal {
      get;
      internal set;
    }


    public decimal Discount {
      get;
      internal set;
    }


    public decimal Taxes {
      get;
      internal set;
    }


    public decimal OrderTotal {
      get;
      internal set;
    }


    public decimal ItemsCount {
      get;
      internal set;
    }

  }


  public class PurchaseOrderActions {


    public bool CanEdit {
      get; set;
    }


    public bool CanEditItems {
      get; set;
    }


    public bool CanDelete {
      get; set;
    }


    public bool CanClose {
      get; set;
    }


    public bool CanOpen {
      get; set;
    }


    public bool CanExport {
      get; set;
    }

  }

} // namespace Empiria.Trade.Inventory.PurchaseOrders.Adapters
