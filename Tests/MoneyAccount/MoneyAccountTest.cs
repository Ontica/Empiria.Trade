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
using Empiria.Trade.Sales.UseCases;
using Empiria.StateEnums;
using Empiria.Trade.Core.Domain;
using Empiria.Trade.MoneyAccounts;

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
    public void ShouldAddCreditTransactionsToMoneyAccountTransasctions() {

      var creditTransactionFields = new CreditTrasnactionFields {
        TypeId = 1,
        CustomerId =  234,
        CreditAmount = 100,
        DebitAmount = Convert.ToDecimal(200),
        PayableOrderId = 3,
        DaysToPay = 9,
        ExtData = "P949494",
        TransactionTime = Convert.ToDateTime("2023/01/10"),
        DueDate = Convert.ToDateTime("2023/01/10")
      };

      var moneyAccount = Empiria.Trade.Financial.MoneyAccount.ParseByOwner(234);

      var moneyAccountTransaction = new MoneyAccountTransaction();
      moneyAccountTransaction.AddCreditTransactions(moneyAccount, creditTransactionFields);
    
      Assert.NotNull(moneyAccount);
    }

    [Fact]
    public void ShouldGetTotalDebtByMoneyAccountOwnerId() {
      var moneyAccount = Empiria.Trade.Financial.MoneyAccount.ParseByOwner(1000);

      var x = MoneyAccountTransaction.ParseByReferenceId(1000);
      x.Cancel("Prueba");

      Assert.NotNull(x);
    }

    [Fact]
    public void ShouldSearchMoneyAccounts() {


      var fields = new SearchMoneyAccountFields {
        Keywords = "ASAFE",
        Status = "",
        
      };

      var moneyAccountUseCase = MoneyAccountUseCases.UseCaseInteractor();
      var moneyAccount = moneyAccountUseCase.SearchMoneyAccounts(fields);

      Assert.NotNull(moneyAccount);
    }


    [Fact]
    public void ShouldGetMoneyAccountTypes() {

      var monyeAccount = Empiria.Trade.Financial.MoneyAccount.Parse(1001);

      var x = MoneyAccountType.GetList<MoneyAccountType>();
      var y = x.MapToNamedEntityList();
      Assert.NotNull(y);
    }

    [Fact]
    public void ShouldGet() {

      var moneyAccount = Empiria.Trade.Financial.MoneyAccount.ParseByOwner(153);
      moneyAccount.LoadMoneyAccountTransactions();

      var y = moneyAccount.GetDebit();

      

      var x = MoneyAccountMapper.Map(moneyAccount);

      Assert.NotNull(x);
    }

  } // class MoneyAccountTest

} // namespace Empiria.Trade.Tests.MoneyAccount
