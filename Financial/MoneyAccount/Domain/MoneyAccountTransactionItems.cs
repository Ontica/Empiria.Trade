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
using Empiria.Trade.Core;
using Empiria.Trade.Financial.Adapters;
using Empiria.Trade.Financial.Data;


namespace Empiria.Trade.Financial {
  ///Represents money account transaction item.
  internal class MoneyAccountTransactionItems : BaseObject {

    #region Constructors and parsers

    public MoneyAccountTransactionItems() {
      //no-op
    }

    public MoneyAccountTransactionItems(MoneyAccountTransactionFields fields) {
      //Update(fields);
    }

    static public MoneyAccountTransactionItems Empty => BaseObject.ParseEmpty<MoneyAccountTransactionItems>();

    static public MoneyAccountTransactionItems Parse(int id) {
      return BaseObject.ParseId<MoneyAccountTransactionItems>(id);
    }

    static public MoneyAccountTransactionItems Parse(string uid) {
      return BaseObject.ParseKey<MoneyAccountTransactionItems>(uid);
    }


    #endregion Constructors and parsers

    #region Public properties

    [DataField("MoneyAccountTransactionItemId")] 
    public int MoneyAccountTransactionItemId {
      get; private set;
    }

    [DataField("MoneyAccountTransactionItemUID")]
    public string MoneyAccountTransactionItemUID {
      get; private set;
    }

    [DataField("MoneyAccountTransactionId")]
    public int MoneyAccountTransactionId {
      get; private set;
    }

    [DataField("MoneyAccountTransactionItemTypeId")]
    public int MoneyAccountTransactionItemTypeId {
      get; private set;
    }

    [DataField("PaymentTypeId")]
    public int PaymentTypeId {
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


    public int AddedById {
      get; private set;
    } = 0;

    [DataField("MoneyAccountTransactionItemStatus")]
    public EntityStatus MoneyAccountTransactionItemStatus {
      get; private set;
    } = EntityStatus.Active;


    #endregion Public properties     
    

    #region Public methods

    #endregion Public methods


    #region Private methods
    #endregion Public methods

  } // class MoneyAccountTransactionItems


} // namespace Empiria.Trade.Financial
