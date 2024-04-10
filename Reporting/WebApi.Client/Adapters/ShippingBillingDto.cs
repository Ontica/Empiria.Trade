/* Empiria Trade Reporting ***********************************************************************************
*                                                                                                            *
*  Module   : Shipping Reports Management                Component : Interface adapters                      *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Data Transfer Object                    *
*  Type     : ShippingBillingDto                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return shipping billing report.                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Reporting.WebApi.Client.Adapters {

    /// <summary>Output DTO used to return shipping billing report.</summary>
    public class ShippingBillingDto {

        public string OrderUID {
            get; set;
        } = string.Empty;


        public string OrderNumber {
            get; set;
        } = string.Empty;


        public string Customer {
            get; set;
        } = string.Empty;


        public string CustomerAddress {
            get; set;
        } = string.Empty;


        public string CustomerContact {
            get; set;
        } = string.Empty;


        public string CustomerPhone {
            get; set;
        } = string.Empty;


        public string Supplier {
            get; set;
        } = string.Empty;


        public string SupplierAddress {
            get; set;
        } = string.Empty;


        public string SupplierPhonoNumber {
            get; set;
        } = string.Empty;


        public string SalesAgent {
            get; set;
        } = string.Empty;


        public string PaymentCondition {
            get; set;
        } = string.Empty;


        public string ShippingMethod {
            get; set;
        } = string.Empty;


        public string OrderNotes {
            get; set;
        } = string.Empty;


        public int ItemsCount {
            get; set;
        }


        public decimal BillingSubtotal {
            get; set;
        } // ItemsTotal


        public decimal ShipmentTotal {
            get; set;
        } // Shipment


        public decimal Taxes {
            get; set;
        }


        public decimal BillingTotal {
            get; set;
        } // orderTotal


        public List<BillingItemDto> BillingItems {
            get; set;
        } = new List<BillingItemDto>();

    } // class ShippingBillingDto


    public class BillingItemDto {


        public string ProductPresentation {
            get; set;
        } = string.Empty;


        public string ProductCode {
            get; set;
        } = string.Empty;


        public string ProductName {
            get; set;
        } = string.Empty;


        public decimal Quantity {
            get; set;
        }


        public decimal UnitPrice {
            get; set;
        }


        public decimal SalesPrice {
            get; set;
        }


        public string DiscountPolicy {
            get; set;
        } = string.Empty;


        public decimal Discount1 {
            get; set;
        }


        public decimal Discount2 {
            get; set;
        }


        public decimal Subtotal {
            get; set;
        }


    } // class BillingItemDto


} // namespace Empiria.Trade.Reporting.WebApi.Client.Adapters
