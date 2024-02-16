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

    
      columns.Add(new DataTableColumn("orderNumber", "No. Orden", "text-link"));
      columns.Add(new DataTableColumn("orderTime", "Fecha", "date"));
      columns.Add(new DataTableColumn("customerName", "Cliente", "text"));
      columns.Add(new DataTableColumn("statusName", "Estatus", "text-tag"));
      columns.Add(new DataTableColumn("salesAgentName", "Vendedor", "text"));
      columns.Add(new DataTableColumn("orderTotal", "Total", "decimal"));

      if ((query.ShippingMethod == "Paqueteria") && (query.Status == Orders.OrderStatus.Shipping)) {
        columns.Add(new DataTableColumn("shipment", "Envío", "text-tag"));
      }

        switch (query.QueryType) {
        case QueryType.SalesAuthorization: {  
          columns.Add(new DataTableColumn("totalDebt", "Adeudo", "decimal")); break;
        }
        case QueryType.SalesPacking: {
          columns.Add(new DataTableColumn("weight", "Peso", "decimal", 2));
          columns.Add(new DataTableColumn("totalPackages", "No. Paquetes", "decimal",0));
          break;
        }
      }

      return columns.ToFixedList();
    }

    static private FixedList<ISalesOrderDto> MapEntries(SearchOrderFields query, FixedList<SalesOrder> salesOrders) {
      switch (query.QueryType) {

        case QueryType.Sales: {
          if ((query.ShippingMethod == "Paqueteria") && (query.Status == Orders.OrderStatus.Shipping)) {
            return MapBaseSalesOrdersShipmentStatus(salesOrders);
          } else {
            return MapBaseSalesOrders(salesOrders);
          }

        }
        case QueryType.SalesAuthorization: {
          return MapBaseSalesOrderAuthorizationList(salesOrders);
        }
        case QueryType.SalesPacking: {
          return MapBaseSalesOrderPackingList(salesOrders);
        }

        default: {
          throw Assertion.EnsureNoReachThisCode($"It is invalid queryType:{query.QueryType}");

        }

      }
    }

    static public FixedList<ISalesOrderDto> MapBaseSalesOrdersShipmentStatus(FixedList<SalesOrder> salesOrders) {
      List<ISalesOrderDto> baseSalesOrderDtoList = new List<ISalesOrderDto>();

      foreach (var salesOrder in salesOrders) {
        baseSalesOrderDtoList.Add(MapBaseSalesOrderShipmentStatus(salesOrder));
      }

      return baseSalesOrderDtoList.ToFixedList();
    }

    static public FixedList<ISalesOrderDto> MapBaseSalesOrders(FixedList<SalesOrder> salesOrders) {
      List<ISalesOrderDto> baseSalesOrderDtoList = new List<ISalesOrderDto>();

      foreach (var salesOrder in salesOrders) {
        baseSalesOrderDtoList.Add(MapBaseSalesOrder(salesOrder));
      }

      return baseSalesOrderDtoList.ToFixedList();
    }

    static public FixedList<ISalesOrderDto> MapBaseSalesOrderAuthorizationList(FixedList<SalesOrder> salesOrders) {
      List<ISalesOrderDto> salesOrderDtoList = new List<ISalesOrderDto>();

      foreach (var salesOrder in salesOrders) {
        salesOrderDtoList.Add(MapBaseSalesOrderAuthorization(salesOrder));
      }

      return salesOrderDtoList.ToFixedList();
    }

    static public FixedList<ISalesOrderDto> MapBaseSalesOrderPackingList(FixedList<SalesOrder> salesOrders) {
      List<ISalesOrderDto> salesOrderDtoList = new List<ISalesOrderDto>();

      foreach (var salesOrder in salesOrders) {
        salesOrderDtoList.Add(MapBaseSalesOrderPacking(salesOrder));
      }

      return salesOrderDtoList.ToFixedList();
    }



    private static ISalesOrderDto MapBaseSalesOrderShipmentStatus(SalesOrder order) {
      var dto = new BaseSalesOrderShipmentDto {
        UID = order.UID,
        OrderNumber = order.OrderNumber,
        OrderTime = order.OrderTime,
        CustomerName = order.Customer.Name,
        SupplierName = order.Supplier.Name,
        SalesAgentName = order.SalesAgent.Name,
        OrderTotal = order.OrderTotal,
        Status = order.Status,
        Shipment = "Pendiente",
        StatusName = SalesOrderMapper.MapOrderStatus(order.Status.ToString())
      };

      return dto;
    }

    static public ISalesOrderDto MapBaseSalesOrder(SalesOrder order) {
      var dto = new BaseSalesOrderDto {
        UID = order.UID,
        OrderNumber = order.OrderNumber,
        OrderTime = order.OrderTime,
        CustomerName = order.Customer.Name,
        SupplierName = order.Supplier.Name,
        SalesAgentName = order.SalesAgent.Name,
        OrderTotal = order.OrderTotal,
        Status = order.Status,
        StatusName = SalesOrderMapper.MapOrderStatus(order.Status.ToString())
      };

      return dto;
    }

    static public ISalesOrderDto MapBaseSalesOrderAuthorization(SalesOrder order) {
      var dto = new BaseSalesOrdersAuthorizationDto {
        UID = order.UID,
        OrderNumber = order.OrderNumber,
        OrderTime = order.OrderTime,
        CustomerName = order.Customer.Name,
        SupplierName = order.Supplier.Name,
        SalesAgentName = order.SalesAgent.Name,
        OrderTotal = order.OrderTotal,
        TotalDebt = order.TotalDebt,
        Status = order.Status,
        StatusName = MapOrderAuthorizationStatus(order.AuthorizationStatus.ToString())
      };

      return dto;
    }

    static public ISalesOrderDto MapBaseSalesOrderPacking(SalesOrder order) {
      var dto = new BaseSalesOrderPackingDto {
        UID = order.UID,
        OrderNumber = order.OrderNumber,
        OrderTime = order.OrderTime,
        CustomerName = order.Customer.Name,
        SupplierName = order.Supplier.Name,
        SalesAgentName = order.SalesAgent.Name,
        OrderTotal = order.OrderTotal,
        Weight = order.Weight,
        TotalPackages = order.TotalPackages,
        Status = order.Status,
        StatusName = MapOrderPackingStatus(order.AuthorizationStatus.ToString())
      };

      return dto;
    }

    static private string MapOrderAuthorizationStatus(string status) {
      switch (status) {
        case "Authorized":
          return "Autorizado";
        case "Pending":
          return "Por Autorizar";
        default:
          return "Por Autorizar";
      }

    }

    static private string MapOrderPackingStatus(string status) {
      switch (status) {
        case "ToSupply":
          return "Por surtir";
        case "InProgress":
          return "En proceso";
        case "Suppled":
          return "Surtido";
        default:
          return "Por surtir";
      }

    }


    #endregion Private methods

  } //  class SearchSealesOrderMapper

} // namespace Empiria.Trade.Sales.Adapters
