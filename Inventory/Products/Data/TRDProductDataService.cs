/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Data Layer                              *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Data Service                            *
*  Type     : TRDProductDataService                      License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data read methods for TRDProducts.                                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Data;
using Empiria.Trade.Inventory.Products.Domain;

namespace Empiria.Trade.Inventory.Products.Data {

  /// <summary>Provides data read methods for TRDProducts.</summary>
  internal class TRDProductDataService {


    internal void AddOrUpdateTRDProduct(Product productEntry) {
      throw new NotImplementedException();
    }


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


  } // class TRDProductDataService

} // namespace Empiria.Trade.Inventory.Products.Data