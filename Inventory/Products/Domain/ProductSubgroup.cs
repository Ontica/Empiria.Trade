/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Information Holder                      *
*  Type     : ProductSubroup                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a product's subgroup.                                                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Inventory.Products {

  /// <summary>Represents a product's subgroup.</summary>
  public class ProductSubgroup : BaseObject {

    #region Constructors and parsers

    internal ProductSubgroup() {

    }

    //protected ProductSubgroup(ProductType productType) : base(productType) {
    //  // Required by Empiria Framework for all partitioned types.
    //}


    //internal Product(ProductType productType,
    //                 ProductFields data) : base(productType) {
    //  LoadData(data);
    //}


    static public ProductSubgroup Parse(int id) => ParseId<ProductSubgroup>(id);

    static public ProductSubgroup Parse(int id, bool reload) => ParseId<ProductSubgroup>(id, reload);

    static public ProductSubgroup Parse(string uid) => ParseKey<ProductSubgroup>(uid);

    static public ProductSubgroup Empty => ParseEmpty<ProductSubgroup>();


    #endregion Constructors and parsers


    #region Properties


    //[DataField("ProductSubgroupId")]
    //public int ProductSubgroupId {
    //  get;
    //  internal set;
    //}


    [DataField("SubgroupCode")]
    internal string SubgroupCode {
      get;
      private set;
    }


    [DataField("ProductSubgroupName")]
    internal string Name {
      get;
      private set;
    }


    [DataField("ProductSubgroupDescription")]
    internal string Description {
      get;
      private set;
    }


    [DataField("ProductSubgroupKeywords")]
    internal string SubgroupKeywords {
      get;
      private set;
    }


    [DataField("ProductSubgroupExtData")]
    internal string ExtData {
      get;
      private set;
    }


    [DataField("ProductSubgroupStatus")]
    public char Status {
      get; set;
    }


    internal string Keywords {
      get {
        return EmpiriaString.BuildKeywords(Name, Description);
      }
    }


    #endregion Properties





  } // class ProductSubroup

} // namespace Empiria.Trade.Inventory.Products