/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Material Management                        Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Information Holder                      *
*  Type     : InventoryEntryV1                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Generate data for Inventory.                                                                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Core {

  /// <summary>Represents an inventory entry.</summary>
  public class InventoryEntryV1 : BaseObject {


    #region Constructors and parsers

    internal InventoryEntryV1() {

    }

    static public InventoryEntryV1 Parse(int id) => ParseId<InventoryEntryV1>(id);

    static public InventoryEntryV1 Parse(string uid) => ParseKey<InventoryEntryV1>(uid);

    static public InventoryEntryV1 Empty => ParseEmpty<InventoryEntryV1>();


    #endregion Constructors and parsers

    #region Properties


    [DataField("InventoryOrderItemId")]
    public int InventoryOrderItemId {
      get; private set;
    }


    [DataField("InventoryOrderItemUID")]
    internal string InventoryOrderItemUID {
      get; private set;
    }


    [DataField("WarehouseBinId")]
    public int WarehouseBinId {
      get; private set;
    }


    [DataField("VendorProductId")]
    internal int VendorProductId {
      get; private set;
    }


    [DataField("InventoryOrderTypeItemId")]
    internal int InventoryOrderTypeItemId {
      get; private set;
    }


    [DataField("InputQuantity")]
    public decimal InputQuantity {
      get; private set;
    }


    [DataField("OutputQuantity")]
    public decimal OutputQuantity {
      get; private set;
    }


    //[DataField("PulledAppartQuantity")]
    //public decimal PulledAppartQuantity {
    //  get; private set;
    //}


    //[DataField("InputCost")]
    //internal decimal InputCost {
    //  get; private set;
    //}


    //[DataField("OutputCost")]
    //internal decimal OutputCost {
    //  get; private set;
    //}


    [DataField("InventoryOrderItemStatus", Default = StateEnums.EntityStatus.Active)]
    public StateEnums.EntityStatus Status {
      get; internal set;
    }

    #endregion Properties


  } // class InventoryEntryV1

} // namespace Empiria.Trade.Core
