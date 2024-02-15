/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Order Management                           Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Sales.dll                    Pattern   : Data Transfer Object                    *
*  Type     : OrderDto                                   License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return serach orders info.                                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;

using Newtonsoft.Json;

using Empiria.Json;
using Empiria.Trade.Core.Common;
using Empiria.Trade.Orders;

// <summary>Output DTO used to return serach orders info. </summary>
namespace Empiria.Trade.Sales.Adapters {


  public class SearchSalesOrderDto {
        
    public SearchOrderFields Query {
      get; internal set;
    }

    public FixedList<DataTableColumn> Columns {
      get; internal set;
    }

   public FixedList<ISalesOrderDto> Entries {
      get; internal set;
   }

  } // class SearchSalesOrderDto

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


  } // Output DTO used to return  basic orders.

  /// Output DTO used to return Shipment status 
  public class BaseSalesOrderShipmentDto : BaseSalesOrderDto {

    public string Shipment {
      get; internal set;
    }

  }

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


} // namespace Empiria.Trade.Sales.Adapters
