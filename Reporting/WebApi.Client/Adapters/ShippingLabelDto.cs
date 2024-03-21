/* Empiria Trade Reporting ***********************************************************************************
*                                                                                                            *
*  Module   : Shipping Management                        Component : Interface adapters                      *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Data Transfer Object                    *
*  Type     : ShippingLabelDto                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return shipping label.                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Reporting.WebApi.Client.Adapters {


    /// <summary>Output DTO used to return shipping label.</summary>
    public class ShippingLabelDto {


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


    /// <summary>Output DTO used to return shipping label by pallet.</summary>
    public class ShippingLabelByPalletDto {

        public string company {
            get; set;
        } = string.Empty;


        public string companyAddress {
            get; set;
        } = string.Empty;


        public string shippingNumber {
            get; set;
        } = string.Empty;


        public string shippingType {
            get; set;
        } = string.Empty;


        public string parcelSupplier {
            get; set;
        } = string.Empty;


        public string customer {
            get; set;
        } = string.Empty;


        public string customerAddress {
            get; set;
        } = string.Empty;


        public string customerPhoneNumber {
            get; set;
        } = string.Empty;


        public string palletName {
            get; set;
        } = string.Empty;


        public string palletCount {
            get; set;
        } = string.Empty;


        public int packageQuantity {
            get; set;
        }


        public int tiedQuantity {
            get; set;
        }


        public int bagQuantity {
            get; set;
        }

    } // class ShippingLabelByPallet

} // namespace Empiria.Trade.Reporting.WebApi.Client.Adapters
