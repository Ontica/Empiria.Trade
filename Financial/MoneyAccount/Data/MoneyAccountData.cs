/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : MoneyAccount Management                    Component : Data Layer                              *
*  Assembly : Empiria.Trade.Financial.dll                Pattern   : Data Service                            *
*  Type     : MoneyAccountData                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data for MoneyAccount Transactions.                                                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Data;

namespace Empiria.Trade.Financial.Data {
  /// <summary>Provides data for MoneyAccount Transactions. </summary>
  internal class MoneyAccountData {

    #region Public methods

    static internal void Write(MoneyAccount o) {

      var op = DataOperation.Parse("writeMoneyAccounts", o.Id, o.UID, o.Description, o.OwnerId, o.Notes, o.ExtData, o.PostedTime,
                                                         o.PostedById, o.FromDate, o.ToDate, o.CreditLimit, o.DaysToPay,
                                                         (char) o.Status);

      DataWriter.Execute(op);
    }

    #endregion Public methods

    #region Private methods
    #endregion Private methods

  }
}
