/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Core Management                            Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Partitioned Type / Information Holder   *
*  Type     : WarehouseBinProduct                        License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a warehouse bin product.                                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Products;

namespace Empiria.Trade.Core.Catalogues {


  /// <summary>Represents a warehouse bin product.</summary>
  public class WarehouseBinProduct : BaseObject {

    #region Constructors and parsers

    internal WarehouseBinProduct() {

    }


    static public WarehouseBinProduct Parse(int id) => ParseId<WarehouseBinProduct>(id);

    static public WarehouseBinProduct Parse(int id, bool reload) => ParseId<WarehouseBinProduct>(id, reload);

    static public WarehouseBinProduct Parse(string uid) => ParseKey<WarehouseBinProduct>(uid);

    static public WarehouseBinProduct Empty => ParseEmpty<WarehouseBinProduct>();


    #endregion Constructors and parsers


    #region Properties



    [DataField("WarehouseBinProductUID")]
    public string WarehouseBinProductUID {
      get;
      internal set;
    }


    [DataField("WarehouseBinId")]
    public WarehouseBin WarehouseBin {
      get;
      internal set;
    }


    [DataField("VendorProductId")]
    public VendorProduct VendorProduct {
      get;
      internal set;
    }


    //[DataField("Rack")]
    //public string Rack {
    //  get;
    //  internal set;
    //}


    [DataField("RackLevel")]
    public int RackLevel {
      get;
      internal set;
    }


    [DataField("RackPosition")]
    public int RackPosition {
      get;
      internal set;
    }


    #endregion Properties

  } // class WarehouseBinProduct

} // namespace Empiria.Trade.Core.Catalogues
