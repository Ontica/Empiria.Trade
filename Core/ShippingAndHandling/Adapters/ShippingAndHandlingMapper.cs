/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Shipping And Handling Management           Component : Interface adapters                      *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Mapper class                            *
*  Type     : ShippingAndHandlingMapper                  License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Methods used to map Shipping And Handling.                                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Empiria.Trade.Orders;
using Empiria.Trade.Products;

namespace Empiria.Trade.ShippingAndHandling.Adapters {


  /// <summary>Methods used to map Shipping And Handling.</summary>
  static internal class ShippingAndHandlingMapper {


    #region Public methods


    static internal IShippingAndHandling MapPackagingOrder(PackageForItem packagings) {

      return MapEntry(packagings);
    }


    internal static IShippingAndHandling MapPackingDto(FixedList<Packing> packings) {

      var packingItems = MapToPackingItems(packings);
      var packingData = MapPackingData(packingItems);
      var missingItems = MapToMissingItems(packings);

      return new PackingDto {
        Data = packingData,
        PackagedItems = packingItems,
        MissingItems = missingItems
      };

    }


    #endregion Public methods


    #region Private methods


    static private void GetWarehouses(PackingItemDto packingOrderItem, Packing item) {

      if (item.InventoryEntry?.WarehouseId > 0) {
        var warehouse = Warehouse.Parse(item.InventoryEntry.WarehouseId);
        var whDto = new WarehouseDto();
        whDto.UID = warehouse.UID;
        whDto.Code = warehouse.Code;
        whDto.Name = warehouse.Name;
        //whDto.Stock = //TODO SACAR STOCK DE INVENTARIO-WAREHOUSE
        packingOrderItem.Warehouse = whDto;
      }

      if (item.InventoryEntry?.WarehouseBinId > 0) {

        var warehouseBin = WarehouseBin.Parse(item.InventoryEntry.WarehouseBinId);
        var whBinDto = new WarehouseBinDto();
        whBinDto.UID = warehouseBin.WarehouseBinUID;
        whBinDto.Name = warehouseBin.BinCode;
        whBinDto.WarehouseName = $"Almacen {warehouseBin.Warehouse.Code}";
        //whBinDto.Stock = //TODO SACAR STOCK DE INVENTARIO-WAREHOUSE
        packingOrderItem.WarehouseBin = whBinDto;
      }
    }


    static private FixedList<PackingItemDto> GetOrderItems(string orderPackingUID,
                                                             FixedList<Packing> packings) {
      var packingOrderItems = new List<PackingItemDto>();

      var items = packings.FindAll(x => x.OrderPackingUID == orderPackingUID);

      foreach (var item in items) {

        var packingOrderItem = new PackingItemDto();
        packingOrderItem.UID = item.PackingItemUID;
        packingOrderItem.MergeCommonFieldsData(item.OrderItemId);
        packingOrderItem.Quantity = item.Quantity;
        GetWarehouses(packingOrderItem, item);
        packingOrderItems.Add(packingOrderItem);
      }

      return packingOrderItems.ToFixedList();
    }


    static private PackagedData MapPackingData(FixedList<PackageForItemDto> packingItems) {
      var data = new PackagedData();

      decimal _vol = 0;
      foreach (var item in packingItems) {
        var type = PackageType.Parse(item.PackageTypeUID);

        if (type != null) {
          type.GetVolumeAttributes();
          _vol += type.TotalVolume;
        }

      }

      data.OrderUID = packingItems.Select(x => x.OrderUID).First();
      data.Size = _vol;
      data.Count = packingItems.Count();

      return data;
    }


    private static FixedList<PackageForItemDto> MapToPackingItems(FixedList<Packing> packings) {

      var packingItems = new List<PackageForItemDto>();

      foreach (var entry in packings) {
        var item = new PackageForItemDto();

        item.UID = entry.OrderPackingUID;
        item.OrderUID = entry.Order.UID;
        item.PackageID = entry.PackageID;
        item.PackageTypeUID = entry.PackageType.ObjectKey;
        item.PackageTypeName = entry.PackageType.Name;

        var exist = packingItems.Find(x => x.UID == item.UID);

        if (exist == null) {
          item.OrderItems = GetOrderItems(entry.OrderPackingUID, packings);
          packingItems.Add(item);

        }

      }

      return packingItems.ToFixedList();
    }


    static private FixedList<MissingItemDto> MapToMissingItems(FixedList<Packing> packings) {

      var missingItems = new List<MissingItemDto>();

      foreach (var pack in packings) {
        var orderItem = OrderItem.Parse(pack.OrderItemId);

        var exist = missingItems.Find(x => x.OrderItemUID == orderItem.UID);

        if (exist == null) {
          var quantityOrderItems = packings.Where(x => x.OrderItemId == orderItem.Id).Sum(x => x.Quantity);

          if (orderItem.Quantity > quantityOrderItems) {
            var missing = new MissingItemDto();
            missing.OrderItemUID = orderItem.UID;
            missing.Quantity = orderItem.Quantity - quantityOrderItems;
            missing.MergeCommonFieldsData(pack.OrderItemId);

            missingItems.Add(missing);
          }

        }

      }

      return missingItems.ToFixedList();
    }


    static private PackingOrderDto MapEntry(PackageForItem packaging) {

      var dto = new PackingOrderDto();
      dto.Order = packaging.Order;
      dto.PackageID = packaging.PackageID;

      return dto;
    }




    #endregion Private methods


  } // class ShippingAndHandlingMapper

} // namespace Empiria.Trade.ShippingAndHandling.Adapters
