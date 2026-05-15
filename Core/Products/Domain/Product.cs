/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Products.dll                Pattern   : Partitioned Type / Information Holder   *
*  Type     : Product                                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a product.                                                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using Empiria.Json;
using Empiria.Ontology;
using Empiria.Products;

using Empiria.Trade.Core;
using Empiria.Trade.Products.Adapters;

namespace Empiria.Trade.Products {

  /// <summary>Represents a product.</summary>
  [PartitionedType(typeof(ProductType))]
  public class Product : Empiria.Products.Product {

    #region Constructors and parsers

    internal Product() {

    }

    static public Product ParseProduct(int id) => ParseId<Product>(id);

    protected Product(ProductType productType) : base(productType) {
      // Required by Empiria Framework for all partitioned types.
    }


    internal Product(ProductType productType,
                     ProductFields data) : base(productType) {
      LoadData(data);
    }

    #endregion Constructors and parsers

    #region Properties

    [DataField("BASE_PRODUCT_ID")]
    internal int BaseProductId {
      get; private set;
    }


    internal Product BaseProduct {
      get {
        return Product.ParseProduct(BaseProductId);
      }
    }


    [DataField("VENDOR_ID")]
    internal Empiria.Parties.Party Vendor {
      get; private set;
    }


    internal string VendorProductUID {
      get; private set;
    }


    internal ProductPresentation ProductPresentation {
      get; private set;
    }


    //internal InventoryEntry InventoryEntry {
    //  get; private set;
    //}


    internal bool IsBaseProduct {
      get; set;
    }

    internal decimal PriceList1 {
      get; private set;
    }


    internal decimal PriceList2 {
      get; private set;
    }


    internal decimal PriceList3 {
      get; private set;
    }


    internal decimal PriceList4 {
      get; private set;
    }


    internal decimal PriceList5 {
      get; private set;
    }


    internal decimal PriceList6 {
      get; private set;
    }


    internal decimal PriceList7 {
      get; private set;
    }


    internal decimal PriceList8 {
      get; private set;
    }


    internal decimal PriceList9 {
      get; private set;
    }


    internal decimal PriceList10 {
      get; private set;
    }


    internal decimal PriceList {
      get; set;
    }


    public string Weight {
      get {
        return Attributes.Get("weight", string.Empty);
      }
      private set {
        Attributes.SetIfValue("weight", value);
      }
    }


    public string Length {
      get {
        return Attributes.Get("length", string.Empty);
      }
      private set {
        Attributes.SetIfValue("length", value);
      }
    }


    public string Hilos {
      get {
        return Attributes.Get("hilos", string.Empty);
      }
      private set {
        Attributes.SetIfValue("hilos", value);
      }
    }


    public string Paso {
      get {
        return Attributes.Get("paso", string.Empty);
      }
      private set {
        Attributes.SetIfValue("paso", value);
      }
    }


    public string FragileProduct {
      get {
        return ExtensionData.Get("fragileProduct", string.Empty);
      }
      private set {
        ExtensionData.SetIfValue("fragileProduct", value);
      }
    }


    public string ProductImageUrl => $"http://apps.sujetsa.com.mx:8080/imagenes-productos/{this.InternalCode}.jpg";


    public List<ProductPresentationForSeach> Presentations {
      get; set;
    } = new List<ProductPresentationForSeach>();

    #endregion Properties

    #region Methods

    private void LoadData(ProductFields data) {
      throw new NotImplementedException();
    }

    #endregion Methods

  }  // class Product

}  // namespace Empiria.Trade.Products
