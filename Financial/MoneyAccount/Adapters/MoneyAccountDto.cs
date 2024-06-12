/* Empiria Trade *********************************************************************************************
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


    public string UID {
      get; internal set;
    }

    public string MoneyAccountNumber {
      get; internal set;
    }

    public NamedEntityDto MoneyAccountType {
      get; internal set;
    }

    public NamedEntityDto MoneyAccountOwner {
      get; internal set;
    }

    public decimal MoneyAccountLimit {
      get; internal set;
    }

    public decimal Balance {
      get; internal set;
    } = 0;

    public int LimitDaysToPay {
      get; internal set;
    } = 0;

    public string Notes {
      get; internal set;
    } = string.Empty;

    public NamedEntityDto Status {
      get; internal set;
    }

    public FixedList<MoneyAccountTransactionDescriptorDto> Transactions {
      get; internal set;
    } = new FixedList<MoneyAccountTransactionDescriptorDto>();

    public MoneyAccountActionsDto Actions {
      get; internal set;
    } = new MoneyAccountActionsDto();

  } // class MoneyAccountDto
} // namespace Empiria.Trade.Financial.Adapters
