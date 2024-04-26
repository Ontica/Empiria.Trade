﻿/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : MoneyAccount Management                    Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Financial.dll                Pattern   : Data Transfer Object                    *
*  Type     : MoneyAcountDto                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to returnmoney accounts info.                                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;


namespace Empiria.Trade.Financial.Adapters {
  ///  Output DTO used to returnmoney accounts info.  
  public class MoneyAccountDto {

    public string MoneyAccountNumber {
      get; internal set;
    }

    public string MoneyAccountType {
      get; internal set;
    }

    public string MoneyAccountOwner {
      get; internal set;
    }

    public decimal MoneyAccountLimit {
      get; internal set;
    }

    public decimal Balance {
      get; internal set;
    }

    public string Status {
      get; internal set;
    }

    public FixedList<MoneyAccountTransactionDto> Transactions {
      get; internal set;
    }


  } // class MoneyAccountDto
} // namespace Empiria.Trade.Financial.Adapters
