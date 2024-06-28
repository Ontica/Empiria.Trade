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
  public class VendorProduct : BaseObject {


    #region Constructors and parsers

    internal VendorProduct() {

    }


    static public VendorProduct Parse(int id) => ParseId<VendorProduct>(id);

    static public VendorProduct Parse(string uid) => ParseKey<VendorProduct>(uid);

    static public VendorProduct Empty => ParseEmpty<VendorProduct>();


    #endregion Constructors and parsers


    #region Properties


    [DataField("VendorProductId")]
    public int VendorProductId {
      get;
      internal set;
    }


    [DataField("VendorProductUID")]
    public string VendorProductUID {
      get;
      internal set;
    }


    [DataField("ProductId")]
    public ProductFields ProductFields {
      get;
      internal set;
    }


    [DataField("PresentationId")]
    public ProductPresentation ProductPresentation {
      get;
      private set;
    }


    [DataField("VendorId")]
    public Party Vendor {
      get;
      private set;
    }


    [DataField("SKU")]
    public string SKU {
      get;
      private set;
    }


    public decimal InputQuantity {
      get;
      set;
    }


    [DataField("VendorProductStatus", Default = StateEnums.EntityStatus.Active)]
    public StateEnums.EntityStatus Status {
      get; internal set;
    }


    #endregion Properties


  } // class ProductPresentation

} // namespace Empiria.Trade.Products
