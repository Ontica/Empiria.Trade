/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Web Api                                 *
*  Assembly : Empiria.Trade.WebApi.Inventory.dll         Pattern   : Controller                              *
*  Type     : TRDProductController                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Query web API used to retrieve TRDProducts.                                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Web.Http;
using Empiria.Trade.Inventory.Products.Adapters;
using Empiria.Trade.Inventory.Products.UseCases;
using Empiria.WebApi;

namespace Empiria.Trade.WebApi.Inventory {

  /// <summary>Query web API used to retrieve TRDProducts.</summary>
  internal class TRDProductController : WebApiController{


    #region Web Apis


    [HttpPost]
    [Route("trade/products/product/{productUID:guid}")]
    public SingleObjectModel GetTRDProduct([FromUri] string productUID) {

      base.RequireBody(productUID);

      using (var usecases = TRDProductUseCases.UseCaseInteractor()) {

        TRDProductsEntryDto productDto = usecases.GetTRDProduct(productUID);

        return new SingleObjectModel(this.Request, productDto);
      }
    }


    #endregion Web Apis


  } // class TRDProductController

} // namespace Empiria.Trade.WebApi.Inventory
