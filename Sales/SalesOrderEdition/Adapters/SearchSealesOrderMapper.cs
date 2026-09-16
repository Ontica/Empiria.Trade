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

using Empiria.Trade.Core.Common;
using Empiria.Trade.Sales.ShippingAndHandling.UseCases;
using Empiria.Trade.Sales.ShippingAndHandling;

using Empiria.Trade.Financial.UseCases;
using Empiria.Trade.Core;
using Empiria.Trade.Packaging.UseCases;

namespace Empiria.Trade.Sales.Adapters {

  /// <summary> Methods used to map Serach Orders. </summary>
  static public class SearchSealesOrderMapper {

    #region Public methods

    static internal SearchSalesOrderDto Map(SearchOrderFields query, FixedList<SalesOrder> salesOrders) {
      return new SearchSalesOrderDto {
        Query = query,
        Columns = DataColumns(query),
        Entries = MapEntries(query, salesOrders)
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

      if (query.Status == OrderStatus.Shipping) {
        columns.Add(new DataTableColumn("shippingStatus", "Envío", "text-tag", 0, true));
      }

      switch (query.QueryType) {
        case QueryType.SalesAuthorization: {
          columns.Add(new DataTableColumn("totalDebt", "Adeudo", "decimal"));
          columns.Add(new DataTableColumn("totalCredit", "Crédito", "decimal"));
          break;
        }
        case QueryType.SalesPacking: {
          columns.Add(new DataTableColumn("weight", "Peso", "decimal", 2));
          columns.Add(new DataTableColumn("totalPackages", "No. Paquetes", "decimal", 0));
          break;
        }
      }

      return columns.ToFixedList();
    }

    static public FixedList<ISalesOrderDto> MapEntries(SearchOrderFields query, FixedList<SalesOrder> salesOrders) {
      switch (query.QueryType) {

        case QueryType.Sales: {
          
          if ((query.ShippingStatus != string.Empty) && (query.Status == OrderStatus.Shipping)) {
            
            var list = MapBaseSalesOrdersShipmentStatus(salesOrders);
            var orders = list.ConvertAll(o => (BaseSalesOrderShipmentDto) o);

            return orders.FindAll(x => x.ShippingStatus == query.ShippingStatus).ToFixedList<ISalesOrderDto>();
          }

          if (query.Status == OrderStatus.Shipping) {

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
        salesOrder.Customer = salesOrder.Beneficiary;
        salesOrder.Supplier = salesOrder.Provider;
        salesOrder.SalesAgent = salesOrder.Responsible;

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
        UID = order.OrderUID,
        OrderNumber = order.OrderNo,
        OrderTime = order.RequestedTime,
        CustomerName = order.Customer.Name,
        SupplierName = order.Supplier.Name,
        SalesAgentName = order.SalesAgent.Name,
        OrderTotal = order.OrderTotal,
        //Status = order.Status,
        ShippingStatus = GetShippingStatus(order.OrderUID),
        TagType = GetShippingStatusTagType(GetShippingStatus(order.OrderUID)),
        StatusName = SalesOrderMapper.MapOrderStatus(order.Status.ToString())
      };

      return dto;
    }

    static public ISalesOrderDto MapBaseSalesOrder(SalesOrder order) {

      var dto = new BaseSalesOrderDto {
        UID = order.OrderUID,
        OrderNumber = order.OrderNo,
        OrderTime = order.PostingTime,
        CustomerName = order.Customer.Name,
        SupplierName = order.Supplier.Name,
        SalesAgentName = order.SalesAgent.Name,
        OrderTotal = order.OrderTotal,
        Status = EnumExtensions.GetOrderStatusEnum(order.SalesOrderProcessStatus),
        StatusName = SalesOrderMapper.MapOrderStatus(order.SalesOrderProcessStatus.ToString())
      };

      return dto;
    }

    static public ISalesOrderDto MapBaseSalesOrderAuthorization(SalesOrder order) {

      var dto = new BaseSalesOrdersAuthorizationDto {
        UID = order.OrderUID,
        OrderNumber = order.OrderNo,
        OrderTime = order.RequestedTime,
        CustomerName = order.Customer.Name,
        SupplierName = order.Supplier.Name,
        SalesAgentName = order.SalesAgent.Name,
        OrderTotal = order.OrderTotal,
        //TotalDebt = GetCustomerTotalDebt(order.Customer.Id),
        TotalCredit = order.Beneficiary.ExtendedData.Get<decimal>("LimiteCredito", 0),
        Status = EnumExtensions.GetOrderStatusEnum(order.SalesOrderProcessStatus),
        StatusName = MapOrderAuthorizationStatus(order.SalesOrderProcessStatus.ToString())
      };

      return dto;
    }

    static public ISalesOrderDto MapBaseSalesOrderPacking(SalesOrder order) {
      var dto = new BaseSalesOrderPackingDto {
        UID = order.OrderUID,
        OrderNumber = order.OrderNo,
        OrderTime = order.RequestedTime,
        CustomerName = order.Customer.Name,
        SupplierName = order.Supplier.Name,
        SalesAgentName = order.SalesAgent.Name,
        OrderTotal = order.OrderTotal,
        Weight = GetWeightTotalPackageByOrder(order),
        TotalPackages = GetTotalPackageByOrder(order),
        //Status = order.Status,
        StatusName = MapOrderPackingStatus(order.Status.ToString())
      };

      return dto;
    }

    static private string MapOrderAuthorizationStatus(string status) {
      switch (status) {
        case "Authorized":
          return "Autorizado";
        case "Applied":
          return "Por Autorizar";
        default:
          return "Por Autorizar";
      }

    }

    static private string MapOrderPackingStatus(string status) {
      switch (status) {
        case "Packing":
          return "Por surtir";
        default:
          return "Surtido";
      }

    }

    static private DataTableTagType GetShippingStatusTagType(string shippingStatus) {
      switch (shippingStatus) {
        case "Pendiente":
          return DataTableTagType.warning;
        case "Asignado":
          return DataTableTagType.success;

        default:
          return DataTableTagType.none;
      }
    }

    static private string GetShippingStatus(string orderUID) {

      var shippingUseCase = ShippingUseCases.UseCaseInteractor();
      var shippingEntryDto = shippingUseCase.GetShippingByOrderUID(orderUID);

      if (shippingEntryDto.ShippingUID == "") {
        return "Pendiente";
      } else {
        return "Asignado";
      }

    }

    static private decimal GetCustomerTotalDebt(int customerId) {
      var moneyAccountUseCase = MoneyAccountUseCases.UseCaseInteractor();

      return moneyAccountUseCase.GetMoneyAccountTotalDebt(customerId);
    }

    static public decimal GetWeightTotalPackageByOrder(SalesOrder order) {
      if (order.OrderUID != "") {
        var usecasePackage = PackagingUseCases.UseCaseInteractor();
        PackagedData packageInfo = usecasePackage.GetPackagedData(order.OrderUID);

        return packageInfo.Weight;
      } else {
        return 0;
      }

    }

    static public int GetTotalPackageByOrder(SalesOrder order) {
      if (order.OrderUID != "") {
        var usecasePackage = PackagingUseCases.UseCaseInteractor();
        PackagedData packageInfo = usecasePackage.GetPackagedData(order.OrderUID);

        return packageInfo.TotalPackages;
      } else {
        return 0;
      }

    }

    #endregion Private methods

  } //  class SearchSealesOrderMapper

} // namespace Empiria.Trade.Sales.Adapters
