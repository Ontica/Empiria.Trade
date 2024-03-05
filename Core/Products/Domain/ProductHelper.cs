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

using Empiria.Trade.Products.Adapters;
using Empiria.Trade.Core;
using Newtonsoft.Json;
using System.Xml.Linq;
using System.Diagnostics;
using Empiria.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace Empiria.Trade.Products.Domain {

  /// <summary>Helper methods to build product structure.</summary>
  internal class ProductHelper {

    private ProductQuery query;

    internal ProductHelper(ProductQuery _query) {
      query = _query;

    }


    #region public methods


    internal decimal DefaultPrice(Product product) {

      if (product.Vendor.Id == 1) {

        return product.PriceList1;

      } else if (product.Vendor.Id == 2) {

        return product.PriceList7;

      } else {
        return 0;
      }
    }


    internal void GetDefaultProductBasePrices(FixedList<Product> products) {

      foreach (var product in products) {

        product.PriceList = DefaultPrice(product);
      }

    }


    internal FixedList<Product> GetProductsByCode(FixedList<Product> products) {

      var hashProducts = new EmpiriaHashTable<Product>();

      foreach (var product in products) {
        //TODO QUITAR URL Y HACER VALIDACION DE IMAGEN EN SERVER
        string url = "http://apps.sujetsa.com.mx:8080/imagenes-productos/";
        product.ProductImageUrl = $"{url}{product.Code}.jpg";
        AssingProductPresentations(hashProducts, product);

      }

      return hashProducts.ToFixedList();
    }


    internal FixedList<Product> GetProductsOrderBy(FixedList<Product> productsByCode) {

      foreach (var product in productsByCode) {
        product.Presentations = product.Presentations.OrderBy(x => x.Units).ToList();
      }

      return productsByCode.OrderBy(p => p.Code)
                                          .ThenBy(p => p.ProductName)
                                          .ToList().ToFixedList();
    }


    internal void GetProductsWithCustomerPrice(FixedList<Product> products) {

      var customerExtData = GetCustomerAssignedPriceNumber();

      GetAssignedPrice(products, customerExtData);

    }


    #endregion public methods


    #region Private methods


    private void AssingProductPresentations(EmpiriaHashTable<Product> hashProducts, Product product) {

      string hash = $"{product.Code}";

      Product productEntry;

      hashProducts.TryGetValue(hash, out productEntry);


      if (productEntry == null) {

        productEntry = product;

        GetProductPresentations(productEntry, product);

        hashProducts.Insert(hash, productEntry);

      } else {

        GetProductPresentations(productEntry, product);
      }

    }


    private void GetAssignedPrice(FixedList<Product> products, PartyExtData customerExtData) {

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

      var customer = Party.Parse(query.CustomerUID);
      var extData = new PartyExtData();

      if (customer != null) {
        extData = JsonConvert.DeserializeObject<PartyExtData>(customer.ExtData);
      }

      return extData;
    }


    private decimal GetPrice(Product product, int customerPriceNumber) {

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


    internal FixedList<Product> GetProductsByStock(FixedList<Product> products) {

      FixedList<Product> productsByStock = new FixedList<Product>(products);

      if (query.OnStock) {
        productsByStock = products.Where(x => x.InventoryEntry.InputQuantity > 0).ToFixedList();
      }

      return productsByStock;
    }


    private void GetProductPresentations(Product productEntry, Product product) {

      var existPresentation = productEntry.Presentations.Find(
                          x => x.PresentationUID == product.ProductPresentation.UID);

      if (existPresentation != null) {

        GetVendorsByPresentation(existPresentation, product);

      } else {

        var presentation = new ProductPresentationForSeach();
        presentation.PresentationUID = product.ProductPresentation.UID;
        presentation.Description = product.ProductPresentation.PresentationDescription;
        presentation.Units = product.ProductPresentation.QuantityAmount;

        GetVendorsByPresentation(presentation, product);

        productEntry.Presentations.Add(presentation);

      }
    }


    private void GetVendorsByPresentation(ProductPresentationForSeach presentation, Product product) {

      var vendorProduct = VendorProduct.Parse(product.VendorProductUID);

      var existVendor = presentation.Vendors.Find(x => x.VendorUID == product.Vendor.UID);

      if (existVendor == null) {

        VendorDto vendor = new VendorDto();
        vendor.VendorProductUID = product.VendorProductUID;
        vendor.VendorUID = product.Vendor.UID;
        vendor.VendorName = product.Vendor.Name;
        vendor.Sku = vendorProduct.SKU;
        vendor.Stock = InventoryBuilder.GetInventoryStockByVendorProduct(vendorProduct.Id).Sum(x=>x.Stock);
        vendor.Price = product.PriceList;

        presentation.Vendors.Add(vendor);
      }

    }


    private int GetVendorId(Product product, string fromDatabase) {

      if (fromDatabase == "NK SUJETSA") {
        return 1;
      }

      return 3;
    }


    #endregion Private methods

  } // class ProductHelper

} // namespace Empiria.Trade.Products.Domain
