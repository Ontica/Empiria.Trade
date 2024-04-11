/* Empiria Trade Reporting ***********************************************************************************
*                                                                                                            *
*  Module   : Shipping Reports Management                Component : Interface adapters                      *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Data Transfer Object                    *
*  Type     : ShippingLabelDto                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return shipping label.                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Reporting.WebApi.Client.Adapters {


  /// <summary>Output DTO used to return shipping label by pallet.</summary>
  public class ShippingLabelDto {

    public string Company {
      get; set;
    } = string.Empty;


    public string CompanyAddress {
      get; set;
    } = string.Empty;


    public string ShippingGuide {
      get; set;
    } = string.Empty;


    public string ShippingNumber {
      get; set;
    } = string.Empty;


    public string DeliveryNumber {
      get; set;
    } = string.Empty;


    public string ShippingType {
      get; set;
    } = string.Empty;


    public string ParcelSupplier {
      get; set;
    } = string.Empty;


    public string Customer {
      get; set;
    } = string.Empty;


    public string CustomerAddress {
      get; set;
    } = string.Empty;


    public string CustomerPhoneNumber {
      get; set;
    } = string.Empty;


    public string PackingName {
      get; set;
    } = string.Empty;


    public string PackingCount {
      get; set;
    } = string.Empty;


    public int PackageQuantity {
      get; set;
    }


    public int TiedQuantity {
      get; set;
    }


    public int BagQuantity {
      get; set;
    }

  } // class ShippingLabelByPallet


  /// <summary>Output DTO used to return shipping label.</summary>
  public class SupplyLabelDto {


    public string orderUID {
      get; set;
    } = string.Empty;


    public string productCode {
      get; set;
    } = string.Empty;


    public string productPresentation {
      get; set;
    } = string.Empty;


    public string description {
      get; set;
    } = string.Empty;


    public string ubication {
      get; set;
    } = string.Empty;


    public string comments {
      get; set;
    } = string.Empty;


    public decimal quantity {
      get; set;
    }


  } // class ShippingLabelDto

} // namespace Empiria.Trade.Reporting.WebApi.Client.Adapters
