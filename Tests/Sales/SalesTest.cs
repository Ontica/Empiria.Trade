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
        Product = new ProductFields {
          ProductUID = "4ffcb71f-5554-446c-8678-56cc10759fa8"
        },
        ProductPriceId = 200,
        PriceListNumber = 3,
        BasePrice = 100,
        SalesPrice = 393.30m,
        AdditionalDiscount = 0,
        Shipment = 339,
        Taxes = 19,
        Total = 800,
        Notes = "",
        Vendor = new VendorFields {
          VendorUID = "85af02f9-964d-4f35-9a00-710fcf04d925"
        },
        Status = EntityStatus.Active
      };

      var salesOrderItem = new SalesOrderItem(item);
      
      

      Assert.NotNull(salesOrderItem);
    }

    [Fact]
    public void ShouldCrateNewOrder() {
      var x = new Trade.Core.Adapters.PartyContactsDto();
      x.UID = 1;
      var order = new SalesOrderFields {
        UID = "",
        OrderNumber = "",
        OrderTime = DateTime.Now,
        Status = EntityStatus.Active,
        CustomerUID = "7ed4164a-24b0-4728-910b-eb26f0684a12",

        CustomerContactUID = "",
        SupplierUID = "211e9e92-c56e-4ed3-b42f-e916211b92ce",
        SalesAgentUID = "8b1d6d37-8d6c-4983-a3a0-42ed6b867bbe",
        PaymentCondition = "1 Mes",
        ShippingMethod = "Red Paq"

      };

      var salesOrder = new SalesOrder(order);

      Assert.NotNull(salesOrder);
    }

  } // public class SalesTest

} // namespace Empiria.Trade.Tests.Sales
