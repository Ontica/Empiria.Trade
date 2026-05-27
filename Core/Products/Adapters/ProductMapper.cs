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
using Empiria.Products;
using Empiria.Trade.Core.Catalogues;

namespace Empiria.Trade.Products.Adapters {

  /// <summary>Methods used to map Products.</summary>
  public class ProductMapper {

    #region Public methods V2

    static internal FixedList<ProductForSearchingDto> Map(FixedList<Product> products) {

      return products.Select(x => MapProduct(x))
                     .ToFixedList();
    }


    static private ProductForSearchingDto MapProduct(Product product) {

      return new ProductForSearchingDto() {
        ProductUID = product.UID,
        ProductCode = product.InternalCode,
        Description = product.Description,
        ProductType = GetProductsType(product),
        Presentations = GetProductPresentations(product)
      };
    }


    static private ProductTypeDto GetProductsType(Product product) {

      return new ProductTypeDto {
        ProductTypeUID = product.ProductType.UID,
        Name = product.ProductType.DisplayPluralName
      };
    }


    static private FixedList<ProductPresentationForSeach> GetProductPresentations(
                                                            Product product) {

      var presentations = product.Presentations;

      var list = new List<ProductPresentationForSeach>();

      if (presentations.Count == 0) {
        ProductPresentationForSeach presentation = new ProductPresentationForSeach();

        presentation.PresentationUID = product.UID;
        presentation.Description = $"{product.InternalCode} - {product.BaseUnit.Description}";
        presentation.Vendors = MapVendors(product);

        list.Add(presentation);
      }

      foreach (var p in presentations) {
        ProductPresentationForSeach presentation = new ProductPresentationForSeach();

        presentation.PresentationUID = p.UID;
        presentation.Description = $"{p.InternalCode} - {p.BaseUnit.Description}";
        presentation.Vendors = MapVendors(p);

        list.Add(presentation);
      }

      return list.ToFixedList();
    }


    private static FixedList<ProductPresentationForSeach> GetDefaultPresentation(Empiria.Products.Product product) {

      var presentations = new List<ProductPresentationForSeach>();

      ProductPresentationForSeach presentation = new ProductPresentationForSeach();
      presentation.PresentationUID = product.BaseUnit.UID;
      presentation.Description = $"{product.InternalCode} - {product.BaseUnit.Description}";
      presentation.Units = 1;
      presentation.Vendors = new List<VendorDto>() {
        new VendorDto {
          VendorUID = product.UID,
          VendorProductUID = product.UID,
          VendorName = product.Name,
        } }.ToFixedList();

      presentations.Add(presentation);

      return presentations.ToFixedList();
    }


    static private FixedList<ProductGroup> GetProductGroup(Empiria.Products.Product product) {

      var groupId = Convert.ToInt32(product.Group);
      var subgroupId = Convert.ToInt32(product.Subgroup);

      var productGroup = ProductGroup.GetListFor(groupId, subgroupId);

      return new FixedList<ProductGroup>(productGroup);
    }

    #endregion Public methods V2

    #region Public methods

    static internal IProductEntryDto MapToDto(ProductFields entry) {

      return MapProductFields(entry);

    }

    //static internal FixedList<IProductEntryDto> MapToListDto(FixedList<Product> products) {
    //  return MapToEntriesDto(products);
    //}


    static internal FixedList<IProductEntryDto> MapToEntriesDto(FixedList<Product> entries) {

      var mappedItems = entries.Select((x) => MapEntry((Product) x));

      return new FixedList<IProductEntryDto>(mappedItems);
    }


    #endregion Public methods

    #region Private methods


    static private IProductEntryDto MapProductFields(ProductFields entry) {
      var dto = new ProductEntryDto();

      dto.ProductUID = entry.ProductUID;
      dto.ProductGroupUID = entry.ProductGroup.UID;
      dto.ProductSubgroupUID = entry.ProductSubgroup.UID;
      dto.ProductCode = entry.ProductCode;
      dto.ProductUPC = entry.ProductUPC;
      dto.ProductName = entry.ProductName;
      dto.ProductDescription = entry.ProductDescription;
      dto.Category = entry.Category;
      dto.ProductWeight = entry.ProductWeight;
      dto.ProductLength = entry.ProductLength;
      dto.FragileProduct = entry.FragileProduct;
      dto.ProductStatus = entry.Status;

      return dto;
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
