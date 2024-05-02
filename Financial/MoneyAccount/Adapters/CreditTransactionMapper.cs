/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Order Management                           Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Order.dll                    Pattern   : Mapper class                            *
*  Type     : CreditTransactionMapper                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Methods used to map CreditTransaction.                                                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using System.Collections.Generic;


namespace Empiria.Trade.Financial.Adapters
{
    /// <summary>Methods used to map CreditTransaction. </summary>
    static public class CreditTransactionMapper
    {
          
    static public CreditTransactionDto Map(MoneyAccountTransaction transaction, int daysToPay) {
      var dto = new CreditTransactionDto {
        TicketNumber = transaction.ExtData,
        TransactionDate = transaction.TransactionTime,
        CreditAmount = transaction.Credit,
        DebitAmount = transaction.Credit,
        DueDate = transaction.TransactionTime.AddDays(daysToPay),
        DaysToPay = GetDaysToPay(transaction.TransactionTime.AddDays(daysToPay))
      };

      return dto;
    }
       

    static public FixedList<CreditTransactionDto> MapCreditTransactions(FixedList<MoneyAccountTransaction> creditTransactions, int daysToPay) {
      List<CreditTransactionDto> creditTransactionList = new List<CreditTransactionDto>();

      foreach (var creditTransaction in creditTransactions) {
        creditTransactionList.Add(Map(creditTransaction, daysToPay));
      }

      return creditTransactionList.ToFixedList();
    }

    static private int GetDaysToPay(DateTime dueDate)
        {
            var difOfDates = dueDate - DateTime.Today;

            return difOfDates.Days;
        }

   

    } // namespace Empiria.Trade.Sales.Adapters

} // class CreditTransactionMapper
