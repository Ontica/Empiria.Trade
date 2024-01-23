/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Order Management                           Component : Data Layer                              *
*  Assembly : Empiria.Trade.Order.dll                    Pattern   : Data Service                            *
*  Type     : OrderItemsData                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data layer for OrderItems.                                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Data;
using Empiria.Data;

namespace Empiria.Trade.Sales.Data {

  /// <summary>Provides data layer for OrderItems. </summary>
  static internal class SalesOrderItemsData {

    internal static FixedList<SalesOrderItem> GetOrderItems(int orderId) {
      string sql = $"SELECT * FROM TRDOrderItems " +
                   $"WHERE OrderId = {orderId} and OrderItemStatus <> 'X'";

      var op = DataOperation.Parse(sql);

      return DataReader.GetFixedList<SalesOrderItem>(op);
    }

    internal static DataRow GetProductPrice(int vendorProductId, int customerPriceListNumber) {

      string pricelistNumber = "PriceList" + customerPriceListNumber.ToString();

      var sql = $"SELECT ProductPriceId, {pricelistNumber} " +
                $"FROM TRDProductPrices " +
                $"WHERE VendorProductId = {vendorProductId}";

      var op = DataOperation.Parse(sql);

      return DataReader.GetDataRow(op);
    }

    static internal void Write(SalesOrderItem o) {
      var op = DataOperation.Parse("writeOrderItems", o.Id, o.UID, o.Order.Id, o.OrderItemTypeId,o.VendorProduct.Id,
                                   o.Quantity, o.ProductPriceId, o.PriceListNumber, o.BasePrice,
                                  o.SalesPrice, o.Discount, o.AdditionalDiscount, o.Shipment, o.TaxesIVA,
                                  o.TaxesIEPS, o.Total, o.Notes, o.Status);
      DataWriter.Execute(op);
    }

    static internal void CancelOrderItems(int orderId) {
      var sql = $"UPDATE TRDOrderItems SET OrderItemStatus = 'X' WHERE orderId = {orderId}";

      var op = DataOperation.Parse(sql);

      DataWriter.Execute(op);
    }

  } // class OrderItemsData

} // namespace Empiria.Trade.Sales.Data
