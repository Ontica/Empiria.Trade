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

using Empiria.Trade.Orders;


namespace Empiria.Trade.Sales.Adapters {

  ///<summary>Sales order dto interface</summary>
  public interface ISalesOrderDto {
  }

  /// <summary>Output DTO used to return orders. </summary>
  public class BaseSalesOrderDto : ISalesOrderDto {
    public string UID {
      get; internal set;
    }

    public string OrderNumber {
      get; internal set;
    }

    public DateTime OrderTime {
      get; internal set;
    }  

    public string CustomerName {
      get; internal set;
    }      

    public string SupplierName {
      get; internal set;
    }

    public string SalesAgentName {
      get; internal set;
    }

    public decimal OrderTotal {
      get; internal set;
    }

    public OrderStatus Status {
      get; internal set;
    }

    public string StatusName {
      get; internal set;
    }

    //public OrderAuthorizationStatus AuthorizationStatus {
    //  get; internal set;
    //}


  } // Output DTO used to return  basic orders.

  /// Output DTO used to return base autorization orders.
  public class BaseSalesOrdersAuthorizationDto : BaseSalesOrderDto {

    public decimal TotalDebt {
      get; internal set;
    }

  } //  Output DTO used to return basico autorization orders.

  /// Output DTO used to return base packing orders.
  public class BaseSalesOrderPackingDto : BaseSalesOrderDto {

    public decimal Weight {
      get; internal set;
    }

    public int TotalPackages {
      get; internal set;
    }

  } //  class SalesOrdersPackingDto


    /// <summary>Output DTO used to return orders. </summary>
    public class SalesOrderDto : ISalesOrderDto {

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

    public NamedEntityDto Customer {
      get; internal set;
    }

    public string PriceList {
      get; internal set;
    }

    ////public PartyContactsDto CustomerContact {
    ////  get; internal set;
    ////}

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

    public FixedList<SalesOrderItemDto> Items {
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

    public OrderAuthorizationStatus AuthorizationStatus {
      get; internal set;
    }

    public DateTime AuthorizationTime {
      get; internal set;
    }

    public int AuthorizatedById {
      get; internal set;
    }

    public CustomerCreditDto CustomerCredit {
      get; internal set;
    }

    public decimal Weight {
      get; internal set;
    }

    public int TotalPackages {
      get; internal set;
    }

    public OrderActionsDto Actions {
      get; internal set;
    }

  } // public class OrderDto


  public class OrderActionsDto {

    public Boolean CanEdit {
      get; internal set;
    } = true;

    public Boolean CanApply {
      get; internal set;
    } = true;

    public Boolean CanAuthorize {
      get; internal set;
    } = true;

    public Boolean CanPackaging {
      get; internal set;
    } = true;

    public Boolean CanSelectCarrier {
      get; internal set;
    } = true;

    public Boolean CanShipping {
      get; internal set;
    } = true;

    public Boolean CanClose {
      get; internal set;
    } = true;

  }  //  class OrderActionsDto


} // namespace Empiria.Trade.Sales.Adapters


