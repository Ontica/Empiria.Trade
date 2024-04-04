/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Shipping Management                        Component : Interface adapters                      *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Data Transfer Object                    *
*  Type     : InventoryOrderFields                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : DTO used to manage inventory order fields.                                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Inventory.Adapters {

    /// <summary>DTO used to manage inventory order fields.</summary>
    public class InventoryOrderFields {

        public string InventoryEntryUID {
            get; set;
        }


        public string InventoryTypeUID {
            get; set;
        }


        public string InventoryUserUID {
            get; set;
        }


        public string InventoryEntryName {
            get; set;
        }


        public FixedList<InventoryOrderItemFields> InventoryItemFields {
            get; set;
        } = new FixedList<InventoryOrderItemFields>();


    } // class InventoryOrderFields


    public class InventoryOrderItemFields {


        public string InventoryItemUID {
            get; set;
        }


        public string InventoryEntryUID {
            get; set;
        }


        public int VendorProductUID {
            get; set;
        }


        public int WarehouseBinUID {
            get; set;
        }


        public int Quantity {
            get; set;
        }


        public string Comments {
            get; set;
        }


    } // class InventoryOrderItemFields

} // namespace Empiria.Trade.Inventory.Adapters
