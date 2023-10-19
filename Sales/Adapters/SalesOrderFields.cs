/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Information Holder                      *
*  Type     : OrderFields                                License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Holds a order attributes list.                                                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.StateEnums;

using Empiria.Trade.Core;
using Empiria.Trade.Orders;

namespace Empiria.Trade.Sales.Adapters {

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
    } = OrderStatus.Captured;

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

    //public FixedList<SalesOrderItemsFields> OrderItems {
    //  get; set;
    //}

    #endregion Properties

    #region Internal methods

    public Party GetCustomer() {
      return Party.Parse(this.CustomerUID);
    }


    public Party GetSalesAgent() {
      return Party.Parse(this.SalesAgentUID);
    }

    public Party GetSupplier() {
      return Party.Parse(this.SupplierUID);

    }
       
    #endregion Internal methods

    #region Private methods


    #endregion Private methods


  }  //  internal class OrderFields

  public class SearchOrderFields {

    public DateTime FromDate {
      get; set;
    }

    public DateTime ToDate {
      get; set;
    }

    public EntityStatus Status {
      get; set;
    }

  } // class SearchOrderFields



} // namespace Empiria.Trade.Sales.Adapters
