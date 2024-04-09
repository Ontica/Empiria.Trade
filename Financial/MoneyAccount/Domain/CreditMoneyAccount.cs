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
using Empiria.Trade.Financial.Adapters;
using Empiria.Trade.Financial.Data;
using Empiria.Trade.MoneyAccounts;

namespace Empiria.Trade.Financial {
  /// <summary>Represents credit money account. </summary>
  public class CreditMoneyAccount : Empiria.Trade.MoneyAccounts.MoneyAccount {
    #region Constructors and parsers

    public CreditMoneyAccount() {
      //no-op
    }

    public CreditMoneyAccount(MoneyAccountFields fields) {
      Update(fields);
    }


    static public new CreditMoneyAccount Parse(int id) {
      return BaseObject.ParseId<CreditMoneyAccount>(id);
    }

    static public new CreditMoneyAccount Parse(string uid) {
      return BaseObject.ParseKey<CreditMoneyAccount>(uid);
    }

    #endregion Constructors and parsers


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

    public string MigrateCreditLineToMoneyAccount() {
      var creditLines = CrediLineData.GetCreditLines();

      int count = 0;
      foreach (var creditLine in creditLines) {
        CreditMoneyAccount moneyAccount = new CreditMoneyAccount {
          Description = "Linea de Credito",
          OwnerId = creditLine.CustomerId,
          Notes = creditLine.CreditLineNotes,
          PostedTime = Convert.ToDateTime("01-01-1980"),
          PostedById = -1,
          FromDate = Convert.ToDateTime("01-01-1980"),
          ToDate = Convert.ToDateTime("31-12-2070"),
          CreditLimit = creditLine.CreditLimit,
          DaysToPay = Convert.ToInt32(creditLine.CreditConditions),
          Status = EntityStatus.Active
        };
        moneyAccount.Save();

        count++;
      }
      return count.ToString();
    }

    #endregion Public methods

  } // class MoneyAccountDebit
} // namespace Empiria.Trade.Financial.MoneyAccount
