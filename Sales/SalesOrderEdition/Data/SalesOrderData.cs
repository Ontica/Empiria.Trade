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
using Empiria.Trade.Inventory;
using Empiria.Trade.Orders;
using Empiria.Trade.Sales.Adapters;

namespace Empiria.Trade.Sales.Data {

    /// <summary>Provides data layer for Orders.</summary>
    static internal class SalesOrderData {


    #region Internal methods

    internal static FixedList<SalesOrder> GetSalesOrders(SearchOrderFields fields) {

      string status = string.Empty;

      if (fields.Status != Orders.OrderStatus.Empty) {
        if (fields.Status == Orders.OrderStatus.Authorized) {
          status = " AND (OrderAuthorizationStatus = 'A')";
        } else {
          status = $" AND (OrderStatus = '{(char) fields.Status}')";
        }
      } else {
         status = $" AND (OrderStatus <> '{(char) Orders.OrderStatus.Cancelled}')";
      }

       return GetOrders(fields, status); 
        
    }

    internal static FixedList<SalesOrder> GetSalesOrdersToAuthorize(SearchOrderFields fields) {
      
        string status = string.Empty;

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

      return GetOrders(fields, status);
    }

    internal static FixedList<SalesOrder> GetSalesOrdersToPacking(SearchOrderFields fields) {

        string status = string.Empty;

      if (fields.Status != Orders.OrderStatus.Empty) {
        if (fields.Status == OrderStatus.Suppled) {
          status = "  AND ((OrderStatus = 'S') or (OrderStatus = 'D') or (OrderStatus = 'F')) ";
        }
        if (fields.Status == OrderStatus.ToSupply) {
          status = " AND (OrderStatus = 'P') ";
        }
        if (fields.Status == OrderStatus.InProgress) {
          status = " AND (OrderAuthorizationStatus = 'U') ";
        }
        } else {
        status = "  AND ((OrderStatus = 'P') or (OrderStatus = 'S') or (OrderStatus = 'D') or (OrderStatus = 'F')) ";
      }

      return GetOrders(fields, status);

    }

      internal static void Write(SalesOrder o) {
        var op = DataOperation.Parse("writeOrder", o.Id, o.UID, o.OrderTypeId, o.Customer.Id, o.Supplier.Id,
                                    o.SalesAgent.Id,o.CustomerContact.Id, o.OrderNumber, o.OrderTime, o.Notes,
                                    o.Keywords, o.ExtData.ToString(), o.CustomerAddress.Id, (char)o.ShippingMethod, (char)o.Status, (char)o.AuthorizationStatus, 
                                    o.AuthorizationTime, o.AuthorizatedById);
        DataWriter.Execute(op);
      }

    #endregion Internal methods

    #region Private methods 

    private static FixedList<SalesOrder> GetOrders(SearchOrderFields fields, string statusFilter) {
      var toDate = fields.ToDate.ToString("yyyy-dd-MM");
      var fromDate = fields.FromDate.ToString("yyyy-dd-MM");

      string keywordsFilter = string.Empty;

      string shippingMethodFilter = string.Empty;
      string customerFilter = string.Empty;

      if (fields.CustomerUID != string.Empty) {
        customerFilter = $"INNER JOIN TRDParties ON TRdOrders.CustomerId = TRdParties.PartyId WHERE (partyUID = '{fields.CustomerUID}') AND ";
      } else {
        customerFilter = "WHERE ";
      }

      if (fields.Keywords != string.Empty) {
        keywordsFilter = $" {SearchExpression.ParseAndLikeKeywords("OrderKeywords", fields.Keywords)} AND ";
      }

      if (fields.ShippingMethod != ShippingMethods.None) {
        shippingMethodFilter = $" AND (ShippingMethod LIKE '%{(char) fields.ShippingMethod}%') ";
      }

      var sql = $"SELECT * FROM TRDOrders {customerFilter} " +
                 $" {keywordsFilter}  (orderTime >= CONVERT(SMALLDATETIME, '{fromDate}') AND " +
                 $"orderTime <= CONVERT(SMALLDATETIME,'{toDate}')) {statusFilter} {shippingMethodFilter}";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetFixedList<SalesOrder>(dataOperation);
    }

    #endregion Private methods

  } // class OrderData

} // namespace Empiria.Trade.Sales.Data
