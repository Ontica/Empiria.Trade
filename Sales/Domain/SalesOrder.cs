﻿/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Order Management                           Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Order.dll                    Pattern   : Partitioned Type / Information Holder   *
*  Type     : Order                                      License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a Order.                                                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Trade.Sales.Data;
using Empiria.Trade.Sales.Adapters;
using System.Collections.Generic;

using Empiria.Trade.Orders;

namespace Empiria.Trade.Sales {

  /// <summary>Represent Order</summary>
  public class SalesOrder : Order {

    #region Constructors and parsers

    public SalesOrder() {
      //no-op
    }

    public SalesOrder(SalesOrderFields fields) {
      Update(fields);
    }

    static public new SalesOrder Parse(int id) {
      return BaseObject.ParseId<SalesOrder>(id);
    }

    static public new SalesOrder Parse(string uid) {
      return BaseObject.ParseKey<SalesOrder>(uid);
    }

    #endregion Constructors and parsers

    #region Public properties

    public FixedList<SalesOrderItem> SalesOrderItems {
      get; private set;
    }

    public int ItemsCount {
      get; private set;
    }

   
  public decimal ItemsTotal {
      get; private set;
  }

  public string Shipment {
      get; private set;
  }
  
  public decimal Discount {
      get; private set;
  }

  public decimal Taxes {
      get; private set;
  }
  
  public decimal OrderTotal {
      get; private set;
  }

  public string PaymentCondition {
      get; private set;
   }


    #endregion


    #region Public methods

    protected override void OnSave() {
      if (IsNew) {
        OrderNumber = "P-" + EmpiriaString.BuildRandomString(10);
        OrderTime = DateTime.Now;
      }
      SalesOrderData.Write(this);
     
    }

    internal void Update(SalesOrderFields fields) {
      this.OrderTypeId = 1025;
      this.Customer = fields.GetCustomer();
      this.Supplier = fields.GetSupplier();
      this.SalesAgent = fields.GetSalesAgent();
      this.Notes = fields.Notes;
      this.Status = fields.Status;
      this.Shipment = fields.Shipment;
      this.PaymentCondition = fields.PaymentCondition;
    }

    #endregion Public methods

    #region Private methods

    
    #endregion

  }  //  class SalesOrder

}  // namespace Empiria.Trade.Sales
