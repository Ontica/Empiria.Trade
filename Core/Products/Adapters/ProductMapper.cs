/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Mapper class                            *
*  Type     : ProductMapper                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Methods used to map TRDProducts.                                                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Empiria.Parties;
using Empiria.Trade.Core.Catalogues;
using Empiria.Trade.Products.Domain;

namespace Empiria.Trade.Products.Adapters {

  /// <summary>Methods used to map Products.</summary>
  public class ProductMapper {

    #region Public methods V2

    static internal FixedList<ProductForSearchingDto> MapToPurchaseOrder(FixedList<ProductEntry> products,
                                                                         string vendorUID) {

      return products.Select(x => MapProduct(x, vendorUID))
                                 .Where(x => x.Presentations.Count > 0)
                                 .ToFixedList();
    }


    static internal FixedList<ProductForSearchingDto> MapToSearcher(FixedList<ProductEntry> products,
                                                                    bool withUnits) {

      return products.Select(x => MapProduct(x, string.Empty, withUnits))
                                 .Where(x => x.Presentations.Count > 0)
                                 .ToFixedList();
    }


    static private ProductForSearchingDto MapProduct(ProductEntry product, string vendorUID,
                                                     bool withUnits = false) {

      return new ProductForSearchingDto() {
        ProductUID = product.UID,
        ProductCode = product.InternalCode,
        Description = product.Description,
        ProductType = GetProductsType(product),
        Presentations = GetProductPresentations(product, vendorUID, withUnits)
      };
    }


    static private ProductTypeDto GetProductsType(ProductEntry product) {

      return new ProductTypeDto {
        ProductTypeUID = product.ProductType.UID,
        Name = product.ProductType.DisplayPluralName
      };
    }


    static private FixedList<ProductPresentationForSeach> GetProductPresentations(ProductEntry product,
                                                            string vendorUID, bool withUnits) {
      
      product.WithUnits = withUnits;

      var _presentations = product.Presentations.OrderBy(x => x.InternalCode.Length)
                                                      .ThenBy(x => x.InternalCode).ToFixedList();

      GetStockForPresentations(product, _presentations);

      var productsByVendor = GetPresentationsByVendor(_presentations, vendorUID);

      var productPresentations = new List<ProductPresentationForSeach>();

      if (productsByVendor.Count == 0 && vendorUID != string.Empty && product.Vendor.UID == vendorUID) {
        
        productPresentations.Add(AssignProductPresentation(product));
      }

      foreach (var p in productsByVendor) {

        productPresentations.Add(AssignProductPresentation(p));
      }

      return productPresentations.ToFixedList();
    }

    #endregion Public methods V2

    #region Public methods

    static internal FixedList<IProductEntryDto> MapToEntriesDto(FixedList<ProductEntry> entries) {

      var mappedItems = entries.Select((x) => MapEntry((ProductEntry) x));

      return new FixedList<IProductEntryDto>(mappedItems);
    }

    #endregion Public methods

    #region Private methods

    private static ProductPresentationForSeach AssignProductPresentation(ProductEntry presentation) {

      return new ProductPresentationForSeach {
        PresentationUID = presentation.UID,
        Name = $"{presentation.InternalCode} " +
               $"| Empaque: {presentation.PackingSmallBag} " +
               $"| Unidades: {presentation.PackagingSize} {presentation.BaseUnit.Description}",
        Description = presentation.Description,
        Units = presentation.Stock,
        Vendors = MapVendors(presentation)
      };
    }


    static private FixedList<Attributes> GetAttributes(ProductEntry entry) {
      try {
        return new FixedList<Attributes>();
      } catch (Exception e) {
        throw new Exception($"{entry.InternalCode}. {e.Message}", e);
      }
    }


    static private FixedList<ProductPresentationForSeach> GetPresentations(ProductEntry entry) {

      var presentations = new List<ProductPresentationForSeach>();

      foreach (var present in entry.Presentations) {
        ProductPresentationForSeach presentation = new ProductPresentationForSeach();

        presentation.PresentationUID = present.UID;
        presentation.Description = present.Description;
        presentation.Units = 10.5m;
        presentation.Vendors = MapVendors(present);
        presentations.Add(presentation);
      }
      return presentations.ToFixedList();
    }


    static private FixedList<ProductEntry> GetPresentationsByVendor(FixedList<ProductEntry> productPresentations,
                                                                string vendorUID) {
      vendorUID = vendorUID == string.Empty ? "Empty" : vendorUID;
      var vendor = Party.Parse(vendorUID);

      if (vendor.Id != -1) {
        return productPresentations.FindAll(x => x.Vendor.Id == vendor.Id).ToFixedList();
      }

      return new FixedList<ProductEntry>(productPresentations);
    }


    static private void GetStockForPresentations(ProductEntry product,
                                                     FixedList<ProductEntry> presentations) {

      FixedList<ProductsTotals> stocks = ProductBuilder.GetStockForPresentations(product);

      foreach (var p in presentations) {
        p.Stock = stocks.FindAll(x=>x.Product_Id == p.Id).Sum(x => x.Stock);
      }
    }


    static private ProductTypeDto GetProductType(ProductEntry entry) {

      var type = new ProductTypeDto();

      var attributes = GetAttributes(entry);

      type.ProductTypeUID = entry.ProductType.UID;
      type.Name = entry.ProductCategory.Name; //Group/Subgroup - Name
      type.Attributes = attributes;

      return type;
    }


    static public ProductForSearchingDto MapEntry(ProductEntry entry) {
      var dto = new ProductForSearchingDto();

      dto.ProductUID = entry.UID;
      dto.ProductCode = entry.InternalCode;
      dto.Description = entry.Name;
      dto.ProductImageUrl = entry.ProductImageUrl;
      dto.ProductType = GetProductType(entry);
      dto.Presentations = GetPresentations(entry);

      return dto;
    }


    static private FixedList<VendorDto> MapVendors(ProductEntry presentation) {

      var vendors = new List<VendorDto>();

      var vendor = new VendorDto {
        VendorProductUID = presentation.VendorProductUID,
        VendorUID = presentation.Vendor.UID,
        VendorName = presentation.Vendor.Name,
        Sku = "SKU"
      };

      vendors.Add(vendor);
      return vendors.ToFixedList();
    }

    #endregion Private methods


  } // class ProductMapper

} // namespace Empiria.Trade.Products.Adapters
