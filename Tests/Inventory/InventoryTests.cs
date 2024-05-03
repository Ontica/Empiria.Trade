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
    public void CreateInventoryOrderItemTest() {

      var usecase = InventoryOrderUseCases.UseCaseInteractor();

      string inventoryOrderUID = "2e9eb67b-6474-4c66-97c5-1d4d3b1d70f6";
      var fields = GetInventoryOrderItemFields();
      InventoryOrderDto sut = usecase.CreateInventoryOrderItem(inventoryOrderUID, fields);

      Assert.NotNull(sut);
    }

    
    [Fact]
    public void DeleteInventoryOrderTest() {

      var usecase = InventoryOrderUseCases.UseCaseInteractor();
      string inventoryOrderUID = "96295bcd-fea9-4c0a-b9d3-5dfd9aeaa920";

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
      string inventoryOrderUID = "e6ec21ce-ad6b-46ac-99e8-4ff71864f138";
      string inventoryItemUID = "26859b32-642a-49c9-8ac6-b24e74ba0faf";

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
      string inventoryOrderUID = "96295bcd-fea9-4c0a-b9d3-5dfd9aeaa920";

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
        Notes = "CONTEO ALMACEN Z"
        //ItemFields = GetItemFields()
      };
      return fields;
    }


    private InventoryOrderItemFields GetInventoryOrderItemFields() {
      return new InventoryOrderItemFields() {
        UID = "",
        Notes = "NOTAS 000101010 CONTEO ALMACEN Z",
        VendorProductUID = "e0655909-8614-40c0-b63e-fe166a377c86",
        WarehouseBinUID = "f06a2b16-e744-412e-bd94-82821a7b5cd9",
        Quantity = 1,
        InputQuantity = 0,
        OutputQuantity = 0,
        Position = 1,
        Level = 1
      };
    }


    #endregion Private methods
  }
}
