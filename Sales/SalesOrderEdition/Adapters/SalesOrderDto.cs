/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Order Management                           Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Sales.dll                    Pattern   : Data Transfer Object                    *
*  Type     : OrderDto                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return orders.                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using Empiria.Trade.Core.Adapters;
using Empiria.Trade.Orders;
using Empiria.Trade.Sales.ShippingAndHandling.Adapters;


namespace Empiria.Trade.Sales.Adapters {

  public interface ISalesOrderDto {
  }  // interface ISalesOrderDto


    /// <summary>Output DTO used to return orders. </summary>
    public class SalesOrderDto : ISalesOrderDto {

    public OrderDataDto OrderData {
      get; internal set;
    }


    public FixedList<SalesOrderItemDto> Items {
      get; internal set;
    }


    public AuthorizationDto Authorization {
      get; internal set;
    }

    public CustomerCreditDto CustomerCredit {
      get; internal set;
    }


    public ShippingEntryDto Shipping {
      get; internal set;
    }

    public IShippingAndHandling Packing {
      get; internal set;
    }

    public TransactionActionsDto Actions {
      get; internal set;
    }


  } // public class OrderDto

  // General Sales OrderInfo
  public class OrderDataDto {
    public string UID {
      get; internal set;
    }

    public string OrderNumber {
      get; internal set;
    }

    public DateTime OrderTime {
      get; internal set;
    }

    public string Notes {
      get; internal set;
    }

    public OrderStatus Status {
      get; internal set;
    }

    public string StatusName {
      get; internal set;
    }

    public NamedEntityDto Customer {
      get; internal set;
    }

    public CustomerShortAddressDto CustomerAddress {
      get; internal set;
    }

    public CustomerContactDto CustomerContact {
      get; internal set;
    }

    public string PriceList {
      get; internal set;
    }

    public NamedEntityDto Supplier {
      get; internal set;
    }

    public NamedEntityDto SalesAgent {
      get; internal set;
    }

    public string PaymentCondition {
      get; internal set;
    }

    public string ShippingMethod {
      get; internal set;
    }

    public int ItemsCount {
      get; internal set;
    }

    public decimal ItemsTotal {
      get; internal set;
    }

    public decimal Shipment {
      get; internal set;
    }

    public decimal Taxes {
      get; internal set;
    }

    public decimal OrderTotal {
      get; internal set;
    }

  } // class Data

  public class AuthorizationDto {

    public OrderAuthorizationStatus AuthorizationStatus {
      get; internal set;
    }

    public DateTime AuthorizationTime {
      get; internal set;
    }

    public int AuthorizatedById {
      get; internal set;
    }

  } // class AuthorizationDto


} // namespace Empiria.Trade.Sales.Adapters


