/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Data Transfer Object                    *
*  Type     : PurchaseOrderFields                        License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : DTO used to manage purchase order fields.                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using Empiria.Financial;
using Empiria.Orders;

namespace Empiria.Trade.Core {


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


    public virtual void EnsureIsValid() {
      
      var orderType = OrderType.Parse(OrderTypeUID);

      Assertion.Require(CurrencyUID != string.Empty,
                        "Favor de especificar moneda.");

      if (CurrencyUID != Currency.Default.UID && orderType.Equals(OrderType.PurchaseOrder)) {
        Assertion.Require(ExchangeRate > 0 && ExchangeRate != decimal.One,
                         "El tipo de cambio debe ser positivo y distinto a uno.");
      }

    }

  } // class PurchaseOrderFields


  /// <summary>DTO used to manage purchase order item fields.</summary>
  public class PurchaseOrderItemFields : OrderItemFields {

    public string Product {
      get; set;
    }


    public string VendorProductUID {
      get; set;
    } = string.Empty;


    public string Notes {
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


} // namespace Empiria.Trade.Core
