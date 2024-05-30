/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Money Account Management                   Component : Domain Layer                            *
*  Assembly : Empiria.Trade.dll                          Pattern   : Partitioned Type / Information Holder   *
*  Type     : Money Account Transaction Items            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents money account transaction item.                                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.StateEnums;
using Empiria.Trade.Financial.Adapters;
using Empiria.Trade.Financial.Data;


namespace Empiria.Trade.Financial {
  ///Represents money account transaction item.
  public class MoneyAccountTransactionItem : BaseObject {

    #region Constructors and parsers

    public MoneyAccountTransactionItem() {
      //no-op
    }

    public MoneyAccountTransactionItem(MoneyAccountTransactionItemFields fields) {
      this.MoneyAccountTransaction = MoneyAccountTransaction.Parse(fields.MoneyAccountTransactionUID);
      Update(fields);
    }

    static public MoneyAccountTransactionItem Empty => BaseObject.ParseEmpty<MoneyAccountTransactionItem>();

    static public MoneyAccountTransactionItem Parse(int id) {
      return BaseObject.ParseId<MoneyAccountTransactionItem>(id);
    }

    static public MoneyAccountTransactionItem Parse(string uid) {
      return BaseObject.ParseKey<MoneyAccountTransactionItem>(uid);
    }


    #endregion Constructors and parsers

    #region Public properties

    [DataField("MoneyAccountTransactionId")]
    public MoneyAccountTransaction MoneyAccountTransaction {
      get; private set;
    }

    [DataField("MoneyAccountTransactionItemTypeId")]
    public MoneyAccountTransactionType MoneyAccountTransactionItemType {
      get; private set;
    }

    [DataField("ReferenceTypeId")]
    public int ReferenceTypeId {
      get; private set;
    } = 1;

    [DataField("ReferenceId")]
    public int ReferenceId {
      get; private set;
    } = -1;

    [DataField("PaymentTypeId")]
    public PaymentType PaymentType {
      get; private set;
    }

    [DataField("Deposit")]
    public decimal Deposit {
      get; protected set;
    } = 0m;

    [DataField("Withdrawal")]
    public decimal Withdrawal {
      get; protected set;
    } = 0m;


    public string Notes {
      get; protected set;
    } = string.Empty;


    public string ExtData {
      get; protected set;
    } = string.Empty;

    [DataField("TransactionTime")]
    public DateTime TransactionTime {
      get; private set;
    }

    [DataField("PostedTime")]
    public DateTime PostedTime {
      get; private set;
    }

    [DataField("PostedById")]
    public int PostedById {
      get; private set;
    }

    [DataField("MoneyAccountTransactionItemStatus", Default = EntityStatus.Active)]
    public EntityStatus MoneyAccountTransactionItemStatus {
      get; private set;
    } = EntityStatus.Active;


    #endregion Public properties     


    #region Public methods

    protected override void OnSave() {
      MoneyAccountTransactionItemData.Write(this);
    }

    public void Update(MoneyAccountTransactionItemFields fields) {
      this.MoneyAccountTransactionItemType = MoneyAccountTransactionType.Parse(fields.TransactionTypeUID);
      this.PaymentType = PaymentType.Parse(fields.PaymentTypeUID);
      this.Deposit = fields.Deposit;
      this.Withdrawal = fields.Withdrawal;
      this.TransactionTime = fields.TransactionTime;
      this.Notes = fields.Notes;
      this.PostedTime = DateTime.Now;
      this.PostedById = ExecutionServer.CurrentUserId;
    }

    public void Cancel() {
      this.MoneyAccountTransactionItemStatus = EntityStatus.Deleted;
      this.Save();
    }

    static public FixedList<MoneyAccountTransactionItem> GetTransactionItems(int moneyAccountTransactionId) {
      return MoneyAccountTransactionItemData.GetTransactionItems(moneyAccountTransactionId);
    }

    #endregion Public methods


    #region Private methods
    #endregion Public methods

  } // class MoneyAccountTransactionItems


} // namespace Empiria.Trade.Financial
