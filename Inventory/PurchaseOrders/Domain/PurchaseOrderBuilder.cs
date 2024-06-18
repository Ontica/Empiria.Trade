/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Partitioned Type / Information Holder   *
*  Type     : PurchaseOrderBuilder                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Generate data for purchase order.                                                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Inventory.PurchaseOrders.Adapters;
using Empiria.Trade.Inventory.PurchaseOrders.Data;

namespace Empiria.Trade.Inventory.PurchaseOrders.Domain {


  /// <summary>Generate data for purchase order.</summary>
  internal class PurchaseOrderBuilder {


    #region Public methods

    internal static FixedList<PurchaseOrderEntry> GetPurchaseOrderList(PurchaseOrderQuery query) {
      
      return PurchaseOrderData.GetPurchaseOrderList(query);
    }

    #endregion Public methods


    #region Private methods

    #endregion Private methods

  } // class PurchaseOrderBuilder

} // namespace Empiria.Trade.Inventory.PurchaseOrders.Domain
