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
using Empiria.Trade.Orders;
using Empiria.Trade.Sales.Data;
using static System.TimeZoneInfo;

namespace Empiria.Trade.Sales {
  /// <summary>Represents a credit Transactions. </summary>
  public class CreditTransaction : BaseObject {

    #region Constructors and parsers

    public CreditTransaction() {
      //no-op
    }

    static public CreditTransaction Empty => BaseObject.ParseEmpty<CreditTransaction>();

    static public CreditTransaction Parse(int id) {
      return BaseObject.ParseId<CreditTransaction>(id);
    }

    static public CreditTransaction Parse(string uid) {
      return BaseObject.ParseKey<CreditTransaction>(uid);
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
    public char Status {
      get; private set;
    }

    #endregion Public properties

    #region Public methods

    public static FixedList<CreditTransaction> GetCreditTransactions(int customerId) {

      var creditLineId = Empiria.Trade.Sales.Data.CrediLineData.GetCreditLineId(customerId);
      var creditTransactions = CreditTransactionsData.GetCreditTrasantions(creditLineId);

      return creditTransactions;
    }

    #endregion Public methods

  } // class CreditTransactions

} // namespace Empiria.Trade.Sales
