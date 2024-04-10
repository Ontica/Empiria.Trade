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
using Empiria.DataTypes;

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
            get; set;
        } = new NamedEntityDto("", "");


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
        } = new NamedEntityDto("", "");


        public string ShippingGuide {
            get; set;
        } = string.Empty;


        public decimal ParcelAmount {
            get; set;
        }


        public decimal CustomerAmount {
            get; set;
        }


        public MediaData ShippingLabelsMedia {
            get; set;
        }


        public MediaData BillingsMedia {
            get; set;
        }


        public DateTime ShippingDate {
            get; set;
        } = new DateTime();


        public string ShippingNumber {
          get;
          internal set;
        }


        public string DeliveryNumber {
          get;
          internal set;
        }


        public ShippingStatus Status {
                get; set;
        }
    
  } // class ShippingEntryDto


    /// <summary>Output DTO used to return the entries of shipping order item.</summary>
    public class ShippingOrderItemDto : ShippingTotalFieldsDto {


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


        public FixedList<PackageForShippingDto> Packages {
            get; set;
        } = new FixedList<PackageForShippingDto>();


        public MediaData BillingMedia {
            get; set;
        }

    }


    public class ShippingPalletDto : ShippingTotalFieldsDto {


        public string ShippingPalletUID {
            get; set;
        }


        public string ShippingPalletName {
            get; set;
        }


        public string[] Packages {
            get; set;
        } = new string[] { };


        //public FixedList<PackageForShippingDto> Packages {
        //  get; set;
        //} = new FixedList<PackageForShippingDto>();


    }


    public class PackageForShippingDto {

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
    public class ShippingTotalFieldsDto {

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
