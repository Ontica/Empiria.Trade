/* Empiria Core  *********************************************************************************************
*                                                                                                            *
*  Module   : Trade Core                                   Component : Entity control enumerations           *
*  Assembly : Empiria.Trade.Core.dll                       Pattern   : Enumeration                           *
*  Type     : GeneralEnums                                 License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Enums to assing item type for reports elements.                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Core {
  
  
  internal class GeneralEnums {

  } // class GeneralEnums


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

  
  public enum QueryType {
    Empty,
    Sales,
    SalesAuthorization,
    SalesPacking,
    SalesShipping
  }


  public enum ShippingMethods {
    RutaLocal = 'L',
    RutaForanea = 'F',
    Ocurre = 'O',
    Paqueteria = 'P',
    None = 'N'
  }


  public enum PaymentCondition {
    Credito,
    Contado,
    None
  }


  /// <summary>Enums to assing item type for reports elements.</summary>
  public enum ReportItemType {

    Entry,

    Summary,

    Group,

    Total

  } // enum ReportItemType


  static public class EnumExtensions {

    static public ShippingMethods GetShippingMethodEnum(string shippingMethod) {
      switch (shippingMethod) {
        case "L":
        case "RutaLocal":
          return ShippingMethods.RutaLocal;
        case "F":
        case "RutaForanea":
          return ShippingMethods.RutaForanea;
        case "O":
        case "Ocurre":
          return ShippingMethods.Ocurre;
        case "P":
        case "Paqueteria":
          return ShippingMethods.Paqueteria;
        default:
          return ShippingMethods.None;
      }
    }

    
    static public OrderStatus GetOrderStatusEnum(string orderStatus) {

      switch (orderStatus) {
        case "Capturada":
        case "Captured":
          return OrderStatus.Captured;

        case "Aplicada":
        case "Applied":
          return OrderStatus.Applied;

        case "Autorizada":
        case "Autorizado":
        case "Authorized":
          return OrderStatus.Authorized;

        case "Surtiendo":
        case "Packing":
          return OrderStatus.Packing;

        case "Envío":
        case "Shipping":
          return OrderStatus.Shipping;

        case "Entregada":
        case "Delivery":
          return OrderStatus.Delivery;

        case "Cerrada":
        case "Closed":
          return OrderStatus.Closed;

        case "Cancelada":
        case "Cancelled":
          return OrderStatus.Cancelled;

        case "Pendiente":
        case "Pending":
          return OrderStatus.Pending;

        case "Por surtir":
        case "ToSupply":
          return OrderStatus.ToSupply;

        case "En proceso":
        case "InProgress":
          return OrderStatus.InProgress;

        case "Surtida":
        case "Suppled":
          return OrderStatus.Suppled;

        default:

          return OrderStatus.Empty;

      }
    }


    static public string GetOrderStatusName(this OrderStatus status) {

      switch (status) {
        case OrderStatus.Captured:
          return "Capturada";

        case OrderStatus.Applied:
          return "Aplicada";

        case OrderStatus.Authorized:
          return "Autorizada";

        case OrderStatus.Packing:
          return "Surtiendo";

        case OrderStatus.Shipping:
          return "Envío";

        case OrderStatus.Delivery:
          return "Entregada";

        case OrderStatus.Closed:
          return "cerrada";

        case OrderStatus.Cancelled:
          return "Cancelada";

        case OrderStatus.Empty:
          return "Empty";

        case OrderStatus.Pending:
          return "Pendiente";

        case OrderStatus.ToSupply:
          return "Por surtir";

        case OrderStatus.InProgress:
          return "En proceso";

        case OrderStatus.Suppled:
          return "Surtida";

        default:
          throw Assertion.EnsureNoReachThisCode($"Unrecognized status {status}");
      }
    }


    static public PaymentCondition GetPaymentConditionEnum(string shippingMethod) {
      switch (shippingMethod) {
        case "Credito":
        case "Crédito":
          return PaymentCondition.Credito;
        case "Contado":
          return PaymentCondition.Contado;
        default:
          return PaymentCondition.None;
      }
    }


    

  }

} // namespace Empiria.Trade.Core
