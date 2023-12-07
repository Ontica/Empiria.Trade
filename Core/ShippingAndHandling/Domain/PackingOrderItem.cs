/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Packaging Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Partitioned Type / Information Holder   *
*  Type     : PackingOrderItem                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a Packaging order item.                                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Orders;
using Empiria.Trade.Products;
using Empiria.Trade.ShippingAndHandling.Adapters;
using Empiria.Trade.ShippingAndHandling.Data;

namespace Empiria.Trade.ShippingAndHandling {

  /// <summary>Represents a Packaging order item.</summary>
  public class PackingOrderItem : BaseObject {



    #region Constructor and parsers


    public PackingOrderItem() {
      //no-op
    }

    static public PackingOrderItem Parse(int id) => ParseId<PackingOrderItem>(id);

    static public PackingOrderItem Parse(int id, bool reload) => ParseId<PackingOrderItem>(id, reload);

    static public PackingOrderItem Parse(string uid) => ParseKey<PackingOrderItem>(uid);

    static public PackingOrderItem Empty => ParseEmpty<PackingOrderItem>();


    public PackingOrderItem(string orderUID, string packingItemUID,
                            int inventoryId, MissingItemField missingItemFields) {

      MapToPackagingOrderItem(orderUID, packingItemUID, inventoryId, missingItemFields);

    }

    #endregion Constructor and parsers


    #region Properties


    [DataField("PackingItemId")]
    public int PackingItemId {
      get;
      internal set;
    }


    [DataField("PackingItemUID")]
    public string PackingItemUID {
      get;
      internal set;
    }


    [DataField("OrderPackingId")]
    public int OrderPackingId {
      get;
      internal set;
    }


    [DataField("OrderId")]
    public int OrderId {
      get;
      internal set;
    }


    [DataField("OrderItemId")]
    public int OrderItemId {
      get;
      internal set;
    }


    [DataField("InventoryEntryId")]
    public int InventoryEntryId {
      get;
      internal set;
    }


    [DataField("PackageQuantity")]
    public decimal Quantity {
      get;
      internal set;
    }


    public InventoryEntry InventoryEntry {
      get; internal set;
    }



    #endregion Properties


    #region Private methods

    protected override void OnSave() {

      ShippingAndHandlingData.WritePackingOrderItem(this);

    }


    private void MapToPackagingOrderItem(string orderUID, string packingItemUID,
                  int inventoryId, MissingItemField missingItemFields) {

      this.PackingItemUID = Guid.NewGuid().ToString();
      this.OrderPackingId = PackageForItem.Parse(packingItemUID).Id;
      this.OrderId = Order.Parse(orderUID).Id;
      this.OrderItemId = OrderItem.Parse(missingItemFields.orderItemUID).Id;
      this.InventoryEntryId = inventoryId;
      this.Quantity = missingItemFields.Quantity;
    }


    #endregion




  } // class PackingOrderItem

} // namespace Empiria.Trade.ShippingAndHandling
