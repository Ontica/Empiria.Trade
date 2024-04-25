/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : MoneyAccount Management                    Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Financial                    Pattern   : Information Holder                      *
*  Type     : MoneyAccountFields                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Input DTO for MoneyAccount.                                                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.StateEnums;


namespace Empiria.Trade.Financial.Adapters {
  /// <summary>Input DTO for MoneyAccount. </summary>
  public class MoneyAccountFields {

    #region Constructors and parsers

    public MoneyAccountFields() {
      // Required by Empiria Framework.
    }

    #endregion Constructors and parsers

    #region Public properties

    public string UID {
      get; set;
    } = string.Empty;

           
    public string Notes {
      get; set;
    } = string.Empty;

    public char Status {
      get; set;
    } = 'A';

    public string Description {
      get; set;
    }

    public int OwnerId {
      get; set;
    }

    public decimal CreditLimit {
      get; set;
    }

    public int DaysToPay {
      get; set;
    }


    #endregion Public properties
  } //class MoneyAccountFields

  public class SearchMoneyAccountFields {

    public string Keywords {
      get; set;
    } = string.Empty;

    public string MoneyAccountTypeUID {
      get; set;
    } = string.Empty;

    public DateTime FromDate {
      get; set;
    } = new DateTime(1950, 1, 1);

    public DateTime ToDate {
      get; set;
    } = new DateTime(2049, 12, 31);

    public string Status {
      get; set;
    } = "All";

    public string CustomerUID {
      get; set;
    } = String.Empty;


  } // class SearchOrderFields


} // namespace Empiria.Trade.Financial.Adapters
