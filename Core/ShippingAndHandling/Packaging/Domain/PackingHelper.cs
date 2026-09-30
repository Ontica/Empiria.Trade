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
using Empiria.Locations;
using Empiria.Trade.Core.Catalogues;

namespace Empiria.Trade.Core {

  /// <summary>Helper methods to build packing structure.</summary>
  public class PackingHelper {

    #region Public methods


    public FixedList<MissingItem> GetMissingItems(string orderUid,
                                        FixedList<PackagedForItem> packagesForItems) {

      var data = new PackagingData();

      var orderItems = data.GetOrderItems(orderUid);

      var packingOrderItems = packagesForItems.SelectMany(x => x.OrderItems).ToList();

      var missingItems = new List<MissingItem>();

      foreach (var item in orderItems) {
        
        var quantityOrderItems = packingOrderItems
                                  .Where(x => x.OrderItemUID == item.OrderItemUID)
                                  .Sum(x => x.Quantity);

        if (item.Quantity > quantityOrderItems) {
          var missing = new MissingItem();
          missing.OrderItemUID = item.OrderItemUID;
          missing.Quantity = item.Quantity - quantityOrderItems;
          missing.MergeCommonFieldsData(item.OrderItemId);
          missing.ItemWeight = missing.Quantity * (missing.Product.Peso * missing.Product.PackingSmallBag);
          missing.WarehouseBins = GetWarehouseBins(item);

          missingItems.Add(missing);
        }
      }
      return missingItems.ToFixedList();
    }


    public FixedList<PackagedForItem> GetPackagesByOrder(string orderUid,
                                          FixedList<PackagingEntry> packagingEntries) {

      if (packagingEntries.Count == 0) {
        return new FixedList<PackagedForItem>();
      }

      var packagesList = new List<PackagedForItem>();

      foreach (var entry in packagingEntries) {
        
        PackageType packageType = GetPackageTypeById(entry.PackageType.Id);

        var package = new PackagedForItem();
        package.UID = entry.OrderPackingUID;
        package.OrderUID = orderUid;
        package.PackageID = entry.PackageID;
        package.PackageTypeUID = packageType.UID;
        package.PackageTypeName = packageType.Name;
        package.OrderItems = GetPackingItems(entry.OrderPackingId);
        package.PackageWeight = package.OrderItems.Sum(x => x.ItemWeight);
        package.PackageVolume = packageType.TotalVolume;

        packagesList.Add(package);

      }

      return packagesList.ToFixedList();
    }


    public PackageType GetPackageTypeById(int packageTypeId) {

      var packageType = PackageType.Parse(packageTypeId);
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


    public FixedList<PackingItem> GetPackingItems(int orderPackingId) {

      var data = new PackagingData();
      var packingItems = data.GetPackingOrderItems(orderPackingId);

      var packingOrderItems = new List<PackingItem>();

      foreach (var item in packingItems) {

        var packingOrderItem = new PackingItem();
        packingOrderItem.MergeCommonFieldsData(item.OrderItemId);

        packingOrderItem.UID = item.PackingItemUID;
        packingOrderItem.OrderPackingUID = item.OrderPacking.OrderPackingUID;
        packingOrderItem.Quantity = item.Quantity;
        packingOrderItem.ItemWeight = item.Quantity * (packingOrderItem.Product.Peso *
                                                       packingOrderItem.Product.PackingSmallBag);

        packingOrderItems.Add(packingOrderItem);
      }

      return packingOrderItems.ToFixedList();
    }


    private FixedList<WarehouseBinForPacking> GetWarehouseBins(OrderItemTemp orderItem) {

      var usecase = CataloguesUseCases.UseCaseInteractor();

      FixedList<SalesInventoryStock> inventoryStocks =
        CataloguesUseCases.GetInventoryStockByVendorProduct(orderItem.ProductId, "");

      var warehouseBins = new List<WarehouseBinForPacking>();

      foreach (var inventory in inventoryStocks) {

        var warehouseRoot = InventoryBuilder.GetRootLocation(inventory.Location);

        WarehouseBinForPacking warehouseBin = new WarehouseBinForPacking {
          UID = inventory.Location.LocationUID,
          OrderItemUID = orderItem.OrderItemUID,
          Name = inventory.Location.Name == "-1" ? "LOCALIZACION VIRTUAL" : inventory.Location.Name,
          WarehouseName = warehouseRoot.Id == -1 ? "ALMACEN VIRTUAL" : $"ALMACEN {warehouseRoot.Name}",
          Stock = inventory.Stock,
        };

        warehouseBins.Add(warehouseBin);
      }

      return warehouseBins.ToFixedList();
    }

    #endregion Private methods

  } // class PackingHelper
} // namespace Empiria.Trade.ShippingAndHandling.Domain
