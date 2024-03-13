/* Trade NetStandard Bridge **********************************************************************************
*                                                                                                            *
*  Module   : Shipping Management                        Component : Use cases Layer                         *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Use case interactor class               *
*  Type     : ShippingReportsUseCase                     License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases used to build shipping.                                                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Empiria.Services;
using Empiria.Trade.Sales.ShippingAndHandling.Adapters;
using Empiria.Trade.Sales.ShippingAndHandling.UseCases;

namespace Trade.NetStandard.Bridge.UseCases {


    public class ShippingReportsUseCase {


        public ShippingReportsUseCase() {
            // no-op
        }


        #region Public methods


        public List<ShippingData> GetOrdersDataForLabel() {

            ShippingUseCases shippingUseCases = new ShippingUseCases();

            ShippingQuery query = new ShippingQuery();
            query.Keywords = "";
            query.ParcelSupplierUID = "";
            query.Status = ShippingStatus.Todos;

            var data = shippingUseCases.GetShippingsList(query).ToList();

            List<ShippingData> list = new List<ShippingData>();

            foreach (var item in data) {

                ShippingData shippingData = new ShippingData() {
                    
                    ShippingUID = item.ShippingUID,
                    ShippingGuide = item.ShippingGuide,
                    OrdersCount = item.OrdersCount,
                    OrdersTotal = item.OrdersTotal,
                    TotalPackages = item.TotalPackages
                };
                list.Add(shippingData);
            }

            return list;
        }


        public List<ShippingData> GetOrdersDataForLabel_() {

            List<ShippingData> list = new List<ShippingData>();

            for (int i = 0; i < 4; i++) {
                ShippingData shippingData = new ShippingData() {

                    ShippingUID = $"UID {i}",
                    ShippingGuide = $"GUIA {i}",
                    OrdersCount = i+1,
                    OrdersTotal = i+2,
                    TotalPackages = i+3
                };
                list.Add(shippingData);
            }
            return list;
        }


            #endregion Public methods

        }

    public class ShippingData {

        public string ShippingUID {
            get; set;
        } = string.Empty;


        public string ShippingGuide {
            get; set;
        } = string.Empty;


        public int OrdersCount {
            get; set;
        }


        public decimal OrdersTotal {
            get; set;
        }


        public int TotalPackages {
            get; set;
        }



    }
}
