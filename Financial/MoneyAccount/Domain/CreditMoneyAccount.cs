/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Money Account Management                   Component : Domain Layer                            *
*  Assembly : Empiria.Trade.dll                Pattern   : Partitioned Type / Information Holder   *
*  Type     : credit Money Account                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents credit money account.                                                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.StateEnums;
using Empiria.Trade.Core;
using Empiria.Trade.Financial.Adapters;
using Empiria.Trade.Financial.Data;
using Empiria.Trade.MoneyAccounts;
using Empiria.Trade.Orders;

namespace Empiria.Trade.Financial {
  /// <summary>Represents credit money account. </summary>
  public class CreditMoneyAccount : BaseObject {
    #region Constructors and parsers

    public CreditMoneyAccount() {
      //no-op
    }

    public CreditMoneyAccount(MoneyAccountFields fields) {
      Update(fields);
    }

    static public  CreditMoneyAccount Empty => BaseObject.ParseEmpty<CreditMoneyAccount>();

    public static CreditMoneyAccount Parse(int id) {
      return BaseObject.ParseId<CreditMoneyAccount>(id);
    }

    static public CreditMoneyAccount Parse(string uid) {
      return BaseObject.ParseKey<CreditMoneyAccount>(uid);
    }

    static public CreditMoneyAccount ParseByOwnder(int ownerId) {
      return MoneyAccountData.GetMoneyAccount(ownerId);
    }

    #endregion Constructors and parsers

    #region Public properties

    [DataField("MoneyAccountTypeId")]
    public int TypeId {
      get; protected set;
    } = 1;

    [DataField("MoneyAccountDescription")]
    public string Description {
      get; protected set;
    }

    [DataField("MoneyAccountNumber")]
    public string Number {
      get; protected set;
    }

    [DataField("OwnerId")]
    public int OwnerId {
      get; protected set;
    }

    [DataField("Notes")]
    public string Notes {
      get; protected set;
    }

    [DataField("MoneyAccountKeywords")]
    public string Keywords {
      get; protected set;
    }

    [DataField("ExtData", Default = "")]
    public string ExtData {
      get; protected set;
    } = string.Empty;

    [DataField("PostedTime")]
    public DateTime PostedTime {
      get; protected set;
    }

    [DataField("PostedById")]
    public int PostedById {
      get; protected set;
    }

    [DataField("FromDate")]
    public DateTime FromDate {
      get; protected set;
    } = new DateTime(1980, 01, 01);

    [DataField("ToDate")]
    public DateTime ToDate {
      get; protected set;
    } = new DateTime(2078, 12, 31);


    [DataField("CreditLimit")]
    public decimal CreditLimit {
      get; protected set;
    } = 0m;

    [DataField("DayToPay")]
    public int DaysToPay {
      get; protected set;
    }

    [DataField("Status", Default = EntityStatus.Active)]
    public EntityStatus Status {
      get; protected set;
    } = EntityStatus.Active;

    #endregion Public properties


    #region Public methods

    protected override void OnSave() {
      MoneyAccountData.Write(this);
    }

    internal void Update(MoneyAccountFields fields) {
      this.Description = fields.Description;
      this.OwnerId = fields.OwnerId;
      this.Notes = fields.Notes;
      this.PostedTime = DateTime.Now;
      this.PostedById = ExecutionServer.CurrentUserId;
      this.FromDate = DateTime.Now;
      this.CreditLimit = fields.CreditLimit;
      this.DaysToPay = fields.DaysToPay;
    }


    public decimal GetDebit() {
      return MoneyAccountTransactionData.GetMoneyAccountTotalDebt(this.Id);
    }

    public FixedList<MoneyAccountTransaction> GetTransactions() {
      return MoneyAccountTransaction.GetTransactions(this.Id);
    }
       
    public FixedList<CreditMoneyAccount> Search(SearchMoneyAccountFields fields) {
      return MoneyAccountData.GetMoneyAccounts(fields);
    }

    #endregion Public methods

  } // class MoneyAccountDebit

} // namespace Empiria.Trade.Financial.MoneyAccount
