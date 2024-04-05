/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : MoneyAccount Management                    Component : Data Layer                              *
*  Assembly : Empiria.Trade.Financial.dll                Pattern   : Data Service                            *
*  Type     : MoneyAccountTransactionData                License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data for MoneyAccountTransactions.                                                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using Empiria.Data;


namespace Empiria.Trade.Financial.Data {
  /// <summary>Provides data for MoneyAccountTransactions.  </summary>
  internal class MoneyAccountTransactionData {
    #region Public methods

    static internal void Write(MoneyAccountTransaction o) {

      var op = DataOperation.Parse("writeMoneyAccountTransactions", o.Id, o.UID, o.MoneyAccount.Id, o.Description, o.Amount,
                                                                    o.PayableOrderId,o.TransactionTime, o.Notes, o.ExtData, o.PostedTime,
                                                                    o.PostedById, (char) o.Status);

      DataWriter.Execute(op);
    }

    #endregion Public methods

    #region Private methods
    #endregion Private methods

  } // class MoneyAccountTransactionData

} // class MoneyAccountTransactionData
