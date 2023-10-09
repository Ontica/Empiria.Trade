/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Order Management                           Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Order.dll                    Pattern   : Partitioned Type / Information Holder   *
*  Type     : Order                                      License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a Order.                                                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Trade.Sales.Data;
using Empiria.Trade.Sales.Adapters;
using System.Collections.Generic;

namespace Empiria.Trade.Sales {

  /// <summary>Represent Order</summary>
  internal class SalesOrder : Order {
    
    #region Constructors and parsers

    public SalesOrder() {
      //no-op
    }

    public SalesOrder(OrderFields fields) {
      Update(fields);
    }

    static public new SalesOrder Parse(int id) {
      return BaseObject.ParseId<SalesOrder>(id);
    }

    static public new SalesOrder Parse(string uid) {
      return BaseObject.ParseKey<SalesOrder>(uid);
    }

    #endregion Constructors and parsers

    #region Public methods

    protected override void OnSave() {
      if (IsNew) {
        OrderNumber = "P-" + EmpiriaString.BuildRandomString(10);
        OrderTime = DateTime.Now;
      }
      OrderData.Write(this);
      SaveOrderItems();
    }

    internal void Update(OrderFields fields) {
      this.Customer = fields.GetCustomer();
      this.Supplier = fields.GetSupplier();
      this.SalesAgent = fields.GetSalesAgent();
      this.Notes = fields.Notes;
      this.Status = fields.Status;
      this.OrderItems = LoadOrderItems(this.Id,fields.OrderItems);
    }

    #endregion Public methods


    #region Private methods

    private FixedList<OrderItems> LoadOrderItems(int salesOrderId, FixedList<OrderItemsFields> orderItemsFields) {
      List<SalesOrderItem> salesOrderItems = new List<SalesOrderItem>();

      foreach (OrderItemsFields itemFields in orderItemsFields) {
        salesOrderItems.Add(new SalesOrderItem(salesOrderId, itemFields));
      }

      return salesOrderItems.ToFixedList<OrderItems>();
    }

    private void SaveOrderItems() {

      foreach (OrderItems orderItem in this.OrderItems) {
        OrderItemsData.Write(orderItem);
      }

    }
    
    #endregion

  }  //  class SalesOrder

}  // namespace Empiria.Trade.Sales
