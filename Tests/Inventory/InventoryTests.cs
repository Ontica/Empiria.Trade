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

namespace Empiria.Trade.Tests.Inventory {


    public class InventoryTests {

        #region Initialization

        public InventoryTests() {
            //TestsCommonMethods.Authenticate();
        }

        #endregion Initialization

        #region Facts

        [Fact]
        public void GetInventoryOrderByParseTest() {

            var usecase = InventoryOrderUseCases.UseCaseInteractor();

            string inventoryUID = "gkr68735-af5c-4e49-85dc-4a177c82b852";

            InventoryOrderEntry sut = usecase.GetInventoryOrderParseUID(inventoryUID);

            Assert.NotNull(sut);

        }


        [Fact]
        public void GetInventoryOrderItemByParseTest() {

            var usecase = InventoryOrderUseCases.UseCaseInteractor();

            string itemUID = "kt5f6a19-9c40-454a-b02f-985bd8c518fk";

            InventoryOrderItem sut = usecase.GetInventoryOrderItemParseUID(itemUID);

            Assert.NotNull(sut);

        }

        #endregion Facts
    }
}
