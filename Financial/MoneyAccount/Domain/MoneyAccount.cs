/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Financial Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Financial.dll                Pattern   : Partitioned Type / Information Holder   *
*  Type     : MoneyAccount                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a MoneyAccount Transactions.                                                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.StateEnums;
using Empiria.Trade.Financial.Adapters;
using Empiria.Trade.Financial.Data;

namespace Empiria.Trade.Financial {
  /// <summary>Represents a MoneyAccount Transactions. </summary>
  public class MoneyAccount : BaseObject {

    #region Constructors and parsers

    public MoneyAccount() {
      //no-op
    }

    public MoneyAccount(MoneyAccountFields fields) {
      Update(fields);
    }

    static public MoneyAccount Empty => BaseObject.ParseEmpty<MoneyAccount>();

    static public MoneyAccount Parse(int id) {
      return BaseObject.ParseId<MoneyAccount>(id);
    }

    static public MoneyAccount Parse(string uid) {
      return BaseObject.ParseKey<MoneyAccount>(uid);
    }

    #endregion Constructors and parsers

    #region Public properties

    [DataField("Description")]
    public string Description {
      get; internal set;
    }

    [DataField("OwnerId")]
    public int OwnerId {
      get; internal set;
    }

    [DataField("Notes")]
    public string Notes {
      get; private set;
    }

    [DataField("ExData", Default = "")]
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

    [DataField("FromDate")]
    public DateTime FromDate {
      get; internal set;
    } = new DateTime(1980, 01, 01);

    [DataField("ToDate")]
    public DateTime ToDate {
      get; set;
    } = new DateTime(2078,12,31);


    [DataField("CreditLimit")]
    public decimal CreditLimit {
      get; private set;
    } = 0m;

    [DataField("DayToPay")]
    public int DaysToPay {
      get; private set;
    }

    [DataField("Status", Default = EntityStatus.Active)]
    public EntityStatus Status {
      get; private set;
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

    #endregion Public methods

    #region Private methods
   

    #endregion Private methods
  }

}
