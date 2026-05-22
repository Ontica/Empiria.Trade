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

using Empiria.Orders;

namespace Empiria.Trade.Procurement.Adapters {


  /// <summary>DTO used to manage purchase order fields.</summary>
  public class PurchaseOrderFields : OrderFields {

    public string SupplierUID {
      get; set;
    } = string.Empty;


    public string Notes {
      get; set;
    } = string.Empty;


    public string ShippingMethod {
      get; set;
    } = string.Empty;


    public DateTime ScheduledTime {
      get; set;
    } = ExecutionServer.DateMaxValue;


    public DateTime ReceptionTime {
      get; set;
    } = ExecutionServer.DateMaxValue;

  } // class PurchaseOrderFields


  /// <summary>DTO used to manage purchase order item fields.</summary>
  public class PurchaseOrderItemFields : OrderItemFields {

    public string Product {
      get; set;
    }


    public string VendorProductUID {
      get; set;
    } = string.Empty;


    public decimal Price {
      get; set;
    }


    public decimal Weight {
      get; set;
    }


    public decimal SalesPrice {
      get; set;
    }


    public decimal Taxes {
      get; set;
    }


    public decimal Total {
      get; set;
    }


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
