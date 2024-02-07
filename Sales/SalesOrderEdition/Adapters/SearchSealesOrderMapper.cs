/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Order Management                           Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Order.dll                    Pattern   : Mapper class                            *
*  Type     : OrderMapper                                License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Methods used to map Search Orders.                                                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Empiria.Trade.Core.Common;

namespace Empiria.Trade.Sales.Adapters {

  /// <summary> Methods used to map Serach Orders. </summary>
  static public class SearchSealesOrderMapper {

    #region Public methods

    static internal SearchSalesOrderDto Map(SearchOrderFields query, FixedList<SalesOrder> salesOrders ){
      return new SearchSalesOrderDto {
        Query = query,
        Columns = DataColumns(query),
        Entries = MapEntries(query, salesOrders) //SalesOrderMapper.MapBaseSalesOrders(salesOrders)
      };
    }

    #endregion Public methods

    #region Private methods

    static private FixedList<DataTableColumn> DataColumns(SearchOrderFields query) {
      List<DataTableColumn> columns = new List<DataTableColumn>();  

      columns.Add(new DataTableColumn("uid", "UID", "text"));
      columns.Add(new DataTableColumn("orderNumber", "No. Orden", "text"));
      columns.Add(new DataTableColumn("orderTime", "Fecha", "datetime"));
      columns.Add(new DataTableColumn("customerName", "Cliente", "text"));
      columns.Add(new DataTableColumn("statusName", "Estatus", "text"));
      columns.Add(new DataTableColumn("salesAgentName", "Vendedor", "text"));
      columns.Add(new DataTableColumn("orderTotal", "Total", "decimal"));
      columns.Add(new DataTableColumn("supplierName", "Proveedor", "Text"));
      columns.Add(new DataTableColumn("status", "Status", "Text"));

      switch (query.QueryType) {
        case QueryType.SalesAuthorization: {  
          columns.Add(new DataTableColumn("totalDebt", "Adeudo", "decimal")); break;
        }
        case QueryType.SalesPacking: {
          columns.Add(new DataTableColumn("weight", "Peso", "decimal"));
          columns.Add(new DataTableColumn("totalPackages", "No. Paquetes", "number"));
          break;
        }
      }

      return columns.ToFixedList();
    }

    static private FixedList<ISalesOrderDto> MapEntries(SearchOrderFields query, FixedList<SalesOrder> salesOrders) {
      switch (query.QueryType) {

        case QueryType.Sales: {
          return SalesOrderMapper.MapBaseSalesOrders(salesOrders);
        }
        case QueryType.SalesAuthorization: {
          return SalesOrderMapper.MapBaseSalesOrderAuthorizationList(salesOrders);
        }
        case QueryType.SalesPacking: {
          return SalesOrderMapper.MapBaseSalesOrderPackingList(salesOrders);
        }

        default: {
          throw Assertion.EnsureNoReachThisCode($"It is invalid queryType:{query.QueryType}");

        }

      }
    }
    #endregion Private methods

  } //  class SearchSealesOrderMapper

} // namespace Empiria.Trade.Sales.Adapters
