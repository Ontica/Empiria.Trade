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
using Empiria.Parties;
using Empiria.StateEnums;

using Empiria.Trade.Orders;
using Empiria.Trade.Procurement.Adapters;

namespace Empiria.Trade.Procurement.Data {


  /// <summary>Provides data read methods for purchase order.</summary>
  internal class PurchaseOrderData {


    #region Public methods V2

    internal static FixedList<PurchaseOrder> GetPurchaseOrdersV2(PurchaseOrderQuery query, int orderTypeId) {

      var queryClauses = GetQueryClausesV2(query);

      string sql = $"SELECT * FROM OMS_Orders " +
                   $"WHERE Order_Type_Id = {orderTypeId} " +
                   $"{queryClauses}";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<PurchaseOrder>(dataOperation);
    }


    static private string GetQueryClausesV2(PurchaseOrderQuery query) {

      var filters = new Filter();

      if (query.SupplierUID != string.Empty) {
        filters.AppendAnd($"Order_Provider_Id = {Party.Parse(query.SupplierUID).Id}");
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

    #endregion Public methods V2

    #region Public methods


    internal static FixedList<PurchaseOrderEntry> GetPurchaseOrders(PurchaseOrderQuery query) {

      var queryClauses = GetQueryClauses(query);

      string sql = $"SELECT * FROM TRDOrders " +
                   $"WHERE OrderTypeId = 1030 " +
                   $"{queryClauses}";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<PurchaseOrderEntry>(dataOperation);
    }


    internal static FixedList<PurchaseOrderItem> GetPurchaseOrderItems(PurchaseOrder order) {
      
      string sql = $"SELECT * FROM OMS_Order_Items " +
                   $"WHERE Order_Item_Order_Id = {order.Id} AND Order_Item_Status <> 'X'";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetFixedList<PurchaseOrderItem>(dataOperation);
    }


    static internal void WritePurchaseOrder(PurchaseOrderEntry entry) {
      
      var op = DataOperation.Parse("writePurchaseOrder",
        entry.OrderId, entry.OrderUID, entry.OrderTypeId,
        entry.Supplier.Id, entry.Customer.Id, entry.CustomerAddress.Id,
        entry.CustomerContact.Id, entry.SalesAgent.Id, entry.OrderNumber,
        entry.Notes, entry.PedimentoImportacion, entry.CartaPorte,
        entry.OrderTime, (char)entry.AuthorizationStatus, entry.AuthorizationTime,
        entry.AuthorizatedById, entry.ScheduledTime, entry.ReceptionTime,
        entry.ShippingMethod, entry.Keywords, (char) entry.Status, entry.ExtData.ToString());

      DataWriter.Execute(op);
    }


    #endregion Public methods


    #region Private methods
    static private string ConvertDateTimeToString(DateTime dateTime) {

      return $"{dateTime.Year}/{dateTime.Day}/{dateTime.Month} " +
             $"{dateTime.Hour}:{dateTime.Minute}:{dateTime.Second}";
    }

    static private string GetQueryClauses(PurchaseOrderQuery query) {

      var filters = new Filter();

      if (query.SupplierUID != string.Empty) {
        filters.AppendAnd($"SupplierId = {Party.Parse(query.SupplierUID).Id}");
      }

      if (query.Keywords != string.Empty) {
        filters.AppendAnd($"{SearchExpression.ParseAndLikeKeywords("OrderKeywords", query.Keywords)}");
      }

      if (query.Status != EntityStatus.All) {
        filters.AppendAnd($"OrderStatus = '{(char) query.Status}'");
      } else {
        filters.AppendAnd($"OrderStatus != '{(char) OrderStatus.Cancelled}'");
      }

      return filters.ToString().Length > 0 ? $"AND {filters}" : "";
    }


    internal static void DeletePurchaseOrder(int orderId) {

      string sql = $"UPDATE TRDOrders " +
                   $"SET OrderStatus = '{(char) OrderStatus.Cancelled}' " +
                   $"WHERE OrderId = {orderId}";

      var dataOperation = DataOperation.Parse(sql);

      DataWriter.Execute(dataOperation);
    }


    internal static void DeletePurchaseOrderItemV1(string purchaseOrderItemUID) {
      string sql = $"UPDATE TRDOrderItems " +
                   $"SET OrderItemStatus = '{(char) EntityStatus.Deleted}' " +
                   $"WHERE OrderItemUID = '{purchaseOrderItemUID}'";

      var dataOperation = DataOperation.Parse(sql);

      DataWriter.Execute(dataOperation);
    }


    #endregion Private methods


  } // class PurchaseOrderData

} // namespace Empiria.Trade.Inventory.PurchaseOrders.Data
