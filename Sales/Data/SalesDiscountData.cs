/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Discount Management                        Component : Data Layer                              *
*  Assembly : Empiria.Trade.Order.dll                    Pattern   : Data Service                            *
*  Type     : DiscountData                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data layer for Sales Discounts.                                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using Empiria.Data;

namespace Empiria.Trade.Sales.Data {

  /// <summary>Provides data layer for Sales Discounts. </summary>
  internal static class SalesDiscountData {

    #region Internal methods

    internal static FixedList<SalesDiscount> GetSalesDiscounts(int DiscountTypeId, int targetId, DateTime orderDate) {
      var date = orderDate.ToString("yyyy-dd-MM");   
            
      var sql = "SELECT * FROM TRDDiscounts " +
            $"WHERE (DiscountTypeId = {DiscountTypeId}) AND (DiscountAppliedListId like '%{targetId}%') AND " +
            $"((DiscountFromDate <= '{date}') AND (DiscountToDate>= '{date}'))and DiscountStatus = 'A' " +
            "ORDER BY DiscountId";


      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetFixedList<SalesDiscount>(dataOperation);
    }

    #endregion Internal methods

  } //  class SalesDiscountData

} // Empiria.Trade.Sales.Data
