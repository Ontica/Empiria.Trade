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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

} // namespace Empiria.Trade.Financial.Adapters
