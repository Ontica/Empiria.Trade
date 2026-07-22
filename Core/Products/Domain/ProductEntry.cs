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

using Empiria.Parties;
using Empiria.Ontology;
using Empiria.Products;
using Empiria.Trade.Products.Data;

namespace Empiria.Trade.Products {

  /// <summary>Represents a product.</summary>
  [PartitionedType(typeof(ProductType))]
  public class ProductEntry : Product {

    private Lazy<FixedList<ProductEntry>> _presentations;

    #region Constructors and parsers

    protected ProductEntry(ProductType productType) : base(productType) {
      // Required by Empiria Framework for all partitioned types.
    }

    internal ProductEntry(ProductType productType, ProductFields data) : base(productType) {
      LoadData(data);
    }

    static public ProductEntry ParseId(int id) => ParseId<ProductEntry>(id);

    static public ProductEntry ParseUID(string uid) => ParseKey<ProductEntry>(uid);


    protected override void OnLoad() {

      _presentations = new Lazy<FixedList<ProductEntry>>(() => ProductDataService.GetProductsPresentations(this));
    }

    #endregion Constructors and parsers

    #region Properties

    [DataField("BASE_PRODUCT_ID")]
    internal int BaseProductId {
      get; private set;
    }


    internal ProductEntry BaseProduct {
      get {
        return ParseId(BaseProductId);
      }
    }


    [DataField("VENDOR_ID")]
    internal Party Vendor {
      get; private set;
    }


    internal string VendorProductUID {
      get {
        return this.UID;
      }
    }


    internal ProductPresentation ProductPresentation {
      get; private set;
    }


    public string Diametro {
      get {
        return Attributes.Get("diametro", string.Empty);
      }
      private set {
        Attributes.SetIfValue("diametro", value);
      }
    }


    public string Largo {
      get {
        return Attributes.Get("largo", string.Empty);
      }
      private set {
        Attributes.SetIfValue("largo", value);
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


    public decimal Peso {
      get {
        return Attributes.Get<decimal>("peso", 0);
      }
      private set {
        Attributes.SetIfValue("peso", value);
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


    public decimal PackingSmallBag {
      get {
        return ExtensionData.Get<decimal>("packingSmallBag", 1);
      }
      private set {
        ExtensionData.SetIfValue("packingSmallBag", value);
      }
    }


    public decimal PackagingSize {
      get {
        return ExtensionData.Get<decimal>("packagingSize", 1);
      }
      private set {
        ExtensionData.SetIfValue("packagingSize", value);
      }
    }


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


    public string ProductImageUrl => $"http://apps.sujetsa.com.mx:8080/imagenes-productos/{this.InternalCode}.jpg";


    public bool WithUnits {
      get; internal set;
    }


    public FixedList<ProductEntry> Presentations {
      get {
        return _presentations.Value;
      }
    }

    #endregion Properties

    #region Methods

    private void LoadData(ProductFields data) {
      throw new NotImplementedException();
    }

    #endregion Methods

  }  // class Product

}  // namespace Empiria.Trade.Products
