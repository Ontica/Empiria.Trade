/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Use cases Layer                         *
*  Assembly : Empiria.Trade.Orders.dll                   Pattern   : Use case interactor class               *
*  Type     : ProductsUseCases                           License   : Please read LICENSE.txt file            *
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

    #endregion Use cases

  } // class OrderUseCases

} //namespace Empiria.Trade.Sales.UseCases

