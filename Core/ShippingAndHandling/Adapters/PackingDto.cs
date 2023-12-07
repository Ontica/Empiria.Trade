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


    public PackagedData Data {
      get; set;
    } = new PackagedData();


    public FixedList<PackageForItemDto> PackagedItems {
      get; set;
    } = new FixedList<PackageForItemDto>();


    public FixedList<MissingItemDto> MissingItems {
      get; set;
    } = new FixedList<MissingItemDto>();


  } // class PackingDto


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


  } // class PackingData


  public class PackageForItemDto {


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


    public FixedList<PackingItemDto> OrderItems {
      get; set;
    } = new FixedList<PackingItemDto>();


  } // class PackingItem


  public class PackingItemDto : CommonPackingItemFields {

    public string UID {
      get; set;
    }


    public WarehouseDto Warehouse {
      get; set;
    } = new WarehouseDto();


    public WarehouseBinDto WarehouseBin {
      get; set;
    } = new WarehouseBinDto();


    public decimal Quantity {
      get; set;
    }


  } // class PackingOrderItem


  public class MissingItemDto : CommonPackingItemFields {


    public FixedList<WarehouseBinDto> WarehouseBins {
      get; set;
    } = new FixedList<WarehouseBinDto>();


    public decimal Quantity {
      get; set;
    }


  } // class MissingItem


  public class WarehouseDto {


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

  }


  public class WarehouseBinDto {


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

  }


  public class CommonPackingItemFields {

    
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


    public void MergeCommonFieldsData(int orderItemId) {
      
      var orderItem = OrderItem.Parse(orderItemId);
      var vendorProduct = VendorProduct.Parse(orderItem.VendorProduct.Id);

      this.OrderItemUID = orderItem.UID;
      this.Product = vendorProduct.ProductFields;
      this.Presentation = vendorProduct.ProductPresentation;
      this.Vendor = vendorProduct.Vendor;
    }

  } // class OrderItemProduct

} // namespace Empiria.Trade.Core.ShippingAndHandling.Adapters
