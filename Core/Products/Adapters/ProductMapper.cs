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
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Empiria.Locations;
using Empiria.Parties;
using Empiria.Trade.Core;
using Empiria.Trade.Core.Catalogues;
using Empiria.Trade.Products.Domain;

namespace Empiria.Trade.Products.Adapters {

  /// <summary>Methods used to map Products.</summary>
  public class ProductMapper {

    #region Public methods V2

    static internal FixedList<ProductForSearchingDto> MapToPurchaseOrder(FixedList<ProductEntry> products) {

      return products.Select(x => MapProduct(x))
                                 .Where(x => x.Presentations.Count > 0)
                                 .ToFixedList();
    }


    static internal FixedList<ProductForSearchingDto> MapToSearcher(FixedList<ProductEntry> products,
                                                                    bool withUnits) {

      return products.Select(x => MapProduct(x, withUnits))
                                 .Where(x => x.Presentations.Count > 0)
                                 .ToFixedList();
    }


    static internal FixedList<ProductForSearchingDto> MapToSalesOrder(FixedList<ProductEntry> products,
                                                                    bool withUnits) {

      return products.Select(x => MapProduct(x, withUnits))
                                 .Where(x => x.Presentations.Count > 0)
                                 .ToFixedList();
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
        Units = presentation.PackingSmallBag,
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


    static private FixedList<ProductEntry> GetPresentationsWithStock(ProductEntry baseProduct,
                                                                     bool withUnits) {

      FixedList<ProductEntry> _presentations = GetPresentationsByBaseProduct(baseProduct);

      FixedList<ProductsTotals> stockAndLocations = ProductBuilder.GetStockAndLocationByBaseProduct(baseProduct);

      MergePresentationAndStockByLocation(_presentations, stockAndLocations);

      if (withUnits) {
        _presentations = _presentations.FindAll(x => x.Stock > 0);
      }

      return _presentations;
    }


    private static void MergePresentationAndStockByLocation(FixedList<ProductEntry> presentations,
                                                            FixedList<ProductsTotals> stocksAndLocations) {
      //TODO VALIDAR TIPO UNIDAD E IDENTIFICAR ALMACENES
      foreach (var p in presentations) {

        var stockAndLocation = stocksAndLocations.Where(x=>x.Product_Id == p.Id && x.Location.Id != -1)
                                                 .ToList();

        var locs = new List<Location>();

        foreach (var stockLocation in stockAndLocation) {
          locs.Add(stockLocation.Location);
        }

        var warehouses = new List<Location>();

        foreach (var loc in locs) {
          var warehouse = InventoryBuilder.GetRootLocation(loc);

          var exist = warehouses.Find(x => x.Id == warehouse.Id);
          
          if (exist == null) {
            warehouses.Add(warehouse);
          }
        }

        p.Stock = stockAndLocation.Sum(x => x.Stock);

        p.Locations = locs.ToFixedList();
      }
    }


    static private FixedList<ProductPresentationForSeach> GetPresentations(ProductEntry baseProduct,
                                                                                  bool withUnits) {

      FixedList<ProductEntry> _presentations = GetPresentationsWithStock(baseProduct, withUnits);

      var productPresentations = _presentations.Select((x) => AssignProductPresentation((ProductEntry) x))
                                               .ToFixedList();

      if (_presentations.Count == 0) {

        productPresentations.ToList().Add(AssignProductPresentation(baseProduct));
      }

      return new FixedList<ProductPresentationForSeach>(productPresentations);
    }


    static private FixedList<ProductPresentationForSeach> GetPresentationsForPurchaseOrder(ProductEntry baseProduct,
                                                                                  bool withUnits) {

      FixedList<ProductEntry> _presentations = GetPresentationsByBaseProduct(baseProduct);

      var productPresentations = _presentations.Select((x) => AssignProductPresentation((ProductEntry) x))
                                               .ToFixedList();

      if (_presentations.Count == 0) {

        productPresentations.ToList().Add(AssignProductPresentation(baseProduct));
      }

      return new FixedList<ProductPresentationForSeach>(productPresentations);
    }

    private static FixedList<ProductEntry> GetPresentationsByBaseProduct(ProductEntry baseProduct) {
      
      return baseProduct.Presentations.OrderBy(x => x.InternalCode.Length)
                                      .ThenBy(x => x.InternalCode).ToFixedList();
    }

    static private ProductTypeDto GetProductsType(ProductEntry product) {

      return new ProductTypeDto {
        ProductTypeUID = product.ProductType.UID,
        Name = product.ProductType.DisplayPluralName
      };
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



    static private ProductForSearchingDto MapProduct(ProductEntry product,
                                                     bool withUnits = false) {

      return new ProductForSearchingDto() {
        ProductUID = product.UID,
        ProductCode = product.InternalCode,
        Description = product.Description,
        ProductType = GetProductsType(product),
        Presentations = GetPresentationsForPurchaseOrder(product, withUnits)
      };
    }


    static private ProductForSearchingDto MapTo(ProductEntry product,
                                                     bool withUnits = false) {

      return new ProductForSearchingDto() {
        ProductUID = product.UID,
        ProductCode = product.InternalCode,
        Description = product.Description,
        ProductType = GetProductsType(product),
        Presentations = GetPresentations(product, withUnits)
      };
    }


    static private FixedList<VendorDto> MapVendors(ProductEntry presentation) {

      var vendors = new List<VendorDto>();

      var vendor = new VendorDto {
        VendorProductUID = presentation.VendorProductUID,
        VendorUID = presentation.Vendor.UID,
        VendorName = presentation.Vendor.Name,
        Stock = presentation.Stock,
        Sku = "SKU"
      };

      vendors.Add(vendor);
      return vendors.ToFixedList();
    }

    #endregion Private methods


  } // class ProductMapper

} // namespace Empiria.Trade.Products.Adapters
