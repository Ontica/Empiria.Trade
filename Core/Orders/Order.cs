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
using Empiria.Json;
using Empiria.Trade.Core;


namespace Empiria.Trade.Orders {

  /// <summary>Represent Order</summary>
  abstract public class Order : BaseObject {

    #region Constructors and parsers

    protected Order() {
      //no-op
    }

    static public Order Empty => BaseObject.ParseEmpty<Order>();

    static public Order Parse(int id) {
      return BaseObject.ParseId<Order>(id);
    }

    static public Order Parse(string uid) {
      return BaseObject.ParseKey<Order>(uid);
    }

    #endregion Constructors and parsers

    #region Public properties

    [DataField("OrderTypeId")]
    public int OrderTypeId {
      get;
      protected set;
    }

    [DataField("CustomerId")]
    public Party Customer {
      get;
      protected set;
    }

    [DataField("SupplierId")]
    public Party Supplier {
      get;
      protected set;
    }

    [DataField("SalesAgentId")]
    public Party SalesAgent {
      get;
      protected set;
    }

    [DataField("OrderNumber")]
    public string OrderNumber {
      get;
      protected set;
    }

    [DataField("OrderTime")]
    public DateTime OrderTime {
      get;
      protected set;
    }

    [DataField("OrderNotes")]
    public string Notes {
      get;
      protected set;
    }

    public virtual string Keywords {
      get {
        return EmpiriaString.BuildKeywords(OrderNumber, Customer.Name, SalesAgent.Name);
      }
    }

    [DataField("OrderExtData", IsOptional = true)]
    public JsonObject ExtData {
      get;
      protected set;
    } = new JsonObject();

    public string PaymentCondition {
      get {
        return this.ExtData.Get("PaymentCondition", string.Empty);
      }
      protected set {
        this.ExtData.SetIfValue("PaymentCondition", value);
      }
    } 

    public string ShippingMethod {
      get {
        return this.ExtData.Get("ShippingMethod", string.Empty);
      }
      protected set {
        this.ExtData.SetIfValue("ShippingMethod", value);
      }
    }

    [DataField("OrderStatus", Default = OrderStatus.Captured)]
    public OrderStatus Status {
      get;
      protected set;
    } = OrderStatus.Captured;

    public FixedList<OrderItem> OrderItems {
      get;
      protected set;
    }

    [DataField("OrderAuthorizationStatus", Default = 'E')]
    public char AuthorizationStatus {
      get;
      protected set;
    } = 'E';

    [DataField("OrderAuthorizationTime")]
    public DateTime AuthorizationTime {
      get;
      protected set;
    } = DateTime.MaxValue;

    [DataField("OrderAuthorizatedById")]
    public int AuthorizatedById {
      get;
      protected set;
    }

    #endregion Public properties

  }  // class Order
  

  public enum OrderStatus {
    Captured = 'C',
    Applied = 'A',
    Closed  = 'F',
    Cancelled = 'X',
    Empty = 'E'
  } // enum OrderStatus

}  // namespace Empiria.Trade.Orders
