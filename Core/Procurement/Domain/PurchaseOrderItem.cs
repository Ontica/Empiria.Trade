/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Information Holder                      *
*  Type     : PurchaseOrderItem                          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a purchase order item.                                                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Orders;

namespace Empiria.Trade.Core {

  /// <summary>Represents a purchase order item.</summary>
  public class PurchaseOrderItem : OrderItem {

    private readonly PurchaseOrder _order;
    #region Constructors and parsers

    public PurchaseOrderItem() {
      // no-op
    }

    protected PurchaseOrderItem(OrderItemType powertype) : base(powertype) {
      // Required by Empiria Framework for all partitioned types.
    }

    public PurchaseOrderItem(OrderItemType powertype, PurchaseOrder order, string productUID) :
                                          base(powertype, order) {
      Assertion.Require(order, nameof(order));
      Assertion.Require(productUID, nameof(productUID));

      _order = order;

      ValuateItems(productUID);
    }


    private void ValuateItems(string productUID) {
      var items = _order.PurchaseOrderItems.FindAll(x => x.Product.UID == productUID);

      Assertion.Require(items.Count == 0,
        $"El producto que intenta guardar, ya se encuentra registrado.");
    }

    static public new PurchaseOrderItem Parse(int id) => ParseId<PurchaseOrderItem>(id);

    static public new PurchaseOrderItem Parse(string uid) => ParseKey<PurchaseOrderItem>(uid);

    static public PurchaseOrderItem Empty => ParseEmpty<PurchaseOrderItem>();

    static public FixedList<PurchaseOrderItem> GetListFor(PurchaseOrder purchaseOrder) {
      Assertion.Require(purchaseOrder, nameof(purchaseOrder));

      return PurchaseOrderData.GetPurchaseOrderItems(purchaseOrder);
    }

    #endregion Constructors and parsers


    #region Properties

    public decimal PackagingSize {
      get {
        return Products.Product.ParseUID(this.Product.UID).PackagingSize;
      }
    }


    public decimal PackingSmallBag {
      get {
        return Products.Product.ParseUID(this.Product.UID).PackingSmallBag;
      }
    }


    public string Notes {
      get {
        return ExtData.Get("notes", string.Empty);
      }
      private set {
        ExtData.SetIfValue("notes", value);
      }
    }


    public decimal Weight {
      get {
        return ExtData.Get<decimal>("weight", 0);
      }
      private set {
        ExtData.SetIfValue("weight", value);
      }
    }

    #endregion Properties


    #region Public methods

    public decimal CalculateTotalUnits() {
      return PackagingSize * this.Quantity;
    }


    public decimal CalculateTotalPrice() {
      return ((PackagingSize * this.Quantity) / 1000) * this.UnitPrice;
    }


    public void Update(PurchaseOrderItemFields fields) {
      Assertion.Require(fields, nameof(fields));

      if (this.IsNew) {

        fields.UnitPrice = fields.UnitPrice == 0 ? 0.000001M : fields.UnitPrice;
        fields.Position = PurchaseOrderItem.GetListFor(_order).Count + 1;
      }
      Notes = fields.Notes;
      Weight = fields.Weight;
      base.Update(fields);
    }

    #endregion Public methods

  } // class PurchaseOrderItem

} // namespace Empiria.Trade.Procurement.PurchaseOrders.Domain
