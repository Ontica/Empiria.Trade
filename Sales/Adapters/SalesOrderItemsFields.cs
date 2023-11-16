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

using Empiria.Trade.Orders;

using Empiria.Trade.Products;

namespace Empiria.Trade.Sales.Adapters {

  /// <summary>Holds a OrderItems properties. </summary>
  public class SalesOrderItemsFields {

    #region Public properties

    public string OrderItemUID {
      get; set;
    } = string.Empty;

    public string VendorProductUID {
      get; set;
    }

    public int Quantity {
      get; set;
    }
        
    public decimal UnitPrice {
      get; set;
    }

    public decimal SalesPrice {
      get; set;
    }

   public string DiscountPolicy {
      get; set;
    }

    public decimal Shipment {
      get; set;
    }

    public decimal Discount1 {
      get; set;
    }

    public decimal Discount2 {
      get; set;
    }

    public decimal Subtotal {
      get; set;
    }
        
    public string Notes {
      get; set;
    } = String.Empty;


    #endregion

    #region Public methods

    internal VendorProduct GetVendorProduct() {
      return VendorProduct.Parse(this.VendorProductUID);
    }

    #endregion Public methods

  }  // class OrderItemsFields

} // namespace Empiria.Trade.Sales.Adapters
