/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Money Account Management                   Component : Domain Layer                            *
*  Assembly : Empiria.Trade.dll                          Pattern   : Partitioned Type / Information Holder   *
*  Type     : credit Money Account                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents credit money account.                                                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.StateEnums;
using Empiria.Trade.Financial.Adapters;
using Empiria.Trade.Financial.Data;
using Empiria.Trade.Orders;
using Empiria.Trade.Core;

namespace Empiria.Trade.Financial {
  ///Represents credit money account.
  public class MoneyAccount : BaseObject {

    #region Constructors and parsers

    public MoneyAccount() {
      //no-op
    }

    public MoneyAccount(MoneyAccountFields fields) {
      Update(fields);
    }

    static public MoneyAccount Empty => BaseObject.ParseEmpty<MoneyAccount>();

    public static MoneyAccount Parse(int id) {
      return BaseObject.ParseId<MoneyAccount>(id);
    }

    static public MoneyAccount Parse(string uid) {
      return BaseObject.ParseKey<MoneyAccount>(uid);
    }

    static public MoneyAccount ParseByOwner(int ownerId) {
      return MoneyAccountData.GetMoneyAccount(ownerId);
    }

    #endregion Constructors and parsers

    #region Public properties

    [DataField("MoneyAccountTypeId")]
    public MoneyAccountType MoneyAccountType {
      get; protected set;
    } 

    [DataField("MoneyAccountDescription")]
    public string Description {
      get; protected set;
    }

    [DataField("MoneyAccountNumber")]
    public string Number {
      get; protected set;
    }

    [DataField("OwnerId")]
    public Party Owner {
      get; protected set;
    }

    [DataField("Notes")]
    public string Notes {
      get; protected set;
    }

    
    public string Keywords {
      get {
        return EmpiriaString.BuildKeywords(Number, Owner.Name);
      }
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

    public FixedList<MoneyAccountTransaction> MoneyAccountTransactions {
      get; private set;
    } = new FixedList<MoneyAccountTransaction>();


    #endregion Public properties

    #region Public methods

    protected override void OnSave() {
      if (IsNew) {
        Number = "MA-" + EmpiriaString.BuildRandomString(10).ToUpperInvariant();
      }

      MoneyAccountData.Write(this);
    }

    internal void Update(MoneyAccountFields fields) {
      this.MoneyAccountType = MoneyAccountType.Parse(fields.TypeUID);
      this.Description = this.MoneyAccountType.Name;
      this.Owner = Party.Parse(fields.OwnerUID);
      this.Notes = fields.Notes;
      this.PostedTime = DateTime.Now;
      this.PostedById = ExecutionServer.CurrentUserId;
      this.FromDate = DateTime.Now;
      this.CreditLimit = fields.MoneyAccountLimit;
      this.DaysToPay = fields.LimitDaysToPay;
    }

    public void Cancel() {
      this.Status = EntityStatus.Deleted;
      MoneyAccountData.Write(this);
    }

    public void Suspend() {
      this.Status = EntityStatus.Suspended;
      MoneyAccountData.Write(this);
    }

    public void Pending() {
      this.Status = EntityStatus.Pending;
      MoneyAccountData.Write(this);
    }

    public void Active() {
      this.Status = EntityStatus.Active;
      MoneyAccountData.Write(this);
    }

    public decimal GetDebit() {
      return MoneyAccountTransactionData.GetMoneyAccountTotalDebt(this.Id);
    }

    public FixedList<MoneyAccountTransaction> GetTransactions() {
      return MoneyAccountTransaction.GetTransactions(this.Id);
    }

    public FixedList<MoneyAccount> Search(SearchMoneyAccountFields fields) {
      return MoneyAccountData.GetMoneyAccounts(fields); 
    }

    public void LoadMoneyAccountTransactions() {
      this.MoneyAccountTransactions = MoneyAccountTransactionData.GetTransactions(this.Id);
    }


    #endregion Public methods

  } // class MoneyAccount
   

} // namespace Empiria.Trade.Financial

