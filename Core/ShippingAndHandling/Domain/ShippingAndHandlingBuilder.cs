/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Packaging Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Partitioned Type / Information Holder   *
*  Type     : OrderShippingBuilder                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Generate data for shipping and handling.                                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Empiria.Trade.Orders;
using Empiria.Trade.Products;
using Empiria.Trade.Products.Adapters;
using Empiria.Trade.ShippingAndHandling.Adapters;
using Empiria.Trade.ShippingAndHandling.Data;
using Newtonsoft.Json;

namespace Empiria.Trade.ShippingAndHandling.Domain {


  /// <summary>Generate data for shipping and handling.</summary>
  internal class ShippingAndHandlingBuilder {


    #region Constructor

    public ShippingAndHandlingBuilder() {

    }


    #endregion Constructor


    #region Public methods


    internal InventoryEntry GetInventoryEntries(string orderItemUID, string warehouseBinUID) {

      var data = new ShippingAndHandlingData();

      var orderItem = OrderItem.Parse(orderItemUID);
      FixedList<InventoryEntry> inventories = data.GetInventoryByVendorProduct(
                                              orderItem.VendorProduct.Id, warehouseBinUID);
      var inventory = inventories.Last();
      return inventory;
    }


    internal PackingEntry GetPackagesAndItemsForOrder(string orderUid) {

      FixedList<PackageForItem> packsForItems = GetPackagesForItemsData(orderUid);

      //if (packsForItems.Count == 0) {
      //  return new PackingEntry();
      //}

      PackingEntry packingEntry = MergePackagesIntoPackingEntry(orderUid, packsForItems);

      return packingEntry;
    }


    public PackagedData GetPackagedData(string orderUid) {

      FixedList<PackageForItem> packsForItems = GetPackagesForItemsData(orderUid);
      
      var helper = new PackingHelper();

      FixedList<PackagedForItem> packagesForItems = helper.GetPackagesByOrder(orderUid, packsForItems);

      PackagedData packingData = helper.GetPackingData(orderUid, packagesForItems);

      return packingData;
    }


    internal FixedList<INamedEntity> GetPackageTypeList() {
      var data = new ShippingAndHandlingData();

      FixedList<PackageType> packageTypes = data.GetPackageTypeList();

      GetVolumeAttributes(packageTypes);

      FixedList<INamedEntity> namedDto = MergePackageTypeToNamedDto(packageTypes);

      return namedDto;
    }


    #endregion Public methods


    #region Private methods

    private FixedList<PackageForItem> GetPackagesForItemsData(string orderUid) {

      var data = new ShippingAndHandlingData();
      return data.GetPackagesForItems(orderUid);
    }


    private void GetVolumeAttributes(FixedList<PackageType> packageTypes) {

      foreach (var packageType in packageTypes) {

        packageType.GetVolumeAttributes();
        //packageType.GetTotalVolume();
      }
    }


    private FixedList<INamedEntity> MergePackageTypeToNamedDto(FixedList<PackageType> packageTypes) {

      var returnedNamed = new List<INamedEntity>();

      foreach (var package in packageTypes) {

        var packageName = $"{package.Name} " +
                          $"({package.Length.ToString()}x" +
                          $"{package.Width.ToString()}x" +
                          $"{package.Height.ToString()})";

        var namedDto = new NamedEntityDto(package.ObjectKey, packageName);

        returnedNamed.Add(namedDto);
      }

      return returnedNamed.ToFixedList();
    }


    private PackingEntry MergePackagesIntoPackingEntry(string orderUid,
                                FixedList<PackageForItem> packsForItems) {

      var helper = new PackingHelper();

      FixedList<PackagedForItem> packagesForItems = helper.GetPackagesByOrder(orderUid, packsForItems);

      PackagedData packingData = helper.GetPackingData(orderUid, packagesForItems);

      FixedList<MissingItem> missingItems = helper.GetMissingItems(orderUid, packagesForItems);

      var packingEntry = new PackingEntry();
      packingEntry.PackagedItems = packagesForItems;
      packingEntry.Data = packingData;
      packingEntry.MissingItems = missingItems;

      return packingEntry;
    }


    #endregion Private methods

  } // class ShippingAndHandlingBuilder


} // namespace Empiria.Trade.ShippingAndHandling.Domain
