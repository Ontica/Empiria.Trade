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
using Empiria.Trade.Core;
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

    #region Facts

    [Fact]
    public void CreateInventoryEntryTest() {

      string orderUID = "32df363b-1395-4ae6-af62-ca322a1876c9";
      string orderItemUID = "6889069c-5d7e-4e86-8437-fe6f12bb249a";

      InventoryEntryFields fields = new InventoryEntryFields {
        Product = "ASF24-100",
        ProductUID = "7e38bd5e-0258-407a-82bb-bc407f86989f",
      };

      var usecase = InventoryEntryUseCases.UseCaseInteractor();

      InventoryHolderDto sut = usecase.CreateInventoryEntry(orderUID, orderItemUID, fields);

      Assert.NotNull(sut);
    }


    [Fact]
    public void CreateInventoryOrderTest() {

      var usecase = InventoryOrderUseCases.UseCaseInteractor();

      InventoryOrderFields fields = new InventoryOrderFields {
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
    public void GetInventoryOrderTest() {

      var usecase = InventoryOrderUseCases.UseCaseInteractor();

      string orderUID = "a44bcbb6-8d7b-40c2-8df8-6c7b97a6efc0";

      InventoryHolderDto sut = usecase.GetInventoryOrder(orderUID);

      Assert.NotNull(sut);
    }


    [Fact]
    public void UpdateInventoryItemTest() {
      TestsCommonMethods.Authenticate();
      var usecase = InventoryOrderUseCases.UseCaseInteractor();

      string orderUID = "51b39b77-64c5-43bf-96e7-57922638ea18";

      InventoryOrderItemFields fields = new InventoryOrderItemFields() {
        Product = "ST9640-12",
        Location = "A-028-01-09",
        Quantity = 1,
        RequestedByUID = "e4b5e0f9-c259-44dc-80ef-6b9c8f48324d",
      };

      InventoryHolderDto sut = usecase.CreateInventoryOrderItem(orderUID, fields);
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

    #endregion Facts

  }
}
