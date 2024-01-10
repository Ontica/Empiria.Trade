/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Sales Order Management                     Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Sales.dll                    Pattern   : Partitioned Type / Information Holder   *
*  Type     : SalesOrderHeleperv                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  :  Helper methods to Seles Order.                                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.Trade.Sales.Adapters;
using Empiria.Trade.Sales.Data;

namespace Empiria.Trade.Sales {

  /// <summary>Helper methods to Seles Order. </summary>
  public class SalesOrderHelper {

    public SalesOrderHelper() {

    }
    #region Public methods


    public FixedList<SalesOrder> GetOrders(SearchOrderFields fields) {
      var orders = SalesOrderData.GetSalesOrders(fields);

      foreach (var order in orders) {
       
        order.GetOrderTotal();
      }
      return orders;
    }

    public FixedList<SalesOrder> GetOrdersToAuthorize(SearchOrderFields fields) {
      var orders = SalesOrderData.GetSalesOrdersToAuthorize(fields);

      foreach (var order in orders) {
        order.SetCustomerCreditInfos();
        order.GetOrderTotal();
      }

      return orders;
    }

    public FixedList<SalesOrder> GetOrdersToPacking(SearchOrderFields fields) {
      var orders = SalesOrderData.GetSalesOrdersToPacking(fields);
      foreach (var order in orders) {
        order.GetWeightTotalPackageByOrder();
        order.GetOrderTotal();
      }

      return orders;
    }

    #endregion Public methods

    #region Private methods
  

    #endregion Private methods

  } // class SalesOrderHelper

} // Empiria.Trade.Sales