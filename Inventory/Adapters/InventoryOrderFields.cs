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
        } = string.Empty;


        public string InventoryTypeUID {
            get; set;
        } = string.Empty;


        public string InventoryUserUID {
            get; set;
        } = string.Empty;


        public string InventoryEntryName {
            get; set;
        } = string.Empty;


        public FixedList<InventoryOrderItemFields> InventoryItemFields {
            get; set;
        } = new FixedList<InventoryOrderItemFields>();


    } // class InventoryOrderFields


    public class InventoryOrderItemFields {


        public string InventoryItemUID {
            get; set;
        } = string.Empty;


        public string InventoryEntryUID {
            get; set;
        } = string.Empty;


        public string VendorProductUID {
            get; set;
        } = string.Empty;


        public string WarehouseBinUID {
            get; set;
        } = string.Empty;


        public int Quantity {
            get; set;
        }


        public string Comments {
            get; set;
        } = string.Empty;


    } // class InventoryOrderItemFields

} // namespace Empiria.Trade.Inventory.Adapters
