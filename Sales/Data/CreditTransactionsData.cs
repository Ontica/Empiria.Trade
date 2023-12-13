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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Empiria.Data;

namespace Empiria.Trade.Sales.Data {
  /// <summary>Provides data for Credit Transactions.   </summary>
  static internal class CreditTransactionsData {

    internal static FixedList<CreditTransaction> GetCreditTrasantions(int CreditLineId) {
      string sql = "SELECT * FROM TRDCreditTransactions " +
                   $"WHERE CreditLineId = {CreditLineId} and CreditTransactionStatus <> 'X'";

      var op = DataOperation.Parse(sql);

      return DataReader.GetFixedList<CreditTransaction>(op);
    }
  } // class Empiria.Trade.Sales.Data

} //  namespace Empiria.Trade.Sales.Data 