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

using Empiria.Trade.Core;

using Empiria.StateEnums;

namespace Empiria.Trade.Orders {

  /// <summary>Represent Order</summary>
  abstract public class Order : BaseObject {

    #region Constructors and parsers

    protected Order() {
      //no-op
    }

    static public Order Parse(int id) {
      return BaseObject.ParseId<Order>(id);
    }

    static public Order Parse(string uid) {
      return BaseObject.ParseKey<Order>(uid);
    }

    #endregion Constructors and parsers

    #region Public properties

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

    [DataField("OrderStatus", Default = EntityStatus.Active)]
    public EntityStatus Status {
      get;
      protected set;
    }

    public FixedList<OrderItem> OrderItems {
      get;
      protected set;
    }

    #endregion Public properties

  }  // class Order

}  // namespace Empiria.Trade.Orders
