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

using Empiria.Trade.Orders;

namespace Empiria.Trade.Sales {

  /// <summary>Represent Order</summary>
  public class SalesOrder : Order {

    #region Constructors and parsers

    public SalesOrder() {
      //no-op
    }

    public SalesOrder(SalesOrderFields fields) {
      Update(fields);
    }

    static public new SalesOrder Parse(int id) {
      return BaseObject.ParseId<SalesOrder>(id);
    }

    static public new SalesOrder Parse(string uid) {
      return BaseObject.ParseKey<SalesOrder>(uid);
    }

    #endregion Constructors and parsers

    #region Public properties

    public FixedList<SalesOrderItem> SalesOrderItems {
      get; private set;
    }

    #endregion


    #region Public methods

    protected override void OnSave() {
      if (IsNew) {
        OrderNumber = "P-" + EmpiriaString.BuildRandomString(10);
        OrderTime = DateTime.Now;
      }
      SalesOrderData.Write(this);
      SaveOrderItems();
    }

    internal void Update(SalesOrderFields fields) {
      this.Customer = fields.GetCustomer();
      this.Supplier = fields.GetSupplier();
      this.SalesAgent = fields.GetSalesAgent();
      this.Notes = fields.Notes;
      this.Status = fields.Status;
      this.SalesOrderItems = LoadSalesOrderItems(this.Id,fields.OrderItems);
    }

    #endregion Public methods

    #region Private methods

    private FixedList<SalesOrderItem> LoadSalesOrderItems(int salesOrderId, FixedList<SalesOrderItemsFields> orderItemsFields) {
      List<SalesOrderItem> orderItems = new List<SalesOrderItem>();

      foreach (SalesOrderItemsFields itemFields in orderItemsFields) {
        orderItems.Add(new SalesOrderItem(salesOrderId, itemFields));
      }

      return orderItems.ToFixedList<SalesOrderItem>();
    }

    private void SaveOrderItems() {

      foreach (SalesOrderItem orderItem in this.SalesOrderItems) {
        SalesOrderItemsData.Write(orderItem);
      }

    }

    #endregion

  }  //  class SalesOrder

}  // namespace Empiria.Trade.Sales
