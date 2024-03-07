/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Delivery Management                           Component : Web Api                              *
*  Assembly : Empiria.Trade.WebApi.ShippingAndHandling.dll  Pattern   : Controller                           *
*  Type     : DeliveryController                            License   : Please read LICENSE.txt file         *
*                                                                                                            *
*  Summary  : Query web API used to manage shippings for delivery.                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Sales.ShippingAndHandling.Adapters;
using Empiria.Trade.Sales.ShippingAndHandling.UseCases;
using System.Web.Http;
using Empiria.WebApi;

namespace Empiria.Trade.WebApi.ShippingAndHandling {


  /// <summary>Query web API used to manage shippings for delivery.</summary>
  internal class DeliveryController : WebApiController {


    #region Web apis


    [HttpPost]
    [Route("v4/trade/sales/delivery/search")]
    [Route("v4/trade/sales/shipping/search-for-delivery")]
    public CollectionModel GetShippingList([FromBody] ShippingQuery query) {

      using (var usecases = DeliveryUseCase.UseCaseInteractor()) {

        FixedList<ShippingEntryDto> shippingList = usecases.GetShippingsForDelivery(query);

        return new CollectionModel(this.Request, shippingList);
      }
    }


    [HttpPost]
    [Route("v4/trade/sales/shipping/{shippingOrderUID:guid}/close-shipping")]
    public SingleObjectModel UpdateShippingStatus([FromUri] string shippingOrderUID) {

      using (var usecases = DeliveryUseCase.UseCaseInteractor()) {

        ShippingDto shippingOrder = usecases.UpdateShippingStatus(shippingOrderUID);

        return new SingleObjectModel(this.Request, shippingOrder);
      }
    }

    #endregion Web apis

  } // class DeliveryController


} // namespace Empiria.Trade.WebApi.ShippingAndHandling
