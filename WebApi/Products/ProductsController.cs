/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Web Api                                 *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Controller                              *
*  Type     : ProductTests                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Query web API used to retrieve Products.                                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Threading.Tasks;

using System.Web.Http;
using Empiria.Trade.Products.Adapters;
using Empiria.Trade.Products.UseCases;
using Empiria.WebApi;


namespace Empiria.Trade.WebApi.Products {

  /// <summary>Query web API used to retrieve Products.</summary>
  [AllowAnonymous]
  public class ProductsController : WebApiController {


    //[HttpPost]
    //[Route("trade/products/products-list")]
    //public async Task<SingleObjectModel> GetProductsList([FromBody] ProductQuery keywords) {

    //  base.RequireBody(keywords);

    //  using (var usecases = ProductsUseCases.UseCaseInteractor()) {

    //    ProductDto productDto = await usecases.BuildProducts(keywords).ConfigureAwait(false);

    //    return new SingleObjectModel(this.Request, productDto);
    //  }
    //}


    [HttpPost]
    [Route("trade/products/products-list")]
    public async Task<CollectionModel> GetProductsDto([FromBody] ProductQuery keywords) {

      base.RequireBody(keywords);

      using (var usecases = ProductsUseCases.UseCaseInteractor()) {

        FixedList<IProductEntryDto> productDto = await usecases.BuildProductsList(keywords)
                                                .ConfigureAwait(false);

        return new CollectionModel(this.Request, productDto);
      }
    }


  } // class ProductsController

} // namespace Empiria.Trade.WebApi.Products
