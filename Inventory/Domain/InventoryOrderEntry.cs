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


        public InventoryOrderEntry(string uid) {

            MapToInventoryOrderEntry();

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


        [DataField("InventoryUserId")]
        internal int InventoryUserId {
            get; set;
        }


        [DataField("InventoryEntryName")]
        internal string InventoryEntryName {
            get; set;
        }


        [DataField("InventoryEntryKeywords")]
        internal string Keywords {
            get; set;
        }


        [DataField("InventoryEntryDate")]
        internal DateTime InventoryEntryDate {
            get; set;
        }


        #endregion Properties


        #region Private methods

        private void MapToInventoryOrderEntry() {
            throw new NotImplementedException();
        }

        #endregion Private methods


    } // class InventoryOrderEntry

} // namespace Empiria.Trade.Inventory.Domain
