﻿/* Empiria Trade *********************************************************************************************
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
using Empiria.Trade.Inventory.Domain;

namespace Empiria.Trade.Inventory.Adapters {


  /// <summary>Methods used to map Inventory order.</summary>
  static internal class InventoryOrderMapper {


    #region Public methods


    static internal FixedList<InventoryOrderDto> MapInventoryList(FixedList<InventoryOrderEntry> list) {

      var mappedList = list.Select((x) => MapInventoryOrder(x));

      return new FixedList<InventoryOrderDto>(mappedList);
    }


    static public FixedList<InventoryOrderDescriptorDto> MapInventoryToDescriptorList(
      FixedList<InventoryOrderEntry> list) {

      var mappedList = list.Select((x) => MapInventoryDescriptorList(x));

      return new FixedList<InventoryOrderDescriptorDto>(mappedList);
    }


    private static InventoryOrderDescriptorDto MapInventoryDescriptorList(InventoryOrderEntry entry) {
      var dto = new InventoryOrderDescriptorDto();

      var responsible = Party.Parse(entry.ResponsibleId);
      var assignedTo = Party.Parse(entry.AssignedToId);
      var postedBy = Party.Parse(entry.PostedById);

      dto.UID = entry.InventoryOrderUID;
      dto.InventoryOrderTypeName = "Orden de conteo de inventario"; //TODO REGISTRSR INVENTORY TYPES EN TYPES
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


    static internal InventoryOrderDto MapInventoryOrder(InventoryOrderEntry entry) {
      var dto = new InventoryOrderDto();

      var responsible = Party.Parse(entry.ResponsibleId);
      var assignedTo = Party.Parse(entry.AssignedToId);
      var postedBy = Party.Parse(entry.PostedById);

      dto.UID = entry.InventoryOrderUID;
      dto.InventoryOrderType = new NamedEntityDto("", "Orden de conteo de inventario"); //TODO REGISTRSR INVENTORY TYPES EN TYPES
      dto.InventoryOrderNo = entry.InventoryOrderNo;
      dto.ExternalObjectReference = new NamedEntityDto("", "External reference"); //External.Parse(entry.ExternalObjectReferenceId).UID;
      dto.Responsible = new NamedEntityDto(responsible.UID, responsible.Name);
      dto.AssignedTo = new NamedEntityDto(assignedTo.UID, assignedTo.Name);
      dto.Notes = entry.Notes;
      dto.ClosingTime = entry.ClosingTime;
      dto.PostingTime = entry.PostingTime;
      dto.PostedBy = new NamedEntityDto(postedBy.UID, postedBy.Name);
      dto.InventoryStatus = entry.Status;

      dto.InventoryItems = MapInventoryItems(entry.InventoryOrderItems);
      return dto;
    }


    #endregion Public methods

    #region Private methods


    static private InventoryOrderItemDto MapInventoryItem(InventoryOrderItem x) {
      var dto = new InventoryOrderItemDto();

      dto.InventoryOrderItemUID = x.InventoryOrderItemUID;
      dto.UID = x.InventoryOrder.InventoryOrderUID;
      dto.ExternalObjectItemReferenceUID = ""; //External.Parse(x.ExternalObjectItemReferenceId).UID;
      dto.ItemNotes = x.ItemNotes;
      dto.VendorProductUID = x.VendorProduct.VendorProductUID;
      dto.WarehouseBinUID = x.WarehouseBin.WarehouseBinUID;
      dto.Quantity = x.Quantity;
      dto.InputQuantity = x.InputQuantity;
      dto.OutputQuantity = x.OutputQuantity;
      dto.ItemStatus = x.Status;
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

    #endregion Private methods


  } // class InventoryOrderMapper

} // namespace Empiria.Trade.Inventory.Adapters
