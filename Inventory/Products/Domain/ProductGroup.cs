/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Information Holder                      *
*  Type     : ProductGroup                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a product's group.                                                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Inventory.Products {

  /// <summary>Represents a product's group.</summary>
  public class ProductGroup : BaseObject {

    #region Constructors and parsers

    internal ProductGroup() {

    }

    //protected ProductGroup(ProductType productType) : base(productType) {
    //  // Required by Empiria Framework for all partitioned types.
    //}


    //internal Product(ProductType productType,
    //                 ProductFields data) : base(productType) {
    //  LoadData(data);
    //}


    static public ProductGroup Parse(int id) => ParseId<ProductGroup>(id);

    static public ProductGroup Parse(int id, bool reload) => ParseId<ProductGroup>(id, reload);

    static public ProductGroup Parse(string uid) => ParseKey<ProductGroup>(uid);

    static public ProductGroup Empty => ParseEmpty<ProductGroup>();


    #endregion Constructors and parsers


    #region Properties


    //[DataField("ProductGroupId")]
    //public int ProductGroupId {
    //  get;
    //  internal set;
    //}


    [DataField("GroupCode")]
    internal string GroupCode {
      get;
      private set;
    }


    [DataField("ProductGroupName")]
    internal string Name {
      get;
      private set;
    }


    [DataField("ProductGroupDescription")]
    internal string Description {
      get;
      private set;
    }


    [DataField("ProductGroupKeywords")]
    internal string GroupKeywords {
      get;
      private set;
    }


    [DataField("ProductGroupExtData")]
    internal string ExtData {
      get;
      private set;
    }


    [DataField("ProductGroupStatus")]
    public char Status {
      get; set;
    }


    internal string Keywords {
      get {
        return EmpiriaString.BuildKeywords(Name, Description);
      }
    }


    #endregion Properties


  } // class ProductGroup

} // namespace Empiria.Trade.Inventory.Products
