/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Core Management                            Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Partitioned Type / Information Holder   *
*  Type     : WarehouseBin                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a warehouse bin.                                                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Core.Catalogues {

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



    [DataField("WarehouseId")]
    public Warehouse Warehouse {
      get;
      internal set;
    }


    [DataField("WarehouseBinUID")]
    public string WarehouseBinUID {
      get;
      internal set;
    }


    [DataField("Rack")]
    public string Rack {
      get;
      internal set;
    }


    [DataField("PositionFrom")]
    public int PositionFrom {
      get;
      internal set;
    }


    [DataField("PositionUp")]
    public int PositionUp {
      get;
      internal set;
    }


    [DataField("BinDescription")]
    public string BinDescription {
      get;
      internal set;
    }


    [DataField("RackLevel")]
    public int RackLevel {
      get;
      internal set;
    }


    #endregion Properties


  } // class WarehouseBin

} // namespace Empiria.Trade.Core.Catalogues
