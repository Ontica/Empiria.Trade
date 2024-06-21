/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Trade Core                                 Component : Data Layer                              *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Data Service                            *
*  Type     : PurchaseOrderData                          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data read methods for purchase order.                                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Procurement.PurchaseOrders.Adapters;
using Empiria.Trade.Procurement.PurchaseOrders.Domain;

namespace Empiria.Trade.Procurement.PurchaseOrders.Data {


  /// <summary>Provides data read methods for purchase order.</summary>
  internal class PurchaseOrderData {



    internal static FixedList<PurchaseOrderEntry> GetPurchaseOrderList(PurchaseOrderQuery query) {
      
      return new FixedList<PurchaseOrderEntry>();
    }


  } // class PurchaseOrderData

} // namespace Empiria.Trade.Inventory.PurchaseOrders.Data
