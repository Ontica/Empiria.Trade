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


  } // class InventoryData

} // namespace Empiria.Trade.Core
