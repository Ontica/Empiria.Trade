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

namespace Empiria.Trade.Inventory.Domain {


    /// <summary>Represents an inventory order entry.</summary>
    internal class InventoryOrderEntry {


        internal int InventoryEntryId {
            get; set;
        }


        internal string InventoryEntryUID {
            get; set;
        }


        internal string InventoryEntryName {
            get; set;
        }


        internal string InventoryEntryType {
            get; set;
        }


    } // class InventoryOrderEntry

} // namespace Empiria.Trade.Inventory.Domain
