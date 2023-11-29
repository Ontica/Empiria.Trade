/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Packing Management                         Component : Interface adapters                      *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Data Transfer Object                    *
*  Type     : PackingDto                                 License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : DTO used to manage order packing detail.                                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Core;
using Empiria.Trade.Orders;
using Empiria.Trade.Products;
using Empiria.Trade.ShippingAndHandling.Adapters;

namespace Empiria.Trade.ShippingAndHandling.Adapters {

  /// <summary>DTO used to manage order packing detail.</summary>
  public class PackingDto : IShippingAndHandling {


    public PackingData PackingData {
      get; set;
    } = new PackingData();


    public FixedList<PackingItem> PackingItem {
      get; set;
    } = new FixedList<PackingItem>();


    public FixedList<MissingItem> MissingItems {
      get; set;
    } = new FixedList<MissingItem>();


  } // class PackingDto


  public class PackingData {


    public string OrderUID {
      get; set;
    }


    public int Size {
      get; set;
    }


    public int Count {
      get; set;
    }


  } // class PackingData


  public class PackingItem {


    public string UID {
      get; set;
    }


    public string OrderUID {
      get; set;
    }


    public string Name {
      get; set;
    }


    public string PackageTypeUID {
      get; set;
    }


    public string PackageTypeName {
      get; set;
    }


    public FixedList<PackingOrderItem> OrderItems {
      get; set;
    } = new FixedList<PackingOrderItem>();


  } // class PackingItem


  public class PackingOrderItem : OrderItemCommonFields {

    public string UID {
      get; set;
    }


    public Warehouse Warehouse {
      get; set;
    } = new Warehouse();


    public WarehouseBin WarehouseBin {
      get; set;
    } = new WarehouseBin();


    public decimal Quantity {
      get; set;
    }


  } // class PackingOrderItem


  public class MissingItem : OrderItemCommonFields {


    public FixedList<WarehouseBin> WarehouseBin {
      get; set;
    } = new FixedList<WarehouseBin>();


    public int RemainingQuantity {
      get; set;
    }


    public decimal Price {
      get; set;
    }


  } // class MissingItem


  public class OrderItemCommonFields {

    
    public string OrderItemUID {
      get; set;
    }


    public ProductFields Product {
      get; set;
    }


    public ProductPresentation Presentation {
      get; set;
    }


    public Party Vendor {
      get; set;
    }


    public void MergeFieldsData(int orderItemId) {
      
      var orderItem = OrderItem.Parse(orderItemId);
      var vendorProduct = VendorProduct.Parse(orderItem.VendorProduct.Id);

      this.OrderItemUID = orderItem.UID;
      this.Product = vendorProduct.ProductFields;
      this.Presentation = vendorProduct.ProductPresentation;
      this.Vendor = vendorProduct.Vendor;
    }

  } // class OrderItemProduct

} // namespace Empiria.Trade.Core.ShippingAndHandling.Adapters
