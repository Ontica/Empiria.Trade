/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Data Layer                              *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Data Service                            *
*  Type     : ProductDataService                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data read methods for TRDProducts.                                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Linq;
using Empiria.Data;
using Empiria.Trade.Products.Adapters;

namespace Empiria.Trade.Products.Data
{

    /// <summary>Provides data read methods for Products.</summary>
    internal class ProductDataService {


    internal static FixedList<Product> GetProductsForOrder(ProductQuery query) {

      return GetProductsList(query.Keywords);

    }


    internal static FixedList<Product> GetProductsList(string keywords) {

      string whereClauses = string.Empty;

      if (keywords != string.Empty) {
        whereClauses = $"WHERE {SearchExpression.ParseAndLikeKeywords("ProductKeywords", keywords)}";
      }

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
                $"{whereClauses}";


      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<Product>(dataOperation);

    }

    
  } // class ProductDataService


} // namespace Empiria.Trade.Products.Data