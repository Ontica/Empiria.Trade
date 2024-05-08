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
using Empiria.Trade.Orders;


namespace Empiria.Trade.Financial.Data {
  /// <summary>Provides data for MoneyAccountTransactions.  </summary>
  internal class MoneyAccountTransactionData {
    #region Public methods

    static internal void Write(MoneyAccountTransaction o) {

      var op = DataOperation.Parse("writeMoneyAccountTransactions", o.Id, o.UID, o.MoneyAccountId, o.TransactionType.Id, o.ReferenceTypeId, o.ReferenceId,
                                                                    o.Description, o.Credit, o.Debit,
                                                                    o.TransactionTime, o.Notes, o.ExtData, o.PostedTime,
                                                                    o.PostedById, (char) o.Status);

      DataWriter.Execute(op);
    }

    static internal decimal GetMoneyAccountTotalDebt(int moneyAccountId) {
     

      var sql = "SELECT (SUM(Credit) - SUM(Debit)) AS Balance FROM TRDMoneyAccountTransactions " +
               $"WHERE MoneyAccountId = {moneyAccountId} AND MoneyAccountTransactionStatus <> 'X' ";

      var dataOperation = DataOperation.Parse(sql);

      var debt = Empiria.Data.DataReader.GetScalar<decimal>(dataOperation);

      return debt;
    }

    internal static FixedList<MoneyAccountTransaction> GetTransactions(int moneyAccountId) {
      string sql = "SELECT * FROM  TRDMoneyAccountTransactions " +
                  $" WHERE MoneyAccountId  = {moneyAccountId} AND MoneyAccountTransactionStatus <> 'X'";

      var op = DataOperation.Parse(sql);

      return DataReader.GetFixedList<MoneyAccountTransaction>(op);
    }

    internal static MoneyAccountTransaction GetMoneyAccountTransactionByReference(int referenceId) {
      string sql = $"SELECT * FROM TRDMoneyAccountTransactions WHERE ReferenceId =  {referenceId} ";                  

      var op = DataOperation.Parse(sql);

      return DataReader.GetObject<MoneyAccountTransaction>(op);
    }


    #endregion Public methods

    #region Private methods
    #endregion Private methods

  } // class MoneyAccountTransactionData

} // class MoneyAccountTransactionData
