/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Information Holder                      *
*  Type     : InventoryStockEntry                        License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents an inventory stock entry.                                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Core;
using Empiria.Trade.Core.Catalogues;
using Empiria.Trade.Inventory.Adapters;
using Empiria.Trade.Products;

namespace Empiria.Trade.Inventory {


  /// <summary>Represents an inventory stock entry.</summary>
  public class InventoryStockEntry : SalesInventoryStock, IReportEntry {


    #region Constructors and parsers

    internal InventoryStockEntry() {
      // no-op
    }


    #endregion Constructors and parsers

    #region Properties


    public ReportItemType ItemType {
      get; set;
    } = ReportItemType.Entry;


    #endregion Properties

  } // class InventoryStockEntry

} // namespace Empiria.Trade.Inventory
