/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Sales Order Management                     Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Sales.dll                    Pattern   : Information Holder                      *
*  Type     : SalesOrderFields                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Input DTO for sales orders.                                                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Trade.Core;
using Empiria.Trade.Orders;

namespace Empiria.Trade.Sales.Adapters {

  public enum QueryType {
    Sales,
    SalesAuthorization,
    SalesPacking,
    SalesShipping,
    SalesOrdersAuthorization, //Temporal 
    SalesOrders, //temporal
    SalesOrdersPacking
  }

  /// <summary>Input DTO for sales orders.</summary>
  public class SalesOrderFields {

    

    #region Constructors and parsers

    public SalesOrderFields() {
      // Required by Empiria Framework.
    }

    #endregion Constructors and parsers

    #region Properties

    public string UID {
      get; set;
    } = string.Empty;


    public string OrderNumber {
      get; set;
    } = string.Empty;


    public DateTime OrderTime {
      get; set;
    } = DateTime.Today;

    public string Notes {
      get; set;
    } = string.Empty;

    public OrderStatus Status {
      get; set;
    }

    public string CustomerUID {
      get; set;
    }

    public string CustomerContactUID {
      get; set;
    }

    public string SupplierUID {
      get; set;
    }

    public string SalesAgentUID {
      get; set;
    }

    public string PaymentCondition {
      get; set;
    }

    public string Shipment {
      get; set;
    }

    public string ShippingMethod {
      get; set;
    }

    public FixedList<SalesOrderItemsFields> Items {
      get; set;
    }

    #endregion Properties

    #region Internal methods

    internal Party GetCustomer() {
      return Party.Parse(this.CustomerUID);
    }

    internal Party GetSalesAgent() {
      return Party.Parse(this.SalesAgentUID);
    }

    internal  Party GetSupplier() {
      return Party.Parse(this.SupplierUID);
    }

    #endregion Internal methods

  }  // class SalesOrderFields


  public class SearchOrderFields {

    public string QueryType {
      get; set;
    } = string.Empty;

    public string Keywords {
      get; set;
    } = string.Empty;

    public DateTime FromDate {
      get; set;
    } = new DateTime(2020, 1, 1);

    public DateTime ToDate {
      get; set;
    } = new DateTime(2049, 12, 31);

    public OrderStatus Status {
      get; set;
    } = OrderStatus.Empty;

  } // class SearchOrderFields

} // namespace Empiria.Trade.Sales.Adapters
