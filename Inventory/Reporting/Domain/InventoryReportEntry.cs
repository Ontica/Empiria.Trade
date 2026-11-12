/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Information Holder                      *
*  Type     : InventoryReportEntry                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents an entry report.                                                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Core;

namespace Empiria.Trade.Inventory {


  /// <summary>Represents an interface to build entry report.</summary>
  public interface IReportEntry {

    ReportItemType ItemType {
      get; set;
    }

  } // interface IReportEntry


  /// <summary>Represents an entry report.</summary>
  public class ReportEntry : IReportEntry {

    #region Constructors and parsers

    internal ReportEntry() {
      // no-op
    }

    public ReportItemType ItemType {
      get;set;
    } = ReportItemType.Entry;


    #endregion Constructors and parsers


  } // class ReportEntry

} // namespace Empiria.Trade.Inventory
