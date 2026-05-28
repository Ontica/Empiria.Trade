/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Data Layer                              *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Data Service                            *
*  Type     : ProductDataService                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data read methods for TRDProducts.                                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using Empiria.Data;
using Empiria.Trade.Products.Adapters;

namespace Empiria.Trade.Products.Data {

  /// <summary>Provides data read methods for Products.</summary>
  internal class ProductDataService {

    #region Methods

    internal static FixedList<Product> GetBaseProducts(string keywords) {

      var sql = "SELECT * FROM OMS_Products "; 
                //"(Base_Product_Id = Product_Id OR Base_Product_Id = -1) ";

      if (keywords != string.Empty) {
        sql += $"WHERE {SearchExpression.ParseAndLikeKeywords("Product_Keywords", keywords)}";
      }

      var op = DataOperation.Parse(sql);

      return DataReader.GetFixedList<Product>(op);
    }


    internal static FixedList<Product> GetProductsPresentations(Product baseProduct) {

      var sql = "SELECT * FROM OMS_Products " +
                $"WHERE Base_Product_Id = {baseProduct.Id} AND " +
                $"Product_Id <> {baseProduct.Id} AND " +
                $"Product_Status <> 'X'";

      var op = DataOperation.Parse(sql);

      return DataReader.GetFixedList<Product>(op);
    }


    internal static FixedList<ProductGroup> GetProductGroups(int typeId, int categoryId,
                                                             int classificationId = 0) {

      var category = categoryId > 0 ? $"AND Object_Category_Id = {categoryId} " : string.Empty;
      var classification = classificationId > 0 ?
                           $"AND Object_Classification_Id = {classificationId} " : string.Empty;


      var sql = "SELECT * FROM Common_Storage " +
                $"WHERE Object_Type_Id = {typeId} " +
                category +
                classification;

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<ProductGroup>(dataOperation);
    }


    internal static FixedList<Product> GetProductsForOrder(ProductQuery query) {

      return GetProductsList(query.Keywords);

    }


    internal static FixedList<Product> GetProductsList(string keywords) {

      string whereClauses = string.Empty;

      if (keywords != string.Empty) {
        whereClauses = $"WHERE {SearchExpression.ParseAndLikeKeywords("ProductKeywords", keywords)}";
      }

      var sql = "SELECT " +
                "P.ProductId, P.ProductUID, VP.VendorProductUID, PRESENT.PresentationId, VENDOR.PartyId VendorId, " +
                "CASE WHEN I.InventoryOrderItemId = NULL THEN -1 ELSE I.InventoryOrderItemId END AS InventoryOrderItemId, " +
                "GROUPS.ProductGroupId, SUBGROUPS.ProductSubgroupId, P.ProductCode, P.ProductUPC, " +
                "P.ProductName, P.ProductDescription, P.Attributes, VP.SKU, " +
                "PRICES.PriceList1, PRICES.PriceList2, PRICES.PriceList3, PRICES.PriceList4, PRICES.PriceList5, " +
                "PRICES.PriceList6, PRICES.PriceList7, PRICES.PriceList8, PRICES.PriceList9, PRICES.PriceList10, " +
                "P.ProductWeight, P.ProductLength, P.FragileProduct, P.ProductStatus " +
                "FROM TRDProducts P " +
                "LEFT JOIN TRDProductGroups GROUPS ON P.ProductGroupId = GROUPS.ProductGroupId " +
                "LEFT JOIN TRDProductSubgroups SUBGROUPS ON P.ProductSubgroupId = SUBGROUPS.ProductSubgroupId " +
                "LEFT JOIN TRDVendorProducts VP ON P.ProductId = VP.ProductId " +
                "LEFT JOIN TRDProductPresentations PRESENT ON VP.PresentationId = PRESENT.PresentationId " +
                "LEFT JOIN TRDParties VENDOR ON VP.VendorId = VENDOR.PartyId " +
                "LEFT JOIN TRDProductPrices PRICES ON VP.VendorProductId = PRICES.VendorProductId " +
                "LEFT JOIN TRDInventoryOrderItems I ON VP.VendorProductId = I.VendorProductId " +
                $"{whereClauses}";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<Product>(dataOperation);
    }


    internal static FixedList<VendorProduct> GetVendorProductByProduct(string productsIn) {

      var sql = $"SELECT * FROM TRDVendorProducts WHERE ProductId IN ({productsIn}) ";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<VendorProduct>(dataOperation);
    }

    #endregion Methods

  } // class ProductDataService


} // namespace Empiria.Trade.Products.Data