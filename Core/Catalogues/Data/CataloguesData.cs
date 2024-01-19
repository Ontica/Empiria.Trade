/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Trade Core                                 Component : Data Layer                              *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Data Service                            *
*  Type     : CataloguesData                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data read methods for catalogues.                                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Data;

namespace Empiria.Trade.Core.Catalogues {

  /// <summary>Provides data read methods for catalogues.</summary>
  internal class CataloguesData {


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
