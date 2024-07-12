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


  public class InventoryReportsTests {

    #region Initialization

    public InventoryReportsTests() {
      //TestsCommonMethods.Authenticate();
    }

    #endregion Initialization

    #region Facts


    [Fact]
    public void GetStocksByProductReportTest() {

      var usecase = ReportGeneratorUseCases.UseCaseInteractor();

      var query = new ReportQuery {
        ReportType = ReportType.StocksByLocation,
        Products = new string[] { "2a30449f-cf61-468c-b52e-544cda8602ab", "850c8bfb-b98f-4412-b9a8-46bf7296fc73", "5bd3d866-f1a2-4e00-8a8a-fcfec186394d" },
        //WarehouseBinUID = "7e1b88e6-5a7f-488f-bb2c-2bf250823f7c",
        WarehouseBins = new string[] { "30b307d4-8e0b-4185-a1d2-ab13e3d47fac", "3ec30b06-c158-45d5-95fb-198a3c96fa29", "7e1b88e6-5a7f-488f-bb2c-2bf250823f7c" }
      };

      ReportDataDto sut = usecase.BuildReport(query);
      Assert.NotNull(sut);
    }


    #endregion Facts

    #region Private methods



    #endregion Private methods
  }
}
