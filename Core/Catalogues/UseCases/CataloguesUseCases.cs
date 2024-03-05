/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Trade Core                                 Component : Common Types                            *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Data Transfer Object                    *
*  Type     : CataloguesUseCases                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a simple data object.                                                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using System.Threading.Tasks;
using Empiria.Services;
using Empiria.Trade.Products.Adapters;

namespace Empiria.Trade.Core.Catalogues
{


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


    public FixedList<SalesInventoryStock> GetInventoryStockByVendorProduct(int vendorProductId) {
      Assertion.Require(vendorProductId, "vendorProductId");
      
      return InventoryBuilder.GetInventoryStockByVendorProduct(vendorProductId);
    }


    public PackageType GetPackageType(string packageTypeUid) {
      Assertion.Require(packageTypeUid, "packageTypeUid");

      PackageType packageType = PackageType.Parse(packageTypeUid);

      packageType.GetVolumeAttributes();

      return packageType;
    }


    public FixedList<INamedEntity> GetParcelSupplierList() {

      var parcelSupplier = new ParcelSupplier();

      return parcelSupplier.GetParcelSupplierList();
    }


    public Warehouse GetWarehouse(string warehouseUid) {
      Assertion.Require(warehouseUid, "warehouseUid");

      return Warehouse.Parse(warehouseUid);
    }


    public WarehouseBin GetWarehouseBin(string warehouseBinUid) {
      Assertion.Require(warehouseBinUid, "warehouseBinUid");

      return WarehouseBin.Parse(warehouseBinUid);
    }


    public WarehouseBinProduct GetWarehouseBinProduct(string warehouseBinProductUID) {
      Assertion.Require(warehouseBinProductUID, "warehouseBinProductUID");

      return WarehouseBinProduct.Parse(warehouseBinProductUID);
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
