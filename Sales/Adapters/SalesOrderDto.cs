﻿/* Empiria Trade *********************************************************************************************
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

namespace Empiria.Trade.Sales.Adapters {

  /// <summary>Output DTO used to return orders. </summary>
  public class SalesOrderDto {

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

    public string Status {
      get; internal set;
    }

    public NamedEntityDto Customer {
      get; internal set;
    }

    public PartyContactsDto CustomerContact {
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

  } // public class OrderDto

} // namespace Empiria.Trade.Sales.Adapters


