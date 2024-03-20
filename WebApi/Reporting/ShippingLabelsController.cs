/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Shipping and handling Management              Component : Web Api                              *
*  Assembly : Empiria.Trade.WebApi.ShippingAndHandling.dll  Pattern   : Controller                           *
*  Type     : ShippingLabelsController                      License   : Please read LICENSE.txt file         *
*                                                                                                            *
*  Summary  : Query web API used to manage shippings labels.                                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Diagnostics;
using System.Reflection.Emit;
using System.Web.Http;
using Empiria.Trade.Core.Catalogues;
using Empiria.Trade.Sales.ShippingAndHandling.Adapters;
using Empiria.Trade.Sales.ShippingAndHandling.UseCases;
using Empiria.WebApi;

namespace Empiria.Trade.WebApi.Reporting {

    
    /// <summary>Query web API used to manage shippings labels.</summary>
    [AllowAnonymous]
    public class ShippingLabelsController : WebApiController {


    #region Web apis

    //trade/reporting/shipping/{shippingUID}/billings

    //trade/reporting/shipping/{shippingUID}/billing/{orderUID}

    //trade/reporting/shipping/{shippingUID}/labels


    [HttpGet]
    [AllowAnonymous]
    [Route("v4/trade/sales/shipping/{shippingUID}/labels")]
    public CollectionModel GetShippingLabels([FromUri] string shippingUID) {

      using (var usecases = ShippingLabelUseCases.UseCaseInteractor()) {

        FixedList<ShippingLabel> shippingLabels = usecases.GetShippingLabels(shippingUID);

        return new CollectionModel(this.Request, shippingLabels);
      }
    }


    #endregion Web apis


  } // public class ShippingLabelsController

} // namespace Empiria.Trade.WebApi.Reporting
