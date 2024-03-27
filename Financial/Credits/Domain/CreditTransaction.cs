/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Sales Order Management                     Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Sales.dll                    Pattern   : Partitioned Type / Information Holder   *
*  Type     : CreditTansactions                          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a credit Transactions.                                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.StateEnums;

using Empiria.Trade.Financial.Adapters;
using Empiria.Trade.Financial.Data;


namespace Empiria.Trade.Financial {
  /// <summary>Represents a credit Transactions. </summary>
  public class CreditTransaction : BaseObject {

    #region Constructors and parsers

    public CreditTransaction() {
      //no-op
    }

    public CreditTransaction(CreditTrasnactionFields fields) {
      Update(fields);
    }

    static public CreditTransaction Empty => BaseObject.ParseEmpty<CreditTransaction>();

    static public CreditTransaction Parse(int id) {
      return BaseObject.ParseId<CreditTransaction>(id);
    }

    static public CreditTransaction Parse(string uid) {
      return BaseObject.ParseKey<CreditTransaction>(uid);
    }

    static public CreditTransaction ParseByOrderId(int orderId) {
      return CreditTransactionsData.GetCreditTrasantion(orderId);
    }

    #endregion Constructors and parsers

    #region Public properties


    [DataField("CreditTransactionTypeId")]
    public int TypeId {
      get; private set;
    }

    [DataField("CreditLineId")]
    public int CreditLineId {
      get; private set;
    }

    [DataField("TransactionTime")]
    public DateTime TransactionTime {
      get; private set;
    }

    [DataField("CreditAmount")]
    public Decimal CreditAmount {
      get; private set;
    } = 0m;

    [DataField("DebitAmount")]
    public Decimal DebitAmount {
      get; private set;
    } = 0m;

    [DataField("PayableOrderId")]
    public int PayableOrderId {
      get; private set;
    }

    [DataField("DueDate")]
    public DateTime DueDate {
      get; private set;
    }

    [DataField("DaysToPay")]
    public int DaysToPay {
      get; private set;
    }

    [DataField("CreditTransactionExtData", Default = "")]
    public string ExtData {
      get; private set;
    } = string.Empty;

    [DataField("CreditTransactionStatus", Default = EntityStatus.Active)]
    public EntityStatus Status {
      get; private set;
    } = EntityStatus.Active;

    #endregion Public properties

    #region Public methods

    protected override void OnSave() {
      CreditTransactionsData.Write(this);
    }

    public void Update(CreditTrasnactionFields fields) {

      this.TypeId = fields.TypeId;
      this.CreditLineId = GetCreditLindId(fields.CustomerId);
      this.TransactionTime = fields.TransactionTime;
      this.CreditAmount = fields.CreditAmount;
      this.DebitAmount = this.CreditAmount;
      this.PayableOrderId = fields.PayableOrderId;
      this.DueDate = GetDueDate(fields.CustomerId);
      this.DaysToPay = GetCreditCondition(fields.CustomerId);
      this.ExtData = fields.ExtData;
      this.Status = EntityStatus.Active;
    }

    public void Cancel() {
      Status = EntityStatus.Deleted;

      this.Save();
    }

    public static FixedList<CreditTransaction> GetCreditTransactions(int customerId) {

      var creditLineId = CrediLineData.GetCreditLineId(customerId);
      var creditTransactions = CreditTransactionsData.GetCreditTrasantions(creditLineId);

      return creditTransactions;
    }

    internal static decimal GetCustomerTotalDebt(int customerId) {
      return CrediLineData.GetCreditDebt(customerId);
    }

    internal static decimal GetCreditLimit(int customerId) {
      return CrediLineData.GetCreditLimit(customerId);
    }

    internal static int GetCreditLindId(int customerId) {
      return CrediLineData.GetCreditLineId(customerId);
    }


    #endregion Public methods

    #region Private methods

    private int GetCreditCondition(int customerId) {
      var conditions =  CrediLineData.GetCreditConditions(customerId);

      return Convert.ToInt32(conditions);
    }

    private DateTime GetDueDate(int customerId) {
      int creditConditionDays = GetCreditCondition(customerId);
      return this.TransactionTime.AddDays(creditConditionDays);
    }

    #endregion Private methods

  } // class CreditTransactions

} // namespace Empiria.Trade.Sales
