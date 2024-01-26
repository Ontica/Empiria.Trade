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
using Empiria.Trade.Sales.ShippingAndHandling.Adapters;
using Empiria.Trade.Sales.ShippingAndHandling.UseCases;


namespace Empiria.Trade.Sales.Adapters {

  /// <summary> Methods used to map Order. </summary>
  static public class SalesOrderMapper {

    static public ISalesOrderDto Map(SalesOrder order) {
      var dto = new SalesOrderDto {
        OrderData = MapDataDto(order),
        Items = MapSalesOrderItems(order.SalesOrderItems),
        Authorization = MapAuthorizationDto(order),
        CustomerCredit = MapCustomerCredit(order),
        //WeightData = MapWeightDataDto(order),
        Shipping = GetShipping(order.UID),
        Packing = GetPacking(order.UID),
        Actions = MapOrderActions(order.Actions)
      };

      return dto;
    }
       
    static public FixedList<ISalesOrderDto> Map(FixedList<SalesOrder> salesOrders) {
      List<ISalesOrderDto> salesOrderDtoList = new List<ISalesOrderDto>();

      foreach (var salesOrder in salesOrders) {
        salesOrderDtoList.Add(Map(salesOrder));
      }

      return salesOrderDtoList.ToFixedList();
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
        StatusName = MapOrderStatus(order.Status.ToString())
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

    #region Private methods

    private static OrderDataDto MapDataDto(SalesOrder order) {
      var dto = new OrderDataDto {
        UID = order.UID,
        OrderNumber = order.OrderNumber,
        OrderTime = order.OrderTime,
        Notes = order.Notes,
        Status = order.Status,
        StatusName = MapOrderStatus(order.Status.ToString()),
        Customer = order.Customer.MapToNamedEntity(),
        Supplier = order.Supplier.MapToNamedEntity(),
        SalesAgent = order.SalesAgent.MapToNamedEntity(),
        ShippingMethod = order.ShippingMethod,
        PaymentCondition = order.PaymentCondition,
        PriceList = order.PriceList,
        ItemsCount = order.ItemsCount,
        ItemsTotal = order.ItemsTotal,
        Shipment = order.Shipment,
        Taxes = order.Taxes,
        OrderTotal = order.OrderTotal
      };

      return dto;
    }

    private static AuthorizationDto MapAuthorizationDto(SalesOrder order) {
      var dto = new AuthorizationDto {
        AuthorizationStatus = order.AuthorizationStatus,
        AuthorizationTime = order.AuthorizationTime,
        AuthorizatedById = order.AuthorizatedById
      };

      return dto;
    }

    private static ShippingEntryDto GetShipping(string orderUID) {

      if (orderUID == "") {
        return new ShippingEntryDto();
      }

      var shippingUseCase = ShippingUseCases.UseCaseInteractor();
      return shippingUseCase.GetShippingByOrderUID(orderUID);     
    }

    private static IShippingAndHandling GetPacking(string orderUID) {
      if (orderUID == "") {
        return new PackingDto();
      }

      var packingUseCase = PackagingUseCases.UseCaseInteractor();
      return packingUseCase.GetPackagingForOrder(orderUID);
    }

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

    private static CustomerCreditDto MapCustomerCredit(SalesOrder order) {
      var dto = new CustomerCreditDto {
        TotalDebt = order.TotalDebt,
        CreditLimit = order.CreditLimit,
        CreditTransactions = MapCreditTransactions(order.CreditTransactions)
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

    static private string MapOrderStatus(string status) {
      switch (status) {
        case "Captured": return "Capturada";
        case "Applied":  return "Aplicada";
        case "Authorized": return "Autorizada";
        case "Packing":    return "Surtiendose";
        case "CarrierSelector": return "Selección de paqueteria";
        case "Shipping": return "Envio";
        case "Delivery": return "Entrega";
        case "Closed": return "Cerrada";
        case "Cancelled": return "Cancelada";
        case "Pending": return "Por Autorizar";
        case "ToSupply": return "Por surtir";
        case "InProgress": return "En proceso";
        case "Suppled": return "Surtido";
        default:  return "Capturada";
      }

    }

    static private string MapOrderAuthorizationStatus(string status) {
      switch (status) {
        case "Authorized": return "Autorizado";
        case "Pending":    return "Por Autorizar";
        default:  return "Por Autorizar";
      }
      
    }

    static private string MapOrderPackingStatus(string status) {
      switch (status) {
        case "ToSupply": return "Por surtir";
        case "InProgress": return "En proceso";
        case "Suppled":    return "Surtido";
        default:
          return "Por surtir";
      }

    }
    #endregion Private methods


  } // static internal class

} // namespace Empiria.Trade.Sales.Adapters
