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
        //TransactionTypeUID = "eccd36f6-f326-4e56-ac28-89504f8ba588",
        TransactionAmount = 3350.456m,
        Reference = "OC-E9399D93D",
        TransactionTime = DateTime.Now,
        Notes = "Esto es una prueba"
      };

      var moneyAccountUseCase = MoneyAccountUseCases.UseCaseInteractor();
      var moneyAccountTransaction = moneyAccountUseCase.AddMoneyAccountTransaction("934fd51e-50e8-4372-af97-eccf6b468f17",fields);

      Assert.NotNull(moneyAccountTransaction);
    }

    [Fact]
    public void ShouldUpdateMoneyAccountTransaction() {

      var fields = new MoneyAccountTransactionFields {
        //UID = "38246295-08b8-4fac-84da-1a3a63b81850",
        TransactionTypeUID = "70c7e1cf-13a0-4b23-9044-1bf7ddbab5e2",
        TransactionAmount = 500,
        Reference = "PEMA-30",
        TransactionTime = DateTime.Now,
        Notes = "Modificacion"
      };

      var moneyAccountUseCase = MoneyAccountUseCases.UseCaseInteractor();
      var moneyAccountTransaction = moneyAccountUseCase.UpdateMoneyAccountTransaction(fields, "38246295-08b8-4fac-84da-1a3a63b81850");

      Assert.NotNull(moneyAccountTransaction);
    }

    [Fact]
    public void ShouldCancelMoneyAccountTransaction() {


      string UID = "ea0693b3-6df3-454c-85f8-e5f34ae17a36";
       
      var moneyAccountUseCase = MoneyAccountUseCases.UseCaseInteractor();
      var moneyAccount = moneyAccountUseCase.CancelMoneyAccountTransaction(UID);

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
    public void ShouldGetMoneyAccount() {

      var moneyAccountUseCase = MoneyAccountUseCases.UseCaseInteractor();
      var ma = moneyAccountUseCase.GetMoneyAccount("30ba6a80-dcfa-4ac8-aae3-d519e7ab8c61");

      Assert.NotNull(ma);
    }

    [Fact]
    public void ShouldGetMoneyAccountTransaction() {

      var moneyAccountUseCase = MoneyAccountUseCases.UseCaseInteractor();
      var ma = moneyAccountUseCase.GetMoneyAccountTransaction("8c10c409-1438-48bf-8df7-66e6094f4096");

      Assert.NotNull(ma);
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

    [Fact]
    public void ShouldGetMoneyAccountTransactionItemTypes() {
      var moneyAccountUseCase = MoneyAccountUseCases.UseCaseInteractor();
      var matItemTypes = moneyAccountUseCase.GetMoneyAccountTransactionItemTypes();

      Assert.NotNull(matItemTypes);
    }

    [Fact]
    public void ShouldAddNewMoneyAccountTransactionItem() {

      var fields = new MoneyAccountTransactionItemFields {
        ItemTypeUID = "51adb2d1-2077-4750-872f-c68fa3891a9e",
        PaymentTypeUID = "36843e8f-0c79-419e-935c-bf814dd56871",
        Deposit = 3350.456m,
        Withdrawal = 30,        
        Notes = "Esto es una prueba",
        TransactionTime = DateTime.Now
      };

      var moneyAccountUseCase = MoneyAccountUseCases.UseCaseInteractor();
      var moneyAccountTransactionItem = moneyAccountUseCase.AddMoneyAccountTransactionItem("d9c3e83b-5409-4c14-b501-88f3613bed3a",fields);

      Assert.NotNull(moneyAccountTransactionItem);
    }

    [Fact]
    public void ShouldUpdateMoneyAccountTransactionItem() {

      var fields = new MoneyAccountTransactionItemFields {
        ItemTypeUID = "eccd36f6-f326-4e56-ac28-89504f8ba588",
        PaymentTypeUID = "36843e8f-0c79-419e-935c-bf814dd56871",
        Deposit = 0.0m,
        Withdrawal = 2340.09m,
        Notes = "CAMBIO",
        TransactionTime = DateTime.Now
      };

      var moneyAccountUseCase = MoneyAccountUseCases.UseCaseInteractor();
      var moneyAccountTransactionItem = moneyAccountUseCase.UpdateMoneyAccountTransactionItem(fields, "d5f02fd5-f8ae-4bf1-add3-1aed20074597");

      Assert.NotNull(moneyAccountTransactionItem);
    }

    [Fact]
    public void ShouldDeleteMoneyAccountTransactionItem() {
         
      var maTransactionItem = MoneyAccountTransactionItem.Parse(1001);

      var moneyAccountUseCase = MoneyAccountUseCases.UseCaseInteractor();
      var moneyAccountTransactionItem = moneyAccountUseCase.CancelMoneyAccountTransactionItem(maTransactionItem.UID);
      

      Assert.NotNull(moneyAccountTransactionItem);
    }

    [Fact]
    public void ShouldGetMoneyAccountTransactionItems() {

      var maTransaction= MoneyAccountTransaction.Parse(1002);

      var moneyAccountUseCase = MoneyAccountUseCases.UseCaseInteractor();
      var moneyAccountTransactionItems = moneyAccountUseCase.GetMoneyAccountTransactionItems(maTransaction.UID);

      Assert.NotNull(moneyAccountTransactionItems);
    }

  } // class MoneyAccountTest

} // namespace Empiria.Trade.Tests.MoneyAccount
