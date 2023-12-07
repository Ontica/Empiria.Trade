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


    public FixedList<MissingItemDto> GetMissingItems(string orderUid,
                                        FixedList<PackageForItemDto> packagesForItems) {

      var missingItems = new List<MissingItemDto>();

      var data = new ShippingAndHandlingData();
      var orderItems = data.GetOrderItems(orderUid);
      var packingOrderItems = packagesForItems.SelectMany(x => x.OrderItems).ToList();

      foreach (var item in orderItems) {
        var quantityOrderItems = packingOrderItems
                                  .Where(x => x.OrderItemUID == item.OrderItemUID)
                                  .Sum(x => x.Quantity);

        if (item.Quantity > quantityOrderItems) {
          var missing = new MissingItemDto();
          missing.OrderItemUID = item.OrderItemUID;
          missing.Quantity = item.Quantity - quantityOrderItems;
          missing.MergeCommonFieldsData(item.OrderItemId);

          GetWarehousesByItem(missing, item.VendorProductId);

          missingItems.Add(missing);
        }
      }
      return missingItems.ToFixedList();
    }



    public FixedList<PackageForItemDto> GetPackagesByOrder(string orderUid, FixedList<PackageForItem> packItems) {
      var packagesList = new List<PackageForItemDto>();

      foreach (var entry in packItems) {
        var packageType = PackageType.Parse(entry.PackageTypeId);

        var package = new PackageForItemDto();
        package.UID = entry.OrderPackingUID;
        package.OrderUID = orderUid;
        package.PackageID = entry.PackageID;
        package.PackageTypeUID = packageType.ObjectKey;
        package.PackageTypeName = packageType.Name;

        package.OrderItems = GetPackingItems(orderUid, entry.OrderPackingId);

        packagesList.Add(package);

      }

      return packagesList.ToFixedList();
    }


    public PackagedData GetPackingData(string orderUid, FixedList<PackageForItemDto> packageForItemsList) {

      var data = new PackagedData();

      decimal _vol = 0;

      foreach (var item in packageForItemsList) {
        var type = PackageType.Parse(item.PackageTypeUID);

        if (type != null) {
          type.GetVolumeAttributes();
          _vol += type.TotalVolume;
        }
      }

      data.OrderUID = orderUid;
      data.Size = _vol;
      data.Count = packageForItemsList.Count();

      return data;
    }


    #endregion Public methods

    #region Private methods


    private FixedList<PackingItemDto> GetPackingItems(string orderUid, int orderPackingId) {

      var data = new ShippingAndHandlingData();
      var packingItems = data.GetPackingOrderItems(orderPackingId);

      var packingOrderItems = new List<PackingItemDto>();

      foreach (var item in packingItems) {
        var packingOrderItem = new PackingItemDto();
        packingOrderItem.UID = item.PackingItemUID;
        packingOrderItem.MergeCommonFieldsData(item.OrderItemId);
        packingOrderItem.Quantity = item.Quantity;
        GetWarehouses(packingOrderItem, item);
        packingOrderItems.Add(packingOrderItem);
      }

      return packingOrderItems.ToFixedList();
    }


    static public void GetWarehouses(PackingItemDto packingOrderItem, PackingOrderItem item) {

      var inventory = InventoryEntry.Parse(item.InventoryEntryId);
      if (inventory?.WarehouseId > 0) {

        var warehouse = Warehouse.Parse(inventory.WarehouseId);

        var whDto = new WarehouseDto();
        whDto.UID = warehouse.UID;
        whDto.Code = warehouse.Code;
        whDto.Name = warehouse.Name;
        //whDto.Stock = //TODO SACAR STOCK DE INVENTARIO-WAREHOUSE
        packingOrderItem.Warehouse = whDto;
      }

      if (inventory?.WarehouseBinId > 0) {

        var warehouseBin = WarehouseBin.Parse(inventory.WarehouseBinId);
        var whBinDto = new WarehouseBinDto();
        whBinDto.UID = warehouseBin.WarehouseBinUID;
        whBinDto.OrderItemUID = packingOrderItem.OrderItemUID;
        whBinDto.Name = warehouseBin.BinCode;
        whBinDto.WarehouseName = $"Almacen {warehouseBin.Warehouse.Code}";
        //whBinDto.Stock = //TODO SACAR STOCK DE INVENTARIO-WAREHOUSE
        packingOrderItem.WarehouseBin = whBinDto;
      }
    }


    public void GetWarehousesByItem(MissingItemDto missing, int vendorProductId) {

      var data = new ShippingAndHandlingData();

      FixedList<InventoryEntry> inventory = data.GetInventoryByVendorProduct(vendorProductId, "");

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
          bin.WarehouseName = $"Almacen {_whBin.Warehouse.Code}";
          bin.Stock = input > output ? input - output : 0;

          whBinDto.Add(bin);
        }

      }
      missing.WarehouseBins = whBinDto.ToFixedList();
    }


    #endregion Private methods

  } // class PackingHelper
} // namespace Empiria.Trade.ShippingAndHandling.Domain
