/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Information Holder                      *
*  Type     : ProductGroup                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a product's group.                                                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Products {

  /// <summary>Represents a product's group.</summary>
  public class ProductGroup : BaseObject {

    #region Constructors and parsers

    internal ProductGroup() {

    }

    
    static public ProductGroup Parse(int id) => ParseId<ProductGroup>(id);

    static public ProductGroup Parse(int id, bool reload) => ParseId<ProductGroup>(id, reload);

    static public ProductGroup Parse(string uid) => ParseKey<ProductGroup>(uid);

    static public ProductGroup Empty => ParseEmpty<ProductGroup>();


    #endregion Constructors and parsers


    #region Properties


    [DataField("GroupCode")]
    public string GroupCode {
      get;
      private set;
    }


    [DataField("ProductGroupName")]
    public string Name {
      get;
      private set;
    }


    [DataField("ProductGroupDescription")]
    public string Description {
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


    [DataField("ProductGroupStatus", Default = StateEnums.EntityStatus.Active)]
    public StateEnums.EntityStatus Status {
      get; internal set;
    }


    internal string Keywords {
      get {
        return EmpiriaString.BuildKeywords(Name, Description);
      }
    }


    #endregion Properties


  } // class ProductGroup

} // namespace Empiria.Trade.Products
