/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : ShippingAndHandlingData Management         Component : Data Layer                              *
*  Assembly : Empiria.Trade.ShippingAndHandlingData.dll  Pattern   : Data Service                            *
*  Type     : PackagingData                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data read  and write methods for shipping and handling.                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Data;
using Empiria.Orders;
using Empiria.Trade.Core.Catalogues;

namespace Empiria.Trade.Core
{


    /// <summary>Provides data read  and write methods for packaging.</summary>
    public class PackagingData {


    public FixedList<Packing> GetPackagingForOrder(string orderUid) {

      int orderId = Order.Parse(orderUid).Id;

      string sql = "SELECT PACK.Order_Packing_Id, PACK.Order_Packing_UID, ITEM.Packing_Item_Id, " +
                   "ITEM.Packing_Item_UID, PACK.Order_Id, ITEM.Order_Item_Id, PACK.Package_Type_Id, " +
                   "ITEM.Inventory_Entry_Id, PACK.Package_ID, ITEM.Package_Quantity " +
                   "FROM OMS_Packaging PACK " +
                   "INNER JOIN OMS_Packaging_Items ITEM ON PACK.Order_Packing_Id = ITEM.Order_Packing_Id " +
                   $"WHERE PACK.Order_Id IN ({orderId})";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<Packing>(dataOperation);

    }


    static public FixedList<PackagingEntry> GetPackagingEntriesByOrder(string orderUid) {

      int orderId = Order.Parse(orderUid).Id;

      string sql = $"SELECT * FROM OMS_Packaging WHERE Order_Id = {orderId}";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<PackagingEntry>(dataOperation);

    }


    public FixedList<PackagingItem> GetPackingOrderItems(int OrderPackingId) {

      string sql = $"SELECT * " +
                   $"FROM OMS_Packaging_Items WHERE Order_Packing_Id = {OrderPackingId}";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<PackagingItem>(dataOperation);

    }


    static public FixedList<PackagingItem> GetPackingOrderItemsByOrder(int OrderId) {

      string sql = $"SELECT * " +
                   $"FROM OMS_Packaging_Items WHERE Order_Id = {OrderId}";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<PackagingItem>(dataOperation);

    }


    static public FixedList<PackagingItem> GetPackingOrderItem(
      string orderPackingUID, string orderItemUID, int warehouseBinId) {

      var orderPackingId = PackagingEntry.Parse(orderPackingUID).OrderPackingId;
      var orderItemId = OrderItem.Parse(orderItemUID).Id;

      string sql = $"SELECT * " +
                   $"FROM OMS_Packaging_Items " +
                   $"WHERE Order_Packing_Id = {orderPackingId} " +
                   $"AND Order_Item_Id = {orderItemId} " +
                   $"AND Warehouse_Bin_Id = {warehouseBinId} ";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<PackagingItem>(dataOperation);

    }


    public FixedList<PackagingItem> GetPackingItemByOrderItemAndWarehouseBin(
      int orderItemId, int warehouseBinId) {

      string sql = $"SELECT * " +
                   $"FROM OMS_Packaging_Items " +
                   $"WHERE Order_Item_Id = {orderItemId} AND Warehouse_Bin_Id IN ({warehouseBinId})";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<PackagingItem>(dataOperation);

    }


    public FixedList<OrderItemTemp> GetOrderItems(string orderUid) {

      int orderId = SalesOrder.Parse(orderUid).Id;

      string sql = $"SELECT Order_Item_Order_Id, Order_Item_Id, Order_Item_UID, " +
                   $"Order_Item_Product_Id, Order_Item_Qty " +
                   $"FROM OMS_Order_Items WHERE Order_Item_Status = 'A' AND Order_Item_Order_Id = {orderId}";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<OrderItemTemp>(dataOperation);

    }


    public static void WritePacking(PackagingEntry packagingEntry) {

      var op = DataOperation.Parse("write_OMS_Packaging",
                packagingEntry.Id, packagingEntry.UID, packagingEntry.PackageType.Id,
                packagingEntry.SalesOrder.Id, packagingEntry.PackageID, packagingEntry.PostedBy.Id,
                packagingEntry.PostingTime);

      DataWriter.Execute(op);
    }


    public void DeletePackingOrderItem(string packingItemEntryUID) {
      
      string sql = $"DELETE FROM OMS_Packaging_Items WHERE Packing_Item_UID = '{packingItemEntryUID}'";

      var dataOperation = DataOperation.Parse(sql);

      DataWriter.Execute(dataOperation);
    }


    public void DeletePackageForItem(string packageForItemUID) {
      
      var package = PackagingEntry.Parse(packageForItemUID);

      if (package?.OrderPackingId > 0) {

        string sql = $"DELETE FROM OMS_Packaging_Items WHERE Order_Packing_Id = {package.OrderPackingId}";

        var dataOpItem = DataOperation.Parse(sql);

        DataWriter.Execute(dataOpItem);

        string sqlPackage = $"DELETE FROM OMS_Packaging WHERE Order_Packing_Id = {package.OrderPackingId}";

        var dataOpPackage = DataOperation.Parse(sqlPackage);

        DataWriter.Execute(dataOpPackage);
      }
      
    }


    #region Private methods


    #endregion Private methods

  } // class PackagingData


  public class OrderItemTemp {


    [DataField("Order_Item_Order_Id")]
    public SalesOrder Order {
      get;
      protected set;
    }

    [DataField("Order_Item_Id")]
    public int OrderItemId {
      get; set;
    }


    [DataField("Order_Item_UID")]
    public string OrderItemUID {
      get; set;
    }


    [DataField("Order_Item_Product_Id")]
    public int ProductId {
      get; set;
    }


    [DataField("Order_Item_Qty")]
    public decimal Quantity {
      get; set;
    }


  }


} // namespace Empiria.Trade.ShippingAndHandling.Data
