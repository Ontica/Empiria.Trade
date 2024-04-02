/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Shipping Management                        Component : Interface adapters                      *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Data Transfer Object                    *
*  Type     : ShippingLabel                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return shipping label.                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Orders;

namespace Empiria.Trade.Sales.ShippingAndHandling.Adapters {

    /// <summary>Output DTO used to return shipping label.</summary>
    public class ShippingLabel {

        public string Company {
            get; set;
        }


        public string CompanyAddress {
            get; set;
        }


        public string ShippingNumber {
            get; set;
        }


        public ShippingMethods ShippingType {
            get; set;
        }


        public string ParcelSupplier {
            get; set;
        }


        public string Customer {
            get; set;
        }


        public string CustomerAddress {
            get; set;
        }


        public string CustomerPhoneNumber {
            get; set;
        }


        public string PackingName {
            get; set;
        }


        public string PackingCount {
            get; set;
        }


        public int PackageQuantity {
            get; set;
        }


        public int TiedQuantity {
            get; set;
        }


        public int BagQuantity {
            get; set;
        }


    }


    public class SupplyLabe {


        public string OrderUID {
            get; set;
        }


        public string ProductCode {
            get; set;
        }


        public string ProductPresentation {
            get; set;
        }


        public string Description {
            get; set;
        }


        public string Comments {
            get; set;
        }


        public decimal Quantity {
            get; set;
        }


        public string Ubication {
            get; set;
        }

    } // class ShippingLabel


} // namespace Empiria.Trade.Sales.ShippingAndHandling.Adapters
