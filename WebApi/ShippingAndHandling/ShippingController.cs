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
using Empiria.WebApi;
using System.Web.Http;
using Empiria.Trade.Sales.ShippingAndHandling.UseCases;
using Empiria.Trade.Sales.ShippingAndHandling.Adapters;

namespace Empiria.Trade.WebApi.ShippingAndHandling {
  
  /// <summary></summary>
  public class ShippingController : WebApiController {


    #region Web Apis


    [HttpGet]
    [Route("v4/trade/shipping-and-handling/shipping/parcel-suppliers")]
    public CollectionModel GetPackageTypeList() {

      using (var usecases = ShippingUseCases.UseCaseInteractor()) {

        FixedList<INamedEntity> packingDetail = usecases.GetParcelSupplierList();

        return new CollectionModel(this.Request, packingDetail);
      }
    }


    [HttpPost]
    [Route("v4/trade/shipping-and-handling/shipping/")]
    public SingleObjectModel CreateShippingOrder([FromBody] ShippingFields fields) {

      using (var usecases = ShippingUseCases.UseCaseInteractor()) {

        ShippingDto shippingOrder = usecases.CreateShippingOrder(fields);

        return new SingleObjectModel(this.Request, shippingOrder);
      }
    }


    [HttpPost]
    [Route("v4/trade/sales/shipping/parcel-delivery")]
    public SingleObjectModel GetShippingOrderForParcelDelivery([FromBody] ShippingQuery query) {

      using (var usecases = ShippingUseCases.UseCaseInteractor()) {

        ShippingDto shippingOrder = usecases.GetShippingOrderByQuery(query);

        return new SingleObjectModel(this.Request, shippingOrder);
      }
    }


    #endregion Web Apis
  }
}
