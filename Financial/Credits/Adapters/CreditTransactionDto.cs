/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Order Management                           Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Sales.dll                    Pattern   : Data Transfer Object                    *
*  Type     : CreditTransactionto                        License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return creditTransaction.                                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Financial.Adapters {
  /// <summary>Output DTO used to return creditTransaction.</summary>
  public class CustomerCreditDto {

    public decimal TotalDebt {
      get; internal set;
    }

    public decimal CreditLimit {
      get; internal set;
    }

    public FixedList<CreditTransactionDto> CreditTransactions {
      get; internal set;
    }

  } //  class CustomerCreditDto

  public class CreditTransactionDto {

    public string TicketNumber {
      get; internal set;
    }

    public DateTime TransactionDate {
      get; internal set;
    }

    public decimal CreditAmount {
      get; internal set;
    }

    public decimal DebitAmount {
      get; internal set;
    }

    public DateTime DueDate {
      get; internal set;
    }

    public int DaysToPay {
      get; internal set;
    }
    
  } // class CreditTransactionDto

} // namespace Empiria.Trade.Sales.Adapters
