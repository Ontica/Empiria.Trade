/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Order Management                           Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Order.dll                    Pattern   : Mapper class                            *
*  Type     : OrderMapper                                License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Methods used to map Order.                                                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

using Empiria.Trade.Core;
using Empiria.Trade.Orders;

namespace Empiria.Trade.Sales.Adapters {

  /// <summary> Methods used to map Order. </summary>
  static public class SalesOrderMapper {

    static public ISalesOrderDto Map(SalesOrder order) {
      var dto = new SalesOrderDto {
        UID = order.UID,
        OrderNumber = order.OrderNumber,
        OrderTime = order.OrderTime,
        Notes = order.Notes,
        Status = order.Status,
        Customer = order.Customer.MapToNamedEntity(),
        Supplier = order.Supplier.MapToNamedEntity(),
        SalesAgent = order.SalesAgent.MapToNamedEntity(),
        ShippingMethod = order.ShippingMethod,
        PaymentCondition = order.PaymentCondition,
        PriceList = order.PriceList,
        Items = MapSalesOrderItems(order.SalesOrderItems), // new FixedList<SalesOrderItem>(),
        ItemsCount = order.ItemsCount,
        ItemsTotal = order.ItemsTotal,
        Shipment = order.Shipment,
        Taxes = order.Taxes,
        OrderTotal = order.OrderTotal,
        AuthorizationStatus = order.AuthorizationStatus,
        AuthorizationTime = order.AuthorizationTime,
        AuthorizatedById = order.AuthorizatedById,
        Actions = MapOrderActions(order.Actions)
      };

      return dto;
    }

    static public ISalesOrderDto MapSalesOrderAuthorization(SalesOrder order) {

      var dto = new SalesOrdersAuthorizationDto {
        UID = order.UID,
        OrderNumber = order.OrderNumber,
        OrderTime = order.OrderTime,
        Notes = order.Notes,
        Status = order.Status,
        Customer = order.Customer.MapToNamedEntity(),
        Supplier = order.Supplier.MapToNamedEntity(),
        SalesAgent = order.SalesAgent.MapToNamedEntity(),
        ShippingMethod = order.ShippingMethod,
        PaymentCondition = order.PaymentCondition,
        Items = MapSalesOrderItems(order.SalesOrderItems), // new FixedList<SalesOrderItem>(),
        ItemsCount = order.ItemsCount,
        ItemsTotal = order.ItemsTotal,
        Shipment = order.Shipment,
        Taxes = order.Taxes,
        OrderTotal = order.OrderTotal,
        AuthorizationStatus = order.AuthorizationStatus,
        AuthorizationTime = order.AuthorizationTime,
        AuthorizatedById = order.AuthorizatedById,
        Actions = MapOrderActions(order.Actions),
        TotalDebt = order.TotalDebt,
        CreditLimit = order.CreditLimit,
        PriceList = order.PriceList,
        CreditTransactions = MapCreditTransactions(order.CreditTransactions)
      };

      return dto;
    }

    static public ISalesOrderDto MapSalesOrderPacking(SalesOrder order) {

      var dto = new SalesOrderPackingDto {
        UID = order.UID,
        OrderNumber = order.OrderNumber,
        OrderTime = order.OrderTime,
        Notes = order.Notes,
        Status = order.Status,
        Customer = order.Customer.MapToNamedEntity(),
        Supplier = order.Supplier.MapToNamedEntity(),
        SalesAgent = order.SalesAgent.MapToNamedEntity(),
        ShippingMethod = order.ShippingMethod,
        PaymentCondition = order.PaymentCondition,
        Items = MapSalesOrderItems(order.SalesOrderItems), // new FixedList<SalesOrderItem>(),
        ItemsCount = order.ItemsCount,
        ItemsTotal = order.ItemsTotal,
        Shipment = order.Shipment,
        Taxes = order.Taxes,
        OrderTotal = order.OrderTotal,
        AuthorizationStatus = order.AuthorizationStatus,
        AuthorizationTime = order.AuthorizationTime,
        AuthorizatedById = order.AuthorizatedById,
        Actions = MapOrderActions(order.Actions),
        PriceList = order.PriceList,
        Weight = order.Weight,
        TotalPackages = order.TotalPackages
      };

      return dto;
    }

    static public FixedList<ISalesOrderDto> MapSalesOrderAuthorizationList(FixedList<SalesOrder> salesOrders) {
      List<ISalesOrderDto> salesOrderDtoList = new List<ISalesOrderDto>();

      foreach (var salesOrder in salesOrders) {
        salesOrderDtoList.Add(MapSalesOrderAuthorization(salesOrder));
      }

      return salesOrderDtoList.ToFixedList();
    }

    static public FixedList<ISalesOrderDto> MapSalesOrderPackingList(FixedList<SalesOrder> salesOrders) {
      List<ISalesOrderDto> salesOrderDtoList = new List<ISalesOrderDto>();

      foreach (var salesOrder in salesOrders) {
        salesOrderDtoList.Add(MapSalesOrderPacking(salesOrder));
      }

      return salesOrderDtoList.ToFixedList();
    }

    static public FixedList<ISalesOrderDto> Map(FixedList<SalesOrder> salesOrders) {
      List<ISalesOrderDto> salesOrderDtoList = new List<ISalesOrderDto>();

      foreach (var salesOrder in salesOrders) {
        salesOrderDtoList.Add(Map(salesOrder));
      }

      return salesOrderDtoList.ToFixedList();
    }

    #region Private methods

    private static OrderActionsDto MapOrderActions(OrderActions actions) {

      var dto = new OrderActionsDto {
        CanEdit = actions.CanEdit,
        CanApply = actions.CanApply,
        CanAuthorize = actions.CanAuthorize,
        CanPackaging = actions.CanPackaging,
        CanSelectCarrier = actions.CanSelectCarrier,
        CanShipping = actions.CanShipping,
        CanClose = actions.CanClose
      };
            
      return dto;
    }


    static private FixedList<SalesOrderItemDto> MapSalesOrderItems(FixedList<SalesOrderItem> salesOrderItems) {
      List<SalesOrderItemDto> salesOrderItemsList = new List<SalesOrderItemDto>();

      foreach (var saleOrderItem in salesOrderItems) {
        salesOrderItemsList.Add(SalesOrderItemsMapper.Map(saleOrderItem));
      }

      return salesOrderItemsList.ToFixedList();
    }

    static private FixedList<CreditTransactionDto> MapCreditTransactions(FixedList<CreditTransaction> creditTransactions) {
      List<CreditTransactionDto> creditTransactionList = new List<CreditTransactionDto>();

      foreach (var creditTransaction in creditTransactions) {
        creditTransactionList.Add(CreditTransactionMapper.Map(creditTransaction));
      }

      return creditTransactionList.ToFixedList();
    }

    #endregion Private methods


  } // static internal class

} // namespace Empiria.Trade.Sales.Adapters
