/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Money Account Management                   Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Order.dll                    Pattern   : Mapper class                            *
*  Type     : MoneyAccountTransactionMapper              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Methods used to map money account transaction.                                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using Empiria.Trade.Financial.Data;

namespace Empiria.Trade.Financial.Adapters {
  /// Methods used to map money account transaction.
  static public class MoneyAccountTransactionMapper {

    static public MoneyAccountTransactionDto Map(MoneyAccountTransaction transaction) {
      var dto = new MoneyAccountTransactionDto {
        UID = transaction.UID,
        OperationNumber = transaction.ExtData,
        OperationType = transaction.TransactionType.Name,
        TransactionDate = transaction.TransactionTime,
        Reference = transaction.ExtData,
        TransactionAmount = transaction.Credit,
        //DebitAmount = transaction.Debit,
        Notes = transaction.Notes,
        Status = transaction.Status
      };
      return dto;
    }

    static public FixedList<MoneyAccountTransactionDto> MapMoneyAccountTransactions(FixedList<MoneyAccountTransaction> transactions) {
      List<MoneyAccountTransactionDto> creditTransactionList = new List<MoneyAccountTransactionDto>();

      foreach (var transaction in transactions) {
        creditTransactionList.Add(Map(transaction));
      }

      return creditTransactionList.ToFixedList();
    }



  } //  class MoneyAccountTransactionMapper

} // namespace Empiria.Trade.Financial.Adapters
