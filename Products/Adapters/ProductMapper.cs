/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Mapper class                            *
*  Type     : ProductMapper                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Methods used to map Products.                                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

namespace Empiria.Trade.Products.Adapters {

  /// <summary>Methods used to map Products.</summary>
  static internal class ProductMapper {


    #region Public methods

    static internal ProductDto Map(FixedList<ProductFields> entries) {

      return new ProductDto {
        ProductList = MapEntries(entries)
      };

    }


    #endregion Public methods


    #region Private methods


    static private FixedList<ProductEntryDto> MapEntries(FixedList<ProductFields> entries) {

      var mappedItems = entries.Select((x) => MapEntry((ProductFields) x));

      return new FixedList<ProductEntryDto>(mappedItems);
    }


    private static ProductEntryDto MapEntry(ProductFields entry) {
      var dto = new ProductEntryDto();


      dto.Product = entry.Product;
      dto.Description = entry.Description;
      dto.StoreId = entry.StoreId;
      dto.ProdServCode = entry.ProdServCode;
      dto.RegistrationDate = entry.RegistrationDate;
      dto.Characteristics = entry.Characteristics;
      dto.ThreadsName = entry.ThreadsName;
      dto.StepsName = entry.StepsName;
      dto.HeadsName = entry.HeadsName;
      dto.ViewDetailsName = entry.ViewDetailsName;
      dto.LastPurchaseDate = entry.LastPurchaseDate;
      dto.Existence = entry.Existence;
      dto.SalesUnit = entry.SalesUnit;
      dto.Currency = entry.Currency;
      dto.Total = entry.Total;
      dto.BasisCost = entry.BasisCost;
      dto.LastPurchaseDateCost = entry.LastPurchaseDateCost;
      dto.MinimumPrice = entry.MinimumPrice;
      dto.Packing = entry.Packing;
      dto.Supplier = entry.Supplier;
      dto.SupplierName = entry.SupplierName;
      dto.ProductType = entry.ProductType;
      dto.Discontinued = entry.Discontinued;
      //dto.Status = entry.Status;
      //dto.ItemLineId = entry.ItemLineId;
      dto.Keywords = entry.Keywords;
      dto.ExtData = entry.ExtData;

      dto.Attributes = GetAttributes(entry);
      dto.PriceList = GetPriceList(entry);
      dto.MeasurementAttributes = GetMeasurementUnits(entry);

      return dto;
    }


    private static ProductAttributes GetAttributes(ProductFields entry) {
      var attributes = new ProductAttributes();

      attributes.Trademark = entry.Trademark;
      attributes.Model = entry.Model;
      attributes.Section = entry.Section;
      attributes.LineName = entry.LineName;
      attributes.GroupName = entry.GroupName;
      attributes.SubgroupName= entry.SubgroupName;

      return attributes;
    }


    private static MeasurementUnits GetMeasurementUnits(ProductFields entry) {

      var units = new MeasurementUnits();

      units.Diameter = entry.Diameter;
      units.Length = entry.Length;
      units.Degree = entry.Degree;
      units.Weight = entry.Weight;

      return units;
    }

    private static FixedList<PriceListOfProduct> GetPriceList(ProductFields entry) {
      var prices = new List<PriceListOfProduct>();

      var price1 = new PriceListOfProduct {
        Name = "Price1",
        Value = entry.ListPrice1
      };
      prices.Add(price1);

      var price2 = new PriceListOfProduct {
        Name = "Price2",
        Value = entry.ListPrice2
      };
      prices.Add(price2);

      var price3 = new PriceListOfProduct {
        Name = "Price3",
        Value = entry.ListPrice3
      };
      prices.Add(price3);

      var price4 = new PriceListOfProduct {
        Name = "Price4",
        Value = entry.ListPrice4
      };
      prices.Add(price4);

      return prices.ToFixedList();
    }


    #endregion Private methods

  } // class ProductMapper

} // namespace Empiria.Trade.Products.Adapters
