/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Core Management                            Component : Test cases                              *
*  Assembly : Empiria.Trade.Test.dll                     Pattern   : Use cases tests                         *
*  Type     : MoneyAccountTest                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Test cases for MoneyAccounts.                                                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Xunit;

using Empiria.Trade.Financial.UseCases;
using Empiria.Trade.Orders;
using Empiria.Trade.Sales.Adapters;
using Empiria.Trade.Financial.Adapters;
using Empiria.Trade.Financial;

namespace Empiria.Trade.Tests.MoneyAccount {
  /// <summary>Test cases for MoneyAccounts.  </summary>
  public class MoneyAccountTest {

    [Fact]
    public void ShouldAddNewMoneyAccount() {

      var fields = new MoneyAccountFields {
        Description = "Linea de Credito",
        OwnerId = 6,
        Notes = "Monedero de prueba",
        CreditLimit = 50000,
        DaysToPay = 50
      };

      var moneyAccountUseCase = MoneyAccountUseCases.UseCaseInteractor();
      var moneyAccount = moneyAccountUseCase.AddMoneyAccount(fields);

      Assert.NotNull(moneyAccount);
    }

    [Fact]
    public void ShouldAddNewMoneyAccountTransaction() {

      var fields = new MoneyAccountTransactionFields {
        MoneyAccountUID = "6760c864-3f14-46c5-a19f-dddccea6ee04",
        Description = "Credito ",
        TransactionAmount = 500,
        PayableOrderId = 30,
        TransactionTime = DateTime.Now,
        Notes = "Esto es una prueba"
      };

      var moneyAccountUseCase = MoneyAccountUseCases.UseCaseInteractor();
      var moneyAccount = moneyAccountUseCase.AddTransaction(fields);

      Assert.NotNull(moneyAccount);
    }

    [Fact]
    public void ShouldMigrateCreditLinesToMoneyAccount() {


      var moneyAccount = new CreditMoneyAccount();
      var x = moneyAccount.MigrateCreditLineToMoneyAccount();
      Assert.NotNull(x);
    }

    [Fact]
    public void ShouldMigrateCreditTransactionsToMoneyAccountTransasctions() {
      var moneyAccountTransaction = new MoneyAccountTransaction();

      var x = moneyAccountTransaction.MigarteCreditTransactionToMoneyAccountTransactions();

      Assert.NotNull(x);
    }

    [Fact]
    public void ShouldGetTotalDebtByMoneyAccountOwnerId() {
      var moneyAccount = CreditMoneyAccount.ParseByOwnder(1023);

      var x = MoneyAccountTransaction.ParseByReferenceId(1000);
      x.Cancel("Prueba");

     

      Assert.NotNull(x);
    }

    

  } // class MoneyAccountTest

} // namespace Empiria.Trade.Tests.MoneyAccount
