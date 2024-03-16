/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Core Management                            Component : Test cases                              *
*  Assembly : Empiria.Trade.Test.dll                     Pattern   : Use cases tests                         *
*  Type     : Credits Tests                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Test cases for credits.                                                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Xunit;

using Empiria.Trade.Sales.Adapters;
using Empiria.Trade.Sales;

using System.Collections.Generic;
using Empiria.Trade.Sales.UseCases;

using Empiria.Trade.Core;
using Xunit.Abstractions;
using Empiria.Trade.Sales.Credits.Adapters;
using Empiria.Trade.Orders;

namespace Empiria.Trade.Tests.Credits {

  /// Test cases for credits.
  public class CreditsTest {

    [Fact]
    public void ShouldCrateNewCreditTransaction() {

      var creditTransactionFields = new CreditTrasnactionFields {
        TypeId = 1,
        CreditLineId = 1,
        CreditAmount = 100,
        DebitAmount = Convert.ToDecimal(200),
        PayableOrderId = 3,
        DaysToPay = 9,
        ExtData = "",
        TransactionTime = Convert.ToDateTime("2023/01/10"),
        DueDate = Convert.ToDateTime("2023/01/10")
      };

      var credit = new CreditTransaction();

      credit.Update(creditTransactionFields);
      credit.Save();
            
      Assert.NotNull(credit);
    }

   

    } // class CreditsTest

} // namespace Empiria.Trade.Tests.Credits
