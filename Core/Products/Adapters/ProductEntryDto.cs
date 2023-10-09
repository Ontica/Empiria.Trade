/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Data Transfer Object                    *
*  Type     : ProductEntryDto                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return the entries of Products.                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Products.Adapters {


  public interface IProductEntryDto {
  }


  /// <summary>Output DTO used to return the entries of Products.</summary>
  public class ProductEntryDto : IProductEntryDto {


    public string UID {
      get; set;
    } = string.Empty;
    public string DET {
      get; set;
    } = string.Empty;


    public string Product {
      get; set;
    } = string.Empty;


    public string ProdServCode {
      get; set;
    } = string.Empty;


    public string Description {
      get; set;
    } = string.Empty;


    public DateTime RegistrationDate {
      get; set;
    } = new DateTime(2077, 01, 01);


    public string Turn {
      get; set;
    } = string.Empty;


    public string Group {
      get; internal set;
    }


    public FixedList<Attributes> Attributes {
      get; set;
    } = new FixedList<Attributes>();


    public string Line {
      get; set;
    } = string.Empty;


    public string Subgroup {
      get; set;
    } = string.Empty;


    public string Characteristics {
      get; set;
    } = string.Empty;


    public string Threads {
      get; set;
    } = string.Empty;


    public string ThreadsName {
      get; set;
    } = string.Empty;


    public string Steps {
      get; set;
    } = string.Empty;


    public string StepsName {
      get; set;
    } = string.Empty;


    public string Heads {
      get; set;
    } = string.Empty;


    public string HeadsName {
      get; set;
    } = string.Empty;


    public string ViewDetails {
      get; set;
    } = string.Empty;


    public string ViewDetailsName {
      get; set;
    } = string.Empty;


    public DateTime LastPurchaseDate {
      get; set;
    } = new DateTime(2077, 01, 01);


    public string Stock {
      get; set;
    } = string.Empty;


    public string Currency {
      get; set;
    } = string.Empty;


    public decimal Total {
      get; set;
    }


    public decimal BasisCost {
      get; set;
    }


    public decimal LastPurchaseDateCost {
      get; set;
    }


    public decimal MinimumPrice {
      get; set;
    }


    public string Packing {
      get; set;
    } = string.Empty;


    public string MultipleRefill {
      get; set;
    } = string.Empty;


    public string MINIMO_RESURTIDO {
      get; set;
    } = string.Empty;


    public string Supplier {
      get; set;
    } = string.Empty;


    public string SupplierName {
      get; set;
    } = string.Empty;


    public string ProductType {
      get; set;
    } = string.Empty;


    public string Discontinued {
      get; set;
    } = string.Empty;


    public string Code {
      get; set;
    } = string.Empty;


    public string Category {
      get; set;
    } = string.Empty;


    public string PurchaseUnit {
      get; set;
    } = string.Empty;


    public string SalesUnit {
      get; set;
    } = string.Empty;


    public string SalesUnitContent {
      get; set;
    } = string.Empty;


    public string IsStored {
      get; set;
    } = string.Empty;


    public string Imported {
      get; set;
    } = string.Empty;


    public string AlwaysImported {
      get; set;
    } = string.Empty;


    public string Status {
      get; set;
    } = string.Empty;


    public string ItemLineId {
      get; set;
    } = string.Empty;


    public string NameOfLine {
      get; set;
    } = string.Empty;


    public string Section {
      get; internal set;
    }


    public string LineName {
      get; internal set;
    }


    public string SubgroupName {
      get; internal set;
    }


    public string Model {
      get; internal set;
    }


    public string Diameter {
      get; internal set;
    }


    public string Length {
      get; internal set;
    }


    public string Degree {
      get; internal set;
    }


    public decimal Weight {
      get; internal set;
    }


    public string Keywords {
      get; set;
    } = string.Empty;


    public int CompanyId {
      get; set;
    }


    public string ExtData {
      get;
      internal set;
    } = string.Empty;


    public string Trademark {
      get; internal set;
    }


    public FixedList<PriceListOfProduct> PriceList {
      get; internal set;
    } = new FixedList<PriceListOfProduct>();


  } // class ProductEntryDto


  public class PriceListOfProduct {


    public string Name {
      get; internal set;
    }


    public decimal Value {
      get; internal set;
    }

  } // class PriceListOfProduct


}
