/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Trade Core                                 Component : Data Layer                              *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Data Service                            *
*  Type     : PurchaseOrderData                          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data read methods for purchase order.                                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Data;
using Empiria.Trade.Procurement.Adapters;
using Empiria.Trade.Procurement.Domain;

namespace Empiria.Trade.Procurement.Data {


  /// <summary>Provides data read methods for purchase order.</summary>
  internal class PurchaseOrderData {


    internal static FixedList<PurchaseOrderEntry> GetPurchaseOrderList(PurchaseOrderQuery query) {

      string sql = $"SELECT * FROM TRDOrders " +
                   $"WHERE OrderTypeId = 1030 ";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<PurchaseOrderEntry>(dataOperation);
    }


  } // class PurchaseOrderData

} // namespace Empiria.Trade.Inventory.PurchaseOrders.Data
