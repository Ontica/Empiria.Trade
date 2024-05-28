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

namespace Empiria.Trade.Core {

  /// <summary>Represents an inventory entry.</summary>
  public class InventoryEntry : BaseObject {


    #region Constructors and parsers

    internal InventoryEntry() {

    }

    static public InventoryEntry Parse(int id) => ParseId<InventoryEntry>(id);

    static public InventoryEntry Parse(string uid) => ParseKey<InventoryEntry>(uid);

    static public InventoryEntry Empty => ParseEmpty<InventoryEntry>();


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


  } // class Inventory

} // namespace Empiria.Trade.Products
