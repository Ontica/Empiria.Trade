/* Empiria Trade *********************************************************************************************
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
using System.IO.Packaging;
using System.Linq;

using Empiria.Trade.Core.Catalogues;

namespace Empiria.Trade.Core
{


    /// <summary>Generate data for packaging.</summary>
    public class PackagingBuilder {


    #region Constructor

    public PackagingBuilder() {

    }


    #endregion Constructor


    #region Public methods

    public PackingEntry GetPackagingEntriesWithItemsByOrder(string orderUID) {

      FixedList<PackagingEntry> packagingEntries = PackagingData.GetPackagingEntriesByOrder(orderUID);

      return MergePackagesIntoPackingEntry(orderUID, packagingEntries);
    }


    public PackagedData GetPackagedData(string orderUID) {

      var helper = new PackingHelper();

      FixedList<PackagedForItem> packagesForItems = GetPackagedForItemList(orderUID);

      PackagedData packingData = helper.GetPackingData(orderUID, packagesForItems);

      return packingData;
    }


    public FixedList<PackagedForItem> GetPackagedForItemList(string orderUID) {
      
      FixedList<PackagingEntry> packsForItems = PackagingData.GetPackagingEntriesByOrder(orderUID);

      var helper = new PackingHelper();

      return helper.GetPackagesByOrder(orderUID, packsForItems);
    }


    public FixedList<INamedEntity> GetPackageTypeList() {

      FixedList<PackageType> packageTypes = PackageType.GetList();

      return MergePackageTypeToNamedDto(packageTypes);
    }


    #endregion Public methods


    #region Private methods

    private FixedList<INamedEntity> MergePackageTypeToNamedDto(FixedList<PackageType> packageTypes) {

      var returnedNamed = new List<INamedEntity>();

      foreach (var package in packageTypes) {
        string length = package.Length > 0 ? $"largo {package.Length}, " : "";
        string width = package.Width > 0 ? $"ancho {package.Width}, " : "";
        string height = package.Height > 0 ? $"alto {package.Height}" : "";

        var packageName = $"{package.Name} " +
                          $"({length}" +
                          $"{width}" +
                          $"{height})";

        var namedDto = new NamedEntity(package.UID, packageName);

        returnedNamed.Add(namedDto);
      }

      return returnedNamed.ToFixedList();
    }


    private PackingEntry MergePackagesIntoPackingEntry(string orderUID,
                                FixedList<PackagingEntry> packagingEntries) {

      var helper = new PackingHelper();

      FixedList<PackagedForItem> packagesForItems = helper.GetPackagesByOrder(orderUID, packagingEntries);

      PackagedData packingData = helper.GetPackingData(orderUID, packagesForItems);

      FixedList<MissingItem> missingItems = helper.GetMissingItems(
        orderUID, packagesForItems);

      PickingData pickingData = GetPickingData(orderUID);

      var packingEntry = new PackingEntry();

      packingEntry.PickingData = pickingData;
      packingEntry.PackagedItems = packagesForItems;
      packingEntry.Data = packingData;
      packingEntry.MissingItems = missingItems;

      return packingEntry;
    }


    private PickingData GetPickingData(string orderUID) {
      
      //TODO averiguar si pickingData es una orden de inventario
      SalesOrder order = SalesOrder.Parse(orderUID);

      return new PickingData {
        OrderUID = order.OrderUID,
        InventoryOrderTypeId = -1,
        InventoryOrderNo = "",
        ResponsibleId = order.PostedBy.Id,
        AssignedToId = order.Responsible.Id,
        Notes = order.Observations
      };
    }


    public FixedList<PackingItem> GetPackingItemsByOrderPackingUID(string orderPackingUID) {

      var packageForItem = PackagingEntry.Parse(orderPackingUID);
      
      var helper = new PackingHelper();

      return helper.GetPackingItems(packageForItem.OrderPackingId);
    }


    #endregion Private methods

  } // class PackagingBuilder


} // namespace Empiria.Trade.ShippingAndHandling.Domain
