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
using Empiria.Trade.Orders;

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

    public MoneyAccountDto UpdateMoneyAccount(MoneyAccountFields fields, string moneyAccountUID) {
      Assertion.Require(fields, "fields");

      var moneyAccount = MoneyAccount.Parse(moneyAccountUID);
      moneyAccount.Update(fields);
      moneyAccount.Save();
    
      return MoneyAccountMapper.Map(moneyAccount);
    }

    public MoneyAccountDto CancelMoneyAccount(string moneyAccountUID) {
      Assertion.Require(moneyAccountUID, "moneyAccountUID");

      var moneyAccount = MoneyAccount.Parse(moneyAccountUID);
      moneyAccount.Cancel();

      return MoneyAccountMapper.Map(moneyAccount);
    }

    public MoneyAccountDto SuspendMoneyAccount(string moneyAccountUID) {
      Assertion.Require(moneyAccountUID, "moneyAccountUID");

      var moneyAccount = MoneyAccount.Parse(moneyAccountUID);
      moneyAccount.Suspend();

      return MoneyAccountMapper.Map(moneyAccount);
    }

    public MoneyAccountDto PendingMoneyAccount(string moneyAccountUID) {
      Assertion.Require(moneyAccountUID, "moneyAccountUID");

      var moneyAccount = MoneyAccount.Parse(moneyAccountUID);
      moneyAccount.Pending();

      return MoneyAccountMapper.Map(moneyAccount);
    }

    public MoneyAccountDto ActiveMoneyAccount(string moneyAccountUID) {
      Assertion.Require(moneyAccountUID, "moneyAccountUID");

      var moneyAccount = MoneyAccount.Parse(moneyAccountUID);
      moneyAccount.Active();

      return MoneyAccountMapper.Map(moneyAccount);
    }


    public MoneyAccountTransactionDto AddMoneyAccountTransaction(string moneyAccountUID, MoneyAccountTransactionFields fields) {
      Assertion.Require(fields, "fields");
            
      var moneyAccountTransaction = new MoneyAccountTransaction(moneyAccountUID,fields);
      moneyAccountTransaction.Save();

      
      return MoneyAccountTransactionMapper.Map(moneyAccountTransaction);
    }

    public MoneyAccountTransactionDto UpdateMoneyAccountTransaction(MoneyAccountTransactionFields fields, string moneyAccountTransactionUID) {
      Assertion.Require(fields, "fields");

      var moneyAccountTransaction = MoneyAccountTransaction.Parse(moneyAccountTransactionUID);
      moneyAccountTransaction.Update(moneyAccountTransaction.MoneyAccount.UID, fields);
      moneyAccountTransaction.Save();


      return MoneyAccountTransactionMapper.Map(moneyAccountTransaction);
    }


    public MoneyAccountTransactionDto CancelMoneyAccountTransaction(string moneyAccountTransactionUID) {
      Assertion.Require(moneyAccountTransactionUID, "moneyAccountTransactionUID");

      var moneyAccountTransaction = MoneyAccountTransaction.Parse(moneyAccountTransactionUID);
      moneyAccountTransaction.Cancel();


      return MoneyAccountTransactionMapper.Map(moneyAccountTransaction);
    }

    public MoneyAccountTransactionDto GetMoneyAccountTransaction(string moneyAccountTransactionUID) {
      Assertion.Require(moneyAccountTransactionUID, "moneyAccountTransactionUID");

      var moneyAccountTransaction = MoneyAccountTransaction.Parse(moneyAccountTransactionUID);
      moneyAccountTransaction.LoadItems();

      return MoneyAccountTransactionMapper.Map(moneyAccountTransaction);
    }

    public FixedList<NamedEntityDto> GetMoneyAccountTransactionTypes(string moneyAccountUID) {

      return MoneyAccountTransactionType.GetList<MoneyAccountTransactionType>().MapToNamedEntityList();
    }

    public MoneyAccountTransactionDto AddMoneyAccountTransactionItem(string moneyAccountTransactionUID, 
                                                                         MoneyAccountTransactionItemFields fields) {
      Assertion.Require(fields, "fields");

      var moneyAccountTransactionItem = new MoneyAccountTransactionItem(moneyAccountTransactionUID, fields);
      moneyAccountTransactionItem.Save();

      moneyAccountTransactionItem.MoneyAccountTransaction.LoadItems();

      return MoneyAccountTransactionMapper.Map(moneyAccountTransactionItem.MoneyAccountTransaction);
    }

    public MoneyAccountTransactionDto UpdateMoneyAccountTransactionItem(MoneyAccountTransactionItemFields fields, string moneyAccountTransactionItemUID) {
      Assertion.Require(fields, "fields");

      var moneyAccountTransactionItem = MoneyAccountTransactionItem.Parse(moneyAccountTransactionItemUID);
      moneyAccountTransactionItem.Update(fields);
      moneyAccountTransactionItem.Save();

      moneyAccountTransactionItem.MoneyAccountTransaction.LoadItems();

      return MoneyAccountTransactionMapper.Map(moneyAccountTransactionItem.MoneyAccountTransaction);
    }

    public MoneyAccountTransactionDto CancelMoneyAccountTransactionItem(string moneyAccountTransactionITemUID) {
      Assertion.Require(moneyAccountTransactionITemUID, "moneyAccountTransactionUID");

      var moneyAccountTransactionItem = MoneyAccountTransactionItem.Parse(moneyAccountTransactionITemUID);
      moneyAccountTransactionItem.Cancel();

      moneyAccountTransactionItem.MoneyAccountTransaction.LoadItems();

      return MoneyAccountTransactionMapper.Map(moneyAccountTransactionItem.MoneyAccountTransaction);
    }

    public FixedList<MoneyAccountTransactionItemDto> GetMoneyAccountTransactionItems(string moneyAccountTransactionUID) {
      Assertion.Require(moneyAccountTransactionUID, "moneyAccountTransactionUID");

      var moneyAccountTransaction = MoneyAccountTransaction.Parse(moneyAccountTransactionUID);

      var moneyAccountTransactionItems = MoneyAccountTransactionItem.GetTransactionItems(moneyAccountTransaction.Id);
      
      return MoneyAccountTransactionItemMapper.MapMoneyAccountTransactionItems(moneyAccountTransactionItems);
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

    public FixedList<NamedEntityDto> GetPaymentTypes() {
       return PaymentType.GetList<PaymentType>().MapToNamedEntityList();
    }

    public FixedList<NamedEntityDto> GetMoneyAccountTransactionItemTypes() {
      return MoneyAccountTransactionItemType.GetList<MoneyAccountTransactionItemType>().MapToNamedEntityList();
    }

    public FixedList<NamedEntityDto> GetMoneyAccountTransactionTypes() {
      return MoneyAccountTransactionType.GetList<MoneyAccountTransactionType>().MapToNamedEntityList();
    }

    #endregion Public properties

    #region Public methods
    #endregion Public methods

    #region Private methods
    #endregion Private methods

  }
}
