/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Order Management                           Component : Data Layer                              *
*  Assembly : Empiria.Trade.Order.dll                    Pattern   : Data Service                            *
*  Type     : CreditLineData                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data for Customer Credit Lines.                                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using Empiria.Data;
using Empiria.DataTypes;

using Empiria.Trade.Financial.Adapters;

namespace Empiria.Trade.Financial.Data {
  /// <summary>Provides data for Customer Credit Lines.  </summary>
  static internal class CrediLineData {

    static internal decimal GetCreditDebt(int customerId) {
      int creditLineId = GetCreditLineId(customerId);

      var sql = "SELECT SUM(debitAmount) AS Debit FROM TRDCreditTransactions " +
               $"WHERE CreditLineId = {creditLineId}";
      
      var dataOperation = DataOperation.Parse(sql);

      var debt = Empiria.Data.DataReader.GetScalar<decimal>(dataOperation);

      return debt;
    }

    static internal decimal GetCreditLimit(int customerId) {
      var sql = "SELECT CreditLimit FROM TRDCreditLines " +
               $"WHERE customerId = {customerId}";


      var dataOperation = DataOperation.Parse(sql);

      var debt = Empiria.Data.DataReader.GetScalar<decimal>(dataOperation);

      return debt;
    }

    static internal int GetCreditLineId(int customerId) {
      var sql = "SELECT * FROM TRDCreditLines " +
               $"WHERE customerId = {customerId}";


      var dataOperation = DataOperation.Parse(sql);

      var debt = Empiria.Data.DataReader.GetScalar<int>(dataOperation);

      return debt;
    }

    static internal string GetCreditConditions(int customerId) {
      var sql = "SELECT CreditConditions FROM TRDCreditLines " +
               $"WHERE customerId = {customerId}";


      var dataOperation = DataOperation.Parse(sql);

      var creditCondition = Empiria.Data.DataReader.GetScalar<string>(dataOperation);

      return creditCondition;
    }

  } // class CrediLineData

} // namespace Empiria.Trade.Sales.Data

