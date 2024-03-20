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

using Empiria.Trade.Orders;
using Empiria.Trade.Financial;
using Empiria.Trade.Financial.Adapters;

namespace Empiria.Trade.Tests.Credits {

  /// Test cases for credits.
  public class CreditsTest {

    [Fact]
    public void ShouldCrateNewCreditTransaction() {

      var creditTransactionFields = new CreditTrasnactionFields {
        TypeId = 1,
        CustomerId = 1,
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


    [Fact]
    public void ShouldUpdateCreditTransaction() {

      var creditTransactionFields = new CreditTrasnactionFields {
        UID = "d93c7e0d-48c5-415e-b5f5-f33f49760620",
        TypeId = 1,
        CustomerId = 28,
        CreditAmount = 55100.40m,
        DebitAmount = Convert.ToDecimal(1234.45),
        PayableOrderId = -1,
        DaysToPay = 50,
        ExtData = "",
        TransactionTime = Convert.ToDateTime("2023/03/10"),
        DueDate = Convert.ToDateTime("2023/05/10")
      };

      var creditTransaction = CreditTransaction.Parse(creditTransactionFields.UID);
      creditTransaction.Update(creditTransactionFields);
      creditTransaction.Save();

      Assert.NotNull(creditTransaction);
    }

    [Fact]
    public void ShouldCancelCreditTransaction() {

      var creditTransaction = CreditTransaction.Parse("d93c7e0d-48c5-415e-b5f5-f33f49760620");
      creditTransaction.Cancel();

      Assert.NotNull(creditTransaction);
    }


  } // class CreditsTest

} // namespace Empiria.Trade.Tests.Credits
