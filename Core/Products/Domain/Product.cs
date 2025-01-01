﻿/* Empiria Trade *********************************************************************************************
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

using Empiria.Products;

using Empiria.Trade.Core;
using Empiria.Trade.Products.Adapters;

namespace Empiria.Trade.Products {

  /// <summary>Represents a product.</summary>
  //[PartitionedType(typeof(ProductType))]
  public class Product : BaseObject {

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
        return EmpiriaString.BuildKeywords(ProductName, Code);
      }
    }


    [DataField("ProductId")]
    public int ProductId {
      get; internal set;
    }


    [DataField("ProductUID")]
    internal string ProductUID {
      get; private set;
    }


    [DataField("VendorProductUID")]
    internal string VendorProductUID {
      get; private set;
    }


    //[DataField("ProductTypeId")]
    public int ProductTypeId {
      get; internal set;
    }


    [DataField("PresentationId")]
    internal ProductPresentation ProductPresentation {
      get; private set;
    }


    [DataField("InventoryOrderItemId")]
    internal InventoryEntry InventoryEntry {
      get; private set;
    }


    [DataField("VendorId")]
    internal Party Vendor {
      get; private set;
    }


    [DataField("ProductGroupId")]
    internal ProductGroup ProductGroup {
      get; private set;
    }


    [DataField("ProductSubgroupId")]
    internal ProductSubgroup ProductSubgroup {
      get; private set;
    }


    [DataField("ProductCode")]
    internal string Code {
      get; private set;
    }


    [DataField("ProductUPC")]
    internal string UPC {
      get; private set;
    }


    [DataField("SKU")]
    internal string SKU {
      get; private set;
    }


    [DataField("ProductName")]
    internal string ProductName {
      get; private set;
    }


    [DataField("ProductDescription")]
    internal string ProductDescription {
      get; private set;
    }


    [DataField("Attributes")]
    internal string Attributes {
      get; private set;
    }


    [DataField("PriceList1")]
    internal decimal PriceList1 {
      get; private set;
    }


    [DataField("PriceList2")]
    internal decimal PriceList2 {
      get; private set;
    }


    [DataField("PriceList3")]
    internal decimal PriceList3 {
      get; private set;
    }


    [DataField("PriceList4")]
    internal decimal PriceList4 {
      get; private set;
    }


    [DataField("PriceList5")]
    internal decimal PriceList5 {
      get; private set;
    }


    [DataField("PriceList6")]
    internal decimal PriceList6 {
      get; private set;
    }


    [DataField("PriceList7")]
    internal decimal PriceList7 {
      get; private set;
    }


    [DataField("PriceList8")]
    internal decimal PriceList8 {
      get; private set;
    }


    [DataField("PriceList9")]
    internal decimal PriceList9 {
      get; private set;
    }


    [DataField("PriceList10")]
    internal decimal PriceList10 {
      get; private set;
    }


    internal decimal PriceList {
      get; set;
    }


    [DataField("ProductWeight")]
    internal decimal Weight {
      get; private set;
    }


    [DataField("ProductLength")]
    public decimal Length {
      get; internal set;
    }


    [DataField("FragileProduct")]
    public bool FragileProduct {
      get; internal set;
    }


    [DataField("ProductStatus", Default = StateEnums.EntityStatus.Active)]
    public StateEnums.EntityStatus Status {
      get; internal set;
    }


    public string ProductImageUrl => $"http://apps.sujetsa.com.mx:8080/imagenes-productos/{this.Code}.jpg";


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
