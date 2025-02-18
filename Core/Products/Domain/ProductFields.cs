﻿/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Information Holder                      *
*  Type     : ProductFields                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Holds a product attributes list.                                                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using System.Collections.Generic;

using Empiria.Products;

namespace Empiria.Trade.Products {

  /// <summary>Holds a product attributes list. </summary>
  public class ProductFields : BaseObject {

    #region Constructors and parsers

    internal ProductFields() {
      // Required by Empiria Framework.
    }


    protected ProductFields(ProductType productType) : base(productType) {
      // Required by Empiria Framework for all partitioned types.
    }


    internal ProductFields(ProductType productType,
                     ProductFields data) : base(productType) {
      LoadData(data);
    }


    static public ProductFields Parse(int id) => ParseId<ProductFields>(id);

    static public ProductFields Parse(string uid) => ParseKey<ProductFields>(uid);

    static public ProductFields Empty => ParseEmpty<ProductFields>();

    static public FixedList<int> ParseIdList(string[] uids) => GetIdList(uids);

    #endregion Constructors and parsers


    #region Properties


    [DataField("ProductId")]
    public int ProductId {
      get; set;
    }


    [DataField("ProductTypeId")]
    public int ProductTypeId {
      get; set;
    }


    [DataField("ProductGroupId")]
    public ProductGroup ProductGroup {
      get; set;
    }


    [DataField("ProductSubgroupId")]
    public ProductSubgroup ProductSubgroup {
      get; set;
    }


    [DataField("ProductUID")]
    public string ProductUID {
      get; set;
    } = string.Empty;


    [DataField("ProductCode")]
    public string ProductCode {
      get; set;
    } = string.Empty;


    [DataField("ProductUPC")]
    public string ProductUPC {
      get; set;
    } = string.Empty;


    [DataField("ProductName")]
    public string ProductName {
      get; set;
    } = string.Empty;


    [DataField("ProductDescription")]
    public string ProductDescription {
      get; set;
    } = string.Empty;


    [DataField("Attributes")]
    public string Attributes {
      get; set;
    } = string.Empty;


    [DataField("Category")]
    public string Category {
      get; set;
    } = string.Empty;


    [DataField("ProductExtData")]
    public string ProductExtData {
      get; set;
    } = string.Empty;


    [DataField("ProductKeywords")]
    public string ProductKeywords {
      get; set;
    } = string.Empty;


    [DataField("ProductWeight")]
    public decimal ProductWeight {
      get; set;
    }


    [DataField("ProductLength")]
    public decimal ProductLength {
      get; set;
    }


    [DataField("FragileProduct")]
    public bool FragileProduct {
      get; internal set;
    }


    [DataField("ProductStatus", Default = StateEnums.EntityStatus.Active)]
    public StateEnums.EntityStatus Status {
      get; internal set;
    }

    //TODO MIGRAR ENLACE A BD
    public string ProductImageUrl => $"http://apps.sujetsa.com.mx:8080/imagenes-productos/{this.ProductCode}.jpg";

    #endregion Properties


    #region Methods

    private void LoadData(ProductFields data) {
      throw new NotImplementedException();
    }


    static private FixedList<int> GetIdList(string[] uids) {
      var list = new List<int>();
      Array.ForEach(uids, uid => list.Add(Parse(uid).Id));
      return list.ToFixedList();
    }

    #endregion Methods

  }  // class ProductFields

}  // namespace Empiria.Trade.Products
