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
using Empiria.Trade.Products.Adapters;
using Empiria.Trade.Products.Domain;

namespace Empiria.Trade.Products.Data {

  /// <summary>Provides data read methods for TRDProducts.</summary>
  internal class TRDProductDataService {


    internal static FixedList<Product> GetProductsForOrder(ProductQuery query) {

      //var whereClauses = GetQueryClauses(query);

      return GetProductsList(query.Keywords);

    }

    private static object GetQueryClauses(ProductQuery query) {
      throw new NotImplementedException();
    }


    internal static FixedList<Product> GetProductsList(string keywords) {

      keywords = SearchExpression.ParseAndLikeKeywords("ProductKeywords", keywords);
      if (keywords != string.Empty) {
        keywords = "WHERE " + keywords;
      }

      //var sql = "SELECT * " +
      //          "FROM TRDProducts " +
      //          $"{keywords}";

      var sql = "SELECT " +
                "P.ProductId, P.ProductUID, VP.VendorProductUID, PRESENT.PresentationId, VENDOR.PartyId VendorId, I.InventoryEntryId, GROUPS.ProductGroupId, " +
                "SUBGROUPS.ProductSubgroupId, P.ProductCode, P.ProductUPC, P.ProductName, P.ProductDescription, P.Attributes, VP.SKU, " +
                "PRICES.PriceList1, PRICES.PriceList2, PRICES.PriceList3, PRICES.PriceList4, PRICES.PriceList5, " +
                "PRICES.PriceList6, PRICES.PriceList7, PRICES.PriceList8, PRICES.PriceList9, PRICES.PriceList10, " +
                "P.ProductWeight, P.ProductLength, P.ProductStatus " +
                "FROM TRDProducts P " +
                "LEFT JOIN TRDProductGroups GROUPS ON P.ProductGroupId = GROUPS.ProductGroupId " +
                "LEFT JOIN TRDProductSubgroups SUBGROUPS ON P.ProductSubgroupId = SUBGROUPS.ProductSubgroupId " +
                "LEFT JOIN TRDVendorProducts VP ON P.ProductId = VP.ProductId " +
                "LEFT JOIN TRDProductPresentations PRESENT ON VP.PresentationId = PRESENT.PresentationId " +
                "LEFT JOIN TRDParties VENDOR ON VP.VendorId = VENDOR.PartyId " +
                "LEFT JOIN TRDProductPrices PRICES ON VP.VendorProductId = PRICES.VendorProductId " +
                "LEFT JOIN TRDInventory I ON VP.VendorProductId = I.VendorProductId " +
                $"{keywords}";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<Product>(dataOperation);

    }


    internal static InventoryEntry GetInventoryEntry(int vendorProductId) {

      var sql = $"SELECT * FROM TRDInventory WHERE VendorProductId = {vendorProductId}";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObject<InventoryEntry>(dataOperation);

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