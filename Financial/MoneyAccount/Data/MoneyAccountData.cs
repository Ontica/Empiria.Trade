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

    static internal FixedList<CreditMoneyAccount> GetMoneyAccounts() {
      string sql = "SELECT * FROM TRDMoneyAccounts " +
                  $"WHERE MoneyAccountId > 0  AND Status <>  'X'";

      var op = DataOperation.Parse(sql);

      return DataReader.GetFixedList<CreditMoneyAccount>(op);
    }

    static internal CreditMoneyAccount GetMoneyAccount(int ownerId) {
      string sql = $"SELECT * FROM TRDMoneyAccounts WHERE  MoneyAccountId = {ownerId}";

      var op = DataOperation.Parse(sql);

      return DataReader.GetObject<CreditMoneyAccount>(op);
    }

    static internal void Write(CreditMoneyAccount o) {

      var op = DataOperation.Parse("writeMoneyAccounts", o.Id, o.UID,o.TypeId, o.Description, o.OwnerId, o.Notes, o.ExtData, o.PostedTime,
                                                         o.PostedById, o.FromDate, o.ToDate, o.CreditLimit, o.DaysToPay,
                                                         (char) o.Status);

      DataWriter.Execute(op);
    }


    #endregion Public methods

    #region Private methods
    #endregion Private methods

  } // class MoneyAccountData

} // namespace Empiria.Trade.Financial.Data
