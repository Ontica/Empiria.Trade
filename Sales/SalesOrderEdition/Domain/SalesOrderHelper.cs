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
using Empiria.Services;
using Empiria.Trade.Core;
using Empiria.Trade.Products;
using Empiria.Trade.Core.Catalogues;
using System.Linq;

namespace Empiria.Trade.Sales {

  /// <summary>Helper methods to Seles Order. </summary>
  public class SalesOrderHelper {

    public SalesOrderHelper() {

    }
    #region Public methods


    internal void CreateInventoryOrderBySale(FixedList<SalesOrderItem> salesOrderItems) {

      FixedList<InventoryItems> inventoryItems = GetDataForInventoryOutput(salesOrderItems);
      
      if (inventoryItems.Count>0) {

        InventoryOrderUseCases inventoryOrderUseCases = new InventoryOrderUseCases();
        inventoryOrderUseCases.CreateInventoryOrderBySale(inventoryItems);
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


    private FixedList<InventoryItems> GetDataForInventoryOutput(FixedList<SalesOrderItem> salesOrderItems) {

      var dataForInventoryList = new List<InventoryItems>();
      foreach (var item in salesOrderItems) {

        var cataloguesUsecase = CataloguesUseCases.UseCaseInteractor();

        var data = new InventoryItems();
        data.OrderId = item.Order.Id;
        data.OrderItemId = item.Id;
        data.VendorProductUID = item.VendorProduct.VendorProductUID;
        data.Quantity = item.Quantity;
        data.WarehouseBinUID = cataloguesUsecase.GetWarehouseBinByVendorProduct(
                                item.VendorProduct.Id).WarehouseBinUID;
        
        dataForInventoryList.Add(data);
      }

      return dataForInventoryList.ToFixedList();
    }


    #endregion Private methods

  } // class SalesOrderHelper

} // Empiria.Trade.Sales