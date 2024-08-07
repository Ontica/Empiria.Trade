﻿/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Trade Core                                 Component : Common Types                            *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Data Transfer Object                    *
*  Type     : CataloguesUseCases                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a simple data object.                                                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Empiria.Services;
using Empiria.Trade.Core.Catalogues.Adapters;
using Empiria.Trade.Products;
using Empiria.Trade.Products.Adapters;

namespace Empiria.Trade.Core.Catalogues {


  public class CataloguesUseCases : UseCase {


    #region Constructors and parsers

    protected CataloguesUseCases() {
      // no-op
    }

    static public CataloguesUseCases UseCaseInteractor() {
      return CreateInstance<CataloguesUseCases>();
    }


    #endregion Constructors and parsers


    #region Use cases


    internal InventoryEntry GetInventoryEntry(string inventoryEntryUid) {
      Assertion.Require(inventoryEntryUid, "inventoryEntryUid");

      return InventoryEntry.Parse(inventoryEntryUid);
    }


    static public FixedList<SalesInventoryStock> GetInventoryStockByVendorProduct(
      int vendorProductId, string warehouseBinClauses) {
      Assertion.Require(vendorProductId, "vendorProductId");

      return InventoryBuilder.GetInventoryStockByVendorProduct(vendorProductId, warehouseBinClauses);
    }


    static public FixedList<SalesInventoryStock> GetInventoryStockByClauses(
      int vendorProductId, int warehouseBinId) {
      Assertion.Require(vendorProductId, "vendorProductId");
      Assertion.Require(warehouseBinId, "warehouseBinId");

      var vendorProductList = new List<int>();
      var warehouseBinList = new List<int>();

      if (vendorProductId > 0) {
        vendorProductList.Add(vendorProductId);
      }
      if (warehouseBinId > 0) {
        warehouseBinList.Add(warehouseBinId);
      }

      var clauses = InventoryData.GetInventoryStockClauses(
                                    SimpleObjects.ConcatIntsToString(vendorProductList),
                                    SimpleObjects.ConcatIntsToString(warehouseBinList));

      return InventoryData.GetInventoryStockByClauses(clauses);
    }


    static public FixedList<SalesInventoryStock> GetStockByUIDList(
      List<int> vendorProducts, List<int> warehouseBins) {

      var clauses = InventoryData.GetInventoryStockClauses(
                    SimpleObjects.ConcatIntsToString(vendorProducts),
                    SimpleObjects.ConcatIntsToString(warehouseBins));

      return InventoryData.GetInventoryStockByClauses(clauses);
    }


    public PackageType GetPackageType(string packageTypeUid) {
      Assertion.Require(packageTypeUid, "packageTypeUid");

      PackageType packageType = PackageType.Parse(packageTypeUid);

      packageType.GetVolumeAttributes();

      return packageType;
    }


    public FixedList<INamedEntity> GetParcelSupplierList() {

      var parcelSupplier = new SimpleObjects();

      var simpleObjectList = parcelSupplier.GetParcelSupplierList();
      return parcelSupplier.MergeSimpleObjectToNamedEntityDto(simpleObjectList);

    }


    public FixedList<INamedEntity> GetSimpleObjectListByObjectTypeId(int objectTypeId) {

      var parcelSupplier = new SimpleObjects();

      var simpleObjectList = parcelSupplier.GetSimpleObjectDataList(objectTypeId);

      return parcelSupplier.MergeSimpleObjectToNamedEntityDto(simpleObjectList);
    }


    public Warehouse GetWarehouse(string warehouseUid) {
      Assertion.Require(warehouseUid, "warehouseUid");

      return Warehouse.Parse(warehouseUid);
    }


    public WarehouseBin GetWarehouseBin(string warehouseBinUid) {
      Assertion.Require(warehouseBinUid, "warehouseBinUid");

      return WarehouseBin.Parse(warehouseBinUid);
    }


    public WarehouseBin GetWarehouseBinByVendorProduct(int vendorProductId) {
      Assertion.Require(vendorProductId, "vendorProductId");

      FixedList<SalesInventoryStock> stock = GetInventoryStockByVendorProduct(vendorProductId, "");

      return stock.FirstOrDefault().WarehouseBin;
    }


    public FixedList<WarehouseBinForInventoryDto> GetWarehouseBinsForInventory() {

      FixedList<WarehouseBin> warehouseBins = CataloguesData.GetWarehouseBinsForInventory();

      return WarehouseMapper.MapWarehouseBins(warehouseBins);
    }


    public FixedList<NamedEntityDto> GetWarehouseBinLocations(string keywords) {

      FixedList<WarehouseBin> warehouseBins = CataloguesData.GetWarehouseBinLocations(keywords);

      return WarehouseMapper.MapToDto(warehouseBins);
    }


    public async Task<string> UpdateGUID(TableQuery query) {

      try {
        return await Task.Run(() => CataloguesData.UpdateTableGUID(
                              query.TableName, query.IdName, query.UidName))
                              .ConfigureAwait(false);
      } catch (Exception ex) {
        throw new Exception(ex.Message, ex);
      }
    }


    #endregion Use cases

  } // class CataloguesUseCases

} // namespace Empiria.Trade.Core.Catalogues
