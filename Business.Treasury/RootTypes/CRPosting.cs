﻿/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Solution  : Empiria Trade                                    System   : Treasury System                   *
*  Namespace : Empiria.Treasury                                 Assembly : Empiria.Treasury.dll              *
*  Type      : CRPosting                                        Pattern  : Standard Class                    *
*  Version   : 2.2                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Represents a Cash Register Transaction Posting.                                               *
*                                                                                                            *
********************************* Copyright (c) 2002-2017. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Data;

using Empiria.DataTypes;

namespace Empiria.Treasury {

  public class CRPosting : BaseObject {

    #region Fields

    private CRTransaction transaction = CRTransaction.Empty;
    private InstrumentType instrumentType = InstrumentType.Empty;
    private int instrumentId = -1;
    private CRDocument document = CRDocument.Empty;
    private Currency currency = Currency.Default;
    private decimal instrumentAmount = 0m;
    private decimal inputAmount = 0m;
    private decimal outputAmount = 0m;
    private TreasuryItemStatus status = TreasuryItemStatus.Pending;

    #endregion Fields

    #region Constructors and parsers

    private CRPosting() {
      // Required by Empiria Framework.
    }

    internal CRPosting(CRTransaction transaction, InstrumentType instrumentType,
                       CRDocument document, decimal amount) {
      this.transaction = transaction;
      this.instrumentType = instrumentType;
      this.instrumentId = -1;
      this.document = document;
      this.instrumentAmount = amount;
      if (this.transaction.TransactionType.UniqueCode.StartsWith("Input.")) {
        this.inputAmount = amount;
      } else {
        this.outputAmount = amount;
      }
    }

    internal CRPosting(CRTransaction transaction, InstrumentType instrumentType,
                       IFinancialAccount account, decimal amount) {
      this.transaction = transaction;
      this.instrumentType = instrumentType;
      this.instrumentId = account.Id;
      this.instrumentAmount = amount;
      if (this.transaction.TransactionType.UniqueCode.StartsWith("Input.")) {
        this.inputAmount = amount;
      } else {
        this.outputAmount = amount;
      }
    }

    static public CRPosting Parse(int id) {
      return BaseObject.ParseId<CRPosting>(id);
    }

    static public CRPosting Empty {
      get { return BaseObject.ParseEmpty<CRPosting>(); }
    }

    #endregion Constructors and parsers

    #region Public properties

    public CRTransaction Transaction {
      get { return transaction; }
    }

    public InstrumentType InstrumentType {
      get { return instrumentType; }
    }

    public int InstrumentId {
      get { return instrumentId; }
      set { instrumentId = value; }
    }

    public Currency Currency {
      get { return currency; }
    }

    public CRDocument Document {
      get { return document; }
    }

    public decimal InstrumentAmount {
      get { return instrumentAmount; }
    }

    public decimal InputAmount {
      get { return inputAmount; }
    }

    public decimal OutputAmount {
      get { return outputAmount; }
    }

    public TreasuryItemStatus Status {
      get { return status; }
      internal set { status = value; }
    }

    #endregion Public properties

    #region Public methods

    public CRDocument CreateDocument(InstrumentType instrumentType) {
      this.document = new CRDocument(instrumentType);

      return this.Document;
    }

    protected override void OnLoadObjectData(DataRow row) {
      this.transaction = CRTransaction.Parse((int) row["TransactionId"]);
      this.instrumentType = InstrumentType.Parse((int) row["InstrumentTypeId"]);
      this.instrumentId = (int) row["InstrumentId"];
      this.document = CRDocument.Parse((int) row["DocumentId"]);
      this.currency = Currency.Parse((int) row["CurrencyId"]);
      this.instrumentAmount = (decimal) row["CurrencyAmount"];
      this.inputAmount = (decimal) row["InputAmount"];
      this.outputAmount = (decimal) row["OutputAmount"];
      this.status = (TreasuryItemStatus) Convert.ToChar(row["PostingStatus"]);
    }

    protected override void OnSave() {
      if (!document.IsEmptyInstance) {
        document.Save();
      }
    }

    #endregion Public methods

  } // class CRPosting

} // namespace Empiria.Treasury
