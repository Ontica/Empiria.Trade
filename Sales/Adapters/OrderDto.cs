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

namespace Empiria.Trade.Sales.Adapters {

  /// <summary>Output DTO used to return orders. </summary>
  public class OrderDto {

    public string OrderUID {
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

    public string Status {
      get; internal set;
    }

    public CustomerDto Customer {
      get; internal set;
    }

    public CustomerContactDto CustomerContact {
      get; internal set;
    }

    public SupplierDto Supplier {
      get; internal set;
    }

    public SalesAgentDto SalesAgent {
       get; internal set;
    }

    public string PaymentCondition {
      get; internal set;
    }

  } // public class OrderDto

  /// <summary>Output DTO for Customer </summary>
  public class CustomerDto {

    public string UID {
      get; internal set;
    }

    public string Name {
      get; internal set;
    }

  } // class CustomerDto

  public class CustomerContactDto {

    public string UID {
      get; internal set;
    }

    public string Name {
      get; internal set;
    }

    public string Phone {
      get; internal set;
    }

  } // class SupplierDto

  public class SupplierDto {

    public string UID {
      get; internal set;
    }

    public string Name {
      get; internal set;
    }

  } // class SupplierDto

  public class SalesAgentDto {

    public string UID {
      get; internal set;
    }

    public string Name {
      get; internal set;
    }

  } // class SupplierDto



} // namespace Empiria.Trade.Sales.Adapters


