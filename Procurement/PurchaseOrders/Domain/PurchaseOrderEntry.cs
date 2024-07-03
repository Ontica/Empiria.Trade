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
using Empiria.Trade.Procurement.Data;

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


    protected override void OnSave() {

      if (IsNew) {

        this.OrderId = this.Id;
        this.OrderUID = this.UID;
        OrderNumber = "OC-" + EmpiriaString.BuildRandomString(10).ToUpperInvariant();
        OrderTime = DateTime.Now;
        Status = OrderStatus.Captured;
      }

      PurchaseOrderData.WritePurchaseOrder(this);
    }


    private void MapToPurchaseOrder(PurchaseOrderFields fields) {

      this.OrderTypeId = 1030;
      this.Supplier = Party.Parse(fields.SupplierUID);
      this.Customer = Party.Parse(fields.CustomerUID);
      this.CustomerAddress = CustomerAddress.Parse(fields.CustomerAddressUID);
      this.CustomerContact = CustomerContact.Parse(fields.CustomerContactUID);
      this.SalesAgent = Party.Parse(fields.SalesAgentUID);
      this.Notes = fields.Notes;
      this.PedimentoImportacion = ""; // ImportFormalEntry
      this.CartaPorte = ""; // BillOfLading
      //this.AuthorizationStatus = fields.OrderAuthorizationStatus;
      //this.AuthorizationTime = DateTime.MaxValue;
      this.AuthorizatedById = -1;
      this.ScheduledTime = fields.ScheduledTime;
      this.ReceptionTime = fields.ReceptionTime;
      

    }


    #endregion Private methods

  } // class PurchaseOrderEntry

} // namespace Empiria.Trade.Inventory.PurchaseOrders.Domain
