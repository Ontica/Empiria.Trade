﻿/* Empiria Trade *********************************************************************************************
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


    [HttpGet]
    [Route("v4/trade/shipping-and-handling/packing/package-types")]
    public CollectionModel GetPackageTypeList() {

      using (var usecases = PackagingUseCases.UseCaseInteractor()) {

        FixedList<INamedEntity> packingDetail = usecases.GetPackageTypeList();

        return new CollectionModel(this.Request, packingDetail);
      }
    }


    [HttpGet]
    [Route("v4/trade/shipping-and-handling/packing/{orderUID:guid}")]
    public SingleObjectModel GetPackagingForOrder([FromUri] string orderUID) {

      using (var usecases = PackagingUseCases.UseCaseInteractor()) {

        IShippingAndHandling packageForItems = usecases.GetPackagingForOrder(orderUID);

        return new SingleObjectModel(this.Request, packageForItems);
      }
    }



    [HttpPost]
    [Route("v4/trade/shipping-and-handling/packing/{orderUID:guid}/packing-item")]
    public SingleObjectModel CreatePackageForItem([FromUri] string orderUID,
                                                [FromBody] PackingItemFields packingItemFields) {

      base.RequireBody(packingItemFields);

      using (var usecases = PackagingUseCases.UseCaseInteractor()) {

        IShippingAndHandling packingItem = usecases.CreatePackageForItem(orderUID, packingItemFields);

        return new SingleObjectModel(this.Request, packingItem);
      }
    }


    [HttpPut]
    [Route("v4/trade/shipping-and-handling/packing/{orderUID:guid}/packing-item/{packingItemUID:guid}")]
    public SingleObjectModel UpdatePackageForItem([FromUri] string orderUID,
                                                [FromUri] string packingItemUID,
                                                [FromBody] PackingItemFields packingItemFields) {
      
      base.RequireBody(packingItemFields);

      using (var usecases = PackagingUseCases.UseCaseInteractor()) {

        IShippingAndHandling packingItem = usecases.UpdatePackageForItem(orderUID, packingItemUID, packingItemFields);

        return new SingleObjectModel(this.Request, packingItem);
      }
    }


    [HttpDelete]
    [Route("v4/trade/shipping-and-handling/packing/{orderUID:guid}/packing-item/{packingItemUID:guid}")]
    public SingleObjectModel DeletePackageForItem([FromUri] string orderUID, [FromUri] string packingItemUID) {
      
      using (var usecases = PackagingUseCases.UseCaseInteractor()) {

        IShippingAndHandling packingItem = usecases.DeletePackageForItem(orderUID, packingItemUID);

        return new SingleObjectModel(this.Request, packingItem);
      }
    }


    [HttpPost]
    [Route("v4/trade/shipping-and-handling/packing/{orderUID:guid}/packing-item/" +
           "{packingItemUID:guid}")]
    public SingleObjectModel CreatePackingOrderItemFields([FromUri] string orderUID,
                                                [FromUri] string packingItemUID,
                                                [FromBody] MissingItemField missingItemFields) {
      
      base.RequireBody(missingItemFields);

      using (var usecases = PackagingUseCases.UseCaseInteractor()) {

        IShippingAndHandling packingOrderItemFields = usecases.CreatePackingOrderItemFields(
                                                        orderUID, packingItemUID, missingItemFields);

        return new SingleObjectModel(this.Request, packingOrderItemFields);
      }
    }


    [HttpDelete]
    [Route("v4/trade/shipping-and-handling/packing/{orderUID:guid}/packing-item/" +
           "{packingItemUID:guid}/{packingItemEntryUID:guid}")]
    public SingleObjectModel DeletePackingOrderItemFields([FromUri] string orderUID,
                                                          [FromUri] string packingItemUID,
                                                          [FromUri] string packingItemEntryUID) {
      
      using (var usecases = PackagingUseCases.UseCaseInteractor()) {

        IShippingAndHandling packingOrderItemFields = usecases.DeletePackingOrderItem(
                                                  orderUID, packingItemUID, packingItemEntryUID);

        return new SingleObjectModel(this.Request, packingOrderItemFields);
      }
    }


    #endregion Web Apis


  } // Class PackagingController

} // namespace Empiria.Trade.WebApi.ShippingAndHandling
