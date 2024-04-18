/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Information Holder                      *
*  Type     : Inventory                                  License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents an inventory order entry.                                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Core;
using Empiria.Trade.Inventory.Adapters;
using Empiria.Trade.Inventory.Data;

namespace Empiria.Trade.Inventory {


    /// <summary>Represents an inventory order entry.</summary>
    public class InventoryOrderEntry : BaseObject {

        #region Constructors and parsers

        public InventoryOrderEntry() {
            //no-op
        }


        static public InventoryOrderEntry Parse(int id) => ParseId<InventoryOrderEntry>(id);

        static public InventoryOrderEntry Parse(string uid) => ParseKey<InventoryOrderEntry>(uid);

        static public InventoryOrderEntry Empty => ParseEmpty<InventoryOrderEntry>();


        public InventoryOrderEntry(InventoryOrderFields fields) {

            MapToInventoryOrderEntry(fields);
        }


        #endregion Constructors and parsers

        #region Properties

        [DataField("InventoryOrderId")]
        internal int InventoryOrderId {
            get; set;
        }


        [DataField("InventoryOrderUID")]
        internal string InventoryOrderUID {
            get; set;
        }


        [DataField("InventoryOrderTypeId")]
        internal int InventoryOrderTypeId {
            get; set;
        }


        [DataField("InventoryOrderNo")]
        internal string InventoryOrderNo {
            get; set;
        }


        [DataField("ExternalObjectReferenceId")]
        internal int ExternalObjectReferenceId {
            get; set;
        }


        [DataField("ResponsibleId")]
        internal int ResponsibleId {
            get; set;
        }


        [DataField("AssignedToId")]
        internal int AssignedToId {
            get; set;
        }


        [DataField("InventoryOrderNotes")]
        internal string Notes {
            get; set;
        }


        [DataField("InventoryOrderExtData")]
        internal string InventoryOrderExtData {
            get; set;
        }


        [DataField("InventoryOrderKeywords")]
        internal string InventoryOrderKeywords {
            get; set;
        }


        [DataField("ClosingTime")]
        internal DateTime ClosingTime {
            get; set;
        }


        [DataField("PostingTime")]
        internal DateTime PostingTime {
            get; set;
        }


        [DataField("PostedById")]
        internal int PostedById {
            get; set;
        }


        [DataField("InventoryOrderStatus", Default = InventoryStatus.Abierto)]
        internal InventoryStatus Status {
            get; set;
        } = InventoryStatus.Abierto;


        internal FixedList<InventoryOrderItem> InventoryOrderItems {
            get; set;
        } = new FixedList<InventoryOrderItem>();


        internal string Keywords {
            get {
                return EmpiriaString.BuildKeywords(

                  InventoryOrderUID, InventoryOrderNo, Notes
                );
            }
        }

        #endregion Properties


        #region Private methods

        protected override void OnSave() {

            if (this.InventoryOrderId == 0) {

                this.InventoryOrderId = this.Id;
                this.InventoryOrderUID = this.UID;
            }
            InventoryOrderData.WriteInventoryEntry(this);
        }


        private void MapToInventoryOrderEntry(InventoryOrderFields fields) {

            if (fields.InventoryOrderUID != string.Empty) {
                this.InventoryOrderId = Parse(fields.InventoryOrderUID).InventoryOrderId;
                this.InventoryOrderUID = fields.InventoryOrderUID;
            }

            this.InventoryOrderTypeId = -1; //TODO REGISTRAR TIPOS EN TABLA TYPES
            this.InventoryOrderNo = $"OCI{this.InventoryOrderId.ToString().PadLeft(9,'0')}";
            this.ExternalObjectReferenceId = -1;
            this.ResponsibleId = Party.Parse(fields.ResponsibleUID).Id;
            this.AssignedToId = Party.Parse(fields.AssignedToUID).Id;
            this.Notes = fields.Notes;
            this.InventoryOrderExtData = "";
            
            if (fields.Status == InventoryStatus.Abierto) {
                this.PostedById = Party.Parse(fields.PostedByUID).Id;
                this.PostingTime = DateTime.Now;
                this.ClosingTime = DateTime.Now;
            }
            if (fields.Status == InventoryStatus.Cerrado) {
                this.ClosingTime = DateTime.Now;
            }
            this.Status = fields.Status;
            
        }


        #endregion Private methods


    } // class InventoryOrderEntry

} // namespace Empiria.Trade.Inventory.Domain
