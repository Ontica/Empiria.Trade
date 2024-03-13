/* Empiria Trade Reporting Web *******************************************************************************
*                                                                                                            *
*  Module   : Shipping Management                        Component : Use cases Layer                         *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Use case interactor class               *
*  Type     : ReportsService                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Services to build shipping reports.                                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Reporting.Web.Models;
using Trade.NetStandard.Bridge.UseCases;

namespace Empiria.Trade.Reporting.Web.Services {

    /// <summary></summary>
    public class ReportsService {

        #region Constructors and parsers

        public ReportsService() {
            // no-op
        }

        #endregion Constructors and parsers


        #region Public methods


        public List<ShippingData> GetOrdersDataForLabel() {

            ShippingReportsUseCase shippingUseCases = new ShippingReportsUseCase();
            
            return shippingUseCases.GetOrdersDataForLabel();
        }


        #endregion Public methods

    }
}
