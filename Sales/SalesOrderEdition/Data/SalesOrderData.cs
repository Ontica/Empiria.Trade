/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Order Management                           Component : Data Layer                              *
*  Assembly : Empiria.Trade.Order.dll                    Pattern   : Data Service                            *
*  Type     : OrderData                                  License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data layer for Orders.                                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Data;
using Empiria.DataTypes;
using Empiria.Trade.Core;
using Empiria.Trade.Orders;
using Empiria.Trade.Sales.Adapters;

namespace Empiria.Trade.Sales.Data {

    /// <summary>Provides data layer for Orders.</summary>
    static internal class SalesOrderData {


    #region Internal methods

    internal static FixedList<SalesOrder> GetSalesOrders(SearchOrderFields fields) {
      var toDate = fields.ToDate.ToString("yyyy-dd-MM");
      var fromDate = fields.FromDate.ToString("yyyy-dd-MM");

      string keywords = string.Empty;
      string status = string.Empty;

      if (fields.Keywords != string.Empty) {
        keywords = $" {SearchExpression.ParseAndLikeKeywords("OrderKeywords", fields.Keywords)} AND ";
      }

      if (fields.Status != Orders.OrderStatus.Empty) {
       status = $" AND (OrderStatus = '{(char)fields.Status}')";
      } else {
        status = $" AND (OrderStatus <> '{(char) Orders.OrderStatus.Cancelled}')";
      }



      var sql = "SELECT * FROM TRDOrders " +
                 $"WHERE {keywords}  (orderTime >= CONVERT(SMALLDATETIME, '{fromDate}') AND " +
                 $"orderTime <= CONVERT(SMALLDATETIME,'{toDate}')) {status} ";
      

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetFixedList<SalesOrder>(dataOperation);      
    }

    internal static FixedList<SalesOrder> GetSalesOrdersToAuthorize(SearchOrderFields fields) {
      var toDate = fields.ToDate.ToString("yyyy-dd-MM");
      var fromDate = fields.FromDate.ToString("yyyy-dd-MM");

      string keywords = string.Empty;
      string status = string.Empty;

      if (fields.Keywords != string.Empty) {
        keywords = $" {SearchExpression.ParseAndLikeKeywords("OrderKeywords", fields.Keywords)} AND ";
      }

      if (fields.Status != Orders.OrderStatus.Empty) {
        if (fields.Status == OrderStatus.Authorized) {
          status = " AND (OrderAuthorizationStatus = 'A')";
        }
        if (fields.Status == OrderStatus.Pending) {
          status = " AND (OrderAuthorizationStatus = 'P')";
        }
      } else {
        status = " AND ((OrderAuthorizationStatus = 'A') or (OrderAuthorizationStatus = 'P'))";
      }

      var sql = "SELECT * FROM TRDOrders " +
                $"WHERE {keywords}  (orderTime >= CONVERT(SMALLDATETIME, '{fromDate}') AND " +
                $"orderTime <= CONVERT(SMALLDATETIME,'{toDate}'))  {status} ";


      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetFixedList<SalesOrder>(dataOperation);
    }

    internal static FixedList<SalesOrder> GetSalesOrdersToPacking(SearchOrderFields fields) {
      var toDate = fields.ToDate.ToString("yyyy-dd-MM");
      var fromDate = fields.FromDate.ToString("yyyy-dd-MM");

      string keywords = string.Empty;
      string status = string.Empty;

      if (fields.Keywords != string.Empty) {
        keywords = $" {SearchExpression.ParseAndLikeKeywords("OrderKeywords", fields.Keywords)} AND ";
      }

      if (fields.Status != Orders.OrderStatus.Empty) {
        status = $" AND (OrderAuthorizationStatus = '{(char) fields.Status}')";
      } else {
        status = " AND ((OrderAuthorizationStatus = 'I') or (OrderAuthorizationStatus = 'U') or (OrderAuthorizationStatus = 'Y'))";
      }

      var sql = "SELECT * FROM TRDOrders " +
               $"WHERE {keywords}  (orderTime >= CONVERT(SMALLDATETIME, '{fromDate}') AND " +
               $"orderTime <= CONVERT(SMALLDATETIME,'{toDate}'))  {status} ";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetFixedList<SalesOrder>(dataOperation);

    }

    internal static void Write(SalesOrder o) {
        var op = DataOperation.Parse("writeOrder", o.Id, o.UID, o.OrderTypeId, o.Customer.Id, o.Supplier.Id,
                                    o.SalesAgent.Id, o.OrderNumber, o.OrderTime, o.Notes,
                                    o.Keywords, o.ExtData.ToString(), (char)o.Status, (char)o.AuthorizationStatus, 
                                    o.AuthorizationTime, o.AuthorizatedById);
        DataWriter.Execute(op);
      }

    #endregion Internal methods

  } // class OrderData

} // namespace Empiria.Trade.Sales.Data
