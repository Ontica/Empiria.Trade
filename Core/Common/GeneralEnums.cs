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


  public enum SalesOrderStatus {
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


    static public SalesOrderStatus GetOrderStatusEnum(string orderStatus) {

      switch (orderStatus) {
        case "Capturada":
        case "Captured":
          return SalesOrderStatus.Captured;

        case "Aplicada":
        case "Applied":
          return SalesOrderStatus.Applied;

        case "Autorizada":
        case "Autorizado":
        case "Authorized":
          return SalesOrderStatus.Authorized;

        case "Surtiendo":
        case "Packing":
          return SalesOrderStatus.Packing;

        case "Envío":
        case "Shipping":
          return SalesOrderStatus.Shipping;

        case "Entregada":
        case "Delivery":
          return SalesOrderStatus.Delivery;

        case "Cerrada":
        case "Closed":
          return SalesOrderStatus.Closed;

        case "Cancelada":
        case "Cancelled":
          return SalesOrderStatus.Cancelled;

        case "Pendiente":
        case "Pending":
          return SalesOrderStatus.Pending;

        case "Por surtir":
        case "ToSupply":
          return SalesOrderStatus.ToSupply;

        case "En proceso":
        case "InProgress":
          return SalesOrderStatus.InProgress;

        case "Surtida":
        case "Suppled":
          return SalesOrderStatus.Suppled;

        default:

          return SalesOrderStatus.Empty;

      }
    }


    static public string GetOrderStatusName(this SalesOrderStatus status) {

      switch (status) {
        case SalesOrderStatus.Captured:
          return "Capturada";

        case SalesOrderStatus.Applied:
          return "Aplicada";

        case SalesOrderStatus.Authorized:
          return "Autorizada";

        case SalesOrderStatus.Packing:
          return "Surtiendo";

        case SalesOrderStatus.Shipping:
          return "Envío";

        case SalesOrderStatus.Delivery:
          return "Entregada";

        case SalesOrderStatus.Closed:
          return "cerrada";

        case SalesOrderStatus.Cancelled:
          return "Cancelada";

        case SalesOrderStatus.Empty:
          return "Empty";

        case SalesOrderStatus.Pending:
          return "Pendiente";

        case SalesOrderStatus.ToSupply:
          return "Por surtir";

        case SalesOrderStatus.InProgress:
          return "En proceso";

        case SalesOrderStatus.Suppled:
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
