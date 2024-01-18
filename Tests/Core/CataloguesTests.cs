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
using Empiria.Trade.Core.Catalogues;
using Empiria.Trade.Products.UseCases;
using Xunit;

namespace Empiria.Trade.Tests.Core {
  
  
  public class CataloguesTests {


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

      string uid = "f06a2b16-e744-412e-bd94-82821a7b5cd9";

      WarehouseBin sut = usecase.GetWarehouseBin(uid);

      Assert.NotNull(sut);

    }



    [Fact]
    public void GetWarehouseBinProductTest() {

      var usecase = CataloguesUseCases.UseCaseInteractor();

      string uid = "be68b023-202d-4bf3-b0b6-3726ed5be44f";

      WarehouseBinProduct sut = usecase.GetWarehouseBinProduct(uid);

      Assert.NotNull(sut);

    }

  } // class CataloguesTests

} // namespace Empiria.Trade.Tests.Core
