﻿/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Sales Order Management                     Component : Use cases Layer                         *
*  Assembly : Empiria.Trade.Orders.dll                   Pattern   : Use case interactor class               *
*  Type     : SalesOrderUseCases                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases used to management Products.                                                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Services;

using Empiria.Trade.Sales.Adapters;

namespace Empiria.Trade.Sales.UseCases {

  /// <summary>Use cases used to management Orders.</summary>
   public class SalesOrderUseCases : UseCase {

    #region Constructors and parsers

    public SalesOrderUseCases() {
      // no-op
    }

    static public SalesOrderUseCases UseCaseInteractor() {
      return UseCase.CreateInstance<SalesOrderUseCases>();
    }

    #endregion Constructors and parsers

    #region Use cases

    public ISalesOrderDto ProcessSalesOrder(SalesOrderFields fields) {
      Assertion.Require(fields, "fields");

      SalesOrder order;

      if (fields.UID.Length != 0) {
        order = SalesOrder.Parse(fields.UID);
        order.Update(fields);
      } else {
        order = new SalesOrder(fields);
      }

      return SalesOrderMapper.Map(order);
    }


    public ISalesOrderDto CreateSalesOrder(SalesOrderFields fields) {
      Assertion.Require(fields, "fields");

      var order = new SalesOrder(fields);

      order.Save();

      var orderDto = SalesOrderMapper.Map(order);

      return orderDto;
    }

    public ISalesOrderDto DeliverySalesOrder(string orderUID) {

      Assertion.Require(orderUID, "orderUID");

      var order = SalesOrder.Parse(orderUID);
      order.Deliver();

      var orderDto = SalesOrderMapper.Map(order);

      return orderDto;
    }

    public FixedList<ISalesOrderDto> GetOrders(SearchOrderFields fields) {
      Assertion.Require(fields, "fields");

      var helper = new SalesOrderHelper();

      switch (fields.QueryType) {

        case QueryType.Sales: {
          var salesOrdersList = helper.GetOrders(fields);
          return SalesOrderMapper.MapBaseSalesOrders(salesOrdersList);
        }
        case QueryType.SalesAuthorization: {
          FixedList<SalesOrder> salesOrders = helper.GetOrdersToAuthorize(fields);
          return SalesOrderMapper.MapBaseSalesOrderAuthorizationList(salesOrders);
        }
        case QueryType.SalesPacking: {
          FixedList<SalesOrder> salesOrdersPacking = helper.GetOrdersToPacking(fields);
          return SalesOrderMapper.MapBaseSalesOrderPackingList(salesOrdersPacking);
        }

        default: {
          throw Assertion.EnsureNoReachThisCode($"It is invalid queryType:{fields.QueryType}");

        }
       
      }

    
    }
    
    public ISalesOrderDto CancelSalesOrder(string orderUID) {
      Assertion.Require(orderUID, "orderUID");

      var order = SalesOrder.Parse(orderUID);
      order.Cancel();

      var orderDto = SalesOrderMapper.Map(order);

      return orderDto;
    }

    public void ChangeOrdersToDeliveryStatus(string [] ordersUID) {
      Assertion.Require(ordersUID, "orderUID");

      foreach(var orderUID in ordersUID) {
        DeliverySalesOrder(orderUID);
      }     
    
    }

    public ISalesOrderDto ApplySalesOrder(string orderUID) {
      Assertion.Require(orderUID, "orderUID");

      var order = SalesOrder.Parse(orderUID);
      order.Apply();

      var orderDto = SalesOrderMapper.Map(order);

      return orderDto;
    }

    public ISalesOrderDto GetSalesOrder(string orderUID, QueryType queryType) {
      var order = SalesOrder.Parse(orderUID);
      order.CalculateSalesOrder(queryType);

      var orderDto = SalesOrderMapper.Map(order);

      return orderDto;
    }

    public ISalesOrderDto UpdateSalesOrder(SalesOrderFields fields) {
      Assertion.Require(fields, "fields");

      if (fields.Status != Orders.OrderStatus.Captured) {
        Assertion.RequireFail($"It is only possible to update orders in the Captured status your order status is:{fields.Status}");
      }
        var order = SalesOrder.Parse(fields.UID);
        order.Modify(fields);

        var orderDto = SalesOrderMapper.Map(order);

        return orderDto;
    }

    public FixedList<NamedEntityDto> GetStatusList() {
      return SalesOrderStatusService.GetStatusList();
    }

    public ISalesOrderDto AuthorizeSalesOrder(string orderUID) {

      Assertion.Require(orderUID, "orderUID");

      var order = SalesOrder.Parse(orderUID);

      if (order.Status != Orders.OrderStatus.Applied) {
        Assertion.RequireFail($"It is only possible to Authorize orders in the Applied status, your order status is: {order.Status}");
      }
      order.Authorize();

      var orderDto = SalesOrderMapper.Map(order);

      return orderDto;
    }


    public ISalesOrderDto SupplySalesOrder(string orderUID) {
      Assertion.Require(orderUID, "orderUID");

      var order = SalesOrder.Parse(orderUID);

      if (order.Status != Orders.OrderStatus.Packing) {
        Assertion.RequireFail($"It is only possible to Supply orders in the Packing status, your order status is: {order.Status}");
      }

      order.Supply();

      var orderDto = SalesOrderMapper.Map(order);

      return orderDto;
    }


    public FixedList<NamedEntityDto> GetAuthorizationStatusList() {
      return SalesOrderStatusService.GetAuthorizationStatusList();
    }

    public FixedList<NamedEntityDto> GetPackingStatusList() {
      return SalesOrderStatusService.GetPackingStatusList();
    }

    



    #endregion Use cases

  } // class SalesOrderUseCases

} //namespace Empiria.Trade.Sales.UseCases



