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

    private const string INVENTORYORDERTYPE = "ObjectTypeInfo.Order.InventoryOrder";

    #region Public methods

    static internal void GenerateInventoryOrder(PurchaseOrder purchaseOrder) {

      var orderType = OrderType.Parse(INVENTORYORDERTYPE);

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
        Description = $"Generado desde: {order.OrderNo}",
        InventoryTypeUID = InventoryType.InventarioEntradasCompra.UID,
        RequestedByUID = order.RequestedBy.UID,
        ResponsibleUID = order.Responsible.UID,
        WarehouseUID = order.Warehouse.UID
      };
    }


    private static InventoryOrderItemFields AssignInventoryOrderItemFields(
                      InventoryOrder inventoryOrder, PurchaseOrderItem purchaseOrderItem) {

      var maxOrderItem = InventoryData.SearchMaxOrderItemPosition(inventoryOrder);

      return new InventoryOrderItemFields {
        Product = purchaseOrderItem.Product.InternalCode,
        Location = "A-001-01-01",
        Position = maxOrderItem.Count > 0 ? maxOrderItem.First().Position + 1 : 1,
        ProductUID = purchaseOrderItem.Product.UID,
        Description = purchaseOrderItem.Product.Description,
        ProductUnitUID = purchaseOrderItem.Product.BaseUnit.UID,
        Quantity = purchaseOrderItem.Quantity,
      };
    }


    static private void GenerateInventoryOrderItems(PurchaseOrder purchaseOrder,
                                                    InventoryOrder inventoryOrder) {
      
      var purchaseOrderItems = PurchaseOrderItem.GetListFor(purchaseOrder);

      foreach (var purchaseOrderItem in purchaseOrderItems) {

        InventoryOrderItemFields inventoryItemFields = AssignInventoryOrderItemFields(inventoryOrder,
                                                                                      purchaseOrderItem);

        var orderItemType = OrderItemType.Parse("ObjectTypeInfo.OrderItem.InventoryOrderItem");

        InventoryOrderItem orderItem = new InventoryOrderItem(orderItemType, inventoryOrder,
                                                              new Locations.Location());

        orderItem.Update(inventoryItemFields);
        orderItem.Save();
      }
    }

    #endregion Private methods

  } // class PurchaseOrderBuilder

} // namespace Empiria.Trade.Inventory.PurchaseOrders.Domain
