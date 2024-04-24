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


        public string UID {
            get; set;
        }


        public NamedEntityDto InventoryOrderType {
            get; set;
        } = new NamedEntityDto("", "");


        public string InventoryOrderNo {
            get;
            internal set;
        }


        public NamedEntityDto ExternalObjectReference {
            get;
            internal set;
        } = new NamedEntityDto("", "");


        public NamedEntityDto Responsible {
            get;
            internal set;
        } = new NamedEntityDto("", "");


        public NamedEntityDto AssignedTo {
            get;
            internal set;
        } = new NamedEntityDto("", "");


        public string Notes {
            get; set;
        }


        public DateTime ClosingTime {
            get; set;
        }


        public DateTime PostingTime {
            get; set;
        }


        public NamedEntityDto PostedBy {
            get;
            internal set;
        } = new NamedEntityDto("", "");


        public InventoryStatus InventoryStatus {
            get; set;
        }


        public FixedList<InventoryOrderItemDto> InventoryItems {
            get; set;
        } = new FixedList<InventoryOrderItemDto>();

    } // class InventoryOrderDto


    public class InventoryOrderItemDto {


        public string InventoryOrderItemUID {
            get; set;
        }


        public string UID {
            get; set;
        }


        public string ExternalObjectItemReferenceUID {
            get; set;
        }


        public string ItemNotes {
            get; set;
        }


        public string VendorProductUID {
            get; set;
        }


        public string WarehouseBinUID {
            get; set;
        }


        public decimal Quantity {
            get; set;
        }


        public decimal InputQuantity {
            get; set;
        }


        public decimal OutputQuantity {
            get; set;
        }


        public InventoryStatus ItemStatus {
            get; set;
        }


    }


} // namespace Empiria.Trade.Inventory.Adapters
