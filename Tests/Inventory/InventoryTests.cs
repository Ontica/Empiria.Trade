/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory and handling Management          Component : Test cases                              *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Use cases tests                         *
*  Type     : InventoryTests                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Test cases for inventory order.                                                                *
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

namespace Empiria.Trade.Tests.Procurement {


  public class InventoryTests {

    #region Initialization

    public InventoryTests() {
      //TestsCommonMethods.Authenticate();
    }

    #endregion Initialization

    #region Facts


    [Fact]
    public void GetInventoryOrderItemByUIDTest() {

      var usecase = InventoryOrderUseCases.UseCaseInteractor();
      var sut = usecase.GetInventoryOrderItemByUID("9444f6ea-0aa4-48b4-aed3-d49feac10c11");

      Assert.NotNull(sut);
    }


    [Fact]
    public void CloseInventoryOrderStatusTest() {

      var usecase = InventoryOrderUseCases.UseCaseInteractor();

      string inventoryOrderUID = "64b96507-37b5-45d5-9a4c-40ef8099a6db";
      InventoryOrderDto sut = usecase.CloseInventoryOrder(inventoryOrderUID);

      Assert.NotNull(sut);
    }


    [Fact]
    public void CreateInventoryCountOrderTest() {

      var usecase = InventoryOrderUseCases.UseCaseInteractor();

      InventoryOrderFields fields = GetInventoryOrderFields();
      InventoryOrderDto sut = usecase.CreateInventoryOrder(fields);

      Assert.NotNull(sut);
    }


    [Fact]
    public void CreateInventoryOrderItemTest() {

      var usecase = InventoryOrderUseCases.UseCaseInteractor();

      string inventoryOrderUID = "bc39e761-90ff-4a5c-8971-34c39cc6af21";
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
        InventoryOrderTypeUID = "5851e71b-3a1f-40ab-836f-ac3d2c9408de",
        AssignedToUID = "",
        Keywords = "OCFI000000008",
        Status = InventoryStatus.Cerrado
      };

      InventoryOrderDataDto sut = usecase.GetInventoryOrderList(query);
      Assert.NotNull(sut);
    }


    [Fact]
    public void GetInventoryOrderByUID() {

      var usecase = InventoryOrderUseCases.UseCaseInteractor();
      string inventoryOrderUID = "64b96507-37b5-45d5-9a4c-40ef8099a6db";

      InventoryOrderDto sut = usecase.GetInventoryOrderByUID(inventoryOrderUID);
      Assert.NotNull(sut);
    }


    [Fact]
    public void UpdateInventoryCountOrderTest() {

      var usecase = InventoryOrderUseCases.UseCaseInteractor();
      string inventoryOrderUID = "7bb09f2b-5491-4304-bdf3-2e88fc41d33a";
      InventoryOrderFields fields = GetInventoryOrderFields();
      InventoryOrderDto sut = usecase.UpdateInventoryOrder(inventoryOrderUID, fields);

      Assert.NotNull(sut);
    }


    [Fact]
    public void GetInventoryOrderTypeTest() {

      var usecase = InventoryOrderCataloguesUseCases.UseCaseInteractor();
      var sut = usecase.GetInventoryOrderTypeByUID("5851e71b-3a1f-40ab-836f-ac3d2c9408de");

      Assert.NotNull(sut);
    }


    [Fact]
    public void GetInventoryOrderTypeListTest() {

      var usecase = InventoryOrderCataloguesUseCases.UseCaseInteractor();
      var sut = usecase.GetInventoryOrderTypesNamedEntity();

      Assert.NotNull(sut);
    }


    #endregion Facts

    #region Private methods


    private InventoryOrderFields GetInventoryOrderFields() {

      var fields = new InventoryOrderFields() {
        InventoryOrderTypeUID = "5851e71b-3a1f-40ab-836f-ac3d2c9408de",
        //ExternalObjectReferenceUID = "",
        ResponsibleUID = "b08b3cd5-8797-454e-8811-afc27b819d41",
        AssignedToUID = "a517e788-8ddf-4772-b6d2-adc3907e3905",
        Notes = "NOTAS XYZ"
        //ItemFields = GetItemFields()
      };
      return fields;
    }


    private InventoryOrderItemFields GetInventoryOrderItemFields() {
      return new InventoryOrderItemFields() {
        UID = "",
        Notes = "NOTA 101010 TRASPASO ALMACEN X",
        VendorProductUID = "0f3b01a8-15b3-45ce-8d55-a5f46d1e2545",
        WarehouseBinUID = "0b334d35-0014-400e-8530-e611858a0932",
        Quantity = 10,
        InputQuantity = 0,
        OutputQuantity = 0
      };
    }


    #endregion Private methods
  }
}
