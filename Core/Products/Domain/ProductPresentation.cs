/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Information Holder                      *
*  Type     : ProductPresentation                        License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a product presentation.                                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Products {

  /// <summary>Represents a product presentation.</summary>
  public class ProductPresentation : BaseObject {

    #region Constructors and parsers

    internal ProductPresentation() {

    }

    static public ProductPresentation Parse(int id) => ParseId<ProductPresentation>(id);

    static public ProductPresentation Parse(int id, bool reload) => ParseId<ProductPresentation>(id, reload);

    static public ProductPresentation Parse(string uid) => ParseKey<ProductPresentation>(uid);

    static public ProductPresentation Empty => ParseEmpty<ProductPresentation>();


    #endregion Constructors and parsers

    #region Properties


    [DataField("PresentationUID")]
    internal string PresentationUID {
      get;
      private set;
    }


    [DataField("PresentationName")]
    internal string PresentationName {
      get;
      private set;
    }


    [DataField("PresentationDescription")]
    internal string PresentationDescription {
      get;
      private set;
    }


    [DataField("QuantityAmount")]
    internal decimal QuantityAmount {
      get;
      private set;
    }


    [DataField("QuantityUnitId")]
    internal int QuantityUnitId {
      get;
      private set;
    }


    [DataField("PresentationStatus", Default = StateEnums.EntityStatus.Active)]
    public StateEnums.EntityStatus Status {
      get; internal set;
    }

    #endregion Properties


  } // class ProductPresentation

} // namespace Empiria.Trade.Products
