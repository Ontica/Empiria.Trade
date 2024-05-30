/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : MoneyAccount Management                    Component : Data Layer                              *
*  Assembly : Empiria.Trade.Financial.dll                Pattern   : Data Service                            *
*  Type     : MoneyAccountTransactionItemData            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data for Money Account Transactions Item.                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using Empiria.Data;
using Empiria.DataTypes;

namespace Empiria.Trade.Financial.Data {
  /// Provides data for Money Account Transactions Item.
  internal class MoneyAccountTransactionItemData {

    #region Public methods

    static internal void Write(MoneyAccountTransactionItem o) {

      var op = DataOperation.Parse("writeMoneyAccountTransactionItems", o.Id, o.UID, o.MoneyAccountTransaction.Id, o.MoneyAccountTransactionItemType.Id,
                                                                        o.ReferenceTypeId, o.ReferenceId, o.PaymentType.Id, o.Deposit, o.Withdrawal,
                                                                        o.Notes, o.ExtData, o.TransactionTime, o.PostedTime, o.PostedById,
                                                                        (char) o.MoneyAccountTransactionItemStatus);

      DataWriter.Execute(op);
    }

    internal static FixedList<MoneyAccountTransactionItem> GetTransactionItems(int moneyAccountTransactionId) {
      string sql = "SELECT * FROM TRDMoneyAccountTransactionItems " +
                  $"WHERE MoneyAccountTransactionId  = {moneyAccountTransactionId} AND MoneyAccountTransactionItemStatus <> 'X' ";

      var op = DataOperation.Parse(sql);

      return DataReader.GetFixedList<MoneyAccountTransactionItem>(op);
    }

    #endregion Public methods

  } // class MoneyAccountTransactionItemData

} // namespace Empiria.Trade.Financial.Data
