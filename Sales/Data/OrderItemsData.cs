﻿/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Order Management                           Component : Data Layer                              *
*  Assembly : Empiria.Trade.Order.dll                    Pattern   : Data Service                            *
*  Type     : OrderItemsData                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data layer for OrderItems.                                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Data;

using Empiria.Trade.Orders;

namespace Empiria.Trade.Sales.Data {

  /// <summary>Provides data layer for OrderItems. </summary>
  static internal class OrderItemsData {

    static internal void Write(OrderItem o) {
      var op = DataOperation.Parse("writeOrderItems", o.Id, o.UID, o.OrderId, o.Product,
                                  o.PresentationId, o.Vendor.Id, o.Quantity, o.BasePrice,
                                  o.SalesPrice, o.Discount, o.Shipment, o.TaxesIVA,
                                  o.TaxesIEPS, o.Total, o.Notes, o.Status);
      DataWriter.Execute(op);
    }

  } // class OrderItemsData

} // namespace Empiria.Trade.Sales.Data
