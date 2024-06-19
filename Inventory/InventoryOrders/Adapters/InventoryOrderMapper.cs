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
using Empiria.Trade.Core;
using Empiria.Trade.Core.Common;
using Empiria.Trade.Inventory.Domain;

namespace Empiria.Trade.Inventory.Adapters {


  /// <summary>Methods used to map Inventory order.</summary>
  static internal class InventoryOrderMapper {


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
        Entries = MapList(list)
      };
    }


    #endregion Public methods

    #region Private methods


    static private FixedList<DataTableColumn> GetColumns() {

      List<DataTableColumn> columns = new List<DataTableColumn>();

      columns.Add(new DataTableColumn("inventoryOrderTypeName", "Tipo", "text"));
      columns.Add(new DataTableColumn("inventoryOrderNo", "Número de orden", "text-link"));
      columns.Add(new DataTableColumn("responsibleName", "Responsable", "text"));
      columns.Add(new DataTableColumn("assignedToName", "Asignado a", "text"));
      columns.Add(new DataTableColumn("postingTime", "Fecha registro", "date"));
      columns.Add(new DataTableColumn("inventoryStatus", "Estatus", "text-tag"));

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
      dto.InventoryOrderTypeName = entry.InventoryOrderType.Name;
      dto.InventoryOrderNo = entry.InventoryOrderNo;
      dto.ExternalObjectReferenceName = "External reference"; //External.Parse(entry.ExternalObjectReferenceId).UID;
      dto.ResponsibleName = responsible.Name;
      dto.AssignedToName = assignedTo.Name;
      dto.Notes = entry.Notes;
      dto.ClosingTime = entry.ClosingTime;
      dto.PostingTime = entry.PostingTime;
      dto.PostedByName = postedBy.Name;
      dto.InventoryStatus = entry.Status;

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

        if (x.InventoryOrderTypeItemId == 5) {
          quantity = x.InProcessOutputQuantity;
        } else {
          quantity = x.CountingQuantity;
        }
        
      }
      
      if (x.Status == InventoryStatus.Cerrado) {
        
        if (x.InventoryOrderTypeItemId == 5) {
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
