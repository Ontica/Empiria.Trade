/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Data Transfer Object                    *
*  Type     : InventoryOrderDto                          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return inventory data.                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Inventory.Adapters {

    /// <summary>Output DTO used to return inventory data.</summary>
    public class InventoryOrderDto {

        public string InventoryEntryUID {
            get; set;
        }


        public string InventoryEntryName {
            get; set;
        }


        public string InventoryEntryType {
            get; set;
        }

    } // class InventoryOrderDto

} // namespace Empiria.Trade.Inventory.Adapters
