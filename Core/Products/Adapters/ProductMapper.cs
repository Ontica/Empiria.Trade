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
using Empiria.Products;
using Empiria.Projects;
using Empiria.Trade.Core;
using Empiria.Trade.Core.Catalogues;
using Newtonsoft.Json;

namespace Empiria.Trade.Products.Adapters {

  /// <summary>Methods used to map Products.</summary>
  public class ProductMapper {

    #region Public methods V2

    static internal FixedList<ProductForSearchingDto> Map(FixedList<Product> products) {

      var mappedItems = products.Select((x) => MapProduct((Product) x));

      return new FixedList<ProductForSearchingDto>(mappedItems);
    }


    static private ProductForSearchingDto MapProduct(Product product) {

      return new ProductForSearchingDto() {
        ProductUID = product.UID,
        ProductCode = product.InternalCode,
        Description = product.Description,
        ProductType = GetProductsType(product),
        Presentations = GetProductPresentations(product.Presentations.ToFixedList())
      };
    }


    static private ProductTypeDto GetProductsType(Product product) {

      return new ProductTypeDto {
        ProductTypeUID = product.ProductType.UID,
        Name = product.ProductType.DisplayPluralName
      };
    }


    static private FixedList<ProductPresentationForSeach> GetProductPresentations(
                      FixedList<ProductPresentationForSeach> productPresentations) {

      var presentations = new List<ProductPresentationForSeach>();

      foreach (var presentationEntry in productPresentations) {
        ProductPresentationForSeach presentation = new ProductPresentationForSeach();

        presentation.PresentationUID = presentationEntry.PresentationUID;
        presentation.Description = presentationEntry.Description;
        presentation.Vendors = GetVendors(presentationEntry);

        presentations.Add(presentation);
      }

      return presentations.ToFixedList();
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

      //AttributesList attrs = new AttributesList();
      try {
        //if (entry.Attributes != "") {
        //  attrs = JsonConvert.DeserializeObject<AttributesList>(entry.Attributes);
        //} return attrs.Attributes.ToFixedList();
        return new FixedList<Attributes>();
      } catch (Exception e) {
        throw new Exception($"{entry.InternalCode}. {e.Message}", e);
      }
    }


    static private FixedList<ProductPresentationForSeach> GetPresentations(Product entry) {

      var presentations = new List<ProductPresentationForSeach>();

      foreach (var present in entry.Presentations) {
        ProductPresentationForSeach presentation = new ProductPresentationForSeach();

        presentation.PresentationUID = present.PresentationUID;
        presentation.Description = present.Description;
        presentation.Units = present.Units;
        presentation.Vendors = GetVendors(present);
        presentations.Add(presentation);
      }
      return presentations.ToFixedList();
    }


    static private FixedList<VendorDto> GetVendors(ProductPresentationForSeach presentation) {
      
      var vendors = new List<VendorDto>();

      foreach (var _vendor in presentation.Vendors) {
        VendorDto vendor = new VendorDto() {
          VendorProductUID = _vendor.VendorProductUID,
          VendorUID = _vendor.VendorUID,
          VendorName = _vendor.VendorName,
          Sku = _vendor.Sku,
          Stock = _vendor.Stock,
          Price = _vendor.Price
        };
        vendors.Add(vendor);
      }
      return vendors.ToFixedList();
    }


    //static private FixedList<Presentation> GetPresentations(Product entry) {

    //  var presentations = new List<Presentation>();

    //  Presentation presentation = new Presentation();

    //  presentation.PresentationUID = entry.ProductPresentation.PresentationUID;
    //  presentation.Description = entry.ProductPresentation.PresentationDescription;
    //  presentation.Units = entry.ProductPresentation.QuantityAmount;
    //  presentation.Vendors = GetVendors(entry);

    //  presentations.Add(presentation);

    //  return presentations.ToFixedList();
    //}


    //static private FixedList<Vendor> GetVendors(Product entry) {

    //  var vendors = new List<Vendor>();

    //  Vendor vendor = new Vendor() {
    //    VendorProductUID = entry.VendorProductUID,
    //    VendorUID = entry.Vendor.UID,
    //    VendorName = entry.Vendor.Name,
    //    Sku = entry.SKU,
    //    Stock = entry.InventoryEntry.InputQuantity,
    //    Price = entry.PriceList
    //  };

    //  vendors.Add(vendor);

    //  return vendors.ToFixedList();
    //}


    #endregion Private methods


  } // class ProductMapper

} // namespace Empiria.Trade.Products.Adapters
