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

namespace Empiria.Trade.Sales.Adapters {

  /// <summary> Methods used to map Order. </summary>
  static public class SalesOrderMapper {

    static public SalesOrderDto Map(SalesOrder order) {
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
        Items = new FixedList<SalesOrderItem>(),
      };

      return dto;
    }

    static public FixedList<SalesOrderDto> Map(FixedList<SalesOrder> salesOrders) {
      List<SalesOrderDto> salesOrderDtoList = new List<SalesOrderDto>();

      foreach (var salesOrder in salesOrders) {
        salesOrderDtoList.Add(Map(salesOrder));
      }

      return salesOrderDtoList.ToFixedList();
    }

  } // static internal class

} // namespace Empiria.Trade.Sales.Adapters
