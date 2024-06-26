/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Information Holder                      *
*  Type     : PurchaseOrderEntry                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a purchase order entry.                                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Core;
using Empiria.Trade.Orders;
using Empiria.Trade.Procurement.Adapters;

namespace Empiria.Trade.Procurement {


  /// <summary>Represents a purchase order entry.</summary>
  public class PurchaseOrderEntry : Order {


    #region Constructors and parsers


    public PurchaseOrderEntry() {
      // no-op
    }


    public PurchaseOrderEntry(PurchaseOrderFields fields) {
      MapToPurchaseOrder(fields);
    }


    static public new PurchaseOrderEntry Parse(int id) {
      return BaseObject.ParseId<PurchaseOrderEntry>(id);
    }


    static public new PurchaseOrderEntry Parse(string uid) {
      return BaseObject.ParseKey<PurchaseOrderEntry>(uid);
    }


    static public PurchaseOrderEntry ParseEmpty() {
      return new PurchaseOrderEntry();
    }


    #endregion Constructors and parsers


    #region Properties


    [DataField("OrderId")]
    public int OrderId {
      get; internal set;
    }


    [DataField("OrderUID")]
    public string OrderUID {
      get; internal set;
    }

    public decimal Total {
      get; internal set;
    }


    public FixedList<PurchaseOrderItem> Items {
      get; internal set;
    } = new FixedList<PurchaseOrderItem>();


    #endregion Properties


    #region Private methods


    private void MapToPurchaseOrder(PurchaseOrderFields fields) {
      throw new NotImplementedException();
    }


    #endregion Private methods

  } // class PurchaseOrderEntry

} // namespace Empiria.Trade.Inventory.PurchaseOrders.Domain
