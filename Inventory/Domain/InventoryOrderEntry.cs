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

        [DataField("InventoryEntryId")]
        internal int InventoryEntryId {
            get; set;
        }


        [DataField("InventoryEntryUID")]
        internal string InventoryEntryUID {
            get; set;
        }


        [DataField("InventoryTypeId")]
        internal int InventoryType {
            get; set;
        }


        [DataField("InventoryAgentId")]
        internal int InventoryAgentId {
            get; set;
        }


        [DataField("InventoryEntryName")]
        internal string InventoryEntryName {
            get; set;
        }


        [DataField("InventoryEntryDate")]
        internal DateTime InventoryEntryDate {
            get; set;
        }


        internal FixedList<InventoryOrderItem> InventoryOrderItems {
            get; set;
        } = new FixedList<InventoryOrderItem>();


        internal string Keywords {
            get {
                return EmpiriaString.BuildKeywords(

                  InventoryEntryUID, InventoryEntryName
                );
            }
        }

        #endregion Properties


        #region Private methods

        protected override void OnSave() {

            if (this.InventoryEntryId == 0) {

                this.InventoryEntryId = this.Id;
                this.InventoryEntryUID = this.UID;
            }
            InventoryOrderData.WriteInventoryEntry(this);
        }


        private void MapToInventoryOrderEntry(InventoryOrderFields fields) {

            if (fields.InventoryEntryUID != string.Empty) {
                this.InventoryEntryId = Parse(fields.InventoryEntryUID).InventoryEntryId;
                this.InventoryEntryUID = fields.InventoryEntryUID;
            }

            this.InventoryAgentId = Party.Parse(fields.InventoryUserUID).Id;
            this.InventoryType = -1; //TODO REGISTRAR TIPOS DE ORDEN DE INVENTARIOS
            this.InventoryEntryName = fields.InventoryEntryName;
            this.InventoryEntryDate = DateTime.Now;
        }


        #endregion Private methods


    } // class InventoryOrderEntry

} // namespace Empiria.Trade.Inventory.Domain
