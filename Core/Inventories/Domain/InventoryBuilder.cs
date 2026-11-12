/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Partitioned Type / Information Holder   *
*  Type     : InventoryBuilder                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Generate data for Inventory.                                                                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Locations;
using Empiria.Trade.Products;

namespace Empiria.Trade.Core {


  /// <summary>Generate data for Inventory.</summary>
  public class InventoryBuilder {


    #region Constructors and parsers

    public InventoryBuilder() {

    }


    #endregion Constructor and parsers


    #region Public methods


    static public FixedList<InventoryStockEntry> GetInventoryStockByProduct(
      int productId, string warehouseBinClauses) {

      return InventoryData.GetInventoryStockByProduct(productId, warehouseBinClauses);
    }


    static public decimal GetItemExistence(int productId) {

      FixedList<InventoryStockEntry> inventoryStock = GetInventoryStockByProduct(productId, "");

      ProductEntry productEntry = ProductEntry.ParseId(productId);

      return ProductBuilder.GetPackingSmallBagStockByInventoryUnit(inventoryStock, productEntry);
    }


    static public Location GetRootLocation(Location location) {
      var current = location;

      while (!current.IsRoot) {
        var parent = current.GetParent<Location>();
        current = parent;
      }

      return current;
    }


    #endregion Public methods


  } // class InventoryBuilder

} // namespace Empiria.Trade.Core
