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

using Empiria.Services;
using Empiria.Trade.Financial.Adapters;
using Empiria.Trade.MoneyAccounts;

namespace Empiria.Trade.Financial.UseCases {
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

    public CreditMoneyAccount AddMoneyAccount(MoneyAccountFields fields) {
      Assertion.Require(fields, "fields");

      var moneyAccount = new CreditMoneyAccount(fields);
      moneyAccount.Save();

      return moneyAccount;
    // return CreditTransactionMapper.Map(creditTransaction);
    }

    public MoneyAccountTransactionDto AddTransaction(MoneyAccountTransactionFields fields) {
      Assertion.Require(fields, "fields");

      var moneyAccountTransaction = new MoneyAccountTransaction(fields);
      moneyAccountTransaction.Save();

      
      return MoneyAccountTransactionMapper.Map(moneyAccountTransaction);
    }

 

    public MoneyAccountTransactionDto Cancel(int referenceId, string notes) {
      Assertion.Require(referenceId, "moneyAccountTransactionId");

      var transaction = MoneyAccountTransaction.ParseByReferenceId(referenceId);
      transaction.Cancel(notes);

      return MoneyAccountTransactionMapper.Map(transaction);
    }

    public decimal GetMoneyAccountTotalDebt(int ownerId) {
      var moneyAccount = CreditMoneyAccount.ParseByOwnder(ownerId);

      return moneyAccount.GetDebit();
    }

    public FixedList<MoneyAccountTransactionDto> GetCreditTransactions(int ownerId) {

      var moneyAccount = CreditMoneyAccount.ParseByOwnder(ownerId);

      var moneyAccountTransactions = moneyAccount.GetTransactions();

      return MoneyAccountTransactionMapper.MapMoneyAccountTransactions(moneyAccountTransactions);

    }

    public decimal GetMoneyAccountCreditLimit(int ownerId) {
      var moneyAccount = CreditMoneyAccount.ParseByOwnder(ownerId);

      return moneyAccount.CreditLimit;
    }


    #endregion Public properties

    #region Public methods
    #endregion Public methods

    #region Private methods
    #endregion Private methods

  }
}
