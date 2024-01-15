﻿/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Discount Management                        Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Discount.dll                 Pattern   : Partitioned Type / Information Holder   *
*  Type     : Discount                                   License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a order ciscount.                                                                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using Empiria.Trade.Orders;
using Empiria.Trade.Products;

using Empiria.Trade.Sales.Data;

namespace Empiria.Trade.Sales {

  public class SalesDiscount : BaseObject {

    #region Constructors and parsers

    public SalesDiscount() {
      //no-op
    }

    #endregion Constructors and parsers

    #region Public properties

    [DataField("DiscountTypeId")]
    public int TypeId {
      get; private set;
    }

    [DataField("DiscountDescription")]
    public string Description {
      get; private set;
    }

    [DataField("Discount")]
    public decimal Discount {
      get; private set;
    }

    [DataField("DiscountUnitId")]
    public int DiscountUnitId {
      get; private set;
    }

    [DataField("DiscountAppliedListId")]
    public string AppliedId {
      get; private set;
    } = String.Empty;

    [DataField("DiscountConditions")]
    public string Conditions {
      get; private set;
    } = string.Empty;

    [DataField("DiscountFromDate")]
    public DateTime FromDate {
      get; private set;
    }

    [DataField("DiscountToDate")]
    public DateTime ToDate {
      get; private set;
    }

    [DataField("AuthorizedById")]
    public int AuthorizedById {
      get; private set;
    }

    [DataField("DiscountExtData")]
    public string ExtData {
      get; private set;
    } = string.Empty;


    [DataField("DiscountStatus", Default ='A')]
    public char Status {
      get; private set;
    } = 'A';

    #endregion Public properties

    #region Public methods

    static public FixedList<SalesDiscount> GetDiscountByGroup(int groupId, DateTime orderDate) {
      return GetDiscount(4, groupId, orderDate);
    }

    static public FixedList<SalesDiscount> GetDiscountBySubGroup(int subGroupId, DateTime orderDate) {
      return GetDiscount(3, subGroupId, orderDate);
    }

    static public FixedList<SalesDiscount> GetDiscountByProduct(int subGroupId, DateTime orderDate) {
      return GetDiscount(2, subGroupId, orderDate);
    }

    static public FixedList<SalesDiscount> GetDiscountByCustomer(int customerId, DateTime orderDate) {
      return GetDiscount(1, customerId, orderDate);
    }

    static public FixedList<SalesDiscount> GetDiscountByVendor(VendorProduct vendor, DateTime orderDate) {
      var discountsList = new List<SalesDiscount>();

      var discountsByGroup = GetDiscountByGroup(vendor.ProductFields.ProductGroup.Id, orderDate);
      var discountsBySubGroup = GetDiscountBySubGroup(vendor.ProductFields.ProductSubgroup.Id, orderDate);
      var discountsByProduct = GetDiscountByProduct(vendor.ProductFields.ProductId, orderDate);

      discountsList.AddRange(discountsByGroup);
      discountsList.AddRange(discountsBySubGroup);
      discountsList.AddRange(discountsByProduct);

      return discountsList.ToFixedList();

    }

    static public FixedList<SalesDiscount> GetDiscountsByCustomer(VendorProduct vendor, DateTime orderDate, int customerId) {
      var discountsList = new List<SalesDiscount>();

      var discountsByGroup = SalesDiscountData.GetSalesDiscountsByDiscountTypeAndCustomerId(4,vendor.ProductFields.ProductGroup.Id, customerId, orderDate);
      var discountsBySubGroup = SalesDiscountData.GetSalesDiscountsByDiscountTypeAndCustomerId(3,vendor.ProductFields.ProductSubgroup.Id, customerId, orderDate);
      var discountsByProduct = SalesDiscountData.GetSalesDiscountsByDiscountTypeAndCustomerId(2,vendor.ProductFields.ProductId, customerId, orderDate);

      discountsList.AddRange(discountsByGroup);
      discountsList.AddRange(discountsBySubGroup);
      discountsList.AddRange(discountsByProduct);

      return discountsList.ToFixedList();

    }

    #endregion Public methods

    #region Private methods

    static private FixedList<SalesDiscount> GetDiscount(int DiscountTypeId, int targetId, DateTime orderDate) {

      FixedList<SalesDiscount> discounts = SalesDiscountData.GetSalesDiscounts(DiscountTypeId, targetId, orderDate);

      return discounts;
    }

    internal static decimal Apply(VendorProduct vendorProduct, Order order) {
      throw new NotImplementedException();
    }

    #endregion Privare methods
  } // class SalesDiscount

} // namespace Empiria.Trade.Sales
