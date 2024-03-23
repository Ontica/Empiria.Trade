/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Shipping Management                        Component : Interface adapters                      *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Data Transfer Object                    *
*  Type     : BillingDto                                 License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return sales order data for billing.                                        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Products.Adapters;

namespace Empiria.Trade.Sales.ShippingAndHandling.Adapters {

    /// <summary>Output DTO used to return sales order data for billing.</summary>
    public class BillingDto {


        public string OrderUID {
            get; set;
        }


        public string OrderNumber {
            get; set;
        }


        public string Customer {
            get; set;
        }


        public string CustomerAddress {
            get; set;
        }


        public string CustomerContact {
            get; set;
        }


        public string CustomerPhone {
            get; set;
        }


        public string Supplier {
            get; set;
        }


        public string SalesAgent {
            get; set;
        }


        public string PaymentCondition {
            get; set;
        }


        public string ShippingMethod {
            get; set;
        }


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


        public FixedList<BillingItemDto> BillingItems {
            get; set;
        } = new FixedList<BillingItemDto>();


    } // class BillingDto


    public class BillingItemDto {


        public string ProductPresentation {
            get; set;
        }


        public string ProductCode {
            get; set;
        }


        public string ProductName {
            get; set;
        } // description


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
        }


        public decimal Discount1 {
            get; set;
        }


        public decimal Discount2 {
            get; set;
        }


        public decimal Subtotal {
            get; set;
        }


    }

} // namespace Empiria.Trade.Sales.ShippingAndHandling.Adapters
