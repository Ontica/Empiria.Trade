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
using DocumentFormat.OpenXml.Presentation;
using Empiria.Financial;
using Empiria.Orders;
using Empiria.Parties;

namespace Empiria.Trade.Core {

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

      if (this.IsNew) {
        base.OrderNo = "OC-" + EmpiriaString.BuildRandomString(8)
                                  .ToUpperInvariant();
      }

      GetDefaultFields(fields, orderType);

      base.Activate();
      base.Update(fields);
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


    public string ShippingMethod {
      get {
        return ConditionsData.Get("shippingMethod", string.Empty);
      }
      private set {
        ConditionsData.SetIfValue("shippingMethod", value);
      }
    }


    public DateTime ScheduledTime {
      get {
        return ConditionsData.Get("scheduledTime", DateTime.MaxValue);
      }
      private set {
        ConditionsData.SetIfValue("scheduledTime", value);
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

    public void CloseOrder() {

      ValuateItemsToClose();

      this.Close(Parties.Party.ParseWithContact(ExecutionServer.CurrentContact));
      this.Save();
    }


    private void ValuateItemsToClose() {

      foreach (var item in PurchaseOrderItems) {
        if (item.Subtotal <= 0.01M) {
          Assertion.RequireFail("Para cerrar la OC, todos los productos deben tener un costo mayor a $0.00");
        }
      }
    }

    public void DeleteOrder() {

      foreach (var item in this.PurchaseOrderItems) {
        item.Delete();
        item.Save();
      }

      this.Delete();
      this.Save();
    }


    public void Update(PurchaseOrderFields fields, OrderType orderType) {

      GetDefaultFields(fields, orderType);

      ShippingMethod = fields.ShippingMethod;
      ScheduledTime = fields.ScheduledTime;

      base.Update(fields);
    }

    #endregion Methods

    #region Private methods

    private void GetDefaultFields(PurchaseOrderFields fields, OrderType orderType) {
      fields.OrderTypeUID = orderType.UID;
      fields.ProviderUID = fields.SupplierUID;
      fields.RequestedByUID = Parties.Party.ParseWithContact(ExecutionServer.CurrentContact).UID;
      fields.Name = "Sin asignar";
      fields.Observations = fields.Notes;
      fields.StartDate = DateTime.Now;

      fields.EnsureIsValid(this.Provider.UID, this.PurchaseOrderItems.Count);
    }

    #endregion

  } // class PurchaseOrder

} // namespace Empiria.Trade.Core
