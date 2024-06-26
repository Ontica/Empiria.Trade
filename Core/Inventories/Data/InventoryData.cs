/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Trade Core                                 Component : Data Layer                              *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Data Service                            *
*  Type     : InventoryData                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data read methods for inventory.                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Data;

namespace Empiria.Trade.Core {


  /// <summary>Provides data read methods for inventory.</summary>
  internal class InventoryData {


    static internal FixedList<SalesInventoryStock> GetInventoryStockByVendorProduct(
                                        int vendorProductId, string warehouseBinClauses) {

      if (warehouseBinClauses != string.Empty) {
        warehouseBinClauses = $"AND WarehouseBinId NOT IN ({warehouseBinClauses})";
      }

      string sql = $"SELECT * FROM vwSalesInventoryStock " +
                   $"WHERE VendorProductId = {vendorProductId} " +
                   $"{warehouseBinClauses} ";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<SalesInventoryStock>(dataOperation);

    }


    static internal FixedList<SalesInventoryStock> GetInventoryStockByClauses(string clauses) {

      string sql = $"SELECT * FROM vwSalesInventoryStock {clauses}";
      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<SalesInventoryStock>(dataOperation);
    }


    static internal string GetInventoryStockClauses(int vendorProductId, int warehouseBinId) {
      var filters = new Filter();

      if (vendorProductId > 0) {
        filters.AppendAnd($"VendorProductId = {vendorProductId}");
      }

      if (warehouseBinId > 0) {
        filters.AppendAnd($"WarehouseBinId IN ({warehouseBinId})");
      }
      return filters.ToString().Length > 0 ? $"WHERE {filters}" : "";
    }


    #region Private methods


    #endregion Private methods


  } // class InventoryData

} // namespace Empiria.Trade.Core
