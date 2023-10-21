﻿using System;
/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Partitioned Type / Information Holder   *
*  Type     : TRDProductHelper                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Helper methods to build product structure.                                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/


using Empiria.Trade.Products.Adapters;
using Empiria.Trade.Core;
using Newtonsoft.Json;
using System.Xml.Linq;
using System.Diagnostics;

namespace Empiria.Trade.Products.Domain {

  /// <summary>Helper methods to build product structure.</summary>
  internal class TRDProductHelper {

    private ProductQuery query;

    internal TRDProductHelper(ProductQuery _query) {
      query = _query;

    }


    #region public methods


    public void GetDefaultProductBasePrices(FixedList<Product> products) {

      foreach (var product in products) {

        product.PriceList = DefaultPrice(product);
      }

    }


    public void GetProductsWithCustomerPrice(FixedList<Product> products) {

      var customerExtData = GetCustomerAssignedPriceNumber();

      GetAssignedPrice(products, customerExtData);

    }


    #endregion public methods


    #region Private methods


    private void GetAssignedPrice(FixedList<Product> products, PartyExtData customerExtData) {

      foreach (var product in products) {
        
        var customerVendorId = GetVendorId(product, customerExtData.FromDatabase);
        var vendorId = product.Vendor.Id;

        if (customerVendorId == vendorId) {
          product.PriceList = GetPrice(product, customerExtData.PriceListId);
        }

        if (customerVendorId != vendorId) {

          product.PriceList = DefaultPrice(product);

        }

      }

    }

    private int GetVendorId(Product product, string fromDatabase) {

      if (fromDatabase == "NK SUJETSA") {
        return 1;
      }

      return 2;
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


    public decimal DefaultPrice(Product product) {
      
      if (product.Vendor.Id == 1) {

        return product.PriceList1;

      } else if (product.Vendor.Id == 2) {

        return product.PriceList7;

      } else {
        return 0;
      }
    }


    #endregion Private methods

  } // class TRDProductHelper

} // namespace Empiria.Trade.Products.Domain
