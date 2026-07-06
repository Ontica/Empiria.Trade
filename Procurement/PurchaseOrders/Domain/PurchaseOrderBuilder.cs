/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Partitioned Type / Information Holder   *
*  Type     : PurchaseOrderBuilder                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Generate data for purchase order.                                                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Linq;
using Empiria.Orders;
using Empiria.Trade.Core;

namespace Empiria.Trade.Procurement.Domain {


  /// <summary>Generate data for purchase order.</summary>
  internal class PurchaseOrderBuilder {

    #region Public methods

    static internal void GenerateInventoryOrder(PurchaseOrder purchaseOrder) {

      var orderType = OrderType.InventoryOrder;

      InventoryOrderFields inventoryFields = AssignInventoryOrderFields(purchaseOrder);

      InventoryOrder inventoryOrder = new InventoryOrder(inventoryFields.WarehouseUID, orderType);

      inventoryOrder.Update(inventoryFields);

      inventoryOrder.Save();

      GenerateInventoryOrderItems(purchaseOrder, inventoryOrder);
    }


    #endregion Public methods


    #region Private methods


    private static InventoryOrderFields AssignInventoryOrderFields(PurchaseOrder order) {

      return new InventoryOrderFields {
        ParentOrderUID = order.UID,
        InventoryTypeUID = InventoryType.InventarioEntradasCompra.UID,
        Description = $"Generado por: {order.OrderNo}",
        CurrencyUID = order.Currency.UID,
        ExchangeRate = order.ExchangeRate,
        RequestedByUID = order.RequestedBy.UID,
        ResponsibleUID = order.Responsible.UID,
        WarehouseUID = order.Warehouse.UID
      };
    }


    private static InventoryOrderItemFields AssignInventoryOrderItemFields(
                      InventoryOrder inventoryOrder, PurchaseOrderItem item) {

      var maxOrderItem = InventoryData.SearchMaxOrderItemPosition(inventoryOrder);

      return new InventoryOrderItemFields {
        Product = item.Product.InternalCode,
        Location = "A-001-01-01",
        Position = maxOrderItem.Count > 0 ? maxOrderItem.First().Position + 1 : 1,
        ProductUID = item.Product.UID,
        Description = item.Product.Description,
        ProductUnitUID = item.ProductUnit.UID, //TODO PREGUNTAR SI ES UNIDAD DE ITEM O BASEUNIT DE PRODUCT
        Quantity = item.Quantity,
        UnitPrice = item.UnitPrice,
      };
    }


    static private void GenerateInventoryOrderItems(PurchaseOrder purchaseOrder,
                                                    InventoryOrder inventoryOrder) {
      
      var purchaseOrderItems = PurchaseOrderItem.GetListFor(purchaseOrder);

      foreach (var purchaseOrderItem in purchaseOrderItems) {

        InventoryOrderItemFields inventoryItemFields = AssignInventoryOrderItemFields(inventoryOrder,
                                                                                      purchaseOrderItem);

        var orderItemType = OrderItemType.InventoryOrderItemType;
        
        InventoryOrderItem orderItem = new InventoryOrderItem(orderItemType, inventoryOrder,
                                                              new Locations.Location());

        orderItem.Update(inventoryItemFields);
        orderItem.Save();
      }
    }

    #endregion Private methods

  } // class PurchaseOrderBuilder

} // namespace Empiria.Trade.Inventory.PurchaseOrders.Domain
