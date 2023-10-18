/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Partitioned Type / Information Holder   *
*  Type     : ProductPresentation                        License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a product presentation.                                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Core;

namespace Empiria.Trade.Products {

  /// <summary>Represents a product presentation.</summary>
  internal class VendorProduct : BaseObject {


    #region Constructors and parsers

    internal VendorProduct() {

    }

    protected VendorProduct(ProductType productType) : base(productType) {
      // Required by Empiria Framework for all partitioned types.
    }


    static public VendorProduct Parse(int id) => ParseId<VendorProduct>(id);

    static public VendorProduct Parse(int id, bool reload) => ParseId<VendorProduct>(id, reload);

    static public VendorProduct Parse(string uid) => ParseKey<VendorProduct>(uid);

    static public VendorProduct Empty => ParseEmpty<VendorProduct>();


    #endregion Constructors and parsers


    #region Properties


    //[DataField("ProductTypeId")]
    public int ProductTypeId {
      get;
      internal set;
    }


    [DataField("ProductId")]
    public Product ProductId {
      get;
      internal set;
    }


    [DataField("ProductPresentationId")]
    internal VendorProduct Presentation{
      get;
      private set;
    }


    [DataField("VendorId")]
    internal string VendorName {
      get;
      private set;
    }


    [DataField("Stock")]
    internal int Stock {
      get;
      private set;
    }


    #endregion Properties


  } // class ProductPresentation

} // namespace Empiria.Trade.Products
