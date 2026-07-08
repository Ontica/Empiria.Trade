/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Mapper class                            *
*  Type     : InventoryOrderMapper                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Methods used to map Inventory order.                                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using System.Linq;
using Empiria.Orders;
using Empiria.Products;
using Empiria.Projects;
using Empiria.StateEnums;
using Empiria.Trade.Core;
using Empiria.Trade.Core.Common;

namespace Empiria.Trade.Inventory.Adapters {


  /// <summary>Methods used to map Inventory order.</summary>
  static internal class InventoryOrderMapper {

    #region Public methods V2

    static public InventoryOrderDataDto InventoryOrderDataDto(FixedList<InventoryOrder> list,
                                                              InventoryOrderQuery query) {

      return new InventoryOrderDataDto {
        Query = query,
        Columns = GetColumns(),
        Entries = MapInventoryOrderDescriptors(list)
      };
    }


    static internal FixedList<InventoryOrderDescriptorDto> MapInventoryOrderDescriptors(
                                                            FixedList<InventoryOrder> orders) {
      return orders.Select(x => MapToDescriptor(x))
                   .ToFixedList();
    }


    static private InventoryOrderDescriptorDto MapToDescriptor(InventoryOrder order) {

      return new InventoryOrderDescriptorDto() {
        UID = order.UID,
        OrderTypeName = order.OrderType.DisplayName,
        OrderNo = order.OrderNo,
        InventoryTypeName = order.InventoryType.Name,
        WarehouseName = order.Warehouse.Id == -1 ? "Sin asignar" : order.Warehouse.Name,
        ResponsibleName = order.Responsible.IsEmptyInstance ? "Sin Asignar" : order.Responsible.Name,
        RequestedByName = order.RequestedBy.Name,
        Description = order.Description,
        DocumentNo = GetDocumentNo(order),
        StakeholderName = GetStakeholderName(order),
        PostedByName = order.PostedBy.Name,
        PostingTime = order.PostingTime,
        Status = order.Status.GetName()
      };
    }


    static internal InventoryHolderDto MapToHolderDto(InventoryOrder order, InventoryOrderActions actions) {

      return new InventoryHolderDto {
        Order = MapToOrderDto(order),
        Items = MapToOrderItemsDto(order.GetItems<InventoryOrderItem>()),
        Actions = actions
      };
    }


    private static FixedList<InventoryOrderItemDto> MapToOrderItemsDto(FixedList<InventoryOrderItem> items) {

      return items.Select((x) => MapToOrderItemDto(x))
                         .ToFixedList();
    }


    static private InventoryOrderItemDto MapToOrderItemDto(InventoryOrderItem item) {

      var itemPackaging = item.Product.BaseUnit != item.ProductUnit ?
                          $"| Empaque: {item.PackingSmallBag} " +
                          $"| Unidades por {item.ProductUnit.Name}: {item.PackagingSize} " +
                          $"{item.Product.BaseUnit.Description}(s)" :
                          $"| {item.ProductUnit.Name}(s)";

      return new InventoryOrderItemDto() {
        UID = item.UID,
        ProductName = $"{item.Product.InternalCode} " +
                      $"{itemPackaging}",
        Quantity = item.Quantity,
        Location = item.Location.Name,
        AssignedQuantity = item.Entries.Sum(x => x.InputQuantity),
        PostedBy = item.PostedBy.MapToNamedEntity(),
        PostingTime = item.PostingTime,
        Entries = MapToInventoryEntriesDto(item.Entries),
        Status = item.Status
      };
    }


    static internal InventoryEntryDto MapToInventoryEntryDto(InventoryEntry entry) {
      decimal quantity = 0;

      if (entry.Order.Category.UID == "0eb5a072-b857-4071-8b06-57a34822ec64") {
        quantity = entry.OutputQuantity;
      } else {
        quantity = entry.InputQuantity;
      }

      return new InventoryEntryDto {
        UID = entry.UID,
        Product = entry.Product.Name,
        Location = entry.Location.Name,
        Quantity = quantity,
        PostedBy = entry.PostedBy.MapToNamedEntity(),
        PostingTime = entry.PostingTime
      };
    }


    static private FixedList<InventoryEntryDto> MapToInventoryEntriesDto(FixedList<InventoryEntry> items) {

      return items.Select((x) => MapToInventoryEntryDto(x))
                         .ToFixedList();
    }


    static private string GetDocumentNo(InventoryOrder inventoryOrder) {

      if (inventoryOrder.ParentOrder.Id == -1) {
        return string.Empty;
      }

      var order = Order.Parse(inventoryOrder.ParentOrder.Id);
      return order.OrderNo;
    }


    private static string GetStakeholderName(InventoryOrder inventoryOrder) {
      if (inventoryOrder.ParentOrder.Id == -1) {
        return string.Empty;
      }

      var order = Order.Parse(inventoryOrder.ParentOrder.Id);

      if (order.Category.UID == "a40c65bd-9a56-48eb-a8bf-f9245ecd3004") {
        return order.Provider.Name;
      } else {
        return order.Beneficiary.Name;
      }
    }


    private static InventoryOrderDto MapToOrderDto(InventoryOrder order) {

      return new InventoryOrderDto {
        UID = order.UID,
        OrderNo = order.OrderNo,
        OrderType = order.OrderType.MapToNamedEntity(), // new NamedEntityDto("X","Orden de inventario"),
        InventoryType = MapToInventoryTypeDto(order.InventoryType),
        Warehouse = order.Warehouse.MapToNamedEntity(),
        Responsible = order.Responsible.MapToNamedEntity(),
        RequestedBy = order.RequestedBy.MapToNamedEntity(),
        Description = order.Description,
        PostedBy = order.PostedBy.MapToNamedEntity(),
        PostingTime = order.PostingTime,
        ClosingTime = order.ClosingTime,
        Status = order.Status.MapToDto()
      };
    }


    private static InventoryTypeDto MapToInventoryTypeDto(InventoryType inventoryType) {

      return new InventoryTypeDto {
        UID = inventoryType.UID,
        Name = inventoryType.Name,
        Rules = MapInventoryTypeRules(inventoryType),
      };
    }


    private static InventoryTypeRulesDto MapInventoryTypeRules(InventoryType inventoryType) {

      return new InventoryTypeRulesDto {
        EntriesRequired = inventoryType.EntriesRequired,
        ItemsRequired = inventoryType.ItemsRequired,
      };
    }

    #endregion Public methods V2

    #region Private methods

    static private FixedList<DataTableColumn> GetColumns() {

      List<DataTableColumn> columns = new List<DataTableColumn>();

      columns.Add(new DataTableColumn("inventoryTypeName", "Tipo", "text"));
      columns.Add(new DataTableColumn("orderNo", "Orden", "text-link"));
      columns.Add(new DataTableColumn("warehouseName", "Almacén", "text"));
      columns.Add(new DataTableColumn("responsibleName", "Responsable", "text"));
      columns.Add(new DataTableColumn("documentNo", "No. Documento", "text"));
      columns.Add(new DataTableColumn("stakeholderName", "Cliente/Proveedor", "text"));
      columns.Add(new DataTableColumn("postingTime", "Registro", "date"));
      columns.Add(new DataTableColumn("status", "Estatus", "text-tag"));

      return columns.ToFixedList();
    }

    #endregion Private methods


  } // class InventoryOrderMapper

} // namespace Empiria.Trade.Inventory.Adapters
