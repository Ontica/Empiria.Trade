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

using Empiria.Trade.Financial.Adapters;
using Empiria.Trade.Financial;


namespace Empiria.Trade.Tests.MoneyAccount {
  /// <summary>Test cases for MoneyAccounts.  </summary>
  public class MoneyAccountTest {

    [Fact]
    public void ShouldAddNewMoneyAccount() {

      var fields = new MoneyAccountFields {
        TypeUID = "f6d3a7db-9f54-4a9f-a021-edbfa34fbf42",        
        OwnerUID = "057c1081-95e7-4b35-9bd0-b139bdf792d4",
        Notes = "Monedero de prueba",
        MoneyAccountLimit = 50000,
        LimitDaysToPay = 50
      };

      var moneyAccountUseCase = MoneyAccountUseCases.UseCaseInteractor();
      var moneyAccount = moneyAccountUseCase.AddMoneyAccount(fields);

      Assert.NotNull(moneyAccount);
    }

    [Fact]
    public void ShouldAddNewMoneyAccountTransaction() {

      var fields = new MoneyAccountTransactionFields {
        MoneyAccountUID = "934fd51e-50e8-4372-af97-eccf6b468f17",
        TransactionTypeUID = "eccd36f6-f326-4e56-ac28-89504f8ba588",
        Description = "Credito ",
        TransactionAmount = 3350.456m,
        ReferenceId = 30,
        TransactionTime = DateTime.Now,
        Notes = "Esto es una prueba"
      };

      var moneyAccountUseCase = MoneyAccountUseCases.UseCaseInteractor();
      var moneyAccountTransaction = moneyAccountUseCase.AddTransaction(fields);

      Assert.NotNull(moneyAccountTransaction);
    }

    [Fact]
    public void ShouldUpdateMoneyAccountTransaction() {

      var fields = new MoneyAccountTransactionFields {
        UID = "38246295-08b8-4fac-84da-1a3a63b81850",
        MoneyAccountUID = "934fd51e-50e8-4372-af97-eccf6b468f17",
        TransactionTypeUID = "70c7e1cf-13a0-4b23-9044-1bf7ddbab5e2",
        Description = "Credito ",
        TransactionAmount = 500,
        ReferenceId = 30,
        TransactionTime = DateTime.Now,
        Notes = "Modificacion"
      };

      var moneyAccountUseCase = MoneyAccountUseCases.UseCaseInteractor();
      var moneyAccountTransaction = moneyAccountUseCase.UpdateTransaction(fields, fields.UID);

      Assert.NotNull(moneyAccountTransaction);
    }

    [Fact]
    public void ShouldCancelMoneyAccountTransaction() {


      string UID = "ea0693b3-6df3-454c-85f8-e5f34ae17a36";
       
      var moneyAccountTransaction  = MoneyAccountTransaction.Parse(UID);

     // var moneyAccount = Empiria.Trade.Financial.MoneyAccount.Parse(moneyAccountTransaction.MoneyAccount);

      //var moneyAccountUseCase = MoneyAccountUseCases.UseCaseInteractor();
      //var moneyAccount = moneyAccountUseCase.CancelTransaction(UID);

      Assert.NotNull(moneyAccountTransaction);
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

    [Fact]
    public void ShouldGetPaymentsTypes() {
      var moneyAccountUseCase = MoneyAccountUseCases.UseCaseInteractor();
      var paymentTypes = moneyAccountUseCase.GetPaymentTypes();

      Assert.NotNull(paymentTypes);
    }

  } // class MoneyAccountTest

} // namespace Empiria.Trade.Tests.MoneyAccount
