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
using Empiria.Trade.Inventory.Data;
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


        public InventoryOrderDto CreateInventoryCountOrder(InventoryOrderFields fields) {

            var builder = new InventoryOrderBuilder();
            var inventoryOrder = builder.CreateInventoryOrder(fields, "");
            return GetInventoryCountOrderByUID(inventoryOrder.InventoryOrderUID);
        }


        public void DeleteInventoryCountOrderByUID(string inventoryOrderUID) {

            InventoryOrderData.DeleteInventoryItemByOrderUID(inventoryOrderUID);
            InventoryOrderData.DeleteInventoryOrderByUID(inventoryOrderUID);
        }


        public InventoryOrderDto DeleteInventoryItemByOrderUID(string inventoryOrderUID) {

            InventoryOrderData.DeleteInventoryItemByOrderUID(inventoryOrderUID);
            return GetInventoryCountOrderByUID(inventoryOrderUID);
        }


        public InventoryOrderDto DeleteInventoryItemByUID(string inventoryOrderUID, string inventoryOrderItemUID) {

            InventoryOrderData.DeleteInventoryItemByUID(inventoryOrderItemUID);
            return GetInventoryCountOrderByUID(inventoryOrderUID);
        }


        public InventoryOrderDto GetInventoryCountOrderByUID(string inventoryOrderUID) {

            var builder = new InventoryOrderBuilder();
            var inventoryOrder = builder.GetInventoryOrderByUID(inventoryOrderUID);

            return InventoryOrderMapper.MapInventoryOrder(inventoryOrder);
        }


        public InventoryOrderDataDto GetInventoryCountOrderList(InventoryOrderQuery query) {

            var list = InventoryOrderData.GetInventoryOrderList(query);
            return InventoryOrderMapper.MapList(list, query);
        }


        public InventoryOrderEntry GetInventoryOrderParseUID(string inventoryOrderUID) {

            return InventoryOrderEntry.Parse(inventoryOrderUID);
        }


        public InventoryOrderItem GetInventoryOrderItemParseUID(string itemUID) {

            return InventoryOrderItem.Parse(itemUID);
        }


        public InventoryOrderDto UpdateInventoryCountOrder(string inventoryOrderUID, InventoryOrderFields fields) {
            var builder = new InventoryOrderBuilder();
            var inventoryOrder = builder.UpdateInventoryCountOrder(inventoryOrderUID, fields);

            return GetInventoryCountOrderByUID(inventoryOrder.InventoryOrderUID);
        }


        #endregion Public methods

    } // class InventoryOrderUseCases

} // namespace Empiria.Trade.Inventory.UseCases
