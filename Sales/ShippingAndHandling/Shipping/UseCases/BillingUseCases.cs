/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Shipping Management                        Component : Use cases Layer                         *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Use case interactor class               *
*  Type     : BillingUseCases                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases used to build shipping billing.                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Services;
using Empiria.Trade.Sales.Adapters;
using Empiria.Trade.Sales.ShippingAndHandling.Adapters;
using Empiria.Trade.Sales.ShippingAndHandling.Domain;
using Empiria.Trade.Sales.UseCases;

namespace Empiria.Trade.Sales.ShippingAndHandling.UseCases {


    /// <summary>Use cases used to build shipping billing.</summary>
    public class BillingUseCases : UseCase {

        #region Constructors and parsers

        public BillingUseCases() {
            // no-op
        }


        static public BillingUseCases UseCaseInteractor() {
            return CreateInstance<BillingUseCases>();
        }


        #endregion Constructors and parsers

        #region Public methods


        public BillingDto GetShippingBilling(string shippingUID, string orderUID) {

            SalesOrder order = SalesOrder.Parse(orderUID);

            order.CalculateSalesOrder(QueryType.SalesShipping);

            var builder = new ShippingLabelBuilder();

            return builder.GetBillingForOrder(order);
        }


        //public FixedList<BillingDto> GetShippingBillingList(string shippingUID) {

        //    var builder = new ShippingLabelBuilder();

        //    return builder.GetShippingBillingList(shippingUID);
        //}

        #endregion Public methods

    } // class BillingUseCases

} // namespace Empiria.Trade.Sales.ShippingAndHandling.UseCases
