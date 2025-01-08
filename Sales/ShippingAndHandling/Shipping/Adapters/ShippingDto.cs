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

using Empiria.Storage;

using Empiria.Trade.Orders;

namespace Empiria.Trade.Sales.ShippingAndHandling.Adapters {


  /// <summary>Output DTO used to return shipping data.</summary>
  public class ShippingDto {


    public ShippingEntryDto ShippingData {
      get; set;
    } = new ShippingEntryDto();


    public FixedList<ShippingOrderItemDto> OrdersForShipping {
      get; set;
    } = new FixedList<ShippingOrderItemDto>();


    public FixedList<ShippingPalletDto> ShippingPalletsWithPackages {
      get; set;
    } = new FixedList<ShippingPalletDto>();


    public ShippingActions Actions {
      get; set;
    } = new ShippingActions();


  }


  public class ShippingActions {


    public bool CanEdit {
      get; set;
    }


    public bool CanDelete {
      get; set;
    }


    public bool CanCloseEdit {
      get; set;
    }


    public bool CanPrintShippingLabel {
      get; set;
    }


    public bool CanPrintOrder {
      get; set;
    }


    public bool CanCloseShipping {
      get; set;
    }


  }


  /// <summary>Output DTO used to return the entries of shipping.</summary>
  public class ShippingEntryDto : ShippingTotalFieldsDto {


    public NamedEntityDto Customer {
      get; internal set;
    }


    public string ShippingUID {
      get; internal set;
    }


    public int OrdersCount {
      get; internal set;
    }


    public decimal OrdersTotal {
      get; internal set;
    }


    public INamedEntity ParcelSupplier {
      get; internal set;
    }


    public string ShippingGuide {
      get; internal set;
    }


    public decimal ParcelAmount {
      get; internal set;
    }


    public decimal CustomerAmount {
      get; internal set;
    }


    public MediaData ShippingLabelsMedia {
      get; internal set;
    }


    public MediaData BillingsMedia {
      get; internal set;
    }


    public DateTime ShippingDate {
      get; internal set;
    } = new DateTime();


    public string ShippingNumber {
      get; internal set;
    }


    public string DeliveryNumber {
      get; internal set;
    }


    public ShippingMethods ShippingMethod {
      get; internal set;
    }


    public ShippingStatus Status {
      get; internal set;
    }

  } // class ShippingEntryDto


  /// <summary>Output DTO used to return the entries of shipping order item.</summary>
  public class ShippingOrderItemDto : ShippingTotalFieldsDto {


    public string OrderUID {
      get; internal set;
    }


    public string OrderNumber {
      get; internal set;
    }


    public decimal OrderTotal {
      get; internal set;
    }


    public INamedEntity Customer {
      get; internal set;
    }


    public INamedEntity Vendor {
      get; internal set;
    }


    public FixedList<PackageForShippingDto> Packages {
      get; internal set;
    } = new FixedList<PackageForShippingDto>();


    public MediaData BillingMedia {
      get; internal set;
    }

  }


  public class ShippingPalletDto : ShippingTotalFieldsDto {


    public string ShippingPalletUID {
      get; internal set;
    }


    public string ShippingPalletName {
      get; internal set;
    }


    public string[] Packages {
      get; internal set;
    } = new string[] { };


    //public FixedList<PackageForShippingDto> Packages {
    //  get; set;
    //} = new FixedList<PackageForShippingDto>();


  }


  public class PackageForShippingDto {

    public string PackingItemUID {
      get; internal set;
    }


    public string PackageID {
      get; internal set;
    }


    public string PackageTypeName {
      get; internal set;
    }


    public decimal TotalWeight {
      get; internal set;
    }


    public decimal TotalVolume {
      get; internal set;
    }

  }


  /// <summary>Common data fields for shipping entries dto.</summary>
  public class ShippingTotalFieldsDto {

    public int TotalPackages {
      get; internal set;
    }


    public decimal TotalWeight {
      get; internal set;
    }


    public decimal TotalVolume {
      get; internal set;
    }

  } // class ShippingCommonFieldsDto


} // namespace Empiria.Trade.ShippingAndHandling.Adapters
