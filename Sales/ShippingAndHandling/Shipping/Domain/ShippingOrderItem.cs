/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Shipping Management                        Component : Domain Layer                            *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Information Holder                      *
*  Type     : ShippingOrderItem                          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a shipping order item entry.                                                        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using Empiria.Trade.Orders;
using Empiria.Trade.Sales.ShippingAndHandling.Adapters;

namespace Empiria.Trade.Sales.ShippingAndHandling.Domain {


  /// <summary>Represents a shipping order item entry.</summary>
  public class ShippingOrderItem : ShippingCommonFields {


    [DataField("ShippingOrderItemId")]
    public int ShippingOrderItemId {
      get; internal set;
    }


    [DataField("ShippingOrderItemUID")]
    public string ShippingOrderItemUID {
      get; internal set;
    }


    [DataField("ShippingOrderId")]
    public ShippingEntry ShippingOrder {
      get; internal set;
    }


    [DataField("OrderId")]
    public Order Order {
      get; internal set;
    }


  } // class ShippingOrderItem


  /// <summary>Common data fields for shipping entries.</summary>
  public class ShippingCommonFields {

    public int TotalPackages {
      get; set;
    }


    public decimal TotalWeight {
      get; set;
    }


    public decimal TotalVolume {
      get; set;
    }

  } // class ShippingCommonFields


} // namespace Empiria.Trade.Sales.ShippingAndHandling.Shipping.Domain
