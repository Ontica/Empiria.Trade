/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : MoneyAccount Management                    Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Financial                    Pattern   : Information Holder                      *
*  Type     : MoneyAccountTransactionFields              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Input DTO for MoneyAccount Transaction.                                                        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Financial.Adapters {
  /// <summary>Input DTO for MoneyAccount Transaction.   </summary>
  public class MoneyAccountTransactionFields {

    #region Constructors and parsers

    public MoneyAccountTransactionFields() {
      // Required by Empiria Framework.
    }

    #endregion Constructors and parsers

    #region Public properties

    public string TransactionTypeUID {
      get; set;
    }

    public Decimal TransactionAmount {
      get; set;
    } = 0m;

    public string Reference {
      get; set;
    } = string.Empty;

    public DateTime TransactionTime {
      get; set;
    } = DateTime.Now;

    public string Notes {
      get; set;
    } = string.Empty;
        
    #endregion Public properties

  } // class MoneyAccountTransactionFields

} // namespace Empiria.Trade.Financial.Adapters 

