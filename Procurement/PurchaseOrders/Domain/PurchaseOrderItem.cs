/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Information Holder                      *
*  Type     : PurchaseOrderItem                          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a purchase order item.                                                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using Empiria.Billing;
using Empiria.Orders;
using Empiria.StateEnums;
using Empiria.Trade.Procurement.Adapters;
using Empiria.Trade.Procurement.Data;

namespace Empiria.Trade.Procurement {

  /// <summary>Represents a purchase order item.</summary>
  public class PurchaseOrderItem : OrderItem {

    #region Constructors and parsers

    public PurchaseOrderItem() {
      // no-op
    }

    protected PurchaseOrderItem(OrderItemType powertype) : base(powertype) {
      // Required by Empiria Framework for all partitioned types.
    }

    protected internal PurchaseOrderItem(OrderItemType powertype, PurchaseOrder order) :
                                          base(powertype, order) {
      Assertion.Require(order, nameof(order));

    }

    static public new PurchaseOrderItem Parse(int id) => ParseId<PurchaseOrderItem>(id);

    static public new PurchaseOrderItem Parse(string uid) => ParseKey<PurchaseOrderItem>(uid);

    static public PurchaseOrderItem Empty => ParseEmpty<PurchaseOrderItem>();
    
    internal static FixedList<PurchaseOrderItem> GetListFor(PurchaseOrder purchaseOrder) {
      Assertion.Require(purchaseOrder, nameof(purchaseOrder));

      return PurchaseOrderData.GetPurchaseOrderItems(purchaseOrder);
    }
    #endregion Constructors and parsers

    #region Properties


    public int OrderItemId {
      get; internal set;
    }


    public string OrderItemUID {
      get; internal set;
    }


    #endregion Properties

    #region Private methods

    internal void Update(PurchaseOrderItemFields fields) {
      Assertion.Require(fields, nameof(fields));

      base.Update(fields);
    }

    #endregion Private methods

  } // class PurchaseOrderItem

} // namespace Empiria.Trade.Procurement.PurchaseOrders.Domain
