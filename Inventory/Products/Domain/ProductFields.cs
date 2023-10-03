/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Information Holder                      *
*  Type     : ProductFields                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Holds a product attributes list.                                                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;

namespace Empiria.Trade.Inventory.Products {

  /// <summary>Holds a product attributes list. </summary>
  internal class ProductFields {

    #region Constructors and parsers

    internal ProductFields() {
      // Required by Empiria Framework.
    }


    #endregion Constructors and parsers


    [DataField("CompanyId")]
    public int CompanyId {
      get; set;
    }


    [DataField("UID")]
    public string UID {
      get; set;
    } = string.Empty;


    [DataField("ProdServCode")]
    public string ProdServCode {
      get; set;
    } = string.Empty;


    [DataField("Product")]
    public string Product {
      get; set;
    } = string.Empty;


    [DataField("Description")]
    public string Description {
      get; set;
    } = string.Empty;


    [DataField("RegistrationDate")]
    public DateTime RegistrationDate {
      get; set;
    } = new DateTime(2077, 01, 01);


    [DataField("Trademark")]
    public string Trademark {
      get; set;
    } = string.Empty;


    [DataField("Model")]
    public string Model {
      get; set;
    } = string.Empty;


    [DataField("Section")]
    public string Section {
      get; set;
    } = string.Empty;


    [DataField("LineName")]
    public string LineName {
      get; set;
    } = string.Empty;


    [DataField("GroupName")]
    public string GroupName {
      get; set;
    } = string.Empty;


    [DataField("SubgroupName")]
    public string SubgroupName {
      get; set;
    } = string.Empty;


    [DataField("Diameter")]
    public string Diameter {
      get; set;
    } = string.Empty;


    [DataField("Length")]
    public string Length {
      get; set;
    } = string.Empty;


    [DataField("Degree")]
    public string Degree {
      get; set;
    } = string.Empty;


    [DataField("Weight")]
    public decimal Weight {
      get; set;
    }


    [DataField("Characteristics")]
    public string Characteristics {
      get; set;
    } = string.Empty;


    [DataField("ThreadsName")]
    public string ThreadsName {
      get; set;
    } = string.Empty;


    [DataField("StepsName")]
    public string StepsName {
      get; set;
    } = string.Empty;


    [DataField("HeadsName")]
    public string HeadsName {
      get; set;
    } = string.Empty;


    [DataField("ViewDetailsName")]
    public string ViewDetailsName {
      get; set;
    } = string.Empty;


    [DataField("Existence")]
    public string Stock {
      get; set;
    } = string.Empty;


    [DataField("SalesUnit")]
    public string SalesUnit {
      get; set;
    } = string.Empty;


    [DataField("Currency")]
    public string Currency {
      get; set;
    } = string.Empty;


    [DataField("LastPurchaseDate")]
    public DateTime LastPurchaseDate {
      get; set;
    } = new DateTime(2077, 01, 01);


    [DataField("LastPurchaseDateCost")]
    public decimal LastPurchaseDateCost {
      get; set;
    }


    [DataField("MinimumPrice")]
    public decimal MinimumPrice {
      get; set;
    }


    [DataField("BasisCost")]
    public decimal BasisCost {
      get; set;
    }


    [DataField("ListPrice1")]
    public decimal ListPrice1 {
      get; set;
    }


    [DataField("ListPrice2")]
    public decimal ListPrice2 {
      get; set;
    }


    [DataField("ListPrice3")]
    public decimal ListPrice3 {
      get; set;
    }


    [DataField("ListPrice4")]
    public decimal ListPrice4 {
      get; set;
    }


    [DataField("Total")]
    public decimal Total {
      get; set;
    }


    [DataField("Packing")]
    public string Packing {
      get; set;
    } = string.Empty;


    [DataField("MinimumRefills")]
    public string MinimumRefills {
      get; set;
    } = string.Empty;


    [DataField("Supplier")]
    public string Supplier {
      get; set;
    } = string.Empty;


    [DataField("SupplierName")]
    public string SupplierName {
      get; set;
    } = string.Empty;


    [DataField("ProductType")]
    public string ProductType {
      get; set;
    } = string.Empty;


    [DataField("Discontinued")]
    public string Discontinued {
      get; set;
    } = string.Empty;


    [DataField("Status")]
    public string Status {
      get; set;
    } = string.Empty;


    [DataField("ItemLineId")]
    public string ItemLineId {
      get; set;
    } = string.Empty;


    [DataField("Keywords")]
    public string Keywords {
      get; set;
    } = string.Empty;


    [DataField("ExtData")]
    public string ExtData {
      get;
      internal set;
    } = string.Empty;


  }  // class ProductFields

}  // namespace Empiria.Trade.Products
