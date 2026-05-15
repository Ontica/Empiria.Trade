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

using Empiria.StateEnums;

using Empiria.Trade.Inventory.Adapters;
using Empiria.Trade.Inventory.UseCases;

using Xunit;

namespace Empiria.Trade.Tests.Procurement {


  public class InventoryTests {

    #region Initialization

    public InventoryTests() {
      //TestsCommonMethods.Authenticate();
    }

    #endregion Initialization

    #region Facts V2

    [Fact]
    public void GetInventoryHolderTest() {
      
      var usecase = InventoryOrderUseCases.UseCaseInteractor();
      InventoryHolderDto sut = usecase.GetInventoryOrder("f54744dd-81d7-4eb6-8587-424431c60e45");
      Assert.NotNull(sut);
    }


    [Fact]
    public void SearchInventoryOrderTest() {

      var usecase = InventoryOrderUseCases.UseCaseInteractor();

      InventoryOrderQuery query = new InventoryOrderQuery {
        Keywords = "",
        WarehouseUID = "",
        InventoryTypeUID = "",
        Status = EntityStatus.All
      };

      InventoryOrderDataDto sut = usecase.SearchInventoryOrder(query);

      Assert.NotNull(sut);
    }

    #endregion Facts V2


    #region Facts


    [Fact]
    public void GetInventoryOrderItemByUIDTest() {

      var usecase = InventoryOrderUseCases.UseCaseInteractor();
      var sut = usecase.GetInventoryOrderItemByUID("9444f6ea-0aa4-48b4-aed3-d49feac10c11");

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
        InventoryTypeUID = "5851e71b-3a1f-40ab-836f-ac3d2c9408de",
        AssignedToUID = "",
        Keywords = "OCFI000000008",
        Status = EntityStatus.Closed
      };

      InventoryOrderDataDto sut = usecase.SearchInventoryOrders(query);
      Assert.NotNull(sut);
    }


    [Fact]
    public void GetInventoryOrderByUID() {

      var usecase = InventoryOrderUseCases.UseCaseInteractor();
      string inventoryOrderUID = "656af915-4dc9-46b6-83a9-8b65722589df";

      InventoryOrderDto sut = usecase.GetInventoryOrderByUID(inventoryOrderUID);
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
