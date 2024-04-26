/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : MoneyAccount Management                    Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Sales.dll                    Pattern   : Data Transfer Object                    *
*  Type     : MoneyAccountTransactionDto                 License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return moneyAccount transactions.                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;


namespace Empiria.Trade.Financial.Adapters {
  ///  Output DTO used to return moneyAccount transactions.
  public class MoneyAccountTransactionDto {

    public string UID {
      get; internal set;
    }

    public string OperationNumber {
      get; internal set;
    }

    public string OperationType {
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
        
  } // class MoneyAccountTransactionsDto 

} // namespace Empiria.Trade.Financial.Adapters 
