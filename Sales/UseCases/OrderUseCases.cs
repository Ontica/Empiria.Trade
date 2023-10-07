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
   public class OrderUseCases : UseCase {

    #region Constructors and parsers

    protected OrderUseCases() {
      // no-op
    }

    static public OrderUseCases UseCaseInteractor() {
      return UseCase.CreateInstance<OrderUseCases>();
    }


    #endregion Constructors and parsers


    #region Use cases


    public OrderDto ProcessSalesOrder(OrderFields fields) {
      Assertion.Require(fields, "fields");

      SalesOrder order;

      if (fields.UID.Length != 0) {
        order = SalesOrder.Parse(fields.UID);
        order.Update(fields);
      } else {
        order = new SalesOrder(fields);
      }

      return OrderMapper.Map(order);
    }


    public OrderDto SaveSalesOrder(OrderFields fields) {
      Assertion.Require(fields, "fields");

      var order = new SalesOrder(fields);

      order.Save();

      var orderDto = OrderMapper.Map(order);

      return orderDto;
    }

    #endregion Use cases

  } // class OrderUseCases

} //namespace Empiria.Trade.Sales.UseCases

