/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Data Layer                              *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Data Service                            *
*  Type     : ProductDataService                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data read methods for Products.                                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using Empiria.Data;

namespace Empiria.Trade.Products.Data {

  /// <summary>Provides data read methods for Products.</summary>
  static internal class ProductDataService {


    static public FixedList<ProductFields> GetProducts(string clauses = "") {

      clauses = SearchExpression.ParseAndLikeKeywords("Keywords", clauses);
      if (clauses != string.Empty) {
        clauses = "WHERE " + clauses;
      }

      var sql = "SELECT StoreId, ProdServCode, Product, Description, RegistrationDate, Trademark, Model, Section, LineName, " +
                "GroupName, SubgroupName, Diameter, Length, Degree, Weight, Characteristics, ThreadsName, StepsName, " +
                "HeadsName, ViewDetailsName, Existence, Currency, LastPurchaseDate, LastPurchaseDateCost, MinimumPrice, " +
                "BasisCost, ListPrice1, ListPrice2, ListPrice3, ListPrice4, Total, Packing, MinimumRefills, Supplier, " +
                "SalesUnit, SupplierName, ProductType, Discontinued, Status, ItemLineId, Keywords, ExtData " +
                "FROM vwProductosTemp " +
                $"{clauses}";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<ProductFields>(dataOperation);
    }


  } // class ProductDataService

} // namespace Empiria.Trade.Products.Data
