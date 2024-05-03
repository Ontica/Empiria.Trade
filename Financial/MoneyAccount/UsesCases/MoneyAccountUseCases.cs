/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : MoneyAccount Management                    Component : Use cases Layer                         *
*  Assembly : Empiria.Trade.Financial.dll                Pattern   : Use case interactor class               *
*  Type     : MoneyAccountUseCases                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases used to management MoneyAccount transactions.                                        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using Empiria.Services;
using Empiria.StateEnums;
using Empiria.Trade.Financial.Adapters;
using Empiria.Trade.MoneyAccounts;

namespace Empiria.Trade.Financial.UseCases
{
    /// <summary>Use cases used to management MoneyAccount transactions.</summary>
    public class MoneyAccountUseCases : UseCase {

    #region Constructors and parsers

    public MoneyAccountUseCases() {
      // no-op
    }

    static public MoneyAccountUseCases UseCaseInteractor() {
      return UseCase.CreateInstance<MoneyAccountUseCases>();
    }

    #endregion Constructors and parsers

    #region Public properties

    public MoneyAccountDto AddMoneyAccount(MoneyAccountFields fields) {
      Assertion.Require(fields, "fields");

      var moneyAccount = new MoneyAccount(fields);
      moneyAccount.Save();
     
      return MoneyAccountMapper.Map(moneyAccount);
    }

    public MoneyAccountTransactionDto AddTransaction(MoneyAccountTransactionFields fields) {
      Assertion.Require(fields, "fields");

      var moneyAccountTransaction = new MoneyAccountTransaction(fields);
      moneyAccountTransaction.Save();

      
      return MoneyAccountTransactionMapper.Map(moneyAccountTransaction);
    }

    public CreditTransactionDto AddCreditTransaction(CreditTrasnactionFields fields) {
      Assertion.Require(fields, "fields");
      var moneyAccount = MoneyAccount.ParseByOwner(fields.CustomerId);

      var moneyAccountTransaction = new MoneyAccountTransaction();
      moneyAccountTransaction.AddCreditTransactions(moneyAccount, fields);
     
      return CreditTransactionMapper.Map(moneyAccountTransaction, moneyAccount.DaysToPay);
    }


    public CreditTransactionDto CancelTransaction(int referenceId, string notes) {
      Assertion.Require(referenceId, "orderId");

      var transaction = MoneyAccountTransaction.ParseByReferenceId(referenceId);
      transaction.Cancel(notes);

      return CreditTransactionMapper.Map(transaction, 10);
    }

    public decimal GetMoneyAccountTotalDebt(int ownerId) {
      var moneyAccount = MoneyAccount.ParseByOwner(ownerId);

      return moneyAccount.GetDebit();
    }
    

    public FixedList<CreditTransactionDto> GetCreditTransactions(int customerId) {

      var moneyAccount = MoneyAccount.ParseByOwner(customerId);

      moneyAccount.LoadMoneyAccountTransactions();

      return CreditTransactionMapper.MapCreditTransactions(moneyAccount.MoneyAccountTransactions, moneyAccount.DaysToPay);
    }

    public decimal GetMoneyAccountCreditLimit(int ownerId) {
      var moneyAccount = MoneyAccount.ParseByOwner(ownerId);

      return moneyAccount.CreditLimit;
    }


    public SearchMoneyAccountDto SearchMoneyAccounts(SearchMoneyAccountFields fields) {
      Assertion.Require(fields, "fields");

      var moneyAccount = new MoneyAccount();

      return SearchMoneyAccountMapper.Map(fields, moneyAccount.Search(fields));
    }


    public FixedList<NamedEntityDto> GetMoneyAccountTypes() {

      return MoneyAccountType.GetList<MoneyAccountType>().MapToNamedEntityList();     
    }

    public FixedList<NamedEntityDto> GetStatusList() {      
      var active = new NamedEntityDto("Active", "Activo");
      var pending = new NamedEntityDto("Pending", "Pendiente");
      var suspended = new NamedEntityDto("Suspended", "Suspendido");
      var deleted = new NamedEntityDto("Deleted", "Cancelado");     

      List<NamedEntityDto> orderSalesStatus = new List<NamedEntityDto>();

      orderSalesStatus.Add(active);
      orderSalesStatus.Add(pending);
      orderSalesStatus.Add(suspended);
      orderSalesStatus.Add(deleted);
     
      return orderSalesStatus.ToFixedList<NamedEntityDto>();
    }

    public MoneyAccountDto GetMoneyAccount(string moneyAccountUID) {
      var moneyAccount = MoneyAccount.Parse(moneyAccountUID);
      moneyAccount.LoadMoneyAccountTransactions();

      return  MoneyAccountMapper.Map(moneyAccount);
    }


    #endregion Public properties

    #region Public methods
    #endregion Public methods

    #region Private methods
    #endregion Private methods

  }
}
