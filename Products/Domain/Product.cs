/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Partitioned Type / Information Holder   *
*  Type     : Product                                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a product.                                                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Ontology;

namespace Empiria.Trade.Products {

  /// <summary>Represents a product.</summary>
  [PartitionedType(typeof(ProductType))]
  public partial class Product : BaseObject {

    #region Constructors and parsers

    protected Product(ProductType productType) : base(productType) {
      // Required by Empiria Framework for all partitioned types.
    }


    internal Product(ProductType productType,
                     ProductFields data) : base(productType) {
      this.LoadData(data);
    }


    static public Product Parse(int id) => BaseObject.ParseId<Product>(id);

    static public Product Parse(int id, bool reload) => BaseObject.ParseId<Product>(id, reload);

    static public Product Parse(string uid) => BaseObject.ParseKey<Product>(uid);

    static public Product Empty => BaseObject.ParseEmpty<Product>();


    #endregion Constructors and parsers

    #region Properties

    public ProductType ProductType {
      get {
        return (ProductType) base.GetEmpiriaType();
      }
    }



    internal string Keywords {
      get {
        return EmpiriaString.BuildKeywords(this.ProductType.DisplayName);
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
