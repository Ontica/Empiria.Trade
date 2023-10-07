/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Order Management                           Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Order.dll                    Pattern   : Partitioned Type / Information Holder   *
*  Type     : Order                                      License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a Order.                                                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Trade.Core.Domain;

using Empiria.Trade.Sales.Data;
using Empiria.Trade.Sales.Adapters;

namespace Empiria.Trade.Sales.Domain {

  /// <summary>Represent Order</summary>
  internal class Order {

    #region Constructors and parsers

    public Order() {
      //no-op
    }

    public Order(OrderFields fields) {
      Create(fields);
    }

    #endregion Constructors and parsers

    #region Public properties

    [DataField("OrderId")]
    public int OrderId {
      get;
      private set;
    }


    [DataField("OrderUID")]
    public string OrderUID {
      get;
      private set;
    }

    [DataField("CustomerId")]
    public Party Customer {
      get;
      private set;
    }

    [DataField("SupplierId")]
    public Party Supplier {
      get;
      private set;
    }
    [DataField("SalesAgentId")]
    public Party SalesAgent {
      get;
      private set;
    }

    [DataField("OrderNumber")]
    public string OrderNumber {
      get;
      private set;
    }

    [DataField("OrderTime")]
    public DateTime OrderTime {
      get;
      private set;
    }

    [DataField("OrderNotes")]
    public string Notes {
      get;
      private set;
    }

    [DataField("OrderKeywords")]
    public string Keywords {
      get;
      private set;
    }

    [DataField("OrderStatus")]
    public char Status {
      get;
      private set;
    }

    #endregion Public properties

    #region Public methods

    public void Save() {
      OrderData.Write(this);
    }

    #endregion Public methos

    #region Private methods


    private void Create(OrderFields fields) {
      this.OrderId = 1;
      this.OrderUID = Guid.NewGuid().ToString();
      this.Customer = fields.GetCustomer();
      this.Supplier = fields.GetSupplier();
      this.SalesAgent = fields.GetSalesAgent();
      this.OrderNumber = "afsaesdfsafa";
      this.OrderTime = DateTime.Today;
      this.Notes = fields.Notes;
      this.Keywords = "";
      this.Status = 'O';

    }
       

   

       

    #endregion

  }

}
