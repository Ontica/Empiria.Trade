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

    public MoneyAccounts AddMoneyAccount(MoneyAccountFields fields) {
      Assertion.Require(fields, "fields");

      var moneyAccount = new MoneyAccounts(fields);
      moneyAccount.Save();

      return moneyAccount;
    // return CreditTransactionMapper.Map(creditTransaction);
    }

    public MoneyAccountTransaction AddTransaction(MoneyAccountTransactionFields fields) {
      Assertion.Require(fields, "fields");

      var moneyAccountTransaction = new MoneyAccountTransaction(fields);
      moneyAccountTransaction.Save();

      return moneyAccountTransaction;
      // return CreditTransactionMapper.Map(creditTransaction);
    }

    #endregion Public properties

    #region Public methods
    #endregion Public methods

    #region Private methods
    #endregion Private methods

  }
}
