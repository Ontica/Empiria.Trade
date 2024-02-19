/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Packaging Management                          Component : Web Api                              *
*  Assembly : Empiria.Trade.WebApi.ShippingAndHandling.dll  Pattern   : Controller                           *
*  Type     : PackagingController                           License   : Please read LICENSE.txt file         *
*                                                                                                            *
*  Summary  : Query web API used to manage shippings.                                                        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.WebApi;
using System.Web.Http;
using Empiria.Trade.Sales.ShippingAndHandling.UseCases;
using Empiria.Trade.Sales.ShippingAndHandling.Adapters;

namespace Empiria.Trade.WebApi.ShippingAndHandling {

  /// <summary></summary>
  public class ShippingController : WebApiController {


    #region Web Apis


    [HttpPost]
    [Route("v4/trade/sales/shipping/{shippingOrderUID}/send")]
    public SingleObjectModel ChangeOrdersForShippingStatus([FromUri] string shippingOrderUID) {

      using (var usecases = ShippingUseCases.UseCaseInteractor()) {

        ShippingDto shippingOrder = usecases.ChangeOrdersForShippingStatus(shippingOrderUID);

        return new SingleObjectModel(this.Request, shippingOrder);
      }
    }


    [HttpPost]
    [Route("v4/trade/sales/shipping/{shippingOrderUID:guid}/order/{orderUID:guid}")]
    public SingleObjectModel CreateOrderForShipping([FromUri] string shippingOrderUID,
                                                     [FromUri] string orderUID) {

      using (var usecases = ShippingUseCases.UseCaseInteractor()) {

        ShippingDto shippingOrder = usecases.CreateOrderForShipping(shippingOrderUID, orderUID);

        return new SingleObjectModel(this.Request, shippingOrder);
      }
    }


    [HttpPost]
    [Route("v4/trade/sales/shipping/")]
    public SingleObjectModel CreateShippingOrder([FromBody] ShippingFields fields) {

      using (var usecases = ShippingUseCases.UseCaseInteractor()) {

        ShippingDto shippingOrder = usecases.CreateShippingOrder(fields);

        return new SingleObjectModel(this.Request, shippingOrder);
      }
    }


    [HttpDelete]
    [Route("v4/trade/sales/shipping/{shippingOrderUID:guid}/order/{orderUID:guid}")]
    public SingleObjectModel DeleteOrderForShipping([FromUri] string shippingOrderUID,
                                                     [FromUri] string orderUID) {

      using (var usecases = ShippingUseCases.UseCaseInteractor()) {

        ShippingDto shippingOrder = usecases.DeleteOrderForShipping(shippingOrderUID, orderUID);

        return new SingleObjectModel(this.Request, shippingOrder);
      }
    }


    [HttpGet]
    [Route("v4/trade/sales/shipping/parcel-suppliers")]
    public CollectionModel GetPackageTypeList() {

      using (var usecases = ShippingUseCases.UseCaseInteractor()) {

        FixedList<INamedEntity> packingDetail = usecases.GetParcelSupplierList();

        return new CollectionModel(this.Request, packingDetail);
      }
    }
    

    [HttpPost]
    [Route("v4/trade/sales/shipping/search")]
    public CollectionModel GetShippingList([FromBody] ShippingQuery query) {

      using (var usecases = ShippingUseCases.UseCaseInteractor()) {

        FixedList<ShippingEntryDto> shippingList = usecases.GetShippingsList(query);

        return new CollectionModel(this.Request, shippingList);
      }
    }


    [HttpPost]
    [Route("v4/trade/sales/shipping/parcel-delivery")]
    public SingleObjectModel GetShippingOrderForParcelDelivery([FromBody] ShippingFieldsQuery query) {

      using (var usecases = ShippingUseCases.UseCaseInteractor()) {

        ShippingDto shippingOrder = usecases.GetShippingOrderByQuery(query);

        return new SingleObjectModel(this.Request, shippingOrder);
      }
    }


    [HttpGet]
    [Route("v4/trade/sales/shipping/parcel-delivery/{shippingOrderUID:guid}")]
    public SingleObjectModel GetShippingOrder([FromUri] string shippingOrderUID) {

      using (var usecases = ShippingUseCases.UseCaseInteractor()) {

        ShippingDto shippingOrder = usecases.GetShippingByUID(shippingOrderUID);

        return new SingleObjectModel(this.Request, shippingOrder);
      }
    }


    [HttpPut]
    [Route("v4/trade/sales/shipping/{shippingOrderUID:guid}")]
    public SingleObjectModel UpdateShippingOrder([FromUri] string shippingOrderUID, [FromBody] ShippingFields fields) {

      using (var usecases = ShippingUseCases.UseCaseInteractor()) {

        ShippingDto shippingOrder = usecases.UpdateShippingOrder(shippingOrderUID, fields);

        return new SingleObjectModel(this.Request, shippingOrder);
      }
    }


    #endregion Web Apis
  }
}
