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
using Empiria.Tests;
using Empiria.Trade.Inventory.Adapters;
using Empiria.Trade.Inventory.UseCases;

using Xunit;

namespace Empiria.Trade.Tests.Procurement {


  public class InventoryTests {

    #region Initialization

    public InventoryTests() {
      TestsCommonMethods.Authenticate();
    }

    #endregion Initialization

    #region Facts V2

    [Fact]
    public void CreateInventoryOrderTest() {

      var usecase = InventoryOrderUseCases.UseCaseInteractor();

      Trade.Core.InventoryOrderFields fields = new Trade.Core.InventoryOrderFields {
        Description = "ABCDE",
        InventoryTypeUID = "D3042817-63FA-4728-BF5B-BED68FECA3FC",
        RequestedByUID = "72b902de-8840-4985-81aa-46700d915ea7",
        ResponsibleUID = "d5527139-02e5-49b1-9e8f-827c5b8630ca",
        WarehouseUID = "C5D74E47-CFEE-4B31-81B8-D9B102EDDE8F"
      };

      InventoryHolderDto sut = usecase.CreateInventoryOrder(fields);

      Assert.NotNull(sut);
    }


    [Fact]
    public void UpdateInventoryItemTest() {
      TestsCommonMethods.Authenticate();
      var usecase = InventoryOrderUseCases.UseCaseInteractor();

      string orderUID = "da4997ce-9a0d-4059-a15e-a808a5f4525c";

      Trade.Core.InventoryOrderItemFields fields = new Trade.Core.InventoryOrderItemFields() {
        Product = "ASF24",
        Location = "A-029-01-09",
        Quantity = 1,
        RequestedByUID = "e4b5e0f9-c259-44dc-80ef-6b9c8f48324d",
      };

      InventoryHolderDto sut = usecase.CreateInventoryOrderItem(orderUID, fields);
      Assert.NotNull(sut);
    }


    [Fact]
    public void GetInventoryHolderTest() {
      
      var usecase = InventoryOrderUseCases.UseCaseInteractor();
      InventoryHolderDto sut = usecase.GetInventoryOrder("98b80314-e79f-447a-a083-431c1045d156");
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


    [Fact]
    public void GetInventoryOrderTypeTest() {

      var usecase = InventoryOrderUseCases.UseCaseInteractor();
      var sut = usecase.GetOrderTypes();

      Assert.NotNull(sut);
    }



    [Fact]
    public void GetWarehousesTest() {

      var usecase = InventoryOrderUseCases.UseCaseInteractor();
      var sut = usecase.GetWarehouses();

      Assert.NotNull(sut);
    }


    [Fact]
    public void GetPartiesByRolTest() {

      var usecase = InventoryOrderUseCases.UseCaseInteractor();
      
      string[] partyRols = new string[] { "Inventory-manager", "Warehouseman" }; 
      
      var sut = usecase.GetPartiesByRol(partyRols[1]);

      Assert.NotNull(sut);
    }

    #endregion Facts V2


    #region Facts





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
