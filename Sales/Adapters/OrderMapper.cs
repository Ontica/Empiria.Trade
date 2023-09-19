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

using Empiria.Trade.Sales.Domain;

namespace Empiria.Trade.Sales.Adapters {

  /// <summary> Methods used to map Order. </summary>
  static internal class OrderMapper {

    static internal OrderDto Map(Order order) {
      var dto = new OrderDto {
        OrderUID = order.OrderUID,
        OrderNumber = order.OrderNumber,
        OrderTime = order.OrderTime,
        Notes = order.Notes,
        Status = "Abierto",
        //Customer = GetCustomer(order.CustomerId),
        //Supplier = GetSupplier(order.SupplierId),
        //SalesAgentD = GetSalesAgent(order.SalesAgentId),
        PaymentCondition = "30 dias"
      };

      return dto;
    }

} // static internal class

  #region Private methods

  

  #endregion

} // namespace Empiria.Trade.Sales.Adapters
