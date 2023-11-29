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
using Empiria.Trade.Orders;
//using Empiria.Trade.Shipping.UseCases;

namespace Empiria.Trade.WebApi.ShippingAndHandling {

  /// <summary>Query web API used to manage order packaging.</summary>
  public class PackagingController : WebApiController {


    #region Web Apis


    //[HttpPost]
    //[Route("v4/trade/shipping-and-handling/packing/get-detail")]
    //public SingleObjectModel GetPackingDetail([FromBody] PackingOrderFields order) {

    //  base.RequireBody(order);

    //  using (var usecases = ShippingAndHandlingUseCases.UseCaseInteractor()) {

    //    IShippingAndHandling packingDetail = usecases.GetPackingDetail();

    //    return new SingleObjectModel(this.Request, packingDetail);
    //  }
    //}


    [HttpGet]
    [Route("v4/trade/shipping-and-handling/packing/package-type")]
    public CollectionModel GetPackageTypeList() {

      using (var usecases = ShippingAndHandlingUseCases.UseCaseInteractor()) {

        FixedList<INamedEntity> packingDetail = usecases.GetPackageTypeList();

        return new CollectionModel(this.Request, packingDetail);
      }
    }


    [HttpGet]
    [Route("v4/trade/shipping-and-handling/packing/{orderUID:guid}")]
    public SingleObjectModel GetPackingDetail([FromUri] string orderUID) {

      using (var usecases = ShippingAndHandlingUseCases.UseCaseInteractor()) {

        IShippingAndHandling packingDetail = usecases.GetPackingByOrder(orderUID);

        return new SingleObjectModel(this.Request, packingDetail);
      }
    }



    [HttpPost]
    [Route("v4/trade/shipping-and-handling/packing/{orderUID:guid}/packing-item")]
    public SingleObjectModel CreatePackingOrder([FromUri] string orderUID,
                                                [FromBody] PackingOrderFields order) {

      base.RequireBody(order);

      using (var usecases = ShippingAndHandlingUseCases.UseCaseInteractor()) {

        IShippingAndHandling orderShippingDto = usecases.CreatePackingOrder(orderUID, order);

        return new SingleObjectModel(this.Request, orderShippingDto);
      }
    }


    [HttpPut]
    [Route("v4/trade/shipping-and-handling/packing/{orderUID:guid}/packing-item/${packingItemUID}")]
    public SingleObjectModel UpdatePackingOrder([FromUri] string packingItemUID,
                                                [FromBody] PackingOrderFields order) {
      //TODO UPDATE
      base.RequireBody(order);

      using (var usecases = ShippingAndHandlingUseCases.UseCaseInteractor()) {

        IShippingAndHandling orderShippingDto = usecases.UpdatePackingOrder(packingItemUID, order);

        return new SingleObjectModel(this.Request, orderShippingDto);
      }
    }


    [HttpDelete]
    [Route("v4/trade/shipping-and-handling/packing/${orderUID}/packing-item/{packingItemUID:guid}")]
    public SingleObjectModel DeletePackingOrder([FromUri] string packingItemUID) {
      //TODO DELETE - REGRESAR EL PACKING
      
      using (var usecases = ShippingAndHandlingUseCases.UseCaseInteractor()) {

        IShippingAndHandling orderShippingDto = usecases.DeletePackingOrder(packingItemUID);

        return new SingleObjectModel(this.Request, orderShippingDto);
      }
    }


    #endregion Web Apis


  } // Class PackagingController

} // namespace Empiria.Trade.WebApi.ShippingAndHandling
