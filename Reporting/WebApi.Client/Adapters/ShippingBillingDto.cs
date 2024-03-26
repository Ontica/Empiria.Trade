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

        public string orderUID {
            get; set;
        } = string.Empty;


        public string orderNumber {
            get; set;
        } = string.Empty;


        public string customer {
            get; set;
        } = string.Empty;


        public string customerAddress {
            get; set;
        } = string.Empty;


        public string customerContact {
            get; set;
        } = string.Empty;


        public string customerPhone {
            get; set;
        } = string.Empty;


        public string supplier {
            get; set;
        } = string.Empty;


        public string supplierAddress {
            get; set;
        } = string.Empty;


        public string supplierPhonoNumber {
            get; set;
        } = string.Empty;


        public string salesAgent {
            get; set;
        } = string.Empty;


        public string paymentCondition {
            get; set;
        } = string.Empty;


        public string shippingMethod {
            get; set;
        } = string.Empty;


        public string OrderNotes {
            get; set;
        } = string.Empty;


        public int itemsCount {
            get; set;
        }


        public decimal billingSubtotal {
            get; set;
        } // ItemsTotal


        public decimal shipmentTotal {
            get; set;
        } // Shipment


        public decimal taxes {
            get; set;
        }


        public decimal billingTotal {
            get; set;
        } // orderTotal


        public List<BillingItemDto> billingItems {
            get; set;
        } = new List<BillingItemDto>();

    } // class ShippingBillingDto


    public class BillingItemDto {


        public string productPresentation {
            get; set;
        } = string.Empty;


        public string productCode {
            get; set;
        } = string.Empty;


        public string productName {
            get; set;
        } = string.Empty;


        public decimal quantity {
            get; set;
        }


        public decimal unitPrice {
            get; set;
        }


        public decimal salesPrice {
            get; set;
        }


        public string discountPolicy {
            get; set;
        } = string.Empty;


        public decimal discount1 {
            get; set;
        }


        public decimal discount2 {
            get; set;
        }


        public decimal subtotal {
            get; set;
        }


    } // class BillingItemDto


} // namespace Empiria.Trade.Reporting.WebApi.Client.Adapters
