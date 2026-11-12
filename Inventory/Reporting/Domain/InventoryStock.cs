/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Information Holder                      *
*  Type     : InventoryStock                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents an inventory stock entry.                                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Trade.Core;

namespace Empiria.Trade.Inventory {


  /// <summary>Represents an inventory stock entry.</summary>
  public class InventoryStock : InventoryStockEntry, IReportEntry {


    #region Constructors and parsers

    internal InventoryStock() {
      // no-op
    }


    #endregion Constructors and parsers

    #region Properties

    public ReportItemType ItemType {
      get; set;
    } = ReportItemType.Entry;

    #endregion Properties

  } // class InventoryStock

} // namespace Empiria.Trade.Inventory
