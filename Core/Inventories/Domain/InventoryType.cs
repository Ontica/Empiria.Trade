/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Common Storage Type                     *
*  Type     : InventoryType                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Describes inventory type.                                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using Empiria.Orders;

namespace Empiria.Trade.Core {

  /// <summary>Describes inventory type.</summary>
  public class InventoryType : OrderCategory {

    #region Constructors and parsers

    static public new InventoryType Parse(int id) => ParseId<InventoryType>(id);

    static public new InventoryType Parse(string uid) => ParseKey<InventoryType>(uid);

    static public new InventoryType Empty => ParseEmpty<InventoryType>();

    static public new FixedList<InventoryType> GetList() {
      return GetStorageObjects<InventoryType>();
    }

    #endregion Constructors and parsers

    #region Properties

    public Boolean EntriesRequired {
      get {
        return base.ExtData.Get("entriesRequired", false);
      }
      private set {
        base.ExtData.SetIfValue("entriesRequired", value);
      }
    }

    public Boolean ItemsRequired {
      get {
        return base.ExtData.Get("itemsRequired", false);
      }
      private set {
        base.ExtData.SetIfValue("itemsRequired", value);
      }
    }
       
    #endregion Properties

  } // class InventoryType

} // namespace Empiria.Inventory
