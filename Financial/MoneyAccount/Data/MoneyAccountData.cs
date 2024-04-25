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
using Empiria.StateEnums;

using Empiria.Trade.Financial.Adapters;


namespace Empiria.Trade.Financial.Data {
  /// <summary>Provides data for MoneyAccount Transactions. </summary>
  internal class MoneyAccountData {

    #region Public methods

    static internal FixedList<MoneyAccount> GetMoneyAccounts() {
      string sql = "SELECT * FROM TRDMoneyAccounts " +
                  $"WHERE MoneyAccountId > 0  AND Status <>  'X'";

      var op = DataOperation.Parse(sql);

      return DataReader.GetFixedList<MoneyAccount>(op);
    }

    static internal MoneyAccount GetMoneyAccount(int ownerId) {
      string sql = $"SELECT * FROM TRDMoneyAccounts WHERE  OwnerId = {ownerId}";

      var op = DataOperation.Parse(sql);

      return DataReader.GetObject<MoneyAccount>(op);
    }

    static internal void Write(MoneyAccount o) {

      var op = DataOperation.Parse("writeMoneyAccounts", o.Id, o.UID,o.MoneyAccountType.Id, o.Description, o.Number, o.Owner.Id, o.Notes, o.Keywords, o.ExtData, o.PostedTime,
                                                         o.PostedById, o.FromDate, o.ToDate, o.CreditLimit, o.DaysToPay,
                                                         (char) o.Status);

      DataWriter.Execute(op);
    }

    internal static FixedList<MoneyAccount> GetMoneyAccounts(SearchMoneyAccountFields fields) {

      string statusFilter = string.Empty;
      EntityStatus status = ParceStatus(fields.Status);

      if (status != EntityStatus.All) {
        statusFilter = $" AND (Status = '{(char) status}')";
      } else {
        statusFilter = $" AND (Status <> '{(char) EntityStatus.Deleted}')";
      }

      var toDate = fields.ToDate.ToString("yyyy-dd-MM");
      var fromDate = fields.FromDate.ToString("yyyy-dd-MM");

      string keywordsFilter = string.Empty;

      string customerFilter = string.Empty;

      string moneyAccountTypeFilter = string.Empty;

      if (fields.CustomerUID != string.Empty) {
        customerFilter = $"INNER JOIN TRDParties ON TRDMoneyAccounts.OwnerId = TRdParties.PartyId WHERE (partyUID = '{fields.CustomerUID}') AND ";
      } else {
        customerFilter = "WHERE ";
      }

      if (fields.Keywords != string.Empty) {
        keywordsFilter = $" {SearchExpression.ParseAndLikeKeywords("MoneyAccountKeywords", fields.Keywords)} AND ";
      }

      if (fields.MoneyAccountTypeUID != string.Empty) {
        var moneyAccountTypeId = GetMoneyAccountTypeId(fields.MoneyAccountTypeUID);
        moneyAccountTypeFilter = $" AND MoneyAccountTypeId ={moneyAccountTypeId} ";
      }

      var sql = $"SELECT * FROM TRDMoneyAccounts {customerFilter} " +
                 $" {keywordsFilter}  (PostedTime >= CONVERT(SMALLDATETIME, '{fromDate}') AND " +
                 $"PostedTime <= CONVERT(SMALLDATETIME,'{toDate}')) {moneyAccountTypeFilter} {statusFilter} ";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetFixedList<MoneyAccount>(dataOperation);
    }

    #endregion Public methods

    #region Private methods

    static internal int GetMoneyAccountTypeId(string moneyAccountUid) {
      var sql = "SELECT * FROM SimpleObjects " +
               $"WHERE ObjectKey = '{moneyAccountUid}'";


      var dataOperation = DataOperation.Parse(sql);

      var debt = Empiria.Data.DataReader.GetScalar<int>(dataOperation);

      return debt;
    }

    static private EntityStatus ParceStatus(string status) {
      if (String.IsNullOrEmpty(status)) {
        return EntityStatus.All;
      }

      Enum.TryParse(status, out EntityStatus maStatus);

      return maStatus;
    }

    #endregion Private methods

  } // class MoneyAccountData

} // namespace Empiria.Trade.Financial.Data
