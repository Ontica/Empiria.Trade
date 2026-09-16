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

namespace Empiria.Trade.Core {

  /// <summary>Provides data layer for Orders.</summary>
  static public class SalesOrderData {


    #region Internal methods

    static public FixedList<SalesOrder> GetOrders(SearchOrderFields fields, string statusFilter = "") {

      var fromDate = fields.FromDate.ToString("yyyy-dd-MM");
      var toDate = fields.ToDate.AddDays(1).ToString("yyyy-dd-MM");
      var minimumDate = new DateTime(2026, 4, 30).ToString("yyyy-dd-MM");

      string keywordsFilter = string.Empty;

      string shippingMethodFilter = string.Empty;
      string customerFilter = string.Empty;

      if (fields.CustomerUID != string.Empty) {
        var customer = Parties.Party.Parse(fields.CustomerUID);

        customerFilter = $"INNER JOIN Parties P ON O.Order_Beneficary_Id = P.Party_Id " +
                         $"WHERE (O.Order_Beneficary_Id = {customer.Id}) AND ";
      } else {
        customerFilter = "WHERE ";
      }

      if (fields.Keywords != string.Empty) {
        keywordsFilter = $"AND {SearchExpression.ParseAndLikeKeywords("Order_Keywords", fields.Keywords)} ";
      }

      //if (fields.ShippingMethod != ShippingMethods.None) {
      //  shippingMethodFilter = $" AND (ShippingMethod LIKE '%{fields.ShippingMethod}%') ";
      //}

      //TODO FILTRO FECHA SERIA POR Order_Start_Date O Order_Posting_Time?
      var sql = $"SELECT * FROM OMS_Orders O {customerFilter} " +
                 $"(O.Order_Type_Id = 5012) " +
                 $"{keywordsFilter} " +
                 $"AND O.Order_Posting_Time >= CONVERT(SMALLDATETIME, '{minimumDate}') " +
                 $"AND (O.Order_Posting_Time >= CONVERT(SMALLDATETIME, '{fromDate}') " +
                 $"AND O.Order_Posting_Time <= CONVERT(SMALLDATETIME,'{toDate}')) " +
                 //$"{shippingMethodFilter} " +
                 $"{statusFilter} " +
                 $"AND ORDER_STATUS != 'X'";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<SalesOrder>(dataOperation);
    }


    static public FixedList<SalesOrder> GetSalesOrdersToPacking(SearchOrderFields fields) {

      string status = string.Empty;

      if (fields.Status != OrderStatus.Empty) {
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


    static public FixedList<SalesOrder> GetSalesByCustomer(int customerId) {
      var sql = $"SELECT * FROM OMS_Orders WHERE CustomerId = {customerId} AND OrderStatus <> 'X' ";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetFixedList<SalesOrder>(dataOperation);
    }


    static public SalesOrder GetSalesOrder(string orderNumber) {
      var sql = $"SELECT * FROM OMS_Orders  WHERE orderNumber = '{orderNumber}' ";

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetObject<SalesOrder>(dataOperation);
    }


    static public void Write(SalesOrder o) {
      var op = DataOperation.Parse("writeOrder", o.Id, o.UID
        //o.OrderTypeId, o.Customer.Id, o.Supplier.Id, o.SalesAgent.Id,o.CustomerContact.Id, o.OrderNumber,
        //o.OrderTime, o.Notes, o.Keywords, o.ExtData.ToString(), o.CustomerAddress.Id,
        //(char)o.ShippingMethod, (char)o.Status, (char)o.AuthorizationStatus, o.AuthorizationTime,
        //o.AuthorizatedById,o.ScheduledTime,o.ReceptionTime, o.PedimentoImportacion, o.CartaPorte
        );
      DataWriter.Execute(op);
    }

    #endregion Internal methods

    #region Private methods 


    #endregion Private methods

  } // class OrderData

} // namespace Empiria.Trade.Sales.Data
