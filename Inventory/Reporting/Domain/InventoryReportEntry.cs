/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Information Holder                      *
*  Type     : InventoryReportEntry                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents an inventory entry report.                                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Inventory {


  /// <summary>Represents an interface to build inventory entry report.</summary>
  public interface IInventoryReport {

  }


  /// <summary>Represents an inventory entry report.</summary>
  public class InventoryReportEntry : IInventoryReport {

    #region Constructors and parsers

    internal InventoryReportEntry() {
      // no-op
    }


    #endregion Constructors and parsers


  } // class InventoryReportEntry

} // namespace Empiria.Trade.Inventory
