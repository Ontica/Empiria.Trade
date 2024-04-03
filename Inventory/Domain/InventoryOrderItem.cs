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
using System.Runtime.Serialization.Formatters;

namespace Empiria.Trade.Inventory {


    /// <summary>Represents an inventory order item entry.</summary>
    internal class InventoryOrderItem {


        internal int InventoryItemId {
            get; set;
        }


        internal string InventoryItemUID {
            get; set;
        }


        internal int InventoryEntryId {
            get; set;
        }


        internal int VendorProductId {
            get; set;
        }


        internal int WarehouseBinId {
            get; set;
        }


    } // class InventoryOrderItem

} // namespace Empiria.Trade.Inventory.Domain
