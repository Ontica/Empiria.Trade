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
using Empiria.Trade.ShippingAndHandling.UseCases;
using Empiria.WebApi;
using System.Web.Http;

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


    #endregion Web Apis
  }
}
