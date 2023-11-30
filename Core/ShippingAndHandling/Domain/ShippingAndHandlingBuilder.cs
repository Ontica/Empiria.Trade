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


    internal FixedList<INamedEntity> GetPackageTypeList() {
      var data = new ShippingAndHandlingData();

      FixedList<PackageType> packageTypes = data.GetPackageTypeList();

      GetVolumeAttributes(packageTypes);

      FixedList<INamedEntity> namedDto = MergePackageTypeToNamedDto(packageTypes);

      return namedDto;
    }


    internal PackingDto GetPackingByOrder(string orderUid) {

      var data = new ShippingAndHandlingData();
      FixedList<Packing> packingList = data.GetPackingByOrder(orderUid);

      var helper = new PackingHelper();

      var packingItems = helper.MapToPackingItems(orderUid, packingList);
      var packingData = helper.MapPackingData(orderUid, packingItems);
      var missingItems = helper.MapToMissingItems(orderUid, packingList);

      var packingDto = new PackingDto();
      packingDto.Data = packingData;
      packingDto.PackagedItems = packingItems;
      packingDto.MissingItems = missingItems;

      return packingDto;
    }


    internal InventoryEntry GetInventoryEntries(string orderItemUID, string warehouseBinUID) {

      var data = new ShippingAndHandlingData();

      var orderItem = OrderItem.Parse(orderItemUID);
      FixedList<InventoryEntry> inventories = data.GetInventoryByVendorProduct(
                                              orderItem.VendorProduct.UID, warehouseBinUID);
      var inventory = inventories.Last();
      return inventory;
    }

    #endregion Public methods


    #region Private methods


    internal void GetVolumeAttributes(FixedList<PackageType> packageTypes) {

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

    
    #endregion Private methods

  } // class ShippingAndHandlingBuilder

  internal class OrderItemTemp {
  }

} // namespace Empiria.Trade.ShippingAndHandling.Domain
