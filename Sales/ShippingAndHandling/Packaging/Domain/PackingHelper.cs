﻿/* Empiria Trade *********************************************************************************************
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
using Empiria.Trade.Core;
using Empiria.Trade.Core.Catalogues;
using Empiria.Trade.Sales.ShippingAndHandling.Data;

namespace Empiria.Trade.Sales.ShippingAndHandling.Domain
{

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
        //var packageType = PackageType.Parse(entry.PackageTypeId);
        //packageType.GetVolumeAttributes();

        PackageType packageType = GetPackageTypeById(entry.PackageTypeId);

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


    internal PackageType GetPackageTypeById(int packageTypeId) {
      
      var packageType = PackageType.Parse(packageTypeId);
      packageType.GetVolumeAttributes();
      return packageType;

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


    internal FixedList<PackingItem> GetPackingItems(int orderPackingId,
                                                    string orderPackingUID) {

      var data = new PackagingData();
      var packingItems = data.GetPackingOrderItems(orderPackingId);

      var packingOrderItems = new List<PackingItem>();

      foreach (var item in packingItems) {
        var packingOrderItem = new PackingItem();
        packingOrderItem.MergeCommonFieldsData(item.OrderItemId);

        packingOrderItem.UID = item.PackingItemUID;
        packingOrderItem.OrderPackingUID = orderPackingUID;
        packingOrderItem.Quantity = item.Quantity;
        packingOrderItem.ItemWeight = item.Quantity * packingOrderItem.Product.ProductWeight;

        GetWarehouses(packingOrderItem, item);

        packingOrderItems.Add(packingOrderItem);
      }

      return packingOrderItems.ToFixedList();
    }


    static private void GetWarehouses(PackingItem packingOrderItem, PackingOrderItem item) {

      var inventory = item.InventoryEntry;//InventoryEntry.Parse(item.InventoryEntry);

      if (inventory?.WarehouseId > 0) {

        var warehouse = Warehouse.Parse(inventory.WarehouseId);
        
        var whForPacking = new WarehouseForPacking();
        whForPacking.UID = warehouse.UID;
        whForPacking.Code = warehouse.Code;
        whForPacking.Name = warehouse.Name;
        //whForPacking.Stock = //TODO SACAR STOCK DE INVENTARIO-WAREHOUSE
        packingOrderItem.WarehouseForPacking = whForPacking;
      }

      if (inventory?.WarehouseBinId > 0) {

        var warehouseBin = WarehouseBin.Parse(inventory.WarehouseBinId);
        var whBinForPacking = new WarehouseBinForPacking();
        whBinForPacking.UID = warehouseBin.UID;
        whBinForPacking.OrderItemUID = packingOrderItem.OrderItemUID;
        whBinForPacking.Name = $"{warehouseBin.BinDescription}";
        whBinForPacking.WarehouseName = $"{warehouseBin.Warehouse.Code}";
        //whBinDto.Stock = //TODO SACAR STOCK DE INVENTARIO-WAREHOUSE
        packingOrderItem.WarehouseBinForPacking = whBinForPacking;
      }
    }


    private void GetWarehousesByItem(MissingItem missing, int vendorProductId) {

      var data = new PackagingData();

      FixedList<InventoryEntry> inventoryByVendorProduct = 
        data.GetInventoryByVendorProduct(vendorProductId, "");
      
      var whBinForPacking = new List<WarehouseBinForPacking>();

      foreach (var inventory in inventoryByVendorProduct) {
        var warehouseBin = WarehouseBin.Parse(inventory.WarehouseBinId);

        var exist = whBinForPacking.Find(
          x => x.UID == warehouseBin.UID && x.OrderItemUID == missing.OrderItemUID);

        if (exist == null) {
          
          var input = inventoryByVendorProduct.Where(x => x.WarehouseBinId == warehouseBin.Id)
                                              .Sum(x => x.InputQuantity);
          var output = inventoryByVendorProduct.Where(x => x.WarehouseBinId == warehouseBin.Id)
                                               .Sum(x => x.OutputQuantity);

          var bin = new WarehouseBinForPacking();
          bin.UID = warehouseBin.UID;
          bin.OrderItemUID = missing.OrderItemUID;
          bin.Name = warehouseBin.BinDescription;
          bin.WarehouseName = $"Almacen {warehouseBin.Warehouse.Code}";
          bin.Stock = input > output ? input - output : 0;

          whBinForPacking.Add(bin);
        }

      }
      missing.WarehouseBins = whBinForPacking.ToFixedList();
    }


    #endregion Private methods

  } // class PackingHelper
} // namespace Empiria.Trade.ShippingAndHandling.Domain
