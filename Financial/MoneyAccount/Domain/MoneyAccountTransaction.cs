/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Financial Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Financial.dll                Pattern   : Partitioned Type / Information Holder   *
*  Type     : MoneyAccountTransaction                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a MoneyAccount Transaction.                                                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.StateEnums;
using Empiria.Trade.Financial.Adapters;
using Empiria.Trade.Financial.Data;


namespace Empiria.Trade.Financial {
  /// <summary>Represents a MoneyAccount Transaction.</summary>
  public class MoneyAccountTransaction : BaseObject {

    #region Constructors and parsers

    public MoneyAccountTransaction() {
      //no-op
    }

    public MoneyAccountTransaction(MoneyAccountTransactionFields fields) {
      Update(fields);
    }

    static public MoneyAccountTransaction Empty => BaseObject.ParseEmpty<MoneyAccountTransaction>();

    static public MoneyAccountTransaction Parse(int id) {
      return BaseObject.ParseId<MoneyAccountTransaction>(id);
    }

    static public MoneyAccountTransaction Parse(string uid) {
      return BaseObject.ParseKey<MoneyAccountTransaction>(uid);
    }

    #endregion Constructors and parsers

    #region Public properties

    [DataField("MoneyAccountId")]
    public CreditMoneyAccount MoneyAccount {
      get; private set;
    }

    [DataField("MoneyAccountTransactionDescription")]
    public string Description {
      get; private set;
    }

    [DataField("MoneyAccountTransactionAmount")]
    public decimal Amount {
      get; private set;
    } = 0m;

    [DataField("PayableOrderId")]
    public int PayableOrderId {
      get; private set;
    }

    [DataField("MoneyAccountTransactionTime")]
    public DateTime TransactionTime {
      get; private set;
    }

    [DataField("Notes")]
    public string Notes {
      get; private set;
    }

    [DataField("ExtData", Default = "")]
    public string ExtData {
      get; private set;
    } = string.Empty;

    [DataField("PostedTime")]
    public DateTime PostedTime {
      get; private set;
    }

    [DataField("PostedById")]
    public int PostedById {
      get; private set;
    }

    [DataField("MoneyAccountTransactionStatus", Default = EntityStatus.Active)]
    public EntityStatus Status {
      get; set;
    } = EntityStatus.Active;

    #endregion Public properties

    #region Public methods

    protected override void OnSave() {
      MoneyAccountTransactionData.Write(this);
    }

    internal void Update(MoneyAccountTransactionFields fields) {
      this.MoneyAccount = CreditMoneyAccount.Parse(fields.MoneyAccountUID);
      this.Description = fields.Description;
      this.Amount = fields.TransactionAmount;
      this.PayableOrderId = fields.PayableOrderId;
      this.TransactionTime = fields.TransactionTime;
      this.Notes = fields.Notes;
      this.PostedTime = DateTime.Now;
      this.PostedById = ExecutionServer.CurrentUserId;
    }

    public string MigarteCreditTransactionToMoneyAccountTransactions() {
     var moneyAccounts = MoneyAccountData.GetMoneyAccounts();

      foreach (var moneyAccount in moneyAccounts) {
        var creditLineId = CrediLineData.GetCreditLineId(moneyAccount.OwnerId);
        var transactions = CreditTransactionsData.GetCreditTrasantions(creditLineId);
        AddCreditTransaction(moneyAccount, transactions);

        }
      return "ok";
      }

    #endregion Public methods

    #region Private methods

    private void AddCreditTransaction(CreditMoneyAccount moneyAccount, FixedList<CreditTransaction> transactions) {
      foreach (var transaction in transactions) {
        MoneyAccountTransaction moneyTransaction = new MoneyAccountTransaction();
        moneyTransaction.MoneyAccount = moneyAccount;
        moneyTransaction.Description = "Credito " + transaction.ExtData;
        moneyTransaction.TransactionTime = transaction.TransactionTime;
        moneyTransaction.Amount = transaction.CreditAmount;
        moneyTransaction.PayableOrderId = transaction.PayableOrderId;
        moneyTransaction.Notes = "";
        moneyTransaction.ExtData = transaction.ExtData;
        moneyTransaction.PostedTime = DateTime.Now;
        moneyTransaction.PostedById = -1;

        moneyTransaction.Save();
      }
    }
    #endregion Private methods

  } // class MoneyAccountTransaction

} // namespace Empiria.Trade.Financial


