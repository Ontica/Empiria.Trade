/* Empiria Trade *********************************************************************************************
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


    public string SupplierUID {
      get; set;
    } = string.Empty;


    public string Notes {
      get; set;
    } = string.Empty;


    public string PaymentCondition {
      get; set;
    } = string.Empty;


    public ShippingMethods ShippingMethod {
      get; set;
    } = ShippingMethods.None;


    public DateTime ScheduledTime {
      get; set;
    } = ExecutionServer.DateMaxValue;


    public DateTime ReceptionTime {
      get; set;
    } = ExecutionServer.DateMaxValue;


    //public string CustomerUID {
    //  get; internal set;
    //} = string.Empty;


    //public string CustomerAddressUID {
    //  get; internal set;
    //} = string.Empty;


    //public string CustomerContactUID {
    //  get;
    //  internal set;
    //} = string.Empty;


    //public string SalesAgentUID {
    //  get; internal set;
    //} = string.Empty;


    //public OrderAuthorizationStatus OrderAuthorizationStatus {
    //  get; internal set;
    //} = OrderAuthorizationStatus.Empty;


    //public string ImportFormalEntry {
    //  get; internal set;
    //} = string.Empty;


    //public string BillOfLading {
    //  get; internal set;
    //} = string.Empty;


  } // class PurchaseOrderFields


  /// <summary>DTO used to manage purchase order item fields.</summary>
  public class PurchaseOrderItemFields {


    public string UID {
      get; internal set;
    } = string.Empty;


    public string VendorProductUID {
      get; internal set;
    } = string.Empty;


    public decimal Quantity {
      get; internal set;
    }


    public decimal BasePrice {
      get; internal set;
    }


    public decimal SalesPrice {
      get; internal set;
    }


    public decimal Discount {
      get; internal set;
    }


    public decimal Taxes {
      get; internal set;
    }


    public decimal Total {
      get; internal set;
    }


    public string Notes {
      get; internal set;
    } = string.Empty;


    public DateTime ScheduledTime {
      get; internal set;
    } = ExecutionServer.DateMaxValue;


    public DateTime ReceptionTime {
      get; internal set;
    } = ExecutionServer.DateMaxValue;


    public string Reviewed {
      get; internal set;
    } = string.Empty;


  } // class PurchaseOrderItemFields


} // namespace Empiria.Trade.Inventory.PurchaseOrders.Adapters
