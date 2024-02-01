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
using System.Collections.Generic;
using System.Linq;
using Empiria.Trade.Sales.ShippingAndHandling.Data;
using Empiria.Trade.Core.Catalogues;

namespace Empiria.Trade.Sales.ShippingAndHandling.Domain {

  /// <summary>Helper methods to build packing structure.</summary>
  internal class PackingHelper {

    #region Public methods


    public FixedList<MissingItem> GetMissingItems(string orderUid,
                                        FixedList<PackagedForItem> packagesForItems) {

      var missingItems = new List<MissingItem>();

      var data = new PackagingData();
      var orderItems = data.GetOrderItems(orderUid);
      var packingOrderItems = packagesForItems.SelectMany(x => x.OrderItems).ToList();

      foreach (var item in orderItems) {
        var quantityOrderItems = packingOrderItems
                                  .Where(x => x.OrderItemUID == item.OrderItemUID)
                                  .Sum(x => x.Quantity);

        if (item.Quantity > quantityOrderItems) {
          var missing = new MissingItem();
          missing.OrderItemUID = item.OrderItemUID;
          missing.Quantity = item.Quantity - quantityOrderItems;
          missing.MergeCommonFieldsData(item.OrderItemId);
          missing.ItemWeight = missing.Quantity * missing.Product.ProductWeight;
          GetWarehousesByItem(missing, item.VendorProductId);

          missingItems.Add(missing);
        }
      }
      return missingItems.ToFixedList();
    }


    public FixedList<PackagedForItem> GetPackagesByOrder(string orderUid,
                                          FixedList<PackageForItem> packItems) {

      if (packItems.Count == 0) {
        return new FixedList<PackagedForItem>();
      }

      var packagesList = new List<PackagedForItem>();

      foreach (var entry in packItems) {
        var packageType = PackageType.Parse(entry.PackageTypeId);
        packageType.GetVolumeAttributes();

        var package = new PackagedForItem();
        package.UID = entry.OrderPackingUID;
        package.OrderUID = orderUid;
        package.PackageID = entry.PackageID;
        package.PackageTypeUID = packageType.ObjectKey;
        package.PackageTypeName = packageType.Name;
        package.OrderItems = GetPackingItems(entry.OrderPackingId, entry.OrderPackingUID);
        package.PackageWeight = package.OrderItems.Sum(x => x.ItemWeight);
        package.PackageVolume = packageType.TotalVolume;

        packagesList.Add(package);

      }

      return packagesList.ToFixedList();
    }


    public PackagedData GetPackingData(string orderUid, FixedList<PackagedForItem> packageForItemsList) {

      if (packageForItemsList.Count == 0) {
        return new PackagedData();
      }

      var data = new PackagedData();

      decimal volume = 0, weight = 0;

      foreach (var item in packageForItemsList) {
        var type = PackageType.Parse(item.PackageTypeUID);

        if (type != null) {
          type.GetVolumeAttributes();
          volume += type.TotalVolume;
        }
        weight += item.PackageWeight;
      }

      data.OrderUID = orderUid;
      data.Volume = volume;
      data.Weight = weight;
      data.TotalPackages = packageForItemsList.Count();

      return data;
    }


    #endregion Public methods


    #region Private methods


    private FixedList<PackingItem> GetPackingItems(int orderPackingId,
                                                    string orderPackingUID) {

      var data = new PackagingData();
      var packingItems = data.GetPackingOrderItems(orderPackingId);

      var packingOrderItems = new List<PackingItem>();

      foreach (var item in packingItems) {
        var packingOrderItem = new PackingItem();
        packingOrderItem.UID = item.PackingItemUID;
        packingOrderItem.OrderPackingUID = orderPackingUID;
        packingOrderItem.MergeCommonFieldsData(item.OrderItemId);
        packingOrderItem.Quantity = item.Quantity;
        packingOrderItem.ItemWeight = item.Quantity * packingOrderItem.Product.ProductWeight;
        GetWarehouses(packingOrderItem, item);
        packingOrderItems.Add(packingOrderItem);
      }

      return packingOrderItems.ToFixedList();
    }


    static private void GetWarehouses(PackingItem packingOrderItem, PackingOrderItem item) {

      var inventory = InventoryEntry.Parse(item.InventoryEntryId);
      if (inventory?.WarehouseId > 0) {

        var warehouse = Warehouse.Parse(inventory.WarehouseId);

        var whDto = new WarehouseForPacking();
        whDto.UID = warehouse.UID;
        whDto.Code = warehouse.Code;
        whDto.Name = warehouse.Name;
        //whDto.Stock = //TODO SACAR STOCK DE INVENTARIO-WAREHOUSE
        packingOrderItem.WarehouseForPacking = whDto;
      }

      if (inventory?.WarehouseBinProductId > 0) {

        var warehouseBinProduct = WarehouseBinProduct.Parse(inventory.WarehouseBinProductId);
        var whBinDto = new WarehouseBinForPacking();
        whBinDto.UID = warehouseBinProduct.WarehouseBinProductUID;
        whBinDto.OrderItemUID = packingOrderItem.OrderItemUID;
        whBinDto.Name = $"{warehouseBinProduct.WarehouseBin.BinDescription}";
        whBinDto.WarehouseName = $"{warehouseBinProduct.WarehouseBin.Warehouse.Code}";
        //$"rack: {warehouseBinProduct.WarehouseBin.BinDescription}";
        //whBinDto.Stock = //TODO SACAR STOCK DE INVENTARIO-WAREHOUSE
        packingOrderItem.WarehouseBinForPacking = whBinDto;
      }
    }


    private void GetWarehousesByItem(MissingItem missing, int vendorProductId) {

      var data = new PackagingData();

      FixedList<InventoryEntry> inventory = data.GetInventoryByVendorProduct(vendorProductId, "");

      var whBinDto = new List<WarehouseBinForPacking>();

      foreach (var item in inventory) {
        var whBinProduct = WarehouseBinProduct.Parse(item.WarehouseBinProductId);

        var exist = whBinDto.Find(x => x.UID == whBinProduct.UID && x.OrderItemUID == missing.OrderItemUID);

        if (exist == null) {
          var input = inventory.Where(x => x.WarehouseBinProductId == whBinProduct.Id).Sum(x => x.InputQuantity);
          var output = inventory.Where(x => x.WarehouseBinProductId == whBinProduct.Id).Sum(x => x.OutputQuantity);

          var bin = new WarehouseBinForPacking();
          bin.UID = whBinProduct.UID;
          bin.OrderItemUID = missing.OrderItemUID;
          bin.Name = whBinProduct.WarehouseBin.BinDescription;
          bin.WarehouseName = $"Almacen {whBinProduct.WarehouseBin.Warehouse.Code}";
          bin.Stock = input > output ? input - output : 0;

          whBinDto.Add(bin);
        }

      }
      missing.WarehouseBins = whBinDto.ToFixedList();
    }


    #endregion Private methods

  } // class PackingHelper
} // namespace Empiria.Trade.ShippingAndHandling.Domain
