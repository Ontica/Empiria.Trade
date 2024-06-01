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
using System.Collections.Generic;
using Empiria.StateEnums;

namespace Empiria.Trade.Core.Catalogues {

  /// <summary>Represents a warehouse bin.</summary>
  public class WarehouseBin : BaseObject {


    #region Constructors and parsers

    internal WarehouseBin() {

    }


    static public WarehouseBin Parse(int id) => ParseId<WarehouseBin>(id);

    static public WarehouseBin Parse(string uid) => ParseKey<WarehouseBin>(uid);

    static public WarehouseBin Empty => ParseEmpty<WarehouseBin>();


    #endregion Constructors and parsers


    #region Properties


    [DataField("WarehouseBinUID")]
    public string WarehouseBinUID {
      get; internal set;
    }


    [DataField("WarehouseId")]
    public Warehouse Warehouse {
      get; internal set;
    }


    [DataField("WarehouseBinTag")]
    public string Tag {
      get; internal set;
    }


    [DataField("Rack")]
    public string Rack {
      get; internal set;
    }


    [DataField("RackRow")]
    public int RackRow {
      get; internal set;
    }


    [DataField("RackColumn")]
    public int RackColumn {
      get; internal set;
    }


    [DataField("WarehouseBinName")]
    public string WarehouseBinName {
      get; internal set;
    }


    [DataField("WarehouseBinStatus", Default = ActivityStatus.Active)]
    public ActivityStatus WarehouseBinStatus {
      get; internal set;
    } = ActivityStatus.Active;


    //[DataField("RackLevel")]
    public int RackLevel {
      get;
      internal set;
    }


    //[DataField("PositionFrom")]
    public int PositionFrom {
      get;
      internal set;
    }


    //[DataField("PositionUp")]
    public int PositionUp {
      get;
      internal set;
    }


    public string WarehouseName => $"Almacen {Warehouse.Code}";


    public int[] Positions => GetPositions();


    public int[] Levels => GetLevels();


    #endregion Properties


    #region Public methods


    private int[] GetPositions() {
      if (PositionFrom < 1 && PositionUp < PositionFrom) {
        return new int[0];
      }

      List<int> positions = new List<int>();
      for (int i = PositionFrom; i <= PositionUp; i++) {
        positions.Add(i);
      }
      return positions.ToArray();
    }


    private int[] GetLevels() {
      if (RackLevel < 1) {
        return new int[0];
      }

      List<int> levels = new List<int>();
      for (int i = 1; i <= RackLevel; i++) {
        levels.Add(i);
      }
      return levels.ToArray();
    }


    #endregion Public methods


    #region Private methods





    #endregion Private methods

  } // class WarehouseBin

} // namespace Empiria.Trade.Core.Catalogues
