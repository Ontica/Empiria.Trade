/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Partitioned Type / Information Holder   *
*  Type     : ProductHelper                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Helper methods to build product structure.                                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using System.Linq;
using Empiria.Collections;
using Empiria.Trade.Core;
using Empiria.Trade.Products.Adapters;
using Empiria.Trade.Products.Data;
using Empiria.Trade.Products.UseCases;

namespace Empiria.Trade.Products.Domain {

  /// <summary>Helper methods to build product structure.</summary>
  internal class ProductHelper {

    private readonly FixedList<ProductEntry> _baseProducts;

    #region Public methods V2

    internal ProductHelper(ProductQuery query) {

      var baseProductIds = ProductDataService.GetProductsByKeywords(query.Keywords)
                                             .SelectDistinct(x => x.BaseProductId).ToArray();

      _baseProducts = ProductDataService.GetBaseProducts(baseProductIds, query.Keywords);
    }

    public FixedList<ProductEntry> BaseProducts {
      get {
        return _baseProducts;
      }
    }

    #endregion Public methods V2


    #region public methods


    internal decimal DefaultPrice(ProductEntry product) {

      if (product.Vendor.Id == 1) {

        return product.PriceList1;

      } else if (product.Vendor.Id == 2) {

        return product.PriceList7;

      } else {
        return 0;
      }
    }


    internal void GetDefaultProductBasePrices(FixedList<ProductEntry> products) {

      foreach (var product in products) {

        product.PriceList = DefaultPrice(product);
      }

    }


    internal FixedList<ProductEntry> GetProductsByCodeForPurchaseOrder(FixedList<ProductEntry> products) {

      var hashProducts = new EmpiriaHashTable<ProductEntry>();

      foreach (var product in products) {

        AssingHashProductByCodeForPurchaseOrder(hashProducts, product);
      }

      return hashProducts.ToFixedList();
    }


    internal FixedList<ProductEntry> GetProductsByCode(FixedList<ProductEntry> products) {

      var hashProducts = new EmpiriaHashTable<ProductEntry>();

      foreach (var product in products) {

        AssingHashProductByCode(hashProducts, product);
      }

      return hashProducts.ToFixedList();
    }


    internal FixedList<ProductEntry> GetProductsOrderBy(FixedList<ProductEntry> productsByCode) {

      return productsByCode.OrderBy(p => p.InternalCode)
                           .ThenBy(p => p.Name)
                           .ToList().ToFixedList();
    }


    internal void GetProductsWithCustomerPrice(FixedList<ProductEntry> products) {

      var customerExtData = GetCustomerAssignedPriceNumber();

      GetAssignedPrice(products, customerExtData);

    }

    #endregion public methods


    #region Private methods

    private void AssingHashProductByCode(EmpiriaHashTable<ProductEntry> hashProducts, ProductEntry product) {

      string hash = $"{product.InternalCode}";

      ProductEntry productEntry;

      hashProducts.TryGetValue(hash, out productEntry);

      if (productEntry == null) {

        productEntry = product;

        GetProductPresentations(productEntry, product);

        hashProducts.Insert(hash, productEntry);

      } else {

        GetProductPresentations(productEntry, product);
      }

    }


    private void AssingHashProductByCodeForPurchaseOrder(
      EmpiriaHashTable<ProductEntry> hashProducts, ProductEntry product) {

      string hash = $"{product.InternalCode}";

      ProductEntry productEntry;

      hashProducts.TryGetValue(hash, out productEntry);

      if (productEntry == null) {

        productEntry = product;

        GetProductPresentationsForPurchaseOrder(productEntry);

        hashProducts.Insert(hash, productEntry);

      }

    }


    private void GetAssignedPrice(FixedList<ProductEntry> products, PartyExtData customerExtData) {

      foreach (var product in products) {

        var vendorId = product.Vendor.Id;
        var customerVendorId = GetVendorId(product, customerExtData.FromDatabase);

        if (customerVendorId == vendorId) {
          product.PriceList = GetPrice(product, customerExtData.PriceListId);
        }

        if (customerVendorId != vendorId) {

          product.PriceList = DefaultPrice(product);

        }

      }

    }


    private PartyExtData GetCustomerAssignedPriceNumber() {

      throw new NotImplementedException();

      //var customer = Party.Parse(_query.CustomerUID);
      //var extData = new PartyExtData();

      //if (customer != null) {
      //  extData = JsonConvert.DeserializeObject<PartyExtData>(customer.ExtData);
      //}

      //return extData;
    }


    private decimal GetPrice(ProductEntry product, int customerPriceNumber) {

      decimal price = 0;

      if (customerPriceNumber == 0) {

        price = DefaultPrice(product);

      }
      if (customerPriceNumber == 1) {

        price = product.PriceList1;
      }
      if (customerPriceNumber == 2) {

        price = product.PriceList2;
      }
      if (customerPriceNumber == 3) {

        price = product.PriceList3;
      }
      if (customerPriceNumber == 4) {

        price = product.PriceList4;
      }
      if (customerPriceNumber == 5) {

        price = product.PriceList5;
      }
      if (customerPriceNumber == 6) {

        price = product.PriceList6;
      }
      if (customerPriceNumber == 7) {

        price = product.PriceList7;
      }
      if (customerPriceNumber == 8) {

        price = product.PriceList8;
      }
      if (customerPriceNumber == 9) {

        price = product.PriceList9;
      }
      if (customerPriceNumber == 10) {

        price = product.PriceList10;
      }

      return price;
    }


    internal FixedList<ProductEntry> GetProductsByStock(FixedList<ProductEntry> products) {

      FixedList<ProductEntry> productsByStock = new FixedList<ProductEntry>(products);

      return productsByStock;
    }


    private void GetProductPresentations(ProductEntry productEntry, ProductEntry product) {

      var existPresentation = productEntry.Presentations.Find(
                          x => x.UID == product.ProductPresentation.UID);

      if (existPresentation != null) {

        // GetVendorsByPresentation(existPresentation, product);

      } else {

        var presentation = new ProductPresentationForSeach();
        presentation.PresentationUID = product.ProductPresentation.UID;
        presentation.Description = product.ProductPresentation.PresentationDescription;
        presentation.Units = product.ProductPresentation.QuantityAmount;

        GetVendorsByPresentation(presentation, product);
      }
    }


    private void GetProductPresentationsForPurchaseOrder(ProductEntry productEntry) {

      var presentations = ProductUseCases.GetProductPresentations();

      foreach (var present in presentations) {
        var presentation = new ProductPresentationForSeach();
        presentation.PresentationUID = present.PresentationUID;
        presentation.Description = present.PresentationDescription;
        presentation.Units = present.QuantityAmount;
        presentation.Vendors = new FixedList<VendorDto>();
      }
    }


    private void GetVendorsByPresentation(ProductPresentationForSeach presentation, ProductEntry product) {

      var vendorProduct = VendorProduct.Parse(product.VendorProductUID);

      var existVendor = presentation.Vendors.Find(x => x.VendorUID == product.Vendor.UID);
      List<VendorDto> vendorDtos = new List<VendorDto>();
      if (existVendor == null) {

        VendorDto vendor = new VendorDto();
        vendor.VendorProductUID = product.VendorProductUID;
        vendor.VendorUID = product.Vendor.UID;
        vendor.VendorName = product.Vendor.Name;
        vendor.Sku = vendorProduct.SKU;
        vendor.Stock = InventoryBuilder.GetInventoryStockByVendorProduct(vendorProduct.Id, "").Sum(x => x.Stock);
        vendor.Price = product.PriceList;
        vendorDtos.Add(vendor);
      }
      presentation.Vendors = vendorDtos.ToFixedList();
    }


    private int GetVendorId(ProductEntry product, string fromDatabase) {

      if (fromDatabase == "NK SUJETSA") {
        return 1;
      }

      return 3;
    }


    #endregion Private methods

  } // class ProductHelper

} // namespace Empiria.Trade.Products.Domain
