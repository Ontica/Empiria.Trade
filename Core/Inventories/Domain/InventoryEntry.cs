/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Material Management                        Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Information Holder                      *
*  Type     : InventoryEntry                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Generate data for Inventory.                                                                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using Empiria.Financial;
using Empiria.Inventory;
using Empiria.Json;
using Empiria.Locations;
using Empiria.Ontology;
using Empiria.Orders;
using Empiria.Parties;
using Empiria.Products;
using Empiria.Trade.Orders;

namespace Empiria.Trade.Core {

  /// <summary>Represents an inventory entry.</summary>
  [PartitionedType(typeof(InventoryEntryType))]
  public class InventoryEntry : BaseObject {

    #region Constructors and parsers

    public InventoryEntry() {
      //no-op
    }

    protected InventoryEntry(InventoryEntryType powertype) : base(powertype) {
      // Required by Empiria Framework for all partitioned types.
    }

    static public InventoryEntry Parse(int id) => ParseId<InventoryEntry>(id);

    static public InventoryEntry Parse(string uid) => ParseKey<InventoryEntry>(uid);

    static public InventoryEntry Empty => ParseEmpty<InventoryEntry>();

    public InventoryEntry(InventoryEntryType powertipe, InventoryOrder order, InventoryOrderItem orderItem) :
                           base(powertipe) {

      Assertion.Require(order, nameof(order));
      Assertion.Require(orderItem, nameof(orderItem));

      this.Order = order;
      this.OrderItem = orderItem;
      this.Unit = orderItem.Product.BaseUnit;
      this.Position = orderItem.Position;
      this.Sku = ProductSku.Empty;
    }


    static public FixedList<InventoryEntry> GetListFor(InventoryOrderItem orderItem) {
      Assertion.Require(orderItem, nameof(orderItem));

      return InventoryData.GetInventoryEntriesByOrderItem(orderItem);
    }


    public static InventoryEntry TryParseWithOrderItemId(int orderItemId) {
      Assertion.Require(orderItemId, nameof(orderItemId));

      return TryParse<InventoryEntry>($"Inv_Entry_Order_Item_Id = {orderItemId}");
    }

    #endregion Constructors and parsers

    #region Properties

    public InventoryEntryType InventoryEntryItemType {
      get {
        return (InventoryEntryType) base.GetEmpiriaType();
      }
    }


    [DataField("Inv_Entry_Order_Id")]
    public InventoryOrder Order {
      get; set;
    }


    [DataField("Inv_Entry_Order_Item_Id")]
    internal InventoryOrderItem OrderItem {
      get; set;
    }


    [DataField("Inv_Entry_Product_Id")]
    public Product Product {
      get; set;
    }


    [DataField("Inv_Entry_Sku_Id")]
    public ProductSku Sku {
      get; set;
    }


    [DataField("Inv_Entry_Location_Id")]
    public Location Location {
      get; set;
    }


    [DataField("Inv_Entry_Observations")]
    public string Observations {
      get; set;
    } = string.Empty;


    [DataField("Inv_Entry_Unit_Id")]
    public ProductUnit Unit {
      get; set;
    }


    [DataField("Inv_Entry_Input_Qty")]
    public decimal InputQuantity {
      get; set;
    } = 0;


    [DataField("Inv_Entry_Input_Cost")]
    public decimal InputCost {
      get; set;
    } = 0;


    [DataField("Inv_Entry_Output_Qty")]
    public decimal OutputQuantity {
      get; set;
    } = 0;


    [DataField("Inv_Entry_Output_Cost")]
    public decimal OutputCost {
      get; set;
    } = 0;


    [DataField("Inv_Entry_Counting_Qty")]
    public decimal CountingQuantity {
      get; set;
    } = 0;


    [DataField("Inv_Entry_Counting_Cost")]
    public decimal CountingCost {
      get; set;
    } = 0;


    [DataField("Inv_Entry_Time")]
    public DateTime EntryTime {
      get; set;
    }


    [DataField("Inv_Entry_Tags")]
    public string Tags {
      get; set;
    } = string.Empty;


    [DataField("Inv_Entry_Ext_Data")]
    internal protected JsonObject ExtData {
      get; private set;
    }


    [DataField("Inv_Entry_Keywords")]
    public string InventoryEntryKeywords {
      get; set;
    } = string.Empty;


    [DataField("Inv_Entry_Position")]
    public int Position {
      get; set;
    } = 0;


    [DataField("Inv_Entry_Posted_By_Id")]
    public Empiria.Parties.Party PostedBy {
      get; set;
    }


    [DataField("Inv_Entry_Posting_Time")]
    public DateTime PostingTime {
      get; set;
    }


    [DataField("Inv_Entry_Status", Default = InventoryStatus.Abierto)]
    public InventoryStatus Status {
      get; set;
    } = InventoryStatus.Abierto;


    internal int OrderItemProductId {
      get; private set;
    }


    public virtual string Keywords {
      get {
        return EmpiriaString.BuildKeywords(this.Order.OrderNo, this.OrderItem.ProductCode,
                                           this.OrderItem.ProductName, Observations);
      }
    }


    public string OriginalCurrency {
      get {
        return ExtData.Get("originalCurrency", string.Empty);
      }
      private set {
        ExtData.SetIfValue("originalCurrency", value);
      }
    }


    public string BaseCurrency {
      get {
        return ExtData.Get("baseCurrency", string.Empty);
      }
      private set {
        ExtData.SetIfValue("baseCurrency", value);
      }
    }


    public decimal CostOriginalCurrency {
      get {
        return ExtData.Get<decimal>("costOriginalCurrency", 0);
      }
      private set {
        ExtData.SetIfValue("costOriginalCurrency", value);
      }
    }


    public decimal ExchangeRate {
      get {
        return ExtData.Get<decimal>("exchangeRate", 1);
      }
      private set {
        ExtData.SetIfValue("exchangeRate", value);
      }
    }


    public string ItemUnitName {
      get {
        return ExtData.Get("itemUnitName", string.Empty);
      }
      private set {
        ExtData.SetIfValue("itemUnitName", value);
      }
    }


    public decimal ItemUnitQuantity {
      get {
        return ExtData.Get<decimal>("itemUnitQuantity", 0);
      }
      private set {
        ExtData.SetIfValue("itemUnitQuantity", value);
      }
    }

    #endregion Properties


    #region Private methods

    public void AddEntry(InventoryEntryFields fields) {

      this.Product = Patcher.Patch(fields.ProductUID, this.Product);
      this.Location = Patcher.Patch(fields.LocationUID, this.Location);
      this.Position = this.OrderItem.Position;

      this.OriginalCurrency = this.Order.Currency.Code;
      this.BaseCurrency = Currency.Default.Code;
      this.ExchangeRate = this.Order.ExchangeRate;

      this.ItemUnitName = this.OrderItem.ProductUnit.Name;
      this.ItemUnitQuantity = fields.Quantity;

      this.InputQuantity = fields.Quantity * this.OrderItem.PackagingSize;
      this.InputCost = (this.OrderItem.Quantity * this.OrderItem.UnitPrice) /
                       (this.OrderItem.Quantity * this.OrderItem.PackagingSize);

    }


    protected override void OnSave() {

      if (IsNew) {
        this.PostedBy = Parties.Party.ParseWithContact(ExecutionServer.CurrentContact);
        this.PostingTime = DateTime.Now;
        this.EntryTime = DateTime.Now;
      }
      InventoryData.WriteInventoryEntry(this);
    }


    public void OutputEntry(InventoryEntryFields fields) {

      this.OutputQuantity = fields.Quantity;
      this.Product = Patcher.Patch(fields.ProductUID, this.Product);
      this.Location = Patcher.Patch(fields.LocationUID, this.Location);
      this.OutputCost = fields.Cost;
    }


    public void OutputEntry(decimal cost) {

      this.OutputQuantity = this.OrderItem.Quantity;
      this.Product = this.OrderItem.Product;
      this.Location = Location.Empty;
      this.OutputCost = cost;
    }


    public void InitialEntry(decimal countingCost, Location location) {

      this.CountingQuantity = this.OrderItem.Quantity;
      this.Product = this.OrderItem.Product;
      this.Location = location;
      this.CountingCost = countingCost;
      this.Position = this.OrderItem.Position;
    }


    internal void Update(InventoryEntryFields fields) {

      this.InputQuantity = fields.Quantity;
      this.Product = Patcher.Patch(fields.ProductUID, this.Product);
      this.Location = Patcher.Patch(fields.LocationUID, this.Location);
      this.Sku = ProductSku.Empty;
      this.InputCost = 0;
      this.OutputQuantity = 0;
      this.OutputCost = 0;
      this.Tags = string.Empty;
      this.Position = 0;
      this.Status = InventoryStatus.Abierto;
    }


    internal void UpdatePosition(int position) {
      this.Position = position;
    }


    public void UpdateCountingQuantity(decimal quantity) {
      this.CountingQuantity = quantity;
    }

    #endregion Private methods

  } // class InventoryEntry

} // namespace Empiria.Inventory
