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
using Empiria.Trade.Core.UsesCases;

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
        InventoryOrderTypeUID = "ab8e950e-94e9-4ae5-943a-49abad514g52",
        AssignedToUID = "",
        Keywords = "",
        Status = InventoryStatus.Todos
      };

      InventoryOrderDataDto sut = usecase.GetInventoryCountOrderList(query);
      Assert.NotNull(sut);
    }


    [Fact]
    public void GetInventoryOrderByUID() {

      var usecase = InventoryOrderUseCases.UseCaseInteractor();
      string inventoryOrderUID = "6f111e9d-91cf-4bbb-a6df-2b70ec2063b2";

      InventoryOrderDto sut = usecase.GetInventoryCountOrderByUID(inventoryOrderUID);
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
      string inventoryOrderUID = "6f111e9d-91cf-4bbb-a6df-2b70ec2063b2";
      InventoryOrderFields fields = GetInventoryOrderFields();
      InventoryOrderDto sut = usecase.UpdateInventoryCountOrder(inventoryOrderUID, fields);

      Assert.NotNull(sut);
    }


    #endregion Facts

    #region Private methods


    private InventoryOrderFields GetInventoryOrderFields() {

      var fields = new InventoryOrderFields() {
        InventoryOrderTypeUID = "ab8e950e-94e9-4ae5-943a-49abad514g52",
        //ExternalObjectReferenceUID = "",
        ResponsibleUID = "c930a33a-e93b-43c9-9379-96bcb86c4e4d",
        AssignedToUID = "a517e788-8ddf-4772-b6d2-adc3907e3905",
        Notes = "CONTEO X002",
        PostedByUID = "ccdd87c5-52f0-4074-8448-5233cc1a4a77",
        Status = InventoryStatus.Abierto,
        ItemFields = GetItemFields()
      };
      return fields;
    }


    private FixedList<InventoryOrderItemFields> GetItemFields() {


      var items = new List<InventoryOrderItemFields>();

      var item1 = new InventoryOrderItemFields() {
        UID = "516f36bc-535c-4a07-8475-3e6568ebbd23",
        //ExternalObjectItemReferenceUID = "",
        Notes = "NOTAS 1",
        VendorProductUID = "e0655909-8614-40c0-b63e-fe166a377c86",
        WarehouseBinUID = "f06a2b16-e744-412e-bd94-82821a7b5cd9",
        Quantity = 10,
        InputQuantity = 9,
        OutputQuantity = 8,
        //ClosingTime = DateTime.Now,
        //PostingTime = DateTime.Now,
        PostedByUID = "a517e788-8ddf-4772-b6d2-adc3907e3905",
        Status = InventoryStatus.Abierto,
        //  Comments = "COMENTARIO X-001",
      };
      items.Add(item1);
      var item2 = new InventoryOrderItemFields() {
        UID = "f53e0410-9fd0-4a1c-9c04-d3450ff47ec9",
        //ExternalObjectItemReferenceUID = "",
        Notes = "NOTAS 2",
        VendorProductUID = "1d47e4e5-ff97-4197-8bd1-b49df2780c32",
        WarehouseBinUID = "48605b90-52e1-43d0-aeab-7125805863aa",
        Quantity = 20,
        InputQuantity = 9,
        OutputQuantity = 8,
        //ClosingTime = DateTime.Now,
        //PostingTime = DateTime.Now,
        PostedByUID = "a517e788-8ddf-4772-b6d2-adc3907e3905",
        Status = InventoryStatus.Abierto,
      };
      items.Add(item2);
      var item3 = new InventoryOrderItemFields() {
        UID = "0f545ebd-d913-4b01-9b70-af72556c6cd3",
        //ExternalObjectItemReferenceUID = "",
        Notes = "NOTAS 3",
        VendorProductUID = "1d47e4e5-ff97-4197-8bd1-b49df2780c32",
        WarehouseBinUID = "48605b90-52e1-43d0-aeab-7125805863aa",
        Quantity = 30,
        InputQuantity = 9,
        OutputQuantity = 8,
        //ClosingTime = DateTime.Now,
        //PostingTime = DateTime.Now,
        PostedByUID = "a517e788-8ddf-4772-b6d2-adc3907e3905",
        Status = InventoryStatus.Abierto,
      };
      items.Add(item3);
      return items.ToFixedList();
    }

    #endregion Private methods
  }
}
