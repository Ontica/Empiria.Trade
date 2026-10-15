/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Packing Management                         Component : Domain Layer                            *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Partitioned Type / Information Holder   *
*  Type     : PackingHelper                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Helper methods to build packing structure.                                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

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
          
          var orderItem = SalesOrderItem.Parse(item.OrderItemId);

          var missing = new MissingItem();

          missing.OrderItemUID = item.OrderItemUID;
          missing.Quantity = item.Quantity - quantityOrderItems;
          missing.MergeCommonFieldsData(orderItem);
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


    public FixedList<PackingItem> GetPackingItems(int orderPackingId) {

      var data = new PackagingData();
      var packingItems = data.GetPackingOrderItems(orderPackingId);

      var packingOrderItems = new List<PackingItem>();

      foreach (var item in packingItems) {

        var location = Location.Parse(item.WarehouseBinId);
        var warehouse = InventoryBuilder.GetRootLocation(location);
        var orderItem = SalesOrderItem.Parse(item.OrderItemId);

        var packingOrderItem = new PackingItem();

        packingOrderItem.MergeCommonFieldsData(orderItem);

        packingOrderItem.UID = item.PackingItemUID;
        packingOrderItem.OrderPackingUID = item.OrderPacking.OrderPackingUID;
        packingOrderItem.Quantity = item.Quantity;
        packingOrderItem.ItemWeight = item.Quantity * (packingOrderItem.Product.Peso *
                                                       packingOrderItem.Product.PackingSmallBag);
        packingOrderItem.WarehouseForPacking = MergeWarehouseForPacking(warehouse);
        packingOrderItem.WarehouseBinForPacking = MergeWarehouseBinForPacking(orderItem, location, warehouse.Name);
        packingOrderItems.Add(packingOrderItem);
      }

      return packingOrderItems.ToFixedList();
    }

    #endregion Public methods


    #region Private methods

    private FixedList<WarehouseBinForPacking> GetWarehouseBins(OrderItemTemp orderItem) {

      var usecase = CataloguesUseCases.UseCaseInteractor();

      FixedList<SalesInventoryStock> inventoryStocks =
        CataloguesUseCases.GetInventoryStockByVendorProduct(orderItem.ProductId, "");

      var warehouseBins = new List<WarehouseBinForPacking>();

      foreach (var inventory in inventoryStocks) {

        var warehouseRoot = InventoryBuilder.GetRootLocation(inventory.Location);
        var warehouseName = warehouseRoot.Id == -1 ? "ALMACEN VW" : $"ALMACEN {warehouseRoot.ShortName}";
        var locationName = inventory.Location.Name == "-1" ? "LOC. VIRTUAL" : inventory.Location.Name;

        WarehouseBinForPacking warehouseBin = new WarehouseBinForPacking {
          UID = inventory.Location.LocationUID,
          OrderItemUID = orderItem.OrderItemUID,
          Name = locationName,
          WarehouseName = warehouseName,
          Description = $"{warehouseName} - {locationName} {inventory.Stock}",
          Stock = inventory.Stock,
        };

        warehouseBins.Add(warehouseBin);
      }

      return warehouseBins.ToFixedList();
    }
    

    private WarehouseBinForPacking MergeWarehouseBinForPacking(SalesOrderItem orderItem,
                                                               Location location, string warehouseName) {

      return new WarehouseBinForPacking {
        UID = location.UID,
        OrderItemUID = orderItem.OrderItemUID,
        WarehouseName = warehouseName == "-1" ? "" : warehouseName,
        Description = location.Id == -1 ? "" : location.Description,
        Name = location.Id == -1 ? "LOCALIZACION VIRTUAL" : location.Name
      };
    }

    private WarehouseForPacking MergeWarehouseForPacking(Location warehouse) {

      return new WarehouseForPacking {
        UID = warehouse.UID,
        Code = warehouse.Id == -1 ? "VW" : $"{warehouse.ShortName}",
        Name = warehouse.Id == -1 ? "ALMACEN VIRTUAL" : $"{warehouse.Name}"
      };
    }

    #endregion Private methods

  } // class PackingHelper
} // namespace Empiria.Trade.ShippingAndHandling.Domain
