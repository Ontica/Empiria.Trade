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
using Empiria.Locations;
using Empiria.Parties;
using Empiria.Products;
using Empiria.Trade.Core;
using Empiria.Trade.Products.Data;
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


    static internal FixedList<ProductForSearchingDto> MapToSearcher(FixedList<ProductEntry> baseProducts,
                                                                    bool withUnits) {

      return baseProducts.Select(x => MapTo(x, withUnits))
                                 .Where(x => x.Presentations.Count > 0)
                                 .ToFixedList();
    }


    static internal FixedList<ProductForSearchingDto> MapToSalesOrder(FixedList<ProductEntry> baseProducts,
                                                                    bool withUnits, string customerUID = "") {

      return baseProducts.Select(x => MapTo(x, withUnits, customerUID))
                                 .Where(x => x.Presentations.Count > 0)
                                 .ToFixedList();
    }

    #endregion Public methods V2


    #region Private methods

    private static ProductPresentationForSeach AssignPresentationForPurchaseOrder(ProductEntry presentation) {

      var description = presentation.Vendor.Id == -100 && presentation.Descripcion_Ing != string.Empty ?
                        presentation.Descripcion_Ing : presentation.Description;

      return new ProductPresentationForSeach {
        PresentationUID = presentation.UID,
        Name = $"{presentation.InternalCode} " +
               $"| Empaque: {presentation.PackingSmallBag} " +
               $"| Unidades: {presentation.PackagingSize} {presentation.BaseUnit.Description}",
        Description = description,
        Units = presentation.PackingSmallBag,
        Vendors = MapVendors(presentation)
      };
    }


    private static ProductPresentationForSeach AssignPresentationForSearcher(ProductEntry presentation,
                                                                             string customerUID = "") {

      return new ProductPresentationForSeach {
        PresentationUID = presentation.UID,
        Name = $"{presentation.InternalCode} " +
               $"| Empaque: {presentation.PackingSmallBag} {presentation.BaseUnit.Description}(s)",
        Description = presentation.Description,
        Units = presentation.PackingSmallBag,
        Vendors = MapVendorsForSearcher(presentation, customerUID)
      };
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

      FixedList<InventoryStockEntry> productsStock = InventoryData.GetInventoryStockForProducts(baseProduct);

      MergePresentationAndStockByLocation(_presentations, productsStock);

      _presentations = _presentations.Where(x => x.Vendor.Id == -100 ||
                                                 x.Vendor.Id == -99 ||
                                                 x.Vendor.Id == -98)
                                     .ToFixedList();

      if (withUnits) {
        _presentations = _presentations.FindAll(x => x.Stock > 0);
      }

      return _presentations;
    }


    private static void MergePresentationAndStockByLocation(FixedList<ProductEntry> presentations,
                                                            FixedList<InventoryStockEntry> productsStock) {
      //TODO VALIDAR TIPO UNIDAD E IDENTIFICAR ALMACENES
      foreach (var p in presentations) {

        var stockAndLocation = productsStock.Where(x => x.ProductId == p.Id && x.Location.Id != -1)
                                                 .ToList();

        p.Stock = ProductBuilder.GetStockByPackingSmallBag(productsStock, p);

        var locs = new List<Location>();

        foreach (var stockLocation in stockAndLocation) {
          locs.Add(stockLocation.Location);
        }

        p.Locations = locs.ToFixedList();
      }
    }


    static internal decimal GetByPackingSmallBag(List<InventoryStockEntry> stockByProductUnit,
                                       ProductUnit inventoryUnit,
                                       ProductEntry product) {
      var bag = product.PackagingSize;
      var smallBag = product.PackingSmallBag;
      var quantity = stockByProductUnit.Sum(x => x.Stock);

      if (inventoryUnit.Name == "Caja" || inventoryUnit.Name == "Kilogramo") {

        if (bag == smallBag) {

          return quantity;

        } else {
          var cantidadEmpaques = Math.Truncate(bag * quantity / smallBag);

          var unidadBase = bag * quantity - (cantidadEmpaques * smallBag);

          return cantidadEmpaques;
        }
      }

      if (inventoryUnit.Name == "Pieza") {

        var cantidadEmpaques = Math.Truncate(quantity / smallBag);
        var unidadBase = quantity - (cantidadEmpaques * smallBag);

        return cantidadEmpaques;

      } else {

        return quantity;
      }
    }


    static private FixedList<ProductPresentationForSeach> GetPresentations(ProductEntry baseProduct,
                                                            bool withUnits, string customerUID) {

      FixedList<ProductEntry> _presentations = GetPresentationsWithStock(baseProduct, withUnits);

      var productPresentations = _presentations.Select((x) => AssignPresentationForSearcher(
                                                                (ProductEntry) x, customerUID))
                                               .ToFixedList();

      if (_presentations.Count == 0) {

        productPresentations.ToList().Add(AssignPresentationForSearcher(baseProduct, customerUID));
      }

      return new FixedList<ProductPresentationForSeach>(productPresentations);
    }


    static private FixedList<ProductPresentationForSeach> GetPresentationsForPurchaseOrder(ProductEntry baseProduct,
                                                                                  bool withUnits) {

      FixedList<ProductEntry> _presentations = GetPresentationsByBaseProduct(baseProduct);

      var productPresentations = _presentations.Select((x) => AssignPresentationForPurchaseOrder((ProductEntry) x))
                                               .ToFixedList();

      if (_presentations.Count == 0) {

        productPresentations.ToList().Add(AssignPresentationForPurchaseOrder(baseProduct));
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


    static private ProductTypeDto GetProductType(ProductEntry product) {

      var type = new ProductTypeDto();

      type.ProductTypeUID = product.ProductType.UID;
      type.Name = product.ProductCategory.Name; //Group/Subgroup - Name
      type.Attributes = product.ProductAttributes;

      return type;
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


    static private ProductForSearchingDto MapTo(ProductEntry baseProduct,
                                                     bool withUnits = false,
                                                     string customerUID = "") {

      return new ProductForSearchingDto() {
        ProductUID = baseProduct.UID,
        ProductCode = baseProduct.InternalCode,
        Description = baseProduct.Description,
        ProductType = GetProductsType(baseProduct),
        Presentations = GetPresentations(baseProduct, withUnits, customerUID)
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


    static private FixedList<VendorDto> MapVendorsForSearcher(ProductEntry presentation,
                                                              string customerUID = "") {
      //TODO CAMBIAR CODIGO PARA CONSULTAR PRECIOS
      var _presentationPrices = presentation.ProductPrices.Find(x => x.PriceType.Id == -25678);

      var _productPrice = _presentationPrices != null ? _presentationPrices.Price : 0;

      var productPrice = presentation.GetProductPrice(3).Price;

      if (customerUID != string.Empty) {
        
        var customer = Party.Parse(customerUID);
        productPrice = presentation.GetProductPrice(customer.ExtendedData.Get<int>("ListaPrecios", 0)).Price;
      }

      var vendors = new List<VendorDto>();

      var vendor = new VendorDto {
        VendorProductUID = presentation.VendorProductUID,
        VendorUID = presentation.Vendor.UID,
        VendorName = presentation.Vendor.Name,
        Stock = presentation.Stock,
        Price = productPrice,
        Sku = "SKU"
      };

      vendors.Add(vendor);
      return vendors.ToFixedList();
    }

    #endregion Private methods


  } // class ProductMapper

} // namespace Empiria.Trade.Products.Adapters
