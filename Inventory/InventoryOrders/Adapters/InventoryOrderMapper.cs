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

using Empiria.Inventory;
using Empiria.Orders;
using Empiria.Trade.Core;
using Empiria.Trade.Core.Common;

using Empiria.StateEnums;

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
        WarehouseName = order.Warehouse.Name,
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

    #endregion Public methods V2


    #region Public methods

    static internal FixedList<InventoryOrderItemDto> MapInventoryItems(
        FixedList<InventoryOrderItem> inventoryOrderItems) {

      if (inventoryOrderItems.Count == 0) {
        return new FixedList<InventoryOrderItemDto>();
      }

      var mappedItems = inventoryOrderItems.Select((x) => MapInventoryItem(x));

      return new FixedList<InventoryOrderItemDto>(mappedItems);
    }


    static internal InventoryOrderDto MapInventoryOrder(InventoryOrderEntry entry,
      InventoryOrderActions actions) {
      var dto = new InventoryOrderDto();

      var responsible = Party.Parse(entry.ResponsibleId);
      var assignedTo = Party.Parse(entry.AssignedToId);
      var postedBy = Party.Parse(entry.PostedById);
      
      dto.UID = entry.InventoryOrderUID;
      dto.InventoryOrderType = 
        new NamedEntityDto(entry.InventoryOrderType.UID, entry.InventoryOrderType.Name);
      dto.InventoryOrderNo = entry.InventoryOrderNo;
      dto.ExternalObjectReference = new NamedEntityDto("", "External reference"); //External.Parse(entry.ExternalObjectReferenceId).UID;
      dto.Responsible = new NamedEntityDto(responsible.UID, responsible.Name);
      dto.AssignedTo = new NamedEntityDto(assignedTo.UID, assignedTo.Name);
      dto.Notes = entry.Notes;
      dto.ClosingTime = entry.ClosingTime;
      dto.PostingTime = entry.PostingTime;
      dto.PostedBy = new NamedEntityDto(postedBy.UID, postedBy.Name);
      dto.Status = entry.Status;
      dto.Actions = actions;
      dto.Items = MapInventoryItems(entry.InventoryOrderItems);
      return dto;
    }


    static public InventoryOrderDataDto MapList(
      FixedList<InventoryOrderEntry> list, InventoryOrderQuery query) {

      return new InventoryOrderDataDto {
        Query = query,
        Columns = GetColumns(),
        EntriesV1 = MapList(list)
      };
    }


    #endregion Public methods

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


    static private InventoryProductDto GetInventoryProductData(InventoryOrderItem x) {
      var dto = new InventoryProductDto();

      dto.ProductCode = x.VendorProduct.ProductFields.ProductCode;
      dto.ProductDescription = x.VendorProduct.ProductFields.ProductDescription;
      dto.Presentation = x.VendorProduct.ProductPresentation.PresentationName;

      return dto;
    }


    static private InventoryWarehouseBinDto GetInventoryWarehouseBinData(InventoryOrderItem x) {
      var dto = new InventoryWarehouseBinDto();
      dto.Rack = x.WarehouseBin.Rack;
      dto.RackDescription = x.WarehouseBin.Tag;
      return dto;
    }


    private static InventoryOrderDescriptorDto MapInventoryDescriptorList(InventoryOrderEntry entry) {
      var dto = new InventoryOrderDescriptorDto();

      var responsible = Party.Parse(entry.ResponsibleId);
      var assignedTo = Party.Parse(entry.AssignedToId);
      var postedBy = Party.Parse(entry.PostedById);

      dto.UID = entry.InventoryOrderUID;
      dto.InventoryTypeName = entry.InventoryOrderType.Name;
      dto.OrderNo = entry.InventoryOrderNo;
      dto.ResponsibleName = responsible.Name;
      dto.ResponsibleName = assignedTo.Name;
      dto.Description = entry.Notes;
      dto.PostingTime = entry.PostingTime;
      dto.PostedByName = postedBy.Name;
      dto.Status = entry.Status.ToString();

      return dto;
    }


    static private InventoryOrderItemDto MapInventoryItem(InventoryOrderItem x) {
      var dto = new InventoryOrderItemDto();

      dto.UID = x.InventoryOrderItemUID;
      dto.Product = GetInventoryProductData(x);
      dto.WarehouseBin = GetInventoryWarehouseBinData(x);
      dto.Quantity = GetInventoryQuantity(x);
      dto.Notes = x.ItemNotes;
      //dto.InputQuantity = x.InputQuantity;
      //dto.OutputQuantity = x.OutputQuantity;
      //dto.Status = x.Status;
      return dto;
    }


    private static decimal GetInventoryQuantity(InventoryOrderItem x) {

      decimal quantity = 0;
      if (x.Status == InventoryStatus.Abierto) {

        if (x.InventoryOrderTypeItemId == 504) {
          quantity = x.InProcessOutputQuantity;
        } else {
          quantity = x.CountingQuantity;
        }
        
      }
      
      if (x.Status == InventoryStatus.Cerrado) {
        
        if (x.InventoryOrderTypeItemId == 504) {
          quantity = x.OutputQuantity;
        } else {
          quantity = x.InputQuantity;
        }

      }
      return quantity;
    }


    static private FixedList<IInventoryOrderDto> MapList(
      FixedList<InventoryOrderEntry> list) {

      var mappedList = list.Select((x) => MapInventoryDescriptorList(x));

      return new FixedList<IInventoryOrderDto>(mappedList);
    }


    #endregion Private methods


  } // class InventoryOrderMapper

} // namespace Empiria.Trade.Inventory.Adapters
