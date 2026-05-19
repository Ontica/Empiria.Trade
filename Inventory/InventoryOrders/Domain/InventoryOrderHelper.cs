/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Partitioned Type / Information Holder   *
*  Type     : InventoryOrderHelper                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Helper methods to build inventory orders.                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using System.Linq;
using Empiria.Trade.Core;
using Empiria.Trade.Core.Catalogues;
using Empiria.Trade.Inventory.Adapters;
using Empiria.Trade.Inventory.Data;
using Empiria.Trade.Orders;
using Empiria.Trade.Products;

namespace Empiria.Trade.Inventory.Domain {

  /// <summary>Helper methods to build inventory orders.</summary>
  internal class InventoryOrderHelper {

    #region Private methods

    static private SalesInventoryStock GetFirstLocationFinded(int vendorProductId,
      List<int> ignoreWarehouseBinId) {

      var warehouseBinClauses = SimpleObjects.ConcatIntsToString(ignoreWarehouseBinId);

      var inventoryStock = CataloguesUseCases.GetInventoryStockByVendorProduct(
        vendorProductId, warehouseBinClauses);

      return inventoryStock.Where(x => x.WarehouseBin.Id > 0 && x.RealStock > 0).FirstOrDefault();
    }


    static private List<SalesInventoryStock> GetOlderLocationList(SalesInventoryStock olderLocation) {

      var inventoryStock = CataloguesUseCases.GetInventoryStockByVendorProduct(olderLocation.VendorProduct.Id, "");

      return inventoryStock.Where(x => x.WarehouseBin.Id == olderLocation.WarehouseBin.Id).ToList();
    }


    #endregion Private methods

  } // class InventoryOrderHelper

} // namespace Empiria.Trade.Inventory.Domain
