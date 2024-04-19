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
      InventoryOrderDto sut = usecase.CreateInventoryOrder(fields);

      Assert.NotNull(sut);
    }


    [Fact]
    public void DeleteInventoryOrderTest() {

      var usecase = InventoryOrderUseCases.UseCaseInteractor();
      string inventoryUID = "f2d2a10d-abb2-467d-9c4f-bdd2bc15d5c6";

      usecase.DeleteInventoryOrderByUID(inventoryUID);
      Assert.True(true);
    }


    [Fact]
    public void DeleteInventoryItemByOrderUIDTest() {

      var usecase = InventoryOrderUseCases.UseCaseInteractor();

      string inventoryUID = "f2d2a10d-abb2-467d-9c4f-bdd2bc15d5c6";
      InventoryOrderDto sut = usecase.DeleteInventoryItemByOrderUID(inventoryUID);
      Assert.NotNull(sut);
    }


    [Fact]
    public void DeleteInventoryItemByUIDTest() {

      var usecase = InventoryOrderUseCases.UseCaseInteractor();
      string inventoryUID = "f2d2a10d-abb2-467d-9c4f-bdd2bc15d5c6";
      string inventoryItemUID = "b9be96b6-b404-4acc-889e-390199a7af32";

      InventoryOrderDto sut = usecase.DeleteInventoryItemByUID(inventoryUID, inventoryItemUID);
      Assert.NotNull(sut);
    }


    [Fact]
    public void GetInventoryOrderListTest() {

      var usecase = InventoryOrderUseCases.UseCaseInteractor();

      FixedList<InventoryOrderDto> sut = usecase.GetInventoryOrderList();
      Assert.NotNull(sut);
    }


    [Fact]
    public void GetInventoryOrderByParseTest() {

      var usecase = InventoryOrderUseCases.UseCaseInteractor();
      string inventoryUID = "f2d2a10d-abb2-467d-9c4f-bdd2bc15d5c6";

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

    #region Private methods


    private InventoryOrderFields GetInventoryOrderFields() {

      var fields = new InventoryOrderFields() {
        InventoryOrderUID = "f2d2a10d-abb2-467d-9c4f-bdd2bc15d5c6",
        ResponsibleUID = "a517e788-8ddf-4772-b6d2-adc3907e3905",
        InventoryOrderTypeUID = "",
        Notes = "CONTEO DE INVENTARIO X001",
        InventoryItemFields = GetItemFields()
      };
      return fields;
    }


    private FixedList<InventoryOrderItemFields> GetItemFields() {


      var items = new List<InventoryOrderItemFields>();

      var item1 = new InventoryOrderItemFields() {
      //  InventoryItemUID = "f79c353f-b021-4e73-83ea-fcff7bf5da87",
      //  InventoryEntryUID = "f2d2a10d-abb2-467d-9c4f-bdd2bc15d5c6",
        WarehouseBinUID = "f06a2b16-e744-412e-bd94-82821a7b5cd9",
        VendorProductUID = "e0655909-8614-40c0-b63e-fe166a377c86",
        Quantity = 10,
      //  Comments = "COMENTARIO X-001",
      };
      items.Add(item1);
      var item2 = new InventoryOrderItemFields() {
        //InventoryItemUID = "0fbcc7e9-9989-4ec3-813a-38acf2b6d4cd",
        //InventoryEntryUID = "f2d2a10d-abb2-467d-9c4f-bdd2bc15d5c6",
        WarehouseBinUID = "48605b90-52e1-43d0-aeab-7125805863aa",
        VendorProductUID = "1d47e4e5-ff97-4197-8bd1-b49df2780c32",
        Quantity = 20,
        //Comments = "COMENTARIO X-002",
      };
      items.Add(item2);
      var item3 = new InventoryOrderItemFields() {
        //InventoryItemUID = "",
        //InventoryEntryUID = "f2d2a10d-abb2-467d-9c4f-bdd2bc15d5c6",
        WarehouseBinUID = "48605b90-52e1-43d0-aeab-7125805863aa",
        VendorProductUID = "1d47e4e5-ff97-4197-8bd1-b49df2780c32",
        Quantity = 30,
        //Comments = "COMENTARIO X-003",
      };
      items.Add(item3);
      return items.ToFixedList();
    }

    #endregion Private methods
  }
}
