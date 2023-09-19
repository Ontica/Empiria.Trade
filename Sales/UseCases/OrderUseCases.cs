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
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

using Empiria.Services;

using Empiria.Trade.Sales.Adapters;
using Empiria.Trade.Sales.Domain;

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
    
    
    public OrderDto CreateOrder(OrderFields fields) {
      Assertion.Require(fields, "fields");

      var order = new Order(fields);
      order.Save();

      var orderDto = OrderMapper.Map(order);

      return orderDto;

    }



    #endregion Use cases

  } // class OrderUseCases

} //namespace Empiria.Trade.Sales.UseCases

