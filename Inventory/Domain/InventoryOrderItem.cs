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


        [DataField("InventoryItemId")]
        internal int InventoryItemId {
            get; set;
        }


        [DataField("InventoryItemUID")]
        internal string InventoryItemUID {
            get; set;
        }


        [DataField("InventoryEntryId")]
        internal InventoryOrderEntry InventoryEntry {
            get; set;
        }


        [DataField("VendorProductId")]
        internal VendorProduct VendorProduct {
            get; set;
        }


        [DataField("WarehouseBinId")]
        internal WarehouseBin WarehouseBin {
            get; set;
        }
        
        
        public int Quantity {
            get; set;
        }


        public string Comments {
            get; set;
        }


        #endregion Properties


        #region Private methods

        protected override void OnSave() {

            if (this.InventoryItemId == 0) {

                this.InventoryItemId = this.Id;
                this.InventoryItemUID = this.UID;
            }
            InventoryOrderData.WriteInventoryItem(this);
        }


        private void MapToInventoryOrderItem(InventoryOrderEntry inventoryOrder,
                                             InventoryOrderItemFields fields) {

            if (fields.InventoryItemUID != string.Empty) {
                this.InventoryItemId = Parse(fields.InventoryItemUID).InventoryItemId;
                this.InventoryItemUID = fields.InventoryEntryUID;
            }

            this.InventoryEntry = inventoryOrder;
            this.WarehouseBin = WarehouseBin.Parse(fields.WarehouseBinUID);
            this.VendorProduct = VendorProduct.Parse(fields.VendorProductUID);
            this.Quantity = fields.Quantity;
            this.Comments = fields.Comments;
        }


        #endregion Private methods

    } // class InventoryOrderItem

} // namespace Empiria.Trade.Inventory.Domain
