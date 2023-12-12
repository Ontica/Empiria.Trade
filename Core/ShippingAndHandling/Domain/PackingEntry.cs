/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Packaging Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Partitioned Type / Information Holder   *
*  Type     : PackingEntry                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a Packing item for order.                                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Core;
using Empiria.Trade.Orders;
using Empiria.Trade.Products.Adapters;
using Empiria.Trade.Products;
using Empiria.Trade.ShippingAndHandling.Adapters;

namespace Empiria.Trade.ShippingAndHandling {

  /// <summary>Represents a Packing item for order.</summary>
  internal class PackingEntry {


    public PackagedData Data {
      get; set;
    } = new PackagedData();


    public FixedList<PackagedForItem> PackagedItems {
      get; set;
    } = new FixedList<PackagedForItem>();


    public FixedList<MissingItem> MissingItems {
      get; set;
    } = new FixedList<MissingItem>();


  } // class PackingEntry


  public class PackagedData {


    public string OrderUID {
      get; set;
    }


    public decimal Size {
      get; set;
    }


    public int Count {
      get; set;
    }


  } // class PackagedData


  public class PackagedForItem {


    public string UID {
      get; set;
    }


    public string OrderUID {
      get; set;
    }


    public string PackageID {
      get; set;
    }


    public string PackageTypeUID {
      get; set;
    }


    public string PackageTypeName {
      get; set;
    }


    public FixedList<PackingItem> OrderItems {
      get; set;
    } = new FixedList<PackingItem>();


  } // class PackagedForItem


  public class PackingItem : CommonPackingItemFields {

    public string UID {
      get; set;
    }


    public string OrderPackingUID {
      get; set;
    }


    public WarehouseForPacking WarehouseForPacking {
      get; set;
    } = new WarehouseForPacking();


    public WarehouseBinForPacking WarehouseBinForPacking {
      get; set;
    } = new WarehouseBinForPacking();


    public decimal Quantity {
      get; set;
    }


  } // class PackingItem


  public class MissingItem : CommonPackingItemFields {


    public FixedList<WarehouseBinForPacking> WarehouseBins {
      get; set;
    } = new FixedList<WarehouseBinForPacking>();


    public decimal Quantity {
      get; set;
    }


  } // class MissingItem


  public class WarehouseForPacking {


    public string UID {
      get; set;
    }


    public string Code {
      get; set;
    }


    public string Name {
      get; set;
    }


    public decimal Stock {
      get; set;
    }

  } // WarehouseForPacking


  public class WarehouseBinForPacking {


    public string UID {
      get; set;
    }


    public string OrderItemUID {
      get; set;
    }


    public string Name {
      get; set;
    }


    public string WarehouseName {
      get; set;
    }


    public decimal Stock {
      get; set;
    }

  } // WarehouseBinForPacking


  public class CommonPackingItemFields {


    public string OrderItemUID {
      get; set;
    }


    public int VendorProductId {
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


    public void MergeCommonFieldsData(int orderItemId) {

      var orderItem = OrderItem.Parse(orderItemId);
      var vendorProduct = VendorProduct.Parse(orderItem.VendorProduct.Id);

      this.OrderItemUID = orderItem.UID;
      this.VendorProductId = orderItem.VendorProduct.Id;
      this.Product = vendorProduct.ProductFields;
      this.Presentation = vendorProduct.ProductPresentation;
      this.Vendor = vendorProduct.Vendor;
    }


  } // class CommonPackingItemFields


} // namespace Empiria.Trade.ShippingAndHandling.Domain
