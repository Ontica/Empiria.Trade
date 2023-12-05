/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Packing Management                         Component : Domain Layer                            *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Partitioned Type / Information Holder   *
*  Type     : PackingHelper                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Helper methods to build packing structure.                                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.ShippingAndHandling.Adapters;
using System.Collections.Generic;
using Empiria.Trade.Products;
using System.Linq;
using Empiria.Trade.Orders;
using Empiria.Trade.ShippingAndHandling.Data;

namespace Empiria.Trade.ShippingAndHandling.Domain {

  /// <summary>Helper methods to build packing structure.</summary>
  internal class PackingHelper {

    #region Public methods


    public PackagedData MapPackingData(string orderUid, FixedList<PackageForItemDto> packingItems) {
      
      var data = new PackagedData();

      decimal _vol = 0;

      if (packingItems.Count > 0) {

        foreach (var item in packingItems) {
          var type = PackageType.Parse(item.PackageTypeUID);

          if (type != null) {
            type.GetVolumeAttributes();
            _vol += type.TotalVolume;
          }
        }

      }

      data.OrderUID = orderUid;
      data.Size = _vol;
      data.Count = packingItems.Count();

      return data;
    }


    public FixedList<MissingItemDto> MapToMissingItems(string orderUid, FixedList<Packing> packings) {
      
      var missingItems = new List<MissingItemDto>();

      if (packings.Count == 0) {
        var data = new ShippingAndHandlingData();

        var orderItems = data.GetOrderItems(orderUid);
        foreach (var item in orderItems) {

          var missing = new MissingItemDto();
          missing.OrderItemUID = item.OrderItemUID;
          missing.Quantity = item.Quantity;

          var vendorProduct = VendorProduct.Parse(item.VendorProduct);
          missing.MergeCommonFieldsData(item.OrderItemId);
          GetWarehousesByItem(missing, vendorProduct.UID);
          missingItems.Add(missing);

        }
        
      } else {

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
              
              GetWarehousesByItem(missing, orderItem.VendorProduct.UID);

              missingItems.Add(missing);
            }
          }
        }
      }
      return missingItems.ToFixedList();
    }


    private void GetWarehousesByItem(MissingItemDto missing, string vendorProductUid) {

      var data = new ShippingAndHandlingData();

      FixedList<InventoryEntry> inventory = data.GetInventoryByVendorProduct(vendorProductUid, "");

      var whBinDto = new List<WarehouseBinDto>();

      foreach (var item in inventory) {
        var _whBin = WarehouseBin.Parse(item.WarehouseBinId);
        
        var exist = whBinDto.Find(x => x.UID == _whBin.UID && x.OrderItemUID == missing.OrderItemUID);

        if (exist == null) {
          var input = inventory.Where(x => x.WarehouseBinId == _whBin.Id).Sum(x => x.InputQuantity);
          var output = inventory.Where(x => x.WarehouseBinId == _whBin.Id).Sum(x => x.OutputQuantity);

          var bin = new WarehouseBinDto();
          bin.UID = _whBin.UID;
          bin.OrderItemUID = missing.OrderItemUID;
          bin.Name = _whBin.BinCode;
          bin.WarehouseName = _whBin.Warehouse.Code;
          bin.Stock = input - output;

          whBinDto.Add(bin);
        } 
        
      }
      missing.WarehouseBins = whBinDto.ToFixedList();
    }

    public FixedList<PackageForItemDto> MapToPackingItems(string orderUid, FixedList<Packing> packings) {
      
      var packingItems = new List<PackageForItemDto>();

      if (packings.Count == 0) {

        var data = new ShippingAndHandlingData();
        var packItems = data.GetPackingItems(orderUid);

        foreach (var entry in packItems) {
          var item = new PackageForItemDto();
          var packageType = PackageType.Parse(entry.PackageTypeId);

          item.UID = entry.OrderPackingUID;
          item.OrderUID = orderUid;
          item.PackageID = entry.PackageID;
          item.PackageTypeUID = packageType.ObjectKey;
          item.PackageTypeName = packageType.Name;
          packingItems.Add(item);
        }

      } else {


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


      }

      return packingItems.ToFixedList();
    }


    #endregion Public methods

    #region Private methods


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
        whBinDto.OrderItemUID = packingOrderItem.OrderItemUID;
        whBinDto.Name = warehouseBin.BinCode;
        whBinDto.WarehouseName = $"Almacen {warehouseBin.Warehouse.Code}";
        //whBinDto.Stock = //TODO SACAR STOCK DE INVENTARIO-WAREHOUSE
        packingOrderItem.WarehouseBin = whBinDto;
      }
    }


    #endregion Private methods

  } // class PackingHelper
} // namespace Empiria.Trade.ShippingAndHandling.Domain
