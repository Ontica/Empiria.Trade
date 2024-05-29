/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Sales Order Management                     Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Sales.dll                    Pattern   : Partitioned Type / Information Holder   *
*  Type     : SalesOrderHeleperv                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  :  Helper methods to Seles Order.                                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using Empiria.Trade.Core.Inventories.Adapters;
using Empiria.Trade.Inventory.UseCases;
using Empiria.Trade.Sales.Adapters;
using Empiria.Trade.Sales.Data;
using Empiria.Trade.Sales.ShippingAndHandling.Data;
using Empiria.Trade.Sales.ShippingAndHandling;
using System.Collections.Generic;

namespace Empiria.Trade.Sales {

  /// <summary>Helper methods to Seles Order. </summary>
  public class SalesOrderHelper {

    public SalesOrderHelper() {

    }
    #region Public methods


    internal void CreateInventoryOrderBySale(int orderId) {

      FixedList<InventoryItems> dataForInventory = GetDataForInventoryOutput(orderId);
      
      if (dataForInventory.Count>0) {

        InventoryOrderUseCases inventoryOrderUseCases = new InventoryOrderUseCases();
        inventoryOrderUseCases.CreateInventoryOrderBySale(dataForInventory);
      }
    }


    public FixedList<SalesOrder> GetOrders(SearchOrderFields fields) {
      var orders = SalesOrderData.GetSalesOrders(fields);

      foreach (var order in orders) {
       
        order.GetOrderTotal();
      }
      return orders;
    }

    public FixedList<SalesOrder> GetOrdersToAuthorize(SearchOrderFields fields) {
      var orders = SalesOrderData.GetSalesOrdersToAuthorize(fields);

      foreach (var order in orders) {
        order.GetOrderTotal();
      }

      return orders;
    }

    public FixedList<SalesOrder> GetOrdersToPacking(SearchOrderFields fields) {
      var orders = SalesOrderData.GetSalesOrdersToPacking(fields);
      foreach (var order in orders) {
        order.GetOrderTotal();
      }

      return orders;
    }

    #endregion Public methods

    #region Private methods


    private FixedList<InventoryItems> GetDataForInventoryOutput(int orderId) {

      FixedList<SalesOrderItem> orderItems = SalesOrderItemsData.GetOrderItems(orderId);
      var dataForInventoryList = new List<InventoryItems>();

      foreach (var item in orderItems) {

        var data = new InventoryItems();
        data.OrderId = orderId;
        data.OrderItemId = item.Id;
        data.VendorProductUID = item.VendorProduct.VendorProductUID;
        data.WarehouseBinUID = "32ccf910-287d-4905-baf2-f41651927824"; //TODO EL ITEM NO SABE SOBRE EL PRODUCTO A NIVEL DE WAREHOUSEBIN
        data.Quantity = item.Quantity;
        dataForInventoryList.Add(data);
      }

      return dataForInventoryList.ToFixedList();
    }


    #endregion Private methods

  } // class SalesOrderHelper

} // Empiria.Trade.Sales