/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Use cases Layer                         *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Use case interactor class               *
*  Type     : InventoryOrderUseCases                     License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases used to build Inventory order.                                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Services;
using Empiria.Trade.Inventory.Adapters;
using Empiria.Trade.Inventory.Domain;

namespace Empiria.Trade.Inventory.UseCases {

    /// <summary>Use cases used to build Inventory order.</summary>
    public class InventoryOrderUseCases : UseCase {


        #region Constructors and parsers

        public InventoryOrderUseCases() {
            // no-op
        }

        static public InventoryOrderUseCases UseCaseInteractor() {
            return CreateInstance<InventoryOrderUseCases>();
        }


        #endregion Constructors and parsers

        #region Public methods

        public InventoryOrderDto CreateInventoryOrder(InventoryOrderFields fields) {

            var builder = new InventoryOrderBuilder();
            var inventoryOrder = builder.CreateInventoryOrder(fields);

            return InventoryOrderMapper.MapInventoryOrder(inventoryOrder);
        }


        public FixedList<InventoryOrderDto> GetInventoryOrderList() {

            var builder = new InventoryOrderBuilder();
            var list = builder.GetInventoryOrderList();

            return InventoryOrderMapper.MapInventoryList(list);
        }


        public InventoryOrderDto GetInventoryOrderByUID(string inventoryUID) {

            var builder = new InventoryOrderBuilder();
            var inventoryOrder = builder.GetInventoryOrderByUID(inventoryUID);

            return InventoryOrderMapper.MapInventoryOrder(inventoryOrder);
        }


        public InventoryOrderEntry GetInventoryOrderParseUID(string inventoryUID) {
            
            return InventoryOrderEntry.Parse(inventoryUID);
        }

        public InventoryOrderItem GetInventoryOrderItemParseUID(string itemUID) {

            return InventoryOrderItem.Parse(itemUID);
        }



        #endregion Public methods

    } // class InventoryOrderUseCases

} // namespace Empiria.Trade.Inventory.UseCases
