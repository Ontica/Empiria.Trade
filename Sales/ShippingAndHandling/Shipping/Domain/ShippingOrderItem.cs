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
using System.Linq;
using Empiria.Trade.Orders;
using Empiria.Trade.Sales.ShippingAndHandling.Adapters;
using Empiria.Trade.Sales.ShippingAndHandling.Data;

namespace Empiria.Trade.Sales.ShippingAndHandling {


  /// <summary>Represents a shipping order item entry.</summary>
  public class ShippingOrderItem : BaseObject {


    #region Constructors and parsers


    public ShippingOrderItem() {
      //no-op
    }


    static public ShippingOrderItem Parse(int id) => ParseId<ShippingOrderItem>(id);

    static public ShippingOrderItem Parse(int id, bool reload) => ParseId<ShippingOrderItem>(id, reload);

    static public ShippingOrderItem Parse(string uid) => ParseKey<ShippingOrderItem>(uid);

    static public ShippingOrderItem Empty => ParseEmpty<ShippingOrderItem>();


    public ShippingOrderItem(string order, ShippingEntry shipping) {

      MapToShippingOrderItem(order, shipping);

    }


    #endregion Constructors and parsers


    [DataField("ShippingOrderItemId")]
    public int OrderForShippingId {
      get; internal set;
    }


    [DataField("ShippingOrderItemUID")]
    public string OrderForShippingUID {
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


    public FixedList<OrderPackageForShipping> OrderPackages {
      get; internal set;
    }


    public decimal OrderTotal {
      get; set;
    }


    public int TotalPackages {
      get; set;
    }


    public decimal TotalWeight {
      get; set;
    }


    public decimal TotalVolume {
      get; set;
    }


    protected override void OnSave() {

      if (this.OrderForShippingId == 0) {

        this.OrderForShippingId = this.Id;
        this.OrderForShippingUID = this.UID;
      }
      ShippingData.WriteShippingOrderItem(this);
    }


    private void MapToShippingOrderItem(string orderUID, ShippingEntry shipping) {

      this.Order = Order.Parse(orderUID);
      this.ShippingOrder = shipping;
      
      var existItem = ShippingData.GetOrdersForShippingByShippingId(
                    shipping.ShippingUID).Find(o => o.Order.Id == this.Order.Id);

      if (existItem != null) {
        
        this.OrderForShippingId = existItem.OrderForShippingId;
        this.OrderForShippingUID = existItem.OrderForShippingUID;
      }

    }


  } // class ShippingOrderItem


  public class OrderPackageForShipping {

    public string PackingItemUID {
      get; set;
    }


    public string PackageID {
      get; set;
    }


    public string PackageTypeName {
      get; set;
    }


    public decimal TotalWeight {
      get; set;
    }


    public decimal TotalVolume {
      get; set;
    }

  } // class OrderPackageForShipping


} // namespace Empiria.Trade.Sales.ShippingAndHandling.Shipping.Domain
