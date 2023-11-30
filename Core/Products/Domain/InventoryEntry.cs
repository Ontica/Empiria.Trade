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
  public class InventoryEntry : BaseObject {


    #region Constructors and parsers

    internal InventoryEntry() {

    }

    static public InventoryEntry Parse(int id) => ParseId<InventoryEntry>(id);

    static public InventoryEntry Parse(int id, bool reload) => ParseId<InventoryEntry>(id, reload);

    static public InventoryEntry Parse(string uid) => ParseKey<InventoryEntry>(uid);

    static public InventoryEntry Empty => ParseEmpty<InventoryEntry>();


    #endregion Constructors and parsers

    #region Properties



    [DataField("InventoryEntryId")]
    internal int InventoryEntryId {
      get;
      private set;
    }


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
