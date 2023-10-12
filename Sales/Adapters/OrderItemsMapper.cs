/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Order Management                           Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Order.dll                    Pattern   : Mapper class                            *
*  Type     : OrderITemMapper                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Methods used to map OrderItem.                                                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Sales.Adapters {

  /// <summary>Methods used to map OrderItem.  </summary>
  static internal class OrderItemsMapper {

    static internal OrderItemDto Map(SalesOrderItem orderItem) {
      var dto = new OrderItemDto {
        UID = orderItem.UID,
        OrderItemTypeId = orderItem.OrderItemTypeId,
        Quantity = orderItem.Quantity,
        ProductPriceId = orderItem.ProductPriceId,
        PriceListNumber = orderItem.PriceListNumber,
        BasePrice = orderItem.BasePrice,
        SalesPrice = orderItem.SalesPrice,
        AdditionalDiscount = orderItem.Discount,
        Shipment = orderItem.Shipment,
        Taxes = orderItem.TaxesIVA,
        Total = orderItem.Total,
        Notes = orderItem.Notes,
        Status  = orderItem.Status
      };

      return dto;
    }

  }
}
