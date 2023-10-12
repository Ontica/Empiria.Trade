/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Core Management                            Component : Test cases                              *
*  Assembly : Empiria.Trade.Test.dll                     Pattern   : Use cases tests                         *
*  Type     : SalesTests                                 License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Test cases for sales.                                                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Xunit;

using Empiria.Tests;

using Empiria.Trade.Sales.UseCases;
using Empiria.Trade.Sales.Adapters;
using Empiria.Trade.Sales;

using Empiria.StateEnums;


namespace Empiria.Trade.Tests.Sales {

  /// <summary>Test cases for sales.   </summary>
  public class SalesTest {

    [Fact]
    public void ShouldCreateOrderTest() {

      var item = new SalesOrderItemsFields {
        UID = "afasfa",
        Quantity = 3,
        ProductUID = "4ffcb71f-5554-446c-8678-56cc10759fa8",
        ProductPriceId = 200,
        PriceListNumber = 3,
        BasePrice = 100,
        SalesPrice = 393.30m,
        AdditionalDiscount = 0,
        Shipment = 339,
        Taxes = 19,
        Total = 800,
        Notes = "",
        VendorUID = "85af02f9-964d-4f35-9a00-710fcf04d925",
        Status = EntityStatus.Active
      };

      var salesOrderItem = new SalesOrderItem(30, item);

      

      Assert.NotNull(salesOrderItem);
    }

  } // public class SalesTest

} // namespace Empiria.Trade.Tests.Sales
