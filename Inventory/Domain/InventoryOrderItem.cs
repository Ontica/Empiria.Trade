/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Information Holder                      *
*  Type     : InventoryOrderItem                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents an inventory order item entry.                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Core;
using Empiria.Trade.Core.Catalogues;
using Empiria.Trade.Inventory.Adapters;
using Empiria.Trade.Inventory.Data;
using Empiria.Trade.Products;

namespace Empiria.Trade.Inventory {


  /// <summary>Represents an inventory order item entry.</summary>
  public class InventoryOrderItem : BaseObject {

    #region Constructors and parsers

    public InventoryOrderItem() {
      //no-op
    }


    static public InventoryOrderItem Parse(int id) => ParseId<InventoryOrderItem>(id);

    static public InventoryOrderItem Parse(string uid) => ParseKey<InventoryOrderItem>(uid);

    static public InventoryOrderItem Empty => ParseEmpty<InventoryOrderItem>();


    public InventoryOrderItem(string inventoryOrderUID, InventoryOrderItemFields item) {

      MapToInventoryOrderItem(inventoryOrderUID, item);
    }

    #endregion Constructors and parsers

    #region Properties


    [DataField("InventoryOrderItemId")]
    internal int InventoryOrderItemId {
      get; set;
    }


    [DataField("InventoryOrderItemUID")]
    internal string InventoryOrderItemUID {
      get; set;
    }


    [DataField("InventoryOrderId")]
    internal InventoryOrderEntry InventoryOrder {
      get; set;
    }


    [DataField("InventoryOrderTypeItemId")]
    internal int InventoryOrderTypeItemId {
      get; set;
    }


    [DataField("ItemReferenceId")]
    internal int ItemReferenceId {
      get; set;
    }


    [DataField("InventoryOrderItemNotes")]
    public string ItemNotes {
      get; set;
    } = string.Empty;


    [DataField("VendorProductId")]
    internal VendorProduct VendorProduct {
      get; set;
    }


    [DataField("WarehouseBinId")]
    internal WarehouseBin WarehouseBin {
      get; set;
    }


    [DataField("CountingQuantity")]
    public decimal CountingQuantity {
      get; set;
    }


    [DataField("InProcessInputQuantity")]
    public decimal InProcessInputQuantity {
      get; set;
    }


    [DataField("InProcessOutputQuantity")]
    public decimal InProcessOutputQuantity {
      get; set;
    }


    [DataField("InputQuantity")]
    public decimal InputQuantity {
      get; set;
    }


    [DataField("OutputQuantity")]
    public decimal OutputQuantity {
      get; set;
    }


    [DataField("UnitId")]
    internal int UnitId {
      get; set;
    }


    [DataField("InputCost")]
    public decimal InputCost {
      get; set;
    }


    [DataField("OutputCost")]
    public decimal OutputCost {
      get; set;
    }


    [DataField("CurrencyId")]
    internal int CurrencyId {
      get; set;
    }


    [DataField("InventoryOrderItemExtData")]
    public string ExtData {
      get; set;
    } = string.Empty;


    [DataField("ClosingTime")]
    public DateTime ClosingTime {
      get; set;
    }


    [DataField("PostingTime")]
    public DateTime PostingTime {
      get; set;
    }


    [DataField("PostedById")]
    internal int PostedById {
      get; set;
    }


    [DataField("InventoryOrderItemStatus", Default = InventoryStatus.Abierto)]
    internal InventoryStatus Status {
      get; set;
    } = InventoryStatus.Abierto;


    [DataField("RackPosition")]
    public int Position {
      get; set;
    }


    [DataField("RackLevel")]
    public int Level {
      get; set;
    }


    #endregion Properties


    #region Private methods

    protected override void OnSave() {

      if (this.InventoryOrderItemId == 0) {

        this.InventoryOrderItemId = this.Id;
        this.InventoryOrderItemUID = this.UID;
      }
      InventoryOrderData.WriteInventoryItem(this);
    }


    private void MapToInventoryOrderItem(string inventoryOrderUID,
                                         InventoryOrderItemFields fields) {

      if (fields.UID != string.Empty) {
        this.InventoryOrderItemId = Parse(fields.UID).InventoryOrderItemId;
        this.InventoryOrderItemUID = inventoryOrderUID;
      }

      this.InventoryOrder = InventoryOrderEntry.Parse(inventoryOrderUID);
      this.InventoryOrderTypeItemId = 5; // TODO AGREGAR REFERENCIA (5 SALIDA POR VENTA)
      this.ItemReferenceId = fields.ItemReferenceId; //External.Parse(fields.ItemReferenceUID).Id;
      this.ItemNotes = fields.Notes;
      this.VendorProduct = VendorProduct.Parse(fields.VendorProductUID);
      this.WarehouseBin = WarehouseBin.Parse(fields.WarehouseBinUID);
      this.CountingQuantity = fields.Quantity;
      this.InProcessInputQuantity = fields.InProcessInputQuantity; // TODO AGREGAR REFERENCIA
      this.InProcessOutputQuantity = fields.InProcessOutputQuantity; // REFERENCIA EN ORDEN DE SALIDA POR PEDIDO
      this.InputQuantity = fields.InputQuantity;
      this.OutputQuantity = fields.OutputQuantity;
      this.UnitId = fields.UnitId;
      this.InputCost = fields.InputCost;
      this.OutputCost = fields.OutputCost; // TODO PREGUNTAR SI ES PRECIO DE VENTA EN PEDIDO
      this.CurrencyId = fields.CurrencyId;
      this.Position = fields.Position; // TODO CAMBIAR, POR EL MOMENTO DE ORDEN POR DEFAULT=1
      this.Level = fields.Level; // TODO CAMBIAR, POR EL MOMENTO DE ORDEN POR DEFAULT=1
      this.ExtData = "";
      this.ClosingTime = new DateTime(2049, 01, 01); //TODO CAMBIA CUANDO SE CIERRA LA ORDEN
      this.PostingTime = DateTime.Now;
      this.PostedById = ExecutionServer.CurrentUserId;
      this.Status = InventoryStatus.Abierto;

    }


    #endregion Private methods

  } // class InventoryOrderItem

} // namespace Empiria.Trade.Inventory.Domain
