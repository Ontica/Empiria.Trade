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
using Empiria.Services;

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


    #endregion Use cases

  } // class CataloguesUseCases

} // namespace Empiria.Trade.Core.Catalogues
