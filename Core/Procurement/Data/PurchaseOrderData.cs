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
using Empiria.StateEnums;

namespace Empiria.Trade.Core {


  /// <summary>Provides data read methods for purchase order.</summary>
  public class PurchaseOrderData {

    #region Public methods

    static public PurchaseOrder GetPurchaseOrder(string purchaseOrderUID, int orderTypeId) {

      string sql = $"SELECT * FROM OMS_Orders " +
                   $"WHERE Order_Type_Id = {orderTypeId} " +
                   $"AND Order_UID = '{purchaseOrderUID}'";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObject<PurchaseOrder>(dataOperation);
    }


    static public FixedList<PurchaseOrder> GetPurchaseOrders(PurchaseOrderQuery query, int orderTypeId) {

      var queryClauses = GetQueryClauses(query);

      string sql = $"SELECT * FROM OMS_Orders " +
                   $"WHERE Order_Type_Id = {orderTypeId} " +
                   $"{queryClauses}";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<PurchaseOrder>(dataOperation);
    }


    static public FixedList<PurchaseOrderItem> GetPurchaseOrderItems(PurchaseOrder order) {

      string sql = $"SELECT * FROM OMS_Order_Items " +
                   $"WHERE Order_Item_Order_Id = {order.OrderId} AND Order_Item_Status <> 'X'";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetFixedList<PurchaseOrderItem>(dataOperation);
    }


    static private string GetQueryClauses(PurchaseOrderQuery query) {

      var filters = new Filter();

      if (query.SupplierUID != string.Empty) {
        filters.AppendAnd($"Order_Provider_Id = {Parties.Party.Parse(query.SupplierUID).Id}");
      }

      if (query.Keywords != string.Empty) {
        filters.AppendAnd($"{SearchExpression.ParseAndLikeKeywords("Order_Keywords", query.Keywords)}");
      }

      if (query.Status != EntityStatus.All) {
        filters.AppendAnd($"Order_Status = '{(char) query.Status}'");
      } else {
        filters.AppendAnd($"Order_Status != '{(char) OrderStatus.Cancelled}'");
      }

      return filters.ToString().Length > 0 ? $"AND {filters}" : "";
    }

    #endregion Public methods


  } // class PurchaseOrderData

} // namespace Empiria.Trade.Core
