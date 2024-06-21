﻿/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Trade Core                                 Component : Data Layer                              *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Data Service                            *
*  Type     : CataloguesData                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data read methods for catalogues.                                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Data;
using Empiria.Trade.Core.Common;

namespace Empiria.Trade.Core.Catalogues
{

    /// <summary>Provides data read methods for catalogues.</summary>
    internal class CataloguesData {


    internal FixedList<SimpleObjectData> GetSimpleObjectDataList(int objectTypeId) {

      string sql = $"SELECT * FROM SimpleObjects WHERE ObjectStatus = 'A' AND ObjectTypeId = {objectTypeId}";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<SimpleObjectData>(dataOperation);
    }


    internal static InventoryEntry GetInventoryEntry(int vendorProductId) {

      var sql = $"SELECT * FROM TRDInventoryOrderItems WHERE VendorProductId = {vendorProductId}";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObject<InventoryEntry>(dataOperation);

    }


    static internal FixedList<WarehouseBin> GetWarehouseBinsForInventory() {

      string sql = $"SELECT * FROM TRDWarehouseBinsTemp WHERE WarehouseBinId > 0 AND WarehouseId > 0";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<WarehouseBin>(dataOperation);
    }


    static internal FixedList<WarehouseBin> GetWarehouseBinLocations(string keywords) {

      var keywordsClauses = string.Empty;

      if (keywords != string.Empty) {
        keywordsClauses += $"AND WarehouseBinTag LIKE '%{keywords}%'";
      }

      string sql = $"SELECT * FROM TRDWarehouseBinsTemp " +
                   $"WHERE WarehouseBinId > 0 AND WarehouseId > 0 " +
                   $"{keywordsClauses} ";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<WarehouseBin>(dataOperation);
    }


    internal static string UpdateTableGUID(string tableName, string idName, string uidName) {

      try {

        var select = $"SELECT {idName} AS ID, {uidName} as UID FROM {tableName} WHERE {uidName} = '' ";

        var selectOperation = DataOperation.Parse(select);

        var entries = DataReader.GetPlainObjectFixedList<DbTableUIDUpdate>(selectOperation);

        int count = 0;
        foreach (var entry in entries) {

          var update = $"UPDATE {tableName} SET {uidName} = '{Guid.NewGuid().ToString()}' WHERE {idName} = {entry.Id}";

          var dataOperation = DataOperation.Parse(update);

          DataReader.IsEmpty(dataOperation);
          count++;
        }

        return $"SE ACTUALIZARON {count} UID DE {entries.Count} EN {uidName} DE LA TABLA {tableName}";

      } catch (Exception ex) {

        throw new Exception(ex.Message, ex);
      }

    }


  } // class CataloguesData


} // namespace Empiria.Trade.Core.Catalogues
