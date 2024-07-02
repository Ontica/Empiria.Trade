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
        ReportType = ReportType.StocksByProduct,
        ProductUID = "2a30449f-cf61-468c-b52e-544cda8602ab"
      };

      ReportDataDto sut = usecase.BuildReport(query);
      Assert.NotNull(sut);
    }


    #endregion Facts

    #region Private methods



    #endregion Private methods
  }
}
