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
using Empiria.Trade.Products.Adapters;

namespace Empiria.Trade.Sales.ShippingAndHandling.Adapters {

  /// <summary>DTO used to manage order packing detail.</summary>
  public class PackingDto : IShippingAndHandling {


    public PackagedDataDto Data {
      get; set;
    } = new PackagedDataDto();


    public FixedList<PackageForItemDto> PackagedItems {
      get; set;
    } = new FixedList<PackageForItemDto>();


    public FixedList<MissingItemDto> MissingItems {
      get; set;
    } = new FixedList<MissingItemDto>();


  } // class PackingDto


  public class PackagedDataDto {


    public string OrderUID {
      get; set;
    } = string.Empty;


    public decimal TotalVolume {
      get; set;
    }


    public decimal TotalWeight {
      get; set;
    }


    public int TotalPackages {
      get; set;
    }


    public FixedList<PackingTypeData> PackingTypeData {
      get; set;
    } = new FixedList<PackingTypeData>();


  } // class PackingData


  public class PackingTypeData {

    public string PackageTypeUID {
      get; set;
    }


    public string PackageTypeName {
      get; set;
    }


    public int Packages {
      get; set;
    }


  }


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


  public class PackingItemDto : CommonPackingItemFieldsDto {

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


  public class MissingItemDto : CommonPackingItemFieldsDto {


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


  public class CommonPackingItemFieldsDto {

    
    public string OrderItemUID {
      get; set;
    }


    public ProductDto Product {
      get; set;
    }


    public ProductPresentationDto Presentation {
      get; set;
    }


    public VendorDto Vendor {
      get; set;
    }


  } // class OrderItemProduct

} // namespace Empiria.Trade.Core.ShippingAndHandling.Adapters
