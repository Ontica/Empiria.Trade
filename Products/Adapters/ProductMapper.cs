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
using System.Diagnostics;
using System.Reflection;
using Empiria.DataTypes;

namespace Empiria.Trade.Products.Adapters {

  /// <summary>Methods used to map Products.</summary>
  static internal class ProductMapper {


    #region Public methods

    static internal ProductDto Map(FixedList<ProductFields> entries) {

      return new ProductDto {
        ProductList = MapEntries(entries)
      };

    }


    static internal FixedList<IProductEntryDto> MapTo(FixedList<ProductFields> entries) {

      return MapToEntriesDto(entries);

    }


    #endregion Public methods


    #region Private methods


    private static FixedList<IProductEntryDto> MapToEntriesDto(FixedList<ProductFields> entries) {
      var mappedItems = entries.Select((x) => MapShortEntry((ProductFields) x));

      return new FixedList<IProductEntryDto>(mappedItems);
    }


    static private FixedList<IProductEntryDto> MapEntries(FixedList<ProductFields> entries) {

      var mappedItems = entries.Select((x) => MapEntry((ProductFields) x));

      return new FixedList<IProductEntryDto>(mappedItems);
    }


    private static ProductEntryDto MapEntry(ProductFields entry) {
      var dto = new ProductEntryDto();


      dto.StoreId = entry.StoreId;
      dto.Product = entry.Product;
      dto.ProdServCode = entry.ProdServCode;
      dto.Description = entry.Description;
      dto.Group = entry.GroupName;
      dto.RegistrationDate = entry.RegistrationDate;
      dto.ViewDetailsName = entry.ViewDetailsName;
      dto.Stock = entry.Stock != "" ? entry.Stock : "0";
      dto.SalesUnit = entry.SalesUnit;
      dto.Currency = entry.Currency;
      dto.Total = entry.Total;
      dto.BasisCost = entry.BasisCost;
      dto.LastPurchaseDateCost = entry.LastPurchaseDateCost;
      dto.MinimumPrice = entry.MinimumPrice;
      dto.Packing = entry.Packing;
      dto.SupplierName = $"({entry.Supplier}) {entry.SupplierName}";
      dto.ProductType = entry.ProductType;
      dto.Discontinued = entry.Discontinued;
      dto.Section = entry.Section;
      dto.LineName = entry.LineName;
      dto.SubgroupName = entry.SubgroupName;
      dto.Diameter = entry.Diameter;
      dto.Length = entry.Length;
      dto.Degree = entry.Degree;
      dto.Weight = entry.Weight;

      dto.PriceList = GetPriceList(entry);
      dto.Attributes = GetAttributes(entry);
      dto.Supplier = entry.Supplier;
      dto.StepsName = entry.StepsName;
      dto.ThreadsName = entry.ThreadsName;
      dto.HeadsName = entry.HeadsName;
      dto.LastPurchaseDate = entry.LastPurchaseDate;
      dto.Characteristics = entry.Characteristics;
      dto.Status = entry.Status;
      dto.Keywords = entry.Keywords;

      return dto;
    }


    private static ProductShortEntryDto MapShortEntry(ProductFields entry) {
      var dto = new ProductShortEntryDto();

      dto.Code = entry.Product;
      dto.Description = entry.Description;
      dto.Group = entry.GroupName;
      dto.Subgroup = entry.SubgroupName;
      //dto.StoreId = entry.StoreId;
      //dto.ProdServCode = entry.ProdServCode;
      //dto.SalesUnit = entry.SalesUnit;
      //dto.Packing = entry.Packing;
      //dto.SupplierName= $"({entry.Supplier}) {entry.SupplierName}";

      dto.Attributes = GetAttributes(entry);
      dto.Presentations = GetPresentations(entry);
      //dto.PriceList = GetPriceList(entry);


      return dto;
    }



    private static ProductAttributes GetAttributes(ProductFields entry) {
      var attributes = new ProductAttributes();

      attributes.Terminado = entry.ViewDetailsName;
      attributes.Cabeza = entry.HeadsName;
      attributes.Grado = entry.Degree;
      attributes.Tamano = entry.Diameter;
      attributes.Hilos = entry.ThreadsName;

      return attributes;
    }


    private static Presentation GetPresentations(ProductFields entry) {

      Presentation presentation = new Presentation();

      presentation.PresentationUID = "";
      presentation.Description = $"{entry.GroupName} {entry.ViewDetailsName} {entry.Stock}";
      presentation.Units = entry.Stock;
      presentation.Vendors = GetVendors(entry);

      return presentation;
    }


    private static FixedList<Vendor> GetVendors(ProductFields entry) {
      var vendors = new List<Vendor>();

      Vendor vendor = new Vendor() {
        VendorUID = "",
        VendorName = GetVendorName(entry.StoreId),
        Sku = "sku-000",
        Stock = entry.Stock,
        Price = entry.MinimumPrice
      };

      vendors.Add(vendor);

      return vendors.ToFixedList();
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



    private static string GetVendorName(int storeId) {

      if (storeId == 1) {
        return "Productos NK";
      }
      if (storeId == 2) {
        return "Productos NK Hidroplomex";
      }
      if (storeId == 3) {
        return "Productos Microsip";
      }

      return string.Empty;
    }


    #endregion Private methods

  } // class ProductMapper

} // namespace Empiria.Trade.Products.Adapters
