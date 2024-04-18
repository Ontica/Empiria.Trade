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


        public string InventoryOrderUID {
            get; set;
        }


        public string InventoryEntryName {
            get; set;
        }


        public NamedEntityDto InventoryOrderType {
            get; set;
        } = new NamedEntityDto("", "");


        public NamedEntityDto InventoryUser {
            get; set;
        } = new NamedEntityDto("", "");


        public FixedList<InventoryOrderItemDto> InventoryItems {
            get; set;
        } = new FixedList<InventoryOrderItemDto>();


    } // class InventoryOrderDto


    public class InventoryOrderItemDto {


        public string InventoryItemUID {
            get; set;
        }


        public string InventoryEntryUID {
            get; set;
        }


        public string VendorProductUID {
            get; set;
        }


        public string WarehouseBinUID {
            get; set;
        }


        public int Quantity {
            get; set;
        }


        public string Comments {
            get; set;
        }
    }


} // namespace Empiria.Trade.Inventory.Adapters
