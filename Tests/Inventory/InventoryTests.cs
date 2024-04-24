/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Shipping and handling Management           Component : Test cases                              *
*  Assembly : Empiria.Trade.Shipping.dll                 Pattern   : Use cases tests                         *
*  Type     : ShippingTest                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Test cases for shipping.                                                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Sales.ShippingAndHandling.UseCases;
using Empiria.Trade.Sales.ShippingAndHandling;
using Xunit;
using Empiria.Trade.Inventory.UseCases;
using Empiria.Trade.Inventory;
using Empiria.Trade.Inventory.Adapters;
using System.Collections.Generic;

namespace Empiria.Trade.Tests.Inventory {


    public class InventoryTests {

        #region Initialization

        public InventoryTests() {
            //TestsCommonMethods.Authenticate();
        }

        #endregion Initialization

        #region Facts

        [Fact]
        public void CreateInventoryCountOrderTest() {

            var usecase = InventoryOrderUseCases.UseCaseInteractor();

            InventoryOrderFields fields = GetInventoryOrderFields();
            InventoryOrderDto sut = usecase.CreateInventoryCountOrder(fields);

            Assert.NotNull(sut);
        }


        [Fact]
        public void DeleteInventoryOrderTest() {

            var usecase = InventoryOrderUseCases.UseCaseInteractor();
            string inventoryOrderUID = "f2d2a10d-abb2-467d-9c4f-bdd2bc15d5c6";

            usecase.DeleteInventoryCountOrderByUID(inventoryOrderUID);
            Assert.True(true);
        }


        [Fact]
        public void DeleteInventoryItemByOrderUIDTest() {

            var usecase = InventoryOrderUseCases.UseCaseInteractor();

            string inventoryOrderUID = "f2d2a10d-abb2-467d-9c4f-bdd2bc15d5c6";
            InventoryOrderDto sut = usecase.DeleteInventoryItemByOrderUID(inventoryOrderUID);
            Assert.NotNull(sut);
        }


        [Fact]
        public void DeleteInventoryItemByUIDTest() {

            var usecase = InventoryOrderUseCases.UseCaseInteractor();
            string inventoryOrderUID = "f2d2a10d-abb2-467d-9c4f-bdd2bc15d5c6";
            string inventoryItemUID = "b9be96b6-b404-4acc-889e-390199a7af32";

            InventoryOrderDto sut = usecase.DeleteInventoryItemByUID(inventoryOrderUID, inventoryItemUID);
            Assert.NotNull(sut);
        }


        [Fact]
        public void GetInventoryOrderListTest() {

            var usecase = InventoryOrderUseCases.UseCaseInteractor();

            InventoryOrderQuery query = new InventoryOrderQuery {
              InventoryOrderTypeUID = "",
              AssignedToUID = "a517e788-8ddf-4772-b6d2-adc3907e3905",
              Status = InventoryStatus.Abierto
            };

            FixedList<InventoryOrderDto> sut = usecase.GetInventoryCountOrderList(query);
            Assert.NotNull(sut);
        }


        [Fact]
        public void GetInventoryOrderByParseTest() {

            var usecase = InventoryOrderUseCases.UseCaseInteractor();
            string inventoryOrderUID = "f2d2a10d-abb2-467d-9c4f-bdd2bc15d5c6";

            InventoryOrderEntry sut = usecase.GetInventoryOrderParseUID(inventoryOrderUID);
            Assert.NotNull(sut);
        }


        [Fact]
        public void GetInventoryOrderItemByParseTest() {

            var usecase = InventoryOrderUseCases.UseCaseInteractor();
            string itemUID = "0071e71b-3a1f-40ab-836f-ac3d2c940290";

            InventoryOrderItem sut = usecase.GetInventoryOrderItemParseUID(itemUID);
            Assert.NotNull(sut);
        }


        [Fact]
        public void UpdateInventoryCountOrderTest() {

            var usecase = InventoryOrderUseCases.UseCaseInteractor();
            string inventoryOrderUID = "2754c88e-ac72-4910-ae3f-e199b1b0391e";
            InventoryOrderFields fields = GetInventoryOrderFields();
            InventoryOrderDto sut = usecase.UpdateInventoryCountOrder(inventoryOrderUID, fields);

            Assert.NotNull(sut);
        }


        #endregion Facts

        #region Private methods


        private InventoryOrderFields GetInventoryOrderFields() {

            var fields = new InventoryOrderFields() {
                InventoryOrderTypeUID = "",
                ExternalObjectReferenceUID = "",
                ResponsibleUID = "c930a33a-e93b-43c9-9379-96bcb86c4e4d",
                AssignedToUID = "a517e788-8ddf-4772-b6d2-adc3907e3905",
                Notes = "CONTEO DE INVENTARIO X001 ACTUALIZADO",
                PostedByUID= "ccdd87c5-52f0-4074-8448-5233cc1a4a77",
                Status = InventoryStatus.Abierto,
                InventoryItemFields = GetItemFields()
            };
            return fields;
        }


        private FixedList<InventoryOrderItemFields> GetItemFields() {


            var items = new List<InventoryOrderItemFields>();

            var item1 = new InventoryOrderItemFields() {
                InventoryOrderItemUID = "0071e71b-3a1f-40ab-836f-ac3d2c940290",
                ExternalObjectItemReferenceUID ="",
                ItemNotes ="NOTAS DE ITEM 1 ACTUALIZADO",
                VendorProductUID = "e0655909-8614-40c0-b63e-fe166a377c86",
                WarehouseBinUID = "f06a2b16-e744-412e-bd94-82821a7b5cd9",
                Quantity = 10,
                InputQuantity = 9,
                OutputQuantity = 8,
                ClosingTime = DateTime.Now,
                PostingTime = DateTime.Now,
                PostedByUID= "a517e788-8ddf-4772-b6d2-adc3907e3905",
                ItemStatus = InventoryStatus.Abierto,
                //  Comments = "COMENTARIO X-001",
            };
            items.Add(item1);
            var item2 = new InventoryOrderItemFields() {
                InventoryOrderItemUID = "ff8e950e-94e9-4ae5-943a-49abad5140cc",
                ExternalObjectItemReferenceUID = "",
                ItemNotes = "NOTAS DE ITEM 2 ACTUALIZADO",
                VendorProductUID = "1d47e4e5-ff97-4197-8bd1-b49df2780c32",
                WarehouseBinUID = "48605b90-52e1-43d0-aeab-7125805863aa",
                Quantity = 20,
                InputQuantity = 9,
                OutputQuantity = 8,
                ClosingTime = DateTime.Now,
                PostingTime = DateTime.Now,
                PostedByUID = "a517e788-8ddf-4772-b6d2-adc3907e3905",
                ItemStatus = InventoryStatus.Abierto,
            };
            items.Add(item2);
            var item3 = new InventoryOrderItemFields() {
                InventoryOrderItemUID = "a54ed868-a7ec-47f5-b1b9-8c0f73b04f3b",
                ExternalObjectItemReferenceUID = "",
                ItemNotes = "NOTAS DE ITEM 3 ACTUALIZADO",
                VendorProductUID = "1d47e4e5-ff97-4197-8bd1-b49df2780c32",
                WarehouseBinUID = "48605b90-52e1-43d0-aeab-7125805863aa",
                Quantity = 30,
                InputQuantity = 9,
                OutputQuantity = 8,
                ClosingTime = DateTime.Now,
                PostingTime = DateTime.Now,
                PostedByUID = "a517e788-8ddf-4772-b6d2-adc3907e3905",
                ItemStatus = InventoryStatus.Abierto,
            };
            items.Add(item3);
            return items.ToFixedList();
        }

        #endregion Private methods
    }
}
