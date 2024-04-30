/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Interface adapters                      *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Mapper class                            *
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


    static internal InventoryOrderDto MapInventoryOrder(InventoryOrderEntry entry) {
      var dto = new InventoryOrderDto();

      var responsible = Party.Parse(entry.ResponsibleId);
      var assignedTo = Party.Parse(entry.AssignedToId);
      var postedBy = Party.Parse(entry.PostedById);
      
      dto.UID = entry.InventoryOrderUID;
      dto.InventoryOrderType = GetInventoryOrderType(entry.InventoryOrderTypeId); //TODO REGISTRSR INVENTORY TYPES EN TYPES
      dto.InventoryOrderNo = entry.InventoryOrderNo;
      dto.ExternalObjectReference = new NamedEntityDto("", "External reference"); //External.Parse(entry.ExternalObjectReferenceId).UID;
      dto.Responsible = new NamedEntityDto(responsible.UID, responsible.Name);
      dto.AssignedTo = new NamedEntityDto(assignedTo.UID, assignedTo.Name);
      dto.Notes = entry.Notes;
      dto.ClosingTime = entry.ClosingTime;
      dto.PostingTime = entry.PostingTime;
      dto.PostedBy = new NamedEntityDto(postedBy.UID, postedBy.Name);
      dto.Status = entry.Status;

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
      columns.Add(new DataTableColumn("inventoryStatus", "Estatus", "text_tag"));

      return columns.ToFixedList();
    }


    private static InventoryOrderDescriptorDto MapInventoryDescriptorList(InventoryOrderEntry entry) {
      var dto = new InventoryOrderDescriptorDto();

      var responsible = Party.Parse(entry.ResponsibleId);
      var assignedTo = Party.Parse(entry.AssignedToId);
      var postedBy = Party.Parse(entry.PostedById);

      dto.UID = entry.InventoryOrderUID;
      dto.InventoryOrderTypeName = GetInventoryOrderType(entry.InventoryOrderTypeId).Name; //TODO REGISTRSR INVENTORY TYPES EN TYPES
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
      dto.InventoryOrderUID = x.InventoryOrder.InventoryOrderUID;
      dto.ExternalObjectItemReferenceUID = ""; //External.Parse(x.ExternalObjectItemReferenceId).UID;
      dto.Notes = x.ItemNotes;
      dto.VendorProduct = new NamedEntityDto(
        x.VendorProduct.VendorProductUID,
        $"{x.VendorProduct.ProductFields.ProductCode} " +
        $"{x.VendorProduct.ProductPresentation.PresentationName}");
      dto.ProductDescription = x.VendorProduct.ProductFields.ProductDescription;
      dto.WarehouseBin = new NamedEntityDto(
        x.WarehouseBin.WarehouseBinUID,
        x.WarehouseBin.BinDescription);
      dto.Quantity = x.Quantity;
      dto.InputQuantity = x.InputQuantity;
      dto.OutputQuantity = x.OutputQuantity;
      dto.Status = x.Status;
      return dto;
    }


    static private FixedList<InventoryOrderItemDto> MapInventoryItems(
        FixedList<InventoryOrderItem> inventoryOrderItems) {

      if (inventoryOrderItems.Count == 0) {
        return new FixedList<InventoryOrderItemDto>();
      }

      var mappedItems = inventoryOrderItems.Select((x) => MapInventoryItem(x));

      return new FixedList<InventoryOrderItemDto>(mappedItems);
    }


    static private FixedList<IInventoryOrderDto> MapList(
      FixedList<InventoryOrderEntry> list) {

      var mappedList = list.Select((x) => MapInventoryDescriptorList(x));

      return new FixedList<IInventoryOrderDto>(mappedList);
    }


    static private NamedEntityDto GetInventoryOrderType(int typeId) {

      if (typeId == 1) {
        return new NamedEntityDto("5851e71b-3a1f-40ab-836f-ac3d2c9408de", "Orden de conteo físico inicial");

      } else if (typeId == 2) {
        return new NamedEntityDto("ab8e950e-94e9-4ae5-943a-49abad514g52", "Orden de conteo físico mensual");

      } else if (typeId == 3) {
        return new NamedEntityDto("wered868-a7ec-47f5-b1b9-8c0f73b04kuk", "Orden de conteo físico anual");

      } else if (typeId == 4) {
        return new NamedEntityDto("2vgf36bc-535c-4a07-8475-3e6568ebbopi", "Orden de traspaso");

      } else {
        return new NamedEntityDto("","");
      }

    }

    #endregion Private methods


  } // class InventoryOrderMapper

} // namespace Empiria.Trade.Inventory.Adapters
