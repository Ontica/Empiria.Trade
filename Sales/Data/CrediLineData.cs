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
using System.Net.NetworkInformation;
using Empiria.Data;
using Empiria.DataTypes;
using Empiria.Trade.Core;
using Empiria.Trade.Orders;
using Empiria.Trade.Sales.Adapters;

namespace Empiria.Trade.Sales.Data {
  /// <summary>Provides data for Customer Credit Lines.  </summary>
  internal class CrediLineData {

    static internal decimal GetCreditDebt(int customerId) {
      var sql = "SELECT InitialDebt FROM TRDCreditLines " +
               $"WHERE customerId = {customerId}";


      var dataOperation = DataOperation.Parse(sql);

      var debt = Empiria.Data.DataReader.GetScalar<decimal>(dataOperation);

      return debt;
    }

  } // class CrediLineData}

} // namespace Empiria.Trade.Sales.Data

