/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : ShippingAndHandlingData Management         Component : Data Layer                              *
*  Assembly : Empiria.Trade.ShippingAndHandlingData.dll  Pattern   : Data Service                            *
*  Type     : ShippingAndHandlingData                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data read  and write methods for shipping and handling.                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Data;
using Empiria.Trade.Core;
using Empiria.Trade.Orders;
using Empiria.Trade.Products;
using Empiria.Trade.Products.Adapters;
using Empiria.Trade.ShippingAndHandling.Adapters;
using Newtonsoft.Json;

namespace Empiria.Trade.ShippingAndHandling.Data {


  /// <summary>Provides data read  and write methods for shipping and handling.</summary>
  internal class ShippingAndHandlingData {


    internal FixedList<Packing> GetPackagingForOrder(string orderUid) {

      int orderId = Order.Parse(orderUid).Id;

      string sql = "SELECT PACK.OrderPackingId, PACK.OrderPackingUID, ITEM.PackingItemId, " +
                   "ITEM.PackingItemUID, PACK.OrderId, ITEM.OrderItemId, PACK.PackageTypeId, " +
                   "ITEM.InventoryEntryId, PACK.PackageID, ITEM.PackageQuantity " +
                   "FROM TRDPackaging PACK " +
                   "INNER JOIN TRDPackagingItems ITEM ON PACK.OrderPackingId = ITEM.OrderPackingId " +
                   $"WHERE PACK.OrderId IN ({orderId})";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<Packing>(dataOperation);

    }


    internal FixedList<InventoryEntry> GetInventoryByVendorProduct(
                                        int vendorProductId, string warehouseBinUid) {

      var warehouseBin = string.Empty;
      if (warehouseBinUid != string.Empty) {
        warehouseBin = $" AND WarehouseBinId = {WarehouseBin.Parse(warehouseBinUid).Id}";
      }
      string sql = $"SELECT * FROM TRDInventory " +
                   $"WHERE EntryStatus = 'A' AND VendorProductId = {vendorProductId} {warehouseBin}";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<InventoryEntry>(dataOperation);

    }


    internal FixedList<PackageForItem> GetPackagesForItems(string orderUid) {

      int orderId = Order.Parse(orderUid).Id;

      string sql = $"SELECT * FROM TRDPackaging WHERE OrderId = {orderId}";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<PackageForItem>(dataOperation);

    }


    internal FixedList<PackingOrderItem> GetPackingOrderItems(int OrderPackingId) {

      string sql = $"SELECT * " +
                   $"FROM TRDPackagingItems WHERE OrderPackingId = {OrderPackingId}";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<PackingOrderItem>(dataOperation);

    }


    internal FixedList<OrderItemTemp> GetOrderItems(string orderUid) {

      int orderId = Order.Parse(orderUid).Id;

      string sql = $"SELECT OrderId, OrderItemId, OrderItemUID, VendorProductId, Quantity " +
                   $"FROM TRDOrderItems WHERE OrderItemStatus = 'A' AND OrderId = {orderId}";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<OrderItemTemp>(dataOperation);

    }


    internal static void WritePacking(PackageForItem order) {

      var op = DataOperation.Parse("writePackaging",
        order.OrderPackingId, order.UID, order.OrderId, order.PackageTypeId, order.PackageID);

      DataWriter.Execute(op);
    }


    internal static void WritePackingOrderItem(PackingOrderItem orderItem) {

      var op = DataOperation.Parse("writePackagingItem",
        orderItem.Id, orderItem.PackingItemUID, orderItem.OrderPackingId, orderItem.OrderId,
        orderItem.OrderItemId, orderItem.InventoryEntryId, orderItem.Quantity);

      DataWriter.Execute(op);
    }


    internal FixedList<PackageType> GetPackageTypeList() {

      string sql = "SELECT * FROM SimpleObjects WHERE ObjectStatus = 'A' AND ObjectTypeId = 1061";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<PackageType>(dataOperation);
    }


    internal void DeletePackingOrderItem(string packingItemEntryUID) {
      
      string sql = $"DELETE FROM TRDPackagingItems WHERE PackingItemUID = '{packingItemEntryUID}'";

      var dataOperation = DataOperation.Parse(sql);

      DataWriter.Execute(dataOperation);
    }


    #region Private methods


    #endregion Private methods

  } // class ShippingAndHandlingData


  internal class OrderItemTemp {


    [DataField("OrderId")]
    public Order Order {
      get;
      protected set;
    }

    [DataField("OrderItemId")]
    public int OrderItemId {
      get; set;
    }


    [DataField("OrderItemUID")]
    public string OrderItemUID {
      get; set;
    }


    [DataField("VendorProductId")]
    public int VendorProductId {
      get; set;
    }


    [DataField("Quantity")]
    public decimal Quantity {
      get; set;
    }


  }


} // namespace Empiria.Trade.ShippingAndHandling.Data
