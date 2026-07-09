/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Trade Core                                 Component : Data Layer                              *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Data Service                            *
*  Type     : InventoryOrderData                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data read methods for inventory order.                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Data;
using Empiria.Locations;

using Empiria.Trade.Core;
using Empiria.Trade.Products;

namespace Empiria.Trade.Inventory.Data {


  /// <summary>Provides data read methods for inventory order.</summary>
  public class InventoryOrderData {

    #region Public methods

    internal static FixedList<InventoryOrder> SearchInventoryOrders(string filter, string sort) {

      var sql = "SELECT * FROM OMS_Orders";

      if (!string.IsNullOrWhiteSpace(filter)) {
        sql += $" WHERE {filter}";
      }
      if (!string.IsNullOrWhiteSpace(sort)) {
        sql += $" ORDER BY {sort}";
      }

      var op = DataOperation.Parse(sql);

      return DataReader.GetFixedList<InventoryOrder>(op);
    }


    internal static int VerifyProductAndLocationInOrder(int orderId, int productID, int locationID) {

      var sql = $"select count(*) from OMS_Order_Items where Order_Item_Order_Id = {orderId} " +
                $" and Order_Item_Product_Id = {productID} and Order_Item_Location_Id = {locationID} and Order_Item_Status <> 'X'";

      var op = DataOperation.Parse(sql);

      return DataReader.GetScalar<int>(op);
    }


    internal static decimal GetProductPriceFromVirtualWarehouse(int productId) {

      var sql = $"SELECT TOP  1 Inv_Entry_Input_Cost FROM " +
                $"OMS_Inventory_Entries " +
                $"where Inv_Entry_Order_Id = -10 and Inv_Entry_Product_Id = {productId} " +
                $"order by Inv_Entry_Time ";

      var op = DataOperation.Parse(sql);

      return DataReader.GetScalar<decimal>(op);
    }

    #endregion Public methods


    #region Methods - inventory entry

    internal static void DeleteEntry(int orderId, int orderItemId) {

      string sql = $"UPDATE OMS_Inventory_Entries " +
                   $"SET Inv_Entry_Status = 'X' " +
                   $"WHERE Inv_Entry_Order_Id = {orderId} AND " +
                   $"Inv_Entry_Order_Item_Id = {orderItemId}";

      var dataOperation = DataOperation.Parse(sql);

      DataWriter.Execute(dataOperation);
    }


    internal static void DeleteEntryStatus(int orderId, int orderItemId,
                                           int inventoryEntryId, InventoryStatus status) {

      string sql = $"UPDATE OMS_Inventory_Entries " +
                   $"SET Inv_Entry_Status = '{(char) status}' " +
                   $"WHERE Inv_Entry_Id = {inventoryEntryId} AND " +
                   $"Inv_Entry_Order_Id = {orderId} AND " +
                   $"Inv_Entry_Order_Item_Id = {orderItemId}";

      var dataOperation = DataOperation.Parse(sql);

      DataWriter.Execute(dataOperation);
    }


    internal static Location GetLocationEntryByName(string locationName) {

      try {

        var sql = $"SELECT * FROM Common_Storage " +
                $"WHERE Object_Type_Id = 5275 AND Object_Name = '{locationName}'";

        var op = DataOperation.Parse(sql);

        return DataReader.GetPlainObject<Location>(op);

      } catch (Exception) {

        throw new Exception("La Localización especificada no existe.");
      }
    }


    internal static ProductEntry GetProductEntryByName(string productName) {

      try {

        var sql = $"SELECT * FROM OMS_Products WHERE Product_Name = '{productName}'";

        var op = DataOperation.Parse(sql);

        return DataReader.GetPlainObject<ProductEntry>(op);

      } catch (Exception) {

        throw new Exception("Producto no coincide con el seleccionado.");
      }

    }


    internal static FixedList<InventoryEntriesReport> GetProductEntryInventoryReport(int orderId) {

      var sql = $"SELECT * FROM VW_Inventory_Report WHERE Inv_Entry_Order_Id = {orderId} ORDER BY Inv_entry_position";

      var op = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<InventoryEntriesReport>(op);
    }


    internal static FixedList<InventoryEntriesReport> GetProductEntryInventoryReportByLocation(string locationName) {

      var sql = $"SELECT * FROM VW_Inventory_Report " +
                 $" inner join Common_Storage on Inv_Entry_Location_Id = Object_Id " +
                 $" WHERE Object_Type_Id = 5275 and Object_Name = {locationName} ORDER BY Product_Name ";

      var op = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<InventoryEntriesReport>(op);
    }


    internal static void UpdateEntriesStatusByOrder(int orderId, InventoryStatus status) {

      string sql = $"UPDATE OMS_Inventory_Entries " +
                   $"SET Inv_Entry_Status = '{(char) status}' " +
                   $"WHERE Inv_Entry_Order_Id = {orderId} AND " +
                   $"Inv_Entry_Status != 'X'";

      var dataOperation = DataOperation.Parse(sql);

      DataWriter.Execute(dataOperation);
    }

    #endregion Methods - inventory entry

  } // class InventoryOrderData

} // namespace Empiria.Trade.Inventory.Data
