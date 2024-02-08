/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Shipping Management                        Component : Interface adapters                      *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Data Transfer Object                    *
*  Type     : ShippingEntryDto                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return shipping data.                                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Sales.ShippingAndHandling.Adapters {


  /// <summary>Output DTO used to return shipping data.</summary>
  public class ShippingDto {


    public ShippingEntryDto ShippingData {
      get; set;
    } = new ShippingEntryDto();


    public FixedList<ShippingOrderItemDto> OrdersForShipping {
      get; set;
    } = new FixedList<ShippingOrderItemDto>();


    public bool CanEdit{
      get; set;
    }


  }


  /// <summary>Output DTO used to return the entries of shipping.</summary>
  public class ShippingEntryDto : ShippingCommonFieldsDto {


    public string ShippingUID {
      get; set;
    } = string.Empty;


    public int OrdersCount {
      get; set;
    }


    public decimal OrdersTotal {
      get; set;
    }


    public INamedEntity ParcelSupplier {
      get; set;
    } = new NamedEntityDto("","");


    public string ShippingGuide {
      get; set;
    } = string.Empty;


    public decimal ParcelAmount {
      get; set;
    }


    public decimal CustomerAmount {
      get; set;
    }


    public DateTime ShippingDate {
      get; set;
    } = new DateTime();


  } // class ShippingEntryDto


  /// <summary>Output DTO used to return the entries of shipping order item.</summary>
  public class ShippingOrderItemDto : ShippingCommonFieldsDto {


    public string OrderUID {
      get; set;
    }


    public string OrderNumber {
      get; set;
    }


    public decimal OrderTotal {
      get; set;
    }


    public INamedEntity Customer {
      get; set;
    } = new NamedEntityDto("", "");


    public INamedEntity Vendor {
      get; set;
    } = new NamedEntityDto("", "");


    public FixedList<OrderPackageForShippingDto> Packages {
      get; set;
    } = new FixedList<OrderPackageForShippingDto>();


  }


  public class OrderPackageForShippingDto {

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

  }


  /// <summary>Common data fields for shipping entries dto.</summary>
  public class ShippingCommonFieldsDto {

    public int TotalPackages {
      get; set;
    }


    public decimal TotalWeight {
      get; set;
    }


    public decimal TotalVolume {
      get; set;
    }

  } // class ShippingCommonFieldsDto


} // namespace Empiria.Trade.ShippingAndHandling.Adapters
