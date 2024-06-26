/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Partitioned Type / Information Holder   *
*  Type     : InventoryReportsBuilder                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Generate data for Inventory order.                                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Inventory.Domain {


  /// <summary></summary>
  internal class InventoryReportsBuilder {


    #region Public methods


    internal FixedList<InventoryStockEntry> GetInventoryStock() {



      return new FixedList<InventoryStockEntry>();
    }


    #endregion Public methods


  } // class InventoryReportsBuilder

} // namespace Empiria.Trade.Inventory.Domain
