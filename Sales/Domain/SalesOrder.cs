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
using System.Collections.Generic;
using Empiria.Trade.Orders;
using Empiria.Trade.Sales.Adapters;
using Empiria.Trade.Sales.Data;

using Newtonsoft.Json;
using Empiria.Trade.Core;

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

    public int ItemsCount {
      get; private set;
    } = 0;


    public decimal ItemsTotal {
      get; private set;
    } = 0m;

    public decimal Shipment {
      get; private set;
    } = 0m;

    public decimal Discount {
      get; private set;
    } = 0m;

    public decimal Taxes {
      get; private set;
    } = 0m;

    public decimal OrderTotal {
      get; private set;
    } = 0m;

    public string PaymentCondition {
      get; private set;
    }

    public string ShippingMethod {
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
     
    }

    internal static FixedList<SalesOrder> GetOrders(SearchOrderFields fields) {
      return SalesOrderData.GetSalesOrders(fields);
    }

    public int GetCustomerPriceListNumber() {

      var ExtData = JsonConvert.DeserializeObject<PartyExtData>(this.Customer.ExtData);
      return ExtData.PriceListId;
    }

    internal void Update(SalesOrderFields fields) {
      this.OrderTypeId = 1025;
      this.Customer = fields.GetCustomer();
      this.Supplier = fields.GetSupplier();
      this.SalesAgent = fields.GetSalesAgent();
      this.Notes = fields.Notes;
      this.Status = fields.Status;
      this.ShippingMethod = fields.ShippingMethod;
      this.PaymentCondition = fields.PaymentCondition;
      this.SalesOrderItems = new FixedList<SalesOrderItem>(); //LoadSalesOrderItems(fields.Items);

    }


    #endregion Public methods

    #region Private methods

    private FixedList<SalesOrderItem> LoadSalesOrderItems(FixedList<SalesOrderItemsFields> orderItemsFields) {
      List<SalesOrderItem> orderItems = new List<SalesOrderItem>();

      int priceListNumber = GetCustomerPriceListNumber();

      foreach (SalesOrderItemsFields itemFields in orderItemsFields) {
        var saleOrderItem = new SalesOrderItem(itemFields, priceListNumber);
        orderItems.Add(saleOrderItem);
        this.ItemsCount++;
        this.ItemsTotal += saleOrderItem.SalesPrice;
        this.Shipment += saleOrderItem.Shipment;
        this.Discount += saleOrderItem.Discount;
        this.Taxes += saleOrderItem.TaxesIVA;
        this.OrderTotal += saleOrderItem.Total;

      }
   

      return orderItems.ToFixedList<SalesOrderItem>();
    }

    #endregion

  }  //  class SalesOrder

 

}  // namespace Empiria.Trade.Sales
