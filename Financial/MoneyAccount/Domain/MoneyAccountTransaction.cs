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
using Empiria.Trade.MoneyAccounts;

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

    static public MoneyAccountTransaction ParseByReferenceId(int referenceId) {
      return MoneyAccountTransactionData.GetMoneyAccountTransactionByReference(referenceId);
    }


    #endregion Constructors and parsers

    #region Public properties

    [DataField("MoneyAccountId")]
    public int MoneyAccount {
      get; private set;
    } 

    [DataField("MoneyAccountTransactionTypeId")]
    public int TransactionTypeId {
      get; private set;
    } = 1;

    [DataField("ReferenceTypeId")]
    public int ReferenceTypeId {
      get; private set;
    } = 1;

    [DataField("ReferenceId")]
    public int ReferenceId {
      get; private set;
    }

    [DataField("MoneyAccountTransactionDescription")]
    public string Description {
      get; private set;
    }

    [DataField("Credit")]
    public decimal Credit {
      get; private set;
    } = 0m;

    [DataField("Debit")]
    public decimal Debit {
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
      this.MoneyAccount = 3; //MoneyAccount.Parse(fields.MoneyAccountUID);
      this.Description = fields.Description;
      this.Credit = fields.TransactionAmount;
      this.ReferenceId = fields.PayableOrderId;
      this.PayableOrderId = fields.PayableOrderId;
      this.TransactionTime = fields.TransactionTime;
      this.Notes = fields.Notes;
      this.PostedTime = DateTime.Now;
      this.PostedById = ExecutionServer.CurrentUserId;
    }

    
    static public FixedList<MoneyAccountTransaction> GetTransactions(int moneyAccountId) {
      return MoneyAccountTransactionData.GetTransactions(moneyAccountId);
    }


    public void Cancel(string notes) {
      this.Status = EntityStatus.Deleted;
      this.Notes = notes;
      this.Save();
    }

    public void AddCreditTransactions(MoneyAccount moneyAccount, CreditTransactionFields fields) {
      MoneyAccountTransaction moneyTransaction = new MoneyAccountTransaction();
      moneyTransaction.MoneyAccount = moneyAccount.Id;
      moneyTransaction.Description = "Credito " + fields.ExtData;
      moneyTransaction.TransactionTime = fields.TransactionTime;
      moneyTransaction.Credit = fields.CreditAmount;
      moneyTransaction.ReferenceId = fields.PayableOrderId;
      moneyTransaction.PayableOrderId = fields.PayableOrderId;
      moneyTransaction.Notes = fields.Notes;
      moneyTransaction.ExtData = fields.ExtData;
      moneyTransaction.PostedTime = DateTime.Now;
      moneyTransaction.PostedById = ExecutionServer.CurrentUserId;

      moneyTransaction.Save();
    }

    public string MigarteCreditTransactionToMoneyAccountTransactions() {
     var moneyAccounts = MoneyAccountData.GetMoneyAccounts();

      foreach (var moneyAccount in moneyAccounts) {
        var creditLineId = CrediLineData.GetCreditLineId(moneyAccount.Owner.Id);
        var transactions = CreditTransactionsData.GetCreditTrasantions(creditLineId);
        AddCreditTransaction(moneyAccount, transactions);

        }
      return "ok";
      }

    #endregion Public methods

    #region Private methods

    private void AddCreditTransaction(MoneyAccount moneyAccount, FixedList<CreditTransaction> transactions) {
      foreach (var transaction in transactions) {
        MoneyAccountTransaction moneyTransaction = new MoneyAccountTransaction();
        moneyTransaction.MoneyAccount = moneyAccount.Id;
        moneyTransaction.Description = "Credito " + transaction.ExtData;
        moneyTransaction.TransactionTime = transaction.TransactionTime;
        moneyTransaction.Credit = transaction.CreditAmount;
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


