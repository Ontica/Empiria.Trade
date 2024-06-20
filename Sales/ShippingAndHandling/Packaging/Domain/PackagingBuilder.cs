﻿/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Packaging Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Partitioned Type / Information Holder   *
*  Type     : PackagingBuilder                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Generate data for packaging.                                                                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using System.Linq;
using Empiria.Trade.Core;
using Empiria.Trade.Core.Catalogues;
using Empiria.Trade.Inventory;
using Empiria.Trade.Inventory.Data;
using Empiria.Trade.Orders;
using Empiria.Trade.Sales.ShippingAndHandling.Data;
using Newtonsoft.Json;

namespace Empiria.Trade.Sales.ShippingAndHandling.Domain
{


    /// <summary>Generate data for packaging.</summary>
    internal class PackagingBuilder {


    #region Constructor

    public PackagingBuilder() {

    }


    #endregion Constructor


    #region Public methods


    internal InventoryOrderItem GetInventoryEntries(string orderItemUID, string warehouseBinUID) {

      var orderItem = OrderItem.Parse(orderItemUID);
      var warehouseBinId = WarehouseBin.Parse(warehouseBinUID).Id;

      var inventories = InventoryOrderData.GetInventoryItemsBySalesOrderAndWhBin(
                                              orderItem.Id, warehouseBinId, 0);
      return inventories.Last();
    }


    internal PackingEntry GetPackagesAndItemsForOrder(string orderUid) {

      FixedList<PackageForItem> packsForItems = GetPackagesForItemsData(orderUid);

      return MergePackagesIntoPackingEntry(orderUid, packsForItems);
    }


    public PackagedData GetPackagedData(string orderUID) {

      var helper = new PackingHelper();

      FixedList<PackagedForItem> packagesForItems = GetPackagedForItemList(orderUID);

      PackagedData packingData = helper.GetPackingData(orderUID, packagesForItems);

      return packingData;
    }


    public FixedList<PackagedForItem> GetPackagedForItemList(string orderUID) {
      
      FixedList<PackageForItem> packsForItems = GetPackagesForItemsData(orderUID);

      var helper = new PackingHelper();

      return helper.GetPackagesByOrder(orderUID, packsForItems);
    }


    static public void ValidateIfExistPackagesForItems(
                        string orderUID, string packageID, string packageForItemUID) {

      FixedList<PackageForItem> packages = GetPackagesForItemsData(orderUID);

      var existPackage = packages.FirstOrDefault(x => x.PackageID.ToUpper() == packageID.ToUpper());
      
      if (packageForItemUID != string.Empty) {
        existPackage = packages.FirstOrDefault(x => x.PackageID.ToUpper() == packageID.ToUpper() &&
                                               x.OrderPackingUID != packageForItemUID);
      }
      Assertion.Require(existPackage == null,
                        $"Ya existe otra caja con el nombre proporcionado: '{packageID}'");
    }


    internal FixedList<INamedEntity> GetPackageTypeList() {
      var data = new PackagingData();

      FixedList<PackageType> packageTypes = data.GetPackageTypeList();

      GetVolumeAttributes(packageTypes);

      FixedList<INamedEntity> namedDto = MergePackageTypeToNamedDto(packageTypes);

      return namedDto;
    }


    #endregion Public methods


    #region Private methods

    static private FixedList<PackageForItem> GetPackagesForItemsData(string orderUID) {

      return PackagingData.GetPackagesForItemsByOrder(orderUID);
    }


    private void GetVolumeAttributes(FixedList<PackageType> packageTypes) {

      foreach (var packageType in packageTypes) {

        packageType.GetVolumeAttributes();
      }
    }


    private FixedList<INamedEntity> MergePackageTypeToNamedDto(FixedList<PackageType> packageTypes) {

      var returnedNamed = new List<INamedEntity>();

      foreach (var package in packageTypes) {
        string length = package.Length > 0 ? $"largo {package.Length} " : "";
        string width = package.Width > 0 ? $"ancho {package.Width} " : "";
        string height = package.Height > 0 ? $"alto {package.Height}" : "";

        var packageName = $"{package.Name} " +
                          $"({length}" +
                          $"{width}" +
                          $"{height})";

        var namedDto = new NamedEntity(package.ObjectKey, packageName);

        returnedNamed.Add(namedDto);
      }

      return returnedNamed.ToFixedList();
    }


    private PackingEntry MergePackagesIntoPackingEntry(string orderUID,
                                FixedList<PackageForItem> packsForItems) {

      var helper = new PackingHelper();

      PickingData pickingData = helper.GetPickingData(orderUID);

      FixedList<PackagedForItem> packagesForItems = helper.GetPackagesByOrder(orderUID, packsForItems);

      PackagedData packingData = helper.GetPackingData(orderUID, packagesForItems);

      FixedList<MissingItem> missingItems = helper.GetMissingItems(
        orderUID, packagesForItems);

      var packingEntry = new PackingEntry();

      packingEntry.PickingData = pickingData;
      packingEntry.PackagedItems = packagesForItems;
      packingEntry.Data = packingData;
      packingEntry.MissingItems = missingItems;

      return packingEntry;
    }


    internal FixedList<PackingItem> GetPackingItemsByOrderPackingUID(string orderPackingUID) {

      var packageForItem = PackageForItem.Parse(orderPackingUID);
      
      var helper = new PackingHelper();

      return helper.GetPackingItems(packageForItem.OrderPackingId);
    }


    #endregion Private methods

  } // class PackagingBuilder


} // namespace Empiria.Trade.ShippingAndHandling.Domain
