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
  static public class SalesOrderItemsMapper {

    #region Public methods

    static public SalesOrderItemDto Map(SalesOrderItem orderItem) {
      var dto = new SalesOrderItemDto {
        OrderItemUID = orderItem.UID,
        Quantity = orderItem.Quantity,        
        BasePrice = orderItem.BasePrice,
        SalesPrice = orderItem.SalesPrice,
        AdditionalDiscount = orderItem.Discount,
        Shipment = orderItem.Shipment,
        Taxes = orderItem.TaxesIVA,
        Total = orderItem.Total,
        Notes = orderItem.Notes,
       
      };

      return dto;
    }

    #endregion Public methods

    #region Private methods



    #endregion Private methods

  } // class SalesOrderItemsMapper

} // namespace Empiria.Trade.Sales.Adapters
