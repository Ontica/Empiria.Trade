﻿/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Order Management                           Component : Data Layer                              *
*  Assembly : Empiria.Trade.Order.dll                    Pattern   : Data Service                            *
*  Type     : OrderData                                  License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data layer for Orders.                                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Data;

namespace Empiria.Trade.Sales.Data {

    /// <summary>Provides data layer for Orders.</summary>
    static internal class SalesOrderData {

    #region Internal methods

      internal static void Write(SalesOrder o) {
        var op = DataOperation.Parse("writeOrder", o.Id, o.UID, o.OrderTypeId, o.Customer.Id, o.Supplier.Id,
                                    o.SalesAgent.Id, o.OrderNumber, o.OrderTime, o.Notes,
                                    o.Keywords, o.Status);
        DataWriter.Execute(op);
      }

    #endregion Internal methods

  } // class OrderData

} // namespace Empiria.Trade.Sales.Data
