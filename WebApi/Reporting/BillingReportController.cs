/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Shipping and handling Management              Component : Web Api                              *
*  Assembly : Empiria.Trade.WebApi.ShippingAndHandling.dll  Pattern   : Controller                           *
*  Type     : ShippingLabelsController                      License   : Please read LICENSE.txt file         *
*                                                                                                            *
*  Summary  : Query web API used to manage billing reports.                                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Web.Http;
using Empiria.Trade.Core.Catalogues;
using Empiria.Trade.Sales.ShippingAndHandling.Adapters;
using Empiria.Trade.Sales.ShippingAndHandling.UseCases;
using Empiria.WebApi;

namespace Empiria.Trade.WebApi.Reporting {
    

    /// <summary>Summary  : Query web API used to manage billing reports.</summary>
    [AllowAnonymous]
    public class BillingReportController : WebApiController {


        #region Web apis


        [HttpGet]
        [AllowAnonymous]
        [Route("v4/trade/sales/shipping/{shippingUID}/billing/{orderUID}")]
        public SingleObjectModel GetShippingBilling([FromUri] string shippingUID, [FromUri] string orderUID) {

            using (var usecases = BillingUseCases.UseCaseInteractor()) {

                BillingDto billing = usecases.GetShippingBilling(shippingUID, orderUID);

                return new SingleObjectModel(this.Request, billing);
            }
        }


        //[HttpGet]
        //[Route("v4/trade/sales/shipping/{shippingUID}/billing")]
        //public CollectionModel GetShippingBillings([FromUri] string shippingUID) {

        //    using (var usecases = BillingUseCases.UseCaseInteractor()) {

        //        FixedList<BillingDto> billingList = usecases.GetShippingBillingList(shippingUID);

        //        return new CollectionModel(this.Request, billingList);
        //    }
        //}


        #endregion Web apis


    } // public class BillingReportController

} // namespace Empiria.Trade.WebApi.Reporting
