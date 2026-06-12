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
using System.Net.NetworkInformation;
using Empiria.Data;
using Empiria.StateEnums;
using Empiria.Trade.Core;
using Empiria.Trade.Inventory;
using Empiria.Trade.Inventory.Adapters;
using Empiria.Trade.Inventory.Domain;
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


    internal static void DeleteEntry(int orderId, int orderItemId) {

      string sql = $"UPDATE OMS_Inventory_Entries " +
                   $"SET Inv_Entry_Status = 'X' " +
                   $"WHERE Inv_Entry_Order_Id = {orderId} AND " +
                   $"Inv_Entry_Order_Item_Id = {orderItemId}";

      var dataOperation = DataOperation.Parse(sql);

      DataWriter.Execute(dataOperation);
    }

    #endregion Public methods

  } // class InventoryOrderData

} // namespace Empiria.Trade.Inventory.Data
