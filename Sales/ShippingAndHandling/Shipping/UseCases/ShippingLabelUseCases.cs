/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Shipping Management                        Component : Use cases Layer                         *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Use case interactor class               *
*  Type     : ShippingLabelUseCases                      License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases used to build shipping labels.                                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Services;
using Empiria.Trade.Sales.Data;
using Empiria.Trade.Sales.ShippingAndHandling.Adapters;
using Empiria.Trade.Sales.ShippingAndHandling.Data;
using Empiria.Trade.Sales.ShippingAndHandling.Domain;
using Empiria.Trade.Sales.ShippingAndHandling.UseCases;

namespace Empiria.Trade.Sales.ShippingAndHandling.UseCases {

    /// <summary>Use cases used to build shipping labels.</summary>
    public class ShippingLabelUseCases : UseCase {

        #region Constructors and parsers

        public ShippingLabelUseCases() {
            // no-op
        }

        static public ShippingLabelUseCases UseCaseInteractor() {
            return CreateInstance<ShippingLabelUseCases>();
        }


        #endregion Constructors and parsers


        public FixedList<SupplyLabe> GetSupplyLabels(string shippingUID) {

            var builder = new ShippingLabelBuilder();

            FixedList<SupplyLabe> shippingLabels = builder.GetSupplyLabels(shippingUID);

            return shippingLabels;
        }


        public FixedList<ShippingLabel> GetShippingLabels(string shippingUID) {

            var builder = new ShippingLabelBuilder();

            FixedList<ShippingLabel> shippingLabels = builder.GetShippingLabels(shippingUID);

            return shippingLabels;
        }


        #region Use cases





        #endregion Use cases

    } // class ShippingLabelUseCases

} // namespace Empiria.Trade.Sales.ShippingAndHandling.UseCases
