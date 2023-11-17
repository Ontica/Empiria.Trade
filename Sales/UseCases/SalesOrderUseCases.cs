/* Empiria Trade *********************************************************************************************
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

    protected SalesOrderUseCases() {
      // no-op
    }

    static public SalesOrderUseCases UseCaseInteractor() {
      return UseCase.CreateInstance<SalesOrderUseCases>();
    }

    #endregion Constructors and parsers

    #region Use cases

    public SalesOrderDto ProcessSalesOrder(SalesOrderFields fields) {
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


    public SalesOrderDto CreateSalesOrder(SalesOrderFields fields) {
      Assertion.Require(fields, "fields");

      var order = new SalesOrder(fields);

      order.Save();

      var orderDto = SalesOrderMapper.Map(order);

      return orderDto;
    }

    public FixedList<SalesOrderDto> GetOrders(SearchOrderFields fields) {
      Assertion.Require(fields, "fields");

      FixedList<SalesOrder> salesOrdersList = SalesOrder.GetOrders(fields);
     
     return SalesOrderMapper.Map(salesOrdersList);
    }

    public FixedList<SalesOrdersAuthorizationDto> GetOrdersAuthorization(SearchOrderFields fields) {
      Assertion.Require(fields, "fields");

      FixedList<SalesOrder> salesOrdersList = SalesOrder.GetOrdersToAuthorize(fields);

      return SalesOrderMapper.MapSalesOrderAuthorizationList(salesOrdersList);
    }

    public SalesOrderDto CancelSalesOrder(string orderUID) {
      Assertion.Require(orderUID, "orderUID");

      var order = SalesOrder.Parse(orderUID);
      order.Cancel();

      var orderDto = SalesOrderMapper.Map(order);

      return orderDto;
    }

    public SalesOrderDto ApplySalesOrder(string orderUID) {
      Assertion.Require(orderUID, "orderUID");

      var order = SalesOrder.Parse(orderUID);
      order.Apply();

      var orderDto = SalesOrderMapper.Map(order);

      return orderDto;
    }

    public SalesOrderDto UpdateSalesOrder(SalesOrderFields fields) {
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
      return SalesOrder.GetStatusList();
    }

    public SalesOrderDto AuthorizeSalesOrder(string orderUID) {

      Assertion.Require(orderUID, "orderUID");

      var order = SalesOrder.Parse(orderUID);

      if (order.Status != Orders.OrderStatus.Applied) {
        Assertion.RequireFail($"It is only possible to Authorize orders in the Applied status, your order status is: {order.Status}");
      }
      order.Authorize();

      var orderDto = SalesOrderMapper.Map(order);

      return orderDto;
    }

    public FixedList<NamedEntityDto> GetAuthorizationStatusList() {
      return SalesOrder.GetAuthorizationStatusList();
    }

    public FixedList<NamedEntityDto> GetPackingStatusList() {
      return Sales.SalesOrder.GetPackingStatusList();
    }

    #endregion Use cases

  } // class SalesOrderUseCases

} //namespace Empiria.Trade.Sales.UseCases
