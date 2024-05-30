/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : MoneyAccount Management                    Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Sales.dll                    Pattern   : Data Transfer Object                    *
*  Type     : MoneyAccountTransactionItemDto             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return moneyAccount transaction item.                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using Empiria.StateEnums;

namespace Empiria.Trade.Financial.Adapters {
  /// Output DTO used to return moneyAccount transaction item.
  public class MoneyAccountTransactionItemDto {

    public string UID {
      get; internal set;
    }

    public string MoneyAccountTransactionItemType {
      get; internal set;
    }

    public string Reference {
      get; internal set;
    }

    public string PaymentType {
      get; internal set;
    }

    public decimal Deposit {
      get; internal set;
    }

    public decimal Withdrawal {
      get; internal set;
    }

    public DateTime PostedTime {
      get; internal set;
    }

    public string Notes {
      get; internal set;
    }

    public EntityStatus Status {
      get; set;
    }

  } // class MoneyAccountTransactionItemDto

} // namespace Empiria.Trade.Financial.MoneyAccount.Adapters
