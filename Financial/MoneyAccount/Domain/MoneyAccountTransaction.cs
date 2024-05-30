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
  /// Represents a MoneyAccount Transaction.
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
    public int MoneyAccountId {
      get; private set;
    } 

    [DataField("MoneyAccountTransactionTypeId")]
    public MoneyAccountTransactionType TransactionType {
      get; private set;
    }

    [DataField("TransactionNumber")]
    public string TransactionNumber {
      get; private set;
    }

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
    } = string.Empty;

    [DataField("Credit")]
    public decimal Credit {
      get; private set;
    } = 0m;

    [DataField("Debit")]
    public decimal Debit {
      get; private set;
    } = 0m;

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
      if (IsNew) {
        TransactionNumber = "MAT-" + EmpiriaString.BuildRandomString(10).ToUpperInvariant();
      }
      MoneyAccountTransactionData.Write(this);
    }

    internal void Update(MoneyAccountTransactionFields fields) {
      this.MoneyAccountId = MoneyAccount.Parse(fields.MoneyAccountUID).Id;
      this.TransactionType = MoneyAccountTransactionType.Parse(fields.TransactionTypeUID);
      this.Description = fields.Description;
      this.Credit = fields.TransactionAmount;
      this.ReferenceId = fields.ReferenceId;
      this.TransactionTime = fields.TransactionTime;
      this.Notes = fields.Notes;
      this.PostedTime = DateTime.Now;
      this.PostedById = ExecutionServer.CurrentUserId;
    }

    
    static public FixedList<MoneyAccountTransaction> GetTransactions(int moneyAccountId) {
      return MoneyAccountTransactionData.GetTransactions(moneyAccountId);
    }


    public void Cancel(string notes = "") {
      this.Status = EntityStatus.Deleted;
      this.Notes = notes;
      this.Save();
    }

    public void AddCreditTransactions(MoneyAccount moneyAccount, CreditTrasnactionFields fields) {
      MoneyAccountTransaction moneyTransaction = new MoneyAccountTransaction();
      moneyTransaction.MoneyAccountId = moneyAccount.Id;
      moneyTransaction.TransactionType = MoneyAccountTransactionType.Parse(750);
      moneyTransaction.Description = "Credito " + fields.ExtData;
      moneyTransaction.TransactionTime = fields.TransactionTime;
      moneyTransaction.Credit = fields.CreditAmount;
      moneyTransaction.ReferenceId = fields.PayableOrderId;
      moneyTransaction.Notes = fields.Notes;
      moneyTransaction.ExtData = fields.ExtData;
      moneyTransaction.PostedTime = DateTime.Now;
      moneyTransaction.PostedById = ExecutionServer.CurrentUserId;

      moneyTransaction.Save();
    }


    #endregion Public methods

    #region Private methods

    
    #endregion Private methods

  } // class MoneyAccountTransaction

} // namespace Empiria.Trade.Financial


