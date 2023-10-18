/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Data Layer                              *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Data Service                            *
*  Type     : TRDProductDataService                      License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data read methods for TRDProducts.                                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Linq;
using Empiria.Data;
using Empiria.Trade.Products.Domain;

namespace Empiria.Trade.Products.Data {

  /// <summary>Provides data read methods for TRDProducts.</summary>
  internal class TRDProductDataService {


    internal static FixedList<Product> GetProductsList(string keywords) {

      keywords = SearchExpression.ParseAndLikeKeywords("ProductKeywords", keywords);
      if (keywords != string.Empty) {
        keywords = "WHERE " + keywords;
      }

      var sql = "SELECT * " +
                "FROM TRDProducts " +
                $"{keywords}";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<Product>(dataOperation);

    }


    internal static string UpdateTableGUID(string tableName, string idName, string uidName) {
      
      try {

        var select = $"SELECT {idName} AS ID, {uidName} as UID FROM {tableName} WHERE {uidName} = '' ";
        
        var selectOperation = DataOperation.Parse(select);

        var entries = DataReader.GetPlainObjectFixedList<ProductUpdate>(selectOperation);

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
  } // class TRDProductDataService


  internal class ProductUpdate {

    [DataField("ID")]
    public int Id {
      get; set;
    }

    [DataField("UID")]
    public string Uid{
      get; set;
    }

  }


} // namespace Empiria.Trade.Products.Data