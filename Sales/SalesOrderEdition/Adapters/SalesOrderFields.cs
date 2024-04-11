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
    Empty,
    Sales,
    SalesAuthorization,
    SalesPacking,
    SalesShipping
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
    } = string.Empty;

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

    public ShippingMethods ShippingMethod {
      get; set;
    } = ShippingMethods.None;

    public FixedList<SalesOrderItemsFields> Items {
      get; set;
    }

    public string customerAddressUID {
      get; set;
    } = "Empty";

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

    internal CustomerAddress GetCustomerAddress() {
      if (this.ShippingMethod == ShippingMethods.Ocurre) {
        return CustomerAddress.Empty;
      }

      return CustomerAddress.Parse(this.customerAddressUID);
    }

    internal CustomerContact GetCustomerContact() {
   
      if (String.IsNullOrEmpty(this.CustomerContactUID) ) {
        return CustomerContact.Empty;
      }
      return CustomerContact.Parse(this.CustomerContactUID);
    }

    #endregion Internal methods

  }  // class SalesOrderFields


  public class SearchOrderFields {

    public QueryType QueryType {
      get; set;
    } = QueryType.Empty;

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

    public ShippingMethods ShippingMethod {
      get; set;
    } = ShippingMethods.None;

    public string CustomerUID {
      get; set;
    } = String.Empty;

    public string ShippingStatus {
      get; set;
    } = String.Empty;

    } // class SearchOrderFields

  public class DeauthorizeFields {

    public string Notes {
      get; set;
    } = String.Empty;

  } //class DeauthorizeFields

} // namespace Empiria.Trade.Sales.Adapters
