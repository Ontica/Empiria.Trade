﻿/* Empiria Trade *********************************************************************************************
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
using Empiria.Trade.Core.Adapters;
using Empiria.Trade.Financial.Adapters;
using Empiria.Trade.Financial.UseCases;
using Empiria.Trade.Orders;

using Empiria.Trade.Sales.ShippingAndHandling.Adapters;
using Empiria.Trade.Sales.ShippingAndHandling.UseCases;


namespace Empiria.Trade.Sales.Adapters
{

  /// <summary> Methods used to map Order. </summary>
  static public class SalesOrderMapper {

    static public ISalesOrderDto Map(SalesOrder order) {
      var dto = new SalesOrderDto {
        OrderData = MapDataDto(order),
        Items = MapSalesOrderItems(order.SalesOrderItems),
        Authorization = MapAuthorizationDto(order),
        CustomerCredit = MapCustomerCredit(order),
        Shipping = GetShipping(order.UID),
        Packing = GetPacking(order.UID),
        Actions = TransactionActionsMapper.Map(order.Actions),
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


    static public string MapOrderStatus(string status) {
      switch (status) {
        case "Captured":
          return "Capturada";
        case "Applied":
          return "Aplicada";
        case "Authorized":
          return "Autorizada";
        case "Packing":
          return "Surtiéndose";
        case "Shipping":
          return "Envío";
        case "Delivery":
          return "Entrega";
        case "Closed":
          return "Cerrada";
        case "Cancelled":
          return "Cancelada";
        case "Pending":
          return "Por Autorizar";
        case "ToSupply":
          return "Por surtir";
        case "InProgress":
          return "En proceso";
        case "Suppled":
          return "Surtido";
        default:
          return "Capturada";
      }

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
        Customer = MapCustomer(order.Customer),
        CustomerAddress = CustomerAddressMapper.MapShortAddress(order.CustomerAddress),
        CustomerContact = CustomerConctacMapper.MapCustomerContact(order.CustomerContact), 
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

    private static ContactDto MapCustomer(Party customer) {
      return PartyMapper.MapToContact(customer);
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



    private static CustomerCreditDto MapCustomerCredit(SalesOrder order) {
      var dto = new CustomerCreditDto {
        TotalDebt = GetCustomerTotalDebt(order.Customer.Id), //order.TotalDebt,
        CreditLimit = GetCusomerCreditLimit(order.Customer.Id), //order.CreditLimit,
        CreditTransactions = GetCreditTransactions(order.Customer.Id)
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

    static private decimal GetCustomerTotalDebt(int customerId) {

      var moneyAccountUseCase = MoneyAccountUseCases.UseCaseInteractor();

      return moneyAccountUseCase.GetMoneyAccountTotalDebt(customerId);
    }

    static private decimal GetCusomerCreditLimit(int customerId) {
      var moneyAccountUseCases = MoneyAccountUseCases.UseCaseInteractor();

      return moneyAccountUseCases.GetMoneyAccountCreditLimit(customerId);
    }

    static private FixedList<CreditTransactionDto> GetCreditTransactions(int customerId) {
      var moneyAccountUseCase = MoneyAccountUseCases.UseCaseInteractor();

      return moneyAccountUseCase.GetCreditTransactions(customerId);      
    }

           
    #endregion Private methods


  } // static internal class

} // namespace Empiria.Trade.Sales.Adapters
