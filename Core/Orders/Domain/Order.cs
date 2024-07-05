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
using System.ComponentModel;
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


    [DataField("ScheduledTime")]
    public DateTime ScheduledTime {
      get;
      protected set;
    }


    [DataField("ReceptionTime")]
    public DateTime ReceptionTime {
      get;
      protected set;
    }


    [DataField("ImportFormalEntry")]
    public string PedimentoImportacion {
      get;
      protected set;
    } = string.Empty;


    [DataField("BillOfLading")]
    public string CartaPorte {
      get;
      protected set;
    } = string.Empty;


    public virtual string Keywords {
      get {
        return EmpiriaString.BuildKeywords(
          OrderNumber, Customer.Name, SalesAgent.Name, PedimentoImportacion, CartaPorte);
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

    [DataField("ShippingMethod", Default = ShippingMethods.None)]
    public ShippingMethods ShippingMethod {
      get;
      protected set;
    } = ShippingMethods.None;

    [DataField("OrderStatus", Default = OrderStatus.Captured)]
    public OrderStatus Status {
      get;
      protected set;
    } = OrderStatus.Captured;

    public FixedList<OrderItem> OrderItems {
      get;
      protected set;
    }

    [DataField("OrderAuthorizationStatus", Default = OrderAuthorizationStatus.Empty)]
    public OrderAuthorizationStatus AuthorizationStatus {
      get;
      protected set;
    } = OrderAuthorizationStatus.Empty;

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

    [DataField("CustomerAddressId")]
    public CustomerAddress CustomerAddress {
      get;
      protected set;
    }

    [DataField("ContactId")]
    public CustomerContact CustomerContact {
      get;
      protected set;
    }

    #endregion Public properties

  }  // class Order


  public enum OrderStatus {
    Captured = 'C',
    Applied = 'A',
    Authorized = 'O',
    Packing = 'P',
    Shipping = 'S',
    Delivery = 'D',
    Closed = 'F',
    Cancelled = 'X',
    Empty = 'E',
    Pending = 'W',
    ToSupply = 'I',
    InProgress = 'U',
    Suppled = 'Y'

  } // enum OrderStatus


  static public class OrderStatusEnumExtensions {

    static public string GetOrderStatusName(this OrderStatus status) {
      
      switch (status) {
        case OrderStatus.Captured:
          return "";
         
        case OrderStatus.Applied:
          return "";

        case OrderStatus.Authorized:
          return "";

        case OrderStatus.Packing:
          return "";

        case OrderStatus.Shipping:
          return "";

        case OrderStatus.Delivery:
          return "";

        case OrderStatus.Closed:
          return "";

        case OrderStatus.Cancelled:
          return "";

        case OrderStatus.Empty:
          return "";

        case OrderStatus.Pending:
          return "";

        case OrderStatus.ToSupply:
          return "";

        case OrderStatus.InProgress:
          return "";

        case OrderStatus.Suppled:
          return "";

        default:
          throw Assertion.EnsureNoReachThisCode($"Unrecognized status {status}");
      }
    }

  } // class OrderStatusEnumExtensions


  public enum OrderAuthorizationStatus {
    Authorized = 'A',
    Pending = 'P',
    Empty = 'E',
    Pendings = 'W',
    ToSupply = 'I',
    InProgress = 'U',
    Suppled = 'S'
  } // enum AutorizationStatus

  public enum ShippingMethods {
    RutaLocal = 'L',
    RutaForanea = 'F',
    Ocurre = 'O',
    Paqueteria = 'P',
    None = 'N'
  }



}  // namespace Empiria.Trade.Orders
