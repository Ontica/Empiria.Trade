/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Information Holder                      *
*  Type     : OrderItemsFields                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Holds a OrderItems properties.                                                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Orders;
using Empiria.Trade.Products;

namespace Empiria.Trade.Core {

  /// <summary>Holds a OrderItems properties. </summary>
  public class SalesOrderItemsFields : OrderItemFields {

    #region Public properties

    public string OrderItemUID {
      get; set;
    } = string.Empty;

    public string VendorProductUID {
      get; set;
    }

    public decimal ProductStock {
      get; set;
    }

    public decimal SalesPrice {
      get; set;
    }

   public string DiscountPolicy {
      get; set;
    } = string.Empty;

    public decimal Shipment {
      get; set;
    }

    public decimal Discount1 {
      get; set;
    }

    public decimal Discount2 {
      get; set;
    }

    public string Notes {
      get; set;
    } = string.Empty;


    #endregion

    #region Public methods


    #endregion Public methods

  }  // class OrderItemsFields

} // namespace Empiria.Trade.Sales.Adapters
