/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Packaging Management                          Component : Web Api                              *
*  Assembly : Empiria.Trade.WebApi.ShippingAndHandling.dll  Pattern   : Controller                           *
*  Type     : PackagingController                           License   : Please read LICENSE.txt file         *
*                                                                                                            *
*  Summary  : Query web API used to manage order packaging.                                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Threading.Tasks;
using System.Web.Http;
using Empiria.Services;
using System.Xml.Linq;
using Empiria.WebApi;
using Empiria.Trade.ShippingAndHandling.Adapters;
using Empiria.Trade.ShippingAndHandling.UseCases;
//using Empiria.Trade.Shipping.UseCases;

namespace Empiria.Trade.WebApi.ShippingAndHandling {

  /// <summary>Query web API used to manage order packaging.</summary>
  public class PackagingController : WebApiController {


    #region Web Apis


    [HttpPost]
    [Route("v4/trade/shipping-and-handling/generate-packing")]
    public CollectionModel CreatePackingOrder([FromBody] PackingOrderFields order) {

      base.RequireBody(order);

      using (var usecases = ShippingAndHandlingUseCases.UseCaseInteractor()) {

        FixedList<IShippingAndHandling> orderShippingDto = usecases.CreatePackingOrder(order);

        return new CollectionModel(this.Request, orderShippingDto);
      }
    }


    #endregion Web Apis


  } // Class PackagingController

} // namespace Empiria.Trade.WebApi.ShippingAndHandling
