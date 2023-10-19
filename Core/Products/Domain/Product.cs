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
using Empiria.Json;
using Empiria.Ontology;
using Empiria.Trade.Core;

namespace Empiria.Trade.Products {

  /// <summary>Represents a product.</summary>
  //[PartitionedType(typeof(ProductType))]
  public partial class Product : BaseObject {

    #region Constructors and parsers

    internal Product() {

    }

    protected Product(ProductType productType) : base(productType) {
      // Required by Empiria Framework for all partitioned types.
    }


    internal Product(ProductType productType,
                     ProductFields data) : base(productType) {
      LoadData(data);
    }


    static public Product Parse(int id) => ParseId<Product>(id);

    static public Product Parse(int id, bool reload) => ParseId<Product>(id, reload);

    static public Product Parse(string uid) => ParseKey<Product>(uid);

    static public Product Empty => ParseEmpty<Product>();


    #endregion Constructors and parsers

    #region Properties

    //public ProductType ProductType {
    //  get {
    //    return (ProductType) GetEmpiriaType();
    //  }
    //}


    internal string Keywords {
      get {
        return EmpiriaString.BuildKeywords(Name, Code);
      }
    }


    [DataField("ProductId")]
    public int ProductId {
      get;
      internal set;
    }


    //[DataField("ProductTypeId")]
    public int ProductTypeId {
      get;
      internal set;
    }

    
    [DataField("PresentationId")]
    internal ProductPresentation PresentationId {
      get;
      private set;
    }

    
    [DataField("VendorId")]
    internal Party Vendor {
      get;
      private set;
    }


    [DataField("ProductGroupId")]
    internal ProductGroup ProductGroup {
      get;
      private set;
    }


    [DataField("ProductSubgroupId")]
    internal ProductSubgroup ProductSubgroup {
      get;
      private set;
    }


    [DataField("ProductCode")]
    internal string Code {
      get;
      private set;
    }


    [DataField("ProductUPC")]
    internal string UPC {
      get;
      private set;
    }


    [DataField("ProductName")]
    internal string Name {
      get;
      private set;
    }


    [DataField("ProductDescription")]
    internal string ProductDescription {
      get;
      private set;
    }


    [DataField("Attributes")]
    internal string Attributes {
      get;
      private set;
    }


    //[DataField("ProductKeywords")]
    internal string ProductKeywords {
      get;
      private set;
    }


    //[DataField("ProductExtData")]
    internal string ProductExtData {
      get;
      private set;
    }


    [DataField("PriceList1")]
    internal decimal PriceList1 {
      get;
      private set;
    }


    [DataField("PriceList2")]
    internal decimal PriceList2 {
      get;
      private set;
    }


    [DataField("PriceList3")]
    internal decimal PriceList3 {
      get;
      private set;
    }


    [DataField("PriceList4")]
    internal decimal PriceList4 {
      get;
      private set;
    }


    [DataField("PriceList5")]
    internal decimal PriceList5 {
      get;
      private set;
    }


    [DataField("PriceList6")]
    internal decimal PriceList6 {
      get;
      private set;
    }


    [DataField("PriceList7")]
    internal decimal PriceList7 {
      get;
      private set;
    }


    [DataField("PriceList8")]
    internal decimal PriceList8 {
      get;
      private set;
    }


    [DataField("PriceList9")]
    internal decimal PriceList9 {
      get;
      private set;
    }


    [DataField("PriceList10")]
    internal decimal PriceList10 {
      get;
      private set;
    }


    [DataField("ProductWeight")]
    internal decimal Weight {
      get;
      private set;
    }


    [DataField("ProductLength")]
    public decimal Length {
      get;
      internal set;
    }


    [DataField("ProductStatus", Default= StateEnums.EntityStatus.Active)]
    public StateEnums.EntityStatus Status {
      get; internal set;
    }


    #endregion Properties

    #region Methods

    private void LoadData(ProductFields data) {
      throw new NotImplementedException();
    }

    #endregion Methods

  }  // class Product

}  // namespace Empiria.Trade.Products
