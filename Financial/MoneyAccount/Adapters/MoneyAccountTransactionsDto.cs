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
using Empiria.StateEnums;


namespace Empiria.Trade.Financial.Adapters {
  ///  Output DTO used to return moneyAccount transactions.
  public class MoneyAccountTransactionDto {

    public string UID {
      get; internal set;
    }

    public string MoneyAccountUID {
      get; internal set;
    }

    public string TransactionNumber {
      get; internal set;
    }

    public NamedEntityDto TransactionType {
      get; internal set;
    }

    public DateTime TransactionDate {
      get; internal set;
    }

    public string Reference {
      get; internal set;
    }

    public decimal TransactionAmount {
      get; internal set;
    }

    //public decimal DebitAmount {
    //  get; internal set;
    //}

    public DateTime DueDate {
      get; internal set;
    }

    public string Notes {
      get; internal set;
    }

    public EntityStatus Status {
      get; set;
    } 

    public FixedList<MoneyAccountTransactionItemDto> Items {
      get; set;
    }

  } // class MoneyAccountTransactionsDto 

  public class MoneyAccountTransactionDescriptorDto {

    public string UID {
      get; internal set;
    }

    public string MoneyAccountUID {
      get; internal set;
    }

    public string TransactionNumber {
      get; internal set;
    }

    public string TransactionType {
      get; internal set;
    }

    public DateTime TransactionDate {
      get; internal set;
    }

    public string Reference {
      get; internal set;
    }

    public decimal TransactionAmount {
      get; internal set;
    }

   

    public DateTime DueDate {
      get; internal set;
    }

    public string Notes {
      get; internal set;
    }

    public EntityStatus Status {
      get; set;
    }

   
  } // class MoneyAccountTransactionsDto

} // namespace Empiria.Trade.Financial.Adapters 
