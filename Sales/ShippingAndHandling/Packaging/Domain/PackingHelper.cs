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
using System.Reflection;
using Empiria.Trade.Core;
using Empiria.Trade.Core.Catalogues;
using Empiria.Trade.Inventory;
using Empiria.Trade.Inventory.Data;
using Empiria.Trade.Orders;
using Empiria.Trade.Sales.ShippingAndHandling.Data;

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
          missing.WarehouseBins = new FixedList<WarehouseBinForPacking>();

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
        
        PackageType packageType = GetPackageTypeById(entry.PackageTypeId);

        var package = new PackagedForItem();
        package.UID = entry.OrderPackingUID;
        package.OrderUID = orderUid;
        package.PackageID = entry.PackageID;
        package.PackageTypeUID = packageType.ObjectKey;
        package.PackageTypeName = packageType.Name;
        package.OrderItems = GetPackingItems(entry.OrderPackingId);
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


    internal FixedList<PackingItem> GetPackingItems(int orderPackingId) {

      var data = new PackagingData();
      var packingItems = data.GetPackingOrderItems(orderPackingId);

      var packingOrderItems = new List<PackingItem>();

      foreach (var item in packingItems) {
        var packingOrderItem = new PackingItem();
        packingOrderItem.MergeCommonFieldsData(item.OrderItemId);

        packingOrderItem.UID = item.PackingItemUID;
        packingOrderItem.OrderPackingUID = item.OrderPacking.OrderPackingUID;
        packingOrderItem.Quantity = item.Quantity;
        packingOrderItem.ItemWeight = item.Quantity * packingOrderItem.Product.ProductWeight;

        packingOrderItems.Add(packingOrderItem);
      }

      return packingOrderItems.ToFixedList();
    }

    #endregion Private methods

  } // class PackingHelper
} // namespace Empiria.Trade.ShippingAndHandling.Domain
