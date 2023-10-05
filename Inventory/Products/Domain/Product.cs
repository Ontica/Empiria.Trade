/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Partitioned Type / Information Holder   *
*  Type     : Product                                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a product.                                                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Json;
using Empiria.Ontology;
using Empiria.Trade.Inventory.Products;

namespace Empiria.Trade.Inventory {

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


    //[DataField("ProductTypeId")]
    public int ProductTypeId {
      get;
      internal set;
    }


    [DataField("ProductId")]
    public int ProductId {
      get;
      internal set;
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


    [DataField("Description")]
    internal string Description {
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


    [DataField("Attributes")]
    internal string Attributes {
      get;
      private set;
    }


    [DataField("ProductKeywords")]
    internal string ProductKeywords {
      get;
      private set;
    }


    [DataField("ProductExtData")]
    internal string ProductExtData {
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
