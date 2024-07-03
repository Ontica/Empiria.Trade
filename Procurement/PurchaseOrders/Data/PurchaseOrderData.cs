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
using Empiria.Trade.Core;
using Empiria.Trade.Orders;
using Empiria.Trade.Procurement.Adapters;

namespace Empiria.Trade.Procurement.Data {


  /// <summary>Provides data read methods for purchase order.</summary>
  internal class PurchaseOrderData {


    #region Public methods


    internal static FixedList<PurchaseOrderEntry> GetPurchaseOrderList(PurchaseOrderQuery query) {

      var queryClauses = GetQueryClauses(query);

      string sql = $"SELECT * FROM TRDOrders " +
                   $"WHERE OrderTypeId = 1030 " +
                   $"{queryClauses}";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<PurchaseOrderEntry>(dataOperation);
    }

    internal static void WritePurchaseOrder(PurchaseOrderEntry purchaseOrderEntry) {
      throw new NotImplementedException();
    }


    #endregion Public methods


    #region Private methods


    static private string GetQueryClauses(PurchaseOrderQuery query) {

      var filters = new Filter();

      if (query.SupplierUID != string.Empty) {
        filters.AppendAnd($"SupplierId = {Party.Parse(query.SupplierUID).Id}");
      }

      if (query.Keywords != string.Empty) {
        filters.AppendAnd($"{SearchExpression.ParseAndLikeKeywords("OrderKeywords", query.Keywords)}");
      }

      if (query.Status != OrderStatus.Empty) {
        filters.AppendAnd($"OrderStatus = '{(char) query.Status}'");
      }

      return filters.ToString().Length > 0 ? $"AND {filters}" : "";
    }


    #endregion Private methods


  } // class PurchaseOrderData

} // namespace Empiria.Trade.Inventory.PurchaseOrders.Data
