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


        public InventoryOrderItem(InventoryOrderEntry inventoryOrder, InventoryOrderItemFields item) {

            MapToInventoryOrderItem(inventoryOrder, item);
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


        [DataField("ExternalObjectItemReferenceId")]
        internal int ExternalObjectItemReferenceId {
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


        [DataField("Quantity")]
        public decimal Quantity {
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


        #endregion Properties


        #region Private methods

        protected override void OnSave() {

            if (this.InventoryOrderItemId == 0) {

                this.InventoryOrderItemId = this.Id;
                this.InventoryOrderItemUID = this.UID;
            }
            InventoryOrderData.WriteInventoryItem(this);
        }


        private void MapToInventoryOrderItem(InventoryOrderEntry inventoryOrder,
                                             InventoryOrderItemFields fields) {

            if (fields.InventoryOrderItemUID != string.Empty) {
                this.InventoryOrderItemId = Parse(fields.InventoryOrderItemUID).InventoryOrderItemId;
                this.InventoryOrderItemUID = inventoryOrder.InventoryOrderUID;
            }

            this.InventoryOrder = inventoryOrder;
            this.ExternalObjectItemReferenceId = -1; //External.Parse(fields.ExternalObjectItemReferenceUID).Id;
            this.ItemNotes = fields.ItemNotes;
            this.VendorProduct = VendorProduct.Parse(fields.VendorProductUID);
            this.WarehouseBin = WarehouseBin.Parse(fields.WarehouseBinUID);
            this.Quantity = fields.Quantity;
            this.InputQuantity = fields.InputQuantity;
            this.OutputQuantity = fields.OutputQuantity;
            this.ExtData = "";
            this.Status = fields.ItemStatus;

            if (fields.ItemStatus == InventoryStatus.Abierto) {
                this.ClosingTime = fields.ClosingTime;
                this.PostingTime = fields.PostingTime;
                this.PostedById = Party.Parse(fields.PostedByUID).Id;
            }
            if (fields.ItemStatus == InventoryStatus.Cerrado) {
                this.ClosingTime = fields.ClosingTime;
            }
        }


        #endregion Private methods

    } // class InventoryOrderItem

} // namespace Empiria.Trade.Inventory.Domain
