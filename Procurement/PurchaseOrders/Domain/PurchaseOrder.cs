/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Information Holder                      *
*  Type     : PurchaseOrder                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a purchase order entry.                                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Linq;
using Empiria.Financial;
using Empiria.Orders;

using Empiria.Trade.Procurement.Adapters;

namespace Empiria.Trade.Procurement {

  /// <summary>Represents a purchase order entry.</summary>
  public class PurchaseOrder : Order {

    #region Constructors and parsers

    public PurchaseOrder() {
      // Required by Empiria Framework for all partitioned types.
    }


    protected PurchaseOrder(OrderType orderType) : base(orderType) {
      // Required by Empiria Framework for all partitioned types.
    }


    public PurchaseOrder(PurchaseOrderFields fields, OrderType orderType) : base(orderType) {
      Assertion.Require(fields, nameof(fields));

      base.Update(fields);
      base.OrderNo = "OC-" + EmpiriaString.BuildRandomString(8)
                                  .ToUpperInvariant();
    }


    static public new PurchaseOrder Parse(int id) => ParseId<PurchaseOrder>(id);

    static public new PurchaseOrder Parse(string uid) => ParseKey<PurchaseOrder>(uid);

    static public new PurchaseOrder Empty => ParseEmpty<PurchaseOrder>();

    public override FixedList<IPayableEntity> GetPayableEntities() {

      return new FixedList<IPayableEntity>();
    }

    #endregion Constructors and parsers

    #region Properties

    [DataField("ORDER_ID")]
    public int OrderId {
      get; protected set;
    }


    [DataField("ORDER_UID")]
    public string OrderUID {
      get; protected set;
    }


    public FixedList<PurchaseOrderItem> PurchaseOrderItems {
      get {
        return PurchaseOrderItem.GetListFor(this);
      }
    }


    public decimal ItemsTotal {
      get; private set;
    }


    public int ItemsCount {
      get; private set;
    }

    #endregion Properties


    #region Methods

    internal void SetTotals() {
      this.ItemsTotal = PurchaseOrderItems.Sum(x => x.Subtotal);
      this.ItemsCount = this.Items.Count;
    }

    internal void DeleteOrder() {

      foreach (var item in this.PurchaseOrderItems) {
        item.Delete();
        item.Save();
      }

      this.Delete();
      this.Save();
    }

    #endregion Methods

  } // class PurchaseOrder

} // namespace Empiria.Trade.Procurement
