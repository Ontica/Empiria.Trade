/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Information Holder                      *
*  Type     : Inventory                                  License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents an inventory entry.                                                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Products {

  /// <summary>Represents an inventory entry.</summary>
  public class Inventory : BaseObject {


    #region Constructors and parsers

    internal Inventory() {

    }

    static public Inventory Parse(int id) => ParseId<Inventory>(id);

    static public Inventory Parse(int id, bool reload) => ParseId<Inventory>(id, reload);

    static public Inventory Parse(string uid) => ParseKey<Inventory>(uid);

    static public Inventory Empty => ParseEmpty<Inventory>();


    #endregion Constructors and parsers

    #region Properties


    [DataField("InventoryEntryUID")]
    internal string InventoryEntryUID {
      get;
      private set;
    }


    [DataField("WarehouseId")]
    internal int WarehouseId {
      get;
      private set;
    }


    [DataField("WarehouseBinId")]
    internal int WarehouseBinId {
      get;
      private set;
    }


    [DataField("VendorProductId")]
    internal int VendorProductId {
      get;
      private set;
    }


    [DataField("InventoryEntryTypeId")]
    internal int InventoryEntryTypeId {
      get;
      private set;
    }


    [DataField("ReferenceId")]
    internal int ReferenceId {
      get;
      private set;
    }


    [DataField("EntryTime")]
    internal DateTime EntryTime {
      get;
      private set;
    }


    [DataField("InputQuantity")]
    internal decimal InputQuantity {
      get;
      private set;
    }


    [DataField("InputCost")]
    internal decimal InputCost {
      get;
      private set;
    }


    [DataField("OutputQuantity")]
    internal decimal OutputQuantity {
      get;
      private set;
    }


    [DataField("OutputCost")]
    internal decimal OutputCost {
      get;
      private set;
    }


    [DataField("EntryStatus", Default = StateEnums.EntityStatus.Active)]
    public StateEnums.EntityStatus Status {
      get; internal set;
    }

    #endregion Properties


  } // class Inventory

} // namespace Empiria.Trade.Products
