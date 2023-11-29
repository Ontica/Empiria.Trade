/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Partitioned Type / Information Holder   *
*  Type     : WarehouseBin                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a warehouse bin.                                                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Products {

  /// <summary>Represents a warehouse bin.</summary>
  public class WarehouseBin : BaseObject {


    #region Constructors and parsers

    internal WarehouseBin() {

    }


    static public WarehouseBin Parse(int id) => ParseId<WarehouseBin>(id);

    static public WarehouseBin Parse(int id, bool reload) => ParseId<WarehouseBin>(id, reload);

    static public WarehouseBin Parse(string uid) => ParseKey<WarehouseBin>(uid);

    static public WarehouseBin Empty => ParseEmpty<WarehouseBin>();


    #endregion Constructors and parsers


    #region Properties



    [DataField("WarehouseBinUID")]
    public string WarehouseBinUID {
      get;
      internal set;
    }


    [DataField("WarehouseId")]
    public Warehouse Warehouse {
      get;
      internal set;
    }


    [DataField("BinCode")]
    public string BinCode {
      get;
      internal set;
    }


    [DataField("BinShelf")]
    public string BinShelf {
      get;
      internal set;
    }


    [DataField("BinRow")]
    public string BinRow {
      get;
      internal set;
    }


    [DataField("BinColumn")]
    public string BinColumn {
      get;
      internal set;
    }


    [DataField("BinDescription")]
    public string BinDescription {
      get;
      internal set;
    }


    [DataField("AssignedVendorProductId")]
    public ProductFields AssignedVendorProduct {
      get;
      internal set;
    }


    [DataField("BinStatus")]
    public string BinStatus {
      get;
      internal set;
    }


    #endregion Properties


  } // class WarehouseBin

} // namespace Empiria.Trade.Products
