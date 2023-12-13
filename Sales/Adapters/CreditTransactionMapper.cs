﻿/* Empiria Trade *********************************************************************************************
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


namespace Empiria.Trade.Sales.Adapters {
  /// <summary>Methods used to map CreditTransaction. </summary>
  static public class CreditTransactionMapper {

    static public CreditTransactionDto Map(CreditTransaction creditTransaction) {
      var dto = new CreditTransactionDto {
        TicketNumber = creditTransaction.ExtData,
       TransactionDate = creditTransaction.TransactionTime,
       CreditAmount = creditTransaction.CreditAmount,
       DebitAmount = creditTransaction.DebitAmount,
       DueDate = creditTransaction.DueDate,
       DaysToPay = creditTransaction.DaysToPay
      };

      return dto;
    }
  } // namespace Empiria.Trade.Sales.Adapters

} // class CreditTransactionMapper
