/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Interface adapters                      *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Mapper class                            *
*  Type     : InventoryOrderMapper                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Methods used to map Inventory order.                                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Inventory.Domain;

namespace Empiria.Trade.Inventory.Adapters {


    /// <summary>Methods used to map Inventory order.</summary>
    internal class InventoryOrderMapper {
        internal static FixedList<InventoryOrderDto> MapInventoryList(FixedList<InventoryOrderEntry> list) {

            return new FixedList<InventoryOrderDto>();
        }


        internal static InventoryOrderDto MapInventoryOrder(InventoryOrderEntry inventoryOrder) {
           
            return new InventoryOrderDto();
        }
    } // class InventoryOrderMapper

} // namespace Empiria.Trade.Inventory.Adapters
