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
using Empiria.Financial;
using Empiria.Orders;
using Empiria.Parties;
using Empiria.StateEnums;

namespace Empiria.Trade.Core {
  
  /// <summary>Input DTO for sales orders.</summary>
  public class SalesOrderFields : OrderFields {

    #region Constructors and parsers

    public SalesOrderFields() {
      // Required by Empiria Framework.
    }

    #endregion Constructors and parsers

    #region Properties

    public string OrderNumber {
      get; set;
    } = string.Empty;


    public DateTime OrderTime {
      get; set;
    } = DateTime.Today;

    //Notes = string Observations

    //OrderStatus
    public OrderStatus Status {
      get; set;
    }


    public string CustomerUID {
      get; set;
    } = string.Empty;


    public string CustomerContactUID {
      get; set;
    } = string.Empty;


    public string CustomerAddressUID {
      get; set;
    } = "Empty";


    public string SupplierUID {
      get; set;
    } = string.Empty;


    public string SalesAgentUID {
      get; set;
    } = string.Empty;


    public string Shipment {
      get; set;
    } = string.Empty;


    public string Notes {
      get; set;
    } = string.Empty;


    public ShippingMethods ShippingMethod {
      get; set;
    } = ShippingMethods.RutaLocal;


    public FixedList<SalesOrderItemsFields> Items {
      get; set;
    }


    public bool CanUpdateOrder {
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


    internal Party GetSupplier() {
      return Party.Parse(this.SupplierUID);
    }


    internal CustomerAddress GetCustomerAddress() {
      if (this.ShippingMethod == ShippingMethods.Ocurre) {
        return CustomerAddress.Empty;
      }

      return CustomerAddress.Parse(this.CustomerAddressUID);
    }


    internal CustomerContact GetCustomerContact() {
   
      if (String.IsNullOrEmpty(this.CustomerContactUID) ) {
        return CustomerContact.Empty;
      }
      return CustomerContact.Parse(this.CustomerContactUID);
    }


    public void MapToOrderFields(OrderType orderType) {

      this.OrderTypeUID = orderType.UID;
      this.ProviderUID = this.SupplierUID;
      this.RequestedByUID = Party.ParseWithContact(ExecutionServer.CurrentContact).UID;
      this.ResponsibleUID = this.SalesAgentUID;
      //TODO INVESTIGAR SI EL CustomerUID DEBERIA DE SER EL BeneficiaryUID
      this.BeneficiaryUID = this.CustomerUID;
      this.Name = this.OrderNumber;
      this.Observations = this.Notes;
      this.StartDate = DateTime.Now;
      this.CurrencyUID = this.CurrencyUID == string.Empty ? Currency.Default.UID : this.CurrencyUID;
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
    } = DateTime.Now.AddDays(-30);

    public DateTime ToDate {
      get; set;
    } = DateTime.Now;

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


  static public class SearchOrderFieldsExtensions {

    static public void EnsureIsValidSearch(this SearchOrderFields fields) {

      Assertion.Require(fields.FromDate > DateTime.MinValue && fields.ToDate > DateTime.MinValue,
                        "Favor de especificar fechas para realizar la búsqueda.");
    }

  }

} // namespace Empiria.Trade.Sales.Adapters
