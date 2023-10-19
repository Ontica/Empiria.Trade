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


namespace Empiria.Trade.Sales.Adapters {

  public class SalesOrderFields {

    #region Constructors and parsers

    public SalesOrderFields() {
      // Required by Empiria Framework.
    }

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


    public EntityStatus Status {
      get; set;
    } = EntityStatus.Active;


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

    #endregion Constructors and parsers

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

 
} // namespace Empiria.Trade.Sales.Adapters
