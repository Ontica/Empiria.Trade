/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Partitioned Type / Information Holder   *
*  Type     : InventoryBuilder                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Generate data for Inventory.                                                                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Core {


  /// <summary>Generate data for Inventory.</summary>
  public class InventoryBuilder {


    #region Constructors and parsers

    public InventoryBuilder() {

    }


    #endregion Constructor and parsers


    #region Public methods


    public FixedList<SalesInventoryStock> GetInventoryStockByVendorProduct(int vendorProducId) {

      return InventoryData.GetInventoryStockByVendorProduct(vendorProducId);
    }


    #endregion Public methods


  } // class InventoryBuilder

} // namespace Empiria.Trade.Core
