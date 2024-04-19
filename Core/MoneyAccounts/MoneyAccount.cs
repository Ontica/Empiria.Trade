/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Money Account Management                   Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Order.dll                    Pattern   : Partitioned Type / Information Holder   *
*  Type     : MoneyAccount                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a money account.                                                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.StateEnums;


namespace Empiria.Trade.MoneyAccounts {

  /// Represents a money account.
  abstract public class MoneyAccount : BaseObject {

    #region Constructors and parsers
    public MoneyAccount() {
      //no-op
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

    [DataField("MoneyAccountTypeId")]
    public int TypeId {
      get; protected set;
    } = 1;

    [DataField("MoneyAccountDescription")]
    public string Description {
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

  } // class MoneyAccount

} // namespace Empiria.Trade.MoneyAccounts
