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

namespace Empiria.Trade.Sales.Adapters {

  /// <summary> Methods used to map Order. </summary>
  static internal class OrderMapper {

    static internal OrderDto Map(SalesOrder order) {
      var dto = new OrderDto {
        UID = order.UID,
        OrderNumber = order.OrderNumber,
        OrderTime = order.OrderTime,
        Notes = order.Notes,
        Status = "Abierto",
        Customer = order.Customer.MapToNamedEntity(),
        Supplier = order.Supplier.MapToNamedEntity(),
        SalesAgent = order.SalesAgent.MapToNamedEntity(),
        PaymentCondition = ""
      };

      return dto;
    }

  } // static internal class

} // namespace Empiria.Trade.Sales.Adapters
