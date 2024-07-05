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
using Empiria.Trade.Orders;

namespace Empiria.Trade.Procurement.Adapters {


  /// <summary>Output DTO used to return purchase order data.</summary>
  public class PurchaseOrderDto {


    public string UID {
      get; internal set;
    } = string.Empty;


    public string OrderNumber {
      get; internal set;
    } = string.Empty;


    public NamedEntityDto Supplier {
      get; internal set;
    } = new NamedEntityDto("", "");


    public NamedEntityDto Customer {
      get; internal set;
    } = new NamedEntityDto("", "");


    public NamedEntityDto Status {
      get; internal set;
    } = new NamedEntityDto("", "");


    //public NamedEntityDto Currency {
    //  get; internal set;
    //}


    public string Notes {
      get; internal set;
    } = string.Empty;


    public string PaymentCondition {
      get; set;
    }


    public ShippingMethods ShippingMethod {
      get;
      internal set;
    }


    public DateTime OrderTime {
      get; set;
    } = DateTime.MaxValue;


    public DateTime ScheduledTime {
      get; set;
    } = DateTime.MaxValue;


    public DateTime ReceptionTime {
      get; set;
    } = DateTime.MaxValue;


    public FixedList<PurchaseOrderItemDto> Items {
      get; internal set;
    } = new FixedList<PurchaseOrderItemDto>();


    public PurchaseOrderTotal Totals {
      get; set;
    } = new PurchaseOrderTotal();


    public PurchaseOrderActions Actions {
      get; set;
    } = new PurchaseOrderActions();
    

  } // class PurchaseOrderDto


  public class PurchaseOrderItemDto {


    public NamedEntityDto MyProperty {
      get; set;
    } = new NamedEntityDto("", "");


    public NamedEntityDto MyProperty2 {
      get; set;
    } = new NamedEntityDto("", "");


    public NamedEntityDto MyProperty3 {
      get; set;
    } = new NamedEntityDto("", "");


    public NamedEntityDto MyProperty4 {
      get; set;
    } = new NamedEntityDto("", "");


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

  }


  public class PurchaseOrderActions {
  }


} // namespace Empiria.Trade.Inventory.PurchaseOrders.Adapters
