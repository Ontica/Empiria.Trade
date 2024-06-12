/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : MoneyAccount Management                    Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Financial                    Pattern   : Information Holder                      *
*  Type     : MoneyAccountTransactionItemsFields         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Input DTO for MoneyAccount Transaction Items.                                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.StateEnums;


namespace Empiria.Trade.Financial {
  /// Input DTO for MoneyAccount Transaction Items.
  public class MoneyAccountTransactionItemFields {

    #region Constructors and parsers

    public MoneyAccountTransactionItemFields() {
      // Required by Empiria Framework.
    }

    #endregion Constructors and parsers

    #region Public properties

    public string ItemTypeUID {
      get; set;
    }

    public string PaymentTypeUID {
      get; set;
    }

    public string Reference {
      get; set;
    } = string.Empty;

    public decimal Deposit {
      get; set;
    } = 0m;

    public decimal Withdrawal {
      get; set;
    } = 0m;

    public string Notes {
      get; set;
    } = string.Empty;

    public DateTime TransactionTime {
      get; set;
    }

    #endregion Public properties


  } // class MoneyAccountTransactionItemFields

} // namespace Empiria.Trade.Financial
