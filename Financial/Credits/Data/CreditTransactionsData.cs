﻿/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Order Management                           Component : Data Layer                              *
*  Assembly : Empiria.Trade.Order.dll                    Pattern   : Data Service                            *
*  Type     : CreditTransactionsData                     License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data for Credit Transactions.                                                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;

using Empiria.Data;


namespace Empiria.Trade.Financial.Data {
  /// <summary>Provides data for Credit Transactions.   </summary>
  static internal class CreditTransactionsData {

    internal static FixedList<CreditTransaction> GetCreditTrasantions(int CreditLineId) {
      string sql = "SELECT * FROM TRDCreditTransactions " +
                   $"WHERE CreditLineId = {CreditLineId} and CreditTransactionStatus <> 'X'";

      var op = DataOperation.Parse(sql);

      return DataReader.GetFixedList<CreditTransaction>(op);
    }

    internal static CreditTransaction GetCreditTrasantion(int orderId) {
      string sql = "SELECT * FROM TRDCreditTransactions " +
                   $"WHERE PayableOrderId = {orderId} and CreditTransactionStatus <> 'X'";

      var op = DataOperation.Parse(sql);

      return DataReader.GetObject<CreditTransaction>(op);
    }

    static internal void Write(CreditTransaction o) {
   
      var op = DataOperation.Parse("writeCreditTransactions", o.Id,o.UID,o.TypeId,o.CreditLineId,  o.TransactionTime,o.CreditAmount,
                                                            o.DebitAmount,o.PayableOrderId,o.DueDate,o.DaysToPay,o.Notes, o.ExtData,
                                                            o.AuthorizatedById, (char)o.Status);
                                  
      DataWriter.Execute(op);
    }

    
  } // class Empiria.Trade.Sales.Data

} //  namespace Empiria.Trade.Sales.Data 
