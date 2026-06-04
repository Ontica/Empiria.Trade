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
using System.Linq;

using Empiria.Parties;
using Empiria.Trade.Core.Catalogues;

namespace Empiria.Trade.Products.Adapters {

  /// <summary>Methods used to map Products.</summary>
  public class ProductMapper {

    #region Public methods V2

    static internal FixedList<ProductForSearchingDto> Map(FixedList<Product> products, string vendorUID) {

      return products.Select(x => MapProduct(x, vendorUID))
                                 .Where(x => x.Presentations.Count > 0)
                                 .ToFixedList();
    }


    static private ProductForSearchingDto MapProduct(Product product, string vendorUID) {

      return new ProductForSearchingDto() {
        ProductUID = product.UID,
        ProductCode = product.InternalCode,
        Description = product.Description,
        ProductType = GetProductsType(product),
        Presentations = GetProductPresentations(product, vendorUID)
      };
    }


    static private ProductTypeDto GetProductsType(Product product) {

      return new ProductTypeDto {
        ProductTypeUID = product.ProductType.UID,
        Name = product.ProductType.DisplayPluralName
      };
    }


    static private FixedList<ProductPresentationForSeach> GetProductPresentations(
                                                            Product product, string vendorUID) {
      
      
      var productPresentations = product.Presentations.OrderBy(x => x.InternalCode.Length)
                                                      .ThenBy(x => x.InternalCode).ToFixedList();

      var presentationsByVendor = GetPresentationsByVendor(productPresentations, vendorUID);

      var list = new List<ProductPresentationForSeach>();

      if (presentationsByVendor.Count == 0 && vendorUID != string.Empty && product.Vendor.UID == vendorUID) {
        
        list.Add(AssignProductPresentation(product));
      }

      foreach (var p in presentationsByVendor) {

        list.Add(AssignProductPresentation(p));
      }

      return list.ToFixedList();
    }

    
    #endregion Public methods V2

    #region Public methods

    static internal FixedList<IProductEntryDto> MapToEntriesDto(FixedList<Product> entries) {

      var mappedItems = entries.Select((x) => MapEntry((Product) x));

      return new FixedList<IProductEntryDto>(mappedItems);
    }

    #endregion Public methods

    #region Private methods

    private static ProductPresentationForSeach AssignProductPresentation(Product product) {

      return new ProductPresentationForSeach {
        PresentationUID = product.UID,
        Name = $"{product.InternalCode} - {product.BaseUnit.Description}",
        Description = product.Description,
        Vendors = MapVendors(product)
      };
    }


    static public ProductForSearchingDto MapEntry(Product entry) {
      var dto = new ProductForSearchingDto();

      dto.ProductUID = entry.UID;
      dto.ProductCode = entry.InternalCode;
      dto.Description = entry.Name;
      dto.ProductImageUrl = entry.ProductImageUrl;
      dto.ProductType = GetProductType(entry);
      dto.Presentations = GetPresentations(entry);

      return dto;
    }


    static private ProductTypeDto GetProductType(Product entry) {

      var type = new ProductTypeDto();

      var attributes = GetAttributes(entry);

      type.ProductTypeUID = entry.ProductType.UID;
      type.Name = entry.ProductCategory.Name; //Group/Subgroup - Name
      type.Attributes = attributes;

      return type;
    }


    static private FixedList<Attributes> GetAttributes(Product entry) {
      try {
        return new FixedList<Attributes>();
      } catch (Exception e) {
        throw new Exception($"{entry.InternalCode}. {e.Message}", e);
      }
    }


    static private FixedList<ProductPresentationForSeach> GetPresentations(Product entry) {

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


    static private FixedList<Product> GetPresentationsByVendor(FixedList<Product> productPresentations,
                                                                string vendorUID) {
      vendorUID = vendorUID == string.Empty ? "Empty" : vendorUID;
      var vendor = Party.Parse(vendorUID);

      if (vendor.Id != -1) {
        return productPresentations.FindAll(x => x.Vendor.Id == vendor.Id).ToFixedList();
      }

      return new FixedList<Product>(productPresentations);
    }


    static private FixedList<VendorDto> MapVendors(Product presentation) {

      var vendor = new VendorDto {
        VendorProductUID = presentation.VendorProductUID,
        VendorUID = presentation.Vendor.UID,
        VendorName = presentation.Vendor.Name,
        Sku = "SKU"
      };

      var vendors = new List<VendorDto>();
      vendors.Add(vendor);
      return vendors.ToFixedList();
    }

    #endregion Private methods


  } // class ProductMapper

} // namespace Empiria.Trade.Products.Adapters
