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
using Empiria.Trade.Inventory.Domain;

namespace Empiria.Trade.Inventory.Adapters {


    /// <summary>Methods used to map Inventory order.</summary>
    static internal class InventoryOrderMapper {


        #region Public methods


        static internal FixedList<InventoryOrderDto> MapInventoryList(FixedList<InventoryOrderEntry> list) {

            var mappedList = list.Select((x) => MapInventoryOrder(x));

            return new FixedList<InventoryOrderDto>(mappedList);
        }


        static internal InventoryOrderDto MapInventoryOrder(InventoryOrderEntry entry) {
            var dto = new InventoryOrderDto();

            dto.InventoryEntryUID = entry.InventoryEntryUID;
            dto.InventoryEntryName = entry.InventoryEntryName;
            dto.InventoryType = new NamedEntityDto("", "");
            dto.InventoryUser = new NamedEntityDto("", "");
            dto.InventoryItems = MapInventoryItems(entry.InventoryOrderItems);
            return dto;
        }


        #endregion Public methods

        #region Private methods


        static private InventoryOrderItemDto MapInventoryItem(InventoryOrderItem x) {
            var dto = new InventoryOrderItemDto();

            dto.InventoryItemUID = x.InventoryItemUID;
            dto.InventoryEntryUID = x.InventoryEntry.InventoryEntryUID;
            dto.VendorProductUID = x.VendorProduct.VendorProductUID;
            dto.WarehouseBinUID = x.WarehouseBin.WarehouseBinUID;
            dto.Quantity = x.Quantity;
            dto.Comments = x.Comments;
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
