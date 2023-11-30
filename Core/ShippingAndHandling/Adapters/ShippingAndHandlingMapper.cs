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
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Empiria.Trade.Orders;
using Empiria.Trade.Products;

namespace Empiria.Trade.ShippingAndHandling.Adapters {


  /// <summary>Methods used to map Shipping And Handling.</summary>
  static internal class ShippingAndHandlingMapper {


    #region Public methods


    static internal IShippingAndHandling MapPackagingOrder(ShippingAndHandling.PackingItem packagings) {

      return MapEntry(packagings);
    }


    internal static IShippingAndHandling MapPackingDto(FixedList<Packing> packings) {

      var packingItems = MapToPackingItems(packings);

      return new PackingDto {
        PackingData = MapPackingData(packingItems),
        PackingItem = packingItems,
        MissingItems = MapToMissingItems(packings)
      };

    }


    #endregion Public methods


    #region Private methods

    
    static private void GetWarehouses(PackingOrderItem packingOrderItem, Packing item) {

      if (item.InventoryEntry?.WarehouseId > 0) {
        packingOrderItem.Warehouse = Warehouse.Parse(item.InventoryEntry.WarehouseId);
      }

      if (item.InventoryEntry?.WarehouseBinId > 0) {
        
        var warehouseBin = WarehouseBin.Parse(item.InventoryEntry.WarehouseBinId);
        var whBinDto = new WarehouseBinDto();
        whBinDto.UID = warehouseBin.WarehouseBinUID;
        whBinDto.Name = warehouseBin.BinCode;
        whBinDto.WarehouseName = $"Almacen {warehouseBin.Warehouse.Code}";
        packingOrderItem.WarehouseBin = whBinDto;
      }
    }


    static private FixedList<PackingOrderItem> GetOrderItems(string orderPackingUID,
                                                             FixedList<Packing> packings) {
      var packingOrderItems = new List<PackingOrderItem>();

      var items = packings.FindAll(x => x.OrderPackingUID == orderPackingUID);

      foreach (var item in items) {

        var packingOrderItem = new PackingOrderItem();
        packingOrderItem.UID = item.PackingItemUID;
        packingOrderItem.MergeFieldsData(item.OrderItemId);
        packingOrderItem.Quantity = item.Quantity;
        GetWarehouses(packingOrderItem, item);
        packingOrderItems.Add(packingOrderItem);
      }

      return packingOrderItems.ToFixedList();
    }


    static private PackingData MapPackingData(FixedList<PackingItem> packingItems) {
      var data = new PackingData();

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


    private static FixedList<PackingItem> MapToPackingItems(FixedList<Packing> packings) {

      var packingItems = new List<PackingItem>();

      foreach (var entry in packings) {
        var item = new PackingItem();

        item.UID= entry.OrderPackingUID;
        item.OrderUID = entry.Order.UID;
        item.Name = entry.PackageID;
        item.PackageTypeUID = entry.PackageType.ObjectKey;
        item.PackageTypeName = entry.PackageType.Name;

        var exist = packingItems.Find(x=>x.UID == item.UID);
        
        if (exist == null) {
          item.OrderItems = GetOrderItems(entry.OrderPackingUID, packings);
          packingItems.Add(item);
          
        }
        
      }

      return packingItems.ToFixedList();
    }


    static private FixedList<MissingItem> MapToMissingItems(FixedList<Packing> packings) {

      var missingItems = new List<MissingItem>();

      //foreach (var item in packings) {
      //  var items = packings.FindAll(x => x.OrderPackingUID == item.OrderPackingUID);
      //}

      return missingItems.ToFixedList();
    }


    static private PackingOrderDto MapEntry(ShippingAndHandling.PackingItem packaging) {

      var dto = new PackingOrderDto();
      dto.Order = packaging.Order;
      dto.PackageID = packaging.PackageID;
      dto.Size = packaging.Size;

      return dto;
    }




    #endregion Private methods


  } // class ShippingAndHandlingMapper

} // namespace Empiria.Trade.ShippingAndHandling.Adapters
