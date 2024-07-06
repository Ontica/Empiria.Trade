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


    internal static FixedList<PurchaseOrderEntry> GetPurchaseOrders(PurchaseOrderQuery query) {

      var queryClauses = GetQueryClauses(query);

      string sql = $"SELECT * FROM TRDOrders " +
                   $"WHERE OrderTypeId = 1030 " +
                   $"{queryClauses}";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<PurchaseOrderEntry>(dataOperation);
    }


    internal static FixedList<PurchaseOrderItem> GetPurchaseOrderItems(int purchaseOrderId) {
      string sql = $"SELECT * FROM TRDOrderItems " +
                   $"WHERE OrderId = {purchaseOrderId} and OrderItemStatus <> 'X'";

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


    static internal void WritePurchaseOrderItem(PurchaseOrderItem item) {
      
      var op = DataOperation.Parse("writePurchaseOrderItem",
        item.OrderItemId, item.OrderItemUID, item.Order.Id,
        item.OrderItemTypeId, item.VendorProduct.Id, item.Quantity,
        item.ReceivedQty, item.ProductPriceId, item.PriceListNumber,
        item.BasePrice, item.SalesPrice, item.Discount,
        item.Shipment, item.TaxesIVA, item.Total,
        item.Notes, item.ScheduledTime, item.ReceptionTime,
        item.Reviewed, (char) item.Status);

      DataWriter.Execute(op);
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


    #endregion Private methods


  } // class PurchaseOrderData

} // namespace Empiria.Trade.Inventory.PurchaseOrders.Data
