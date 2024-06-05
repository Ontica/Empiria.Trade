/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Core Management                            Component : Test cases                              *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Use cases tests                         *
*  Type     : CataloguesTests                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Test cases for products.                                                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Threading.Tasks;
using Empiria.Trade.Core;
using Empiria.Trade.Core.Catalogues;
using Empiria.Trade.Products.Adapters;
using Xunit;

namespace Empiria.Trade.Tests.Core
{


    public class CataloguesTests {


    [Fact]
    public void GetInventoryEntryTest() {

      var usecase = CataloguesUseCases.UseCaseInteractor();

      string uid = "1250b007-9d8a-46b9-83e3-bbf2e00c7ef1";

      InventoryEntry sut = usecase.GetInventoryEntry(uid);

      Assert.NotNull(sut);

    }


    [Fact]
    public void GetInventoryStockByVendorProductTest() {

      int vendorProductId = 36;
      FixedList<SalesInventoryStock> sut = 
        CataloguesUseCases.GetInventoryStockByVendorProduct(vendorProductId);

      Assert.NotNull(sut);

    }


    [Fact]
    public void GetPackageTypeTest() {

      var usecase = CataloguesUseCases.UseCaseInteractor();

      string uid = "0452a10b-0607-4d45-8614-385dda701b54";

      PackageType sut = usecase.GetPackageType(uid);

      Assert.NotNull(sut);

    }


    [Fact]
    public void GetWarehouseTest() {

      var usecase = CataloguesUseCases.UseCaseInteractor();

      string uid = "2f6dfb0d-137b-4309-94ac-c5f7b8fbc9df";

      Warehouse sut = usecase.GetWarehouse(uid);

      Assert.NotNull(sut);

    }


    [Fact]
    public void GetWarehouseBinTest() {

      var usecase = CataloguesUseCases.UseCaseInteractor();

      string warehouseBinUid = "f06a2b16-e744-412e-bd94-82821a7b5cd9";

      WarehouseBin sut = usecase.GetWarehouseBin(warehouseBinUid);

      Assert.NotNull(sut);

    }



    [Fact]
    public void GetWarehouseBinProductTest() {

      var usecase = CataloguesUseCases.UseCaseInteractor();

      string uid = "be68b023-202d-4bf3-b0b6-3726ed5be44f";

      WarehouseBinProduct sut = usecase.GetWarehouseBinProduct(uid);

      Assert.NotNull(sut);

    }


    [Fact]
    public void GetWarehouseBinsForInventory() {

      var usecase = CataloguesUseCases.UseCaseInteractor();

      var sut = usecase.GetWarehouseBinsForInventory();

      Assert.NotNull(sut);

    }


    [Fact]
    public void GetWarehouseBinLocations() {

      var usecase = CataloguesUseCases.UseCaseInteractor();

      var sut = usecase.GetWarehouseBinLocations("AG-Y-1-6");

      Assert.NotNull(sut);

    }
    

    [Fact]
    public async Task ShouldUpdateUID() {

      var usecase = CataloguesUseCases.UseCaseInteractor();

      var query = new TableQuery {
        TableName = "TRDProductPrices",
        IdName = "ProductPriceId",
        UidName = "ProductPriceUID"
      };

      string sut = await usecase.UpdateGUID(query).ConfigureAwait(false);

      Assert.NotEmpty(sut);

    }


  } // class CataloguesTests

} // namespace Empiria.Trade.Tests.Core
