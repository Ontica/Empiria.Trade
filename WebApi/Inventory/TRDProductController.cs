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
using System.Threading.Tasks;
using System.Web.Http;
using Empiria.Services;
using System.Xml.Linq;
using Empiria.Trade.Products.Adapters;
using Empiria.Trade.Products.UseCases;
using Empiria.WebApi;
using Empiria.Trade.Sales.UseCases;

namespace Empiria.Trade.WebApi.Inventory {

  /// <summary>Query web API used to retrieve TRDProducts.</summary>
  public class TRDProductController : WebApiController{


    #region Web Apis


    [HttpGet]
    [Route("v4/trade/products/product/{productUID}")]
    public SingleObjectModel GetTRDProduct([FromUri] string productUID) {

      base.RequireBody(productUID);

      using (var usecases = TRDProductUseCases.UseCaseInteractor()) {

        IProductEntryDto productDto = usecases.GetTRDProduct(productUID);
        
        return new SingleObjectModel(this.Request, productDto);
      }
    }


    [HttpPost]
    [Route("v4/trade/products/search-products")]
    public async Task<CollectionModel> GetProductsDto([FromBody] ProductQuery keywords) {

      base.RequireBody(keywords);

      using (var usecases = TRDProductUseCases.UseCaseInteractor()) {

        FixedList<IProductEntryDto> productDto = await usecases.GetProductsList(keywords)
                                                .ConfigureAwait(false);

        return new CollectionModel(this.Request, productDto);
      }
    }


    [HttpPost]
    [Route("v4/trade/products/search-products-for-order")]
    public async Task<CollectionModel> GetProductsByCustomer([FromBody] ProductQuery keywords) {

      base.RequireBody(keywords);

      using (var usecases = ProductForOrderUseCases.UseCaseInteractor()) {

        FixedList<IProductEntryDto> productDto = await usecases.GetProductsForOrder(keywords)
                                                .ConfigureAwait(false);

        return new CollectionModel(this.Request, productDto);
      }
    }


    [HttpPost]
    [Route("v4/trade/products/update-uid")]
    public async Task<SingleObjectModel> UpdateTableUID([FromBody] TableQuery query) {

      base.RequireBody(query);

      using (var usecases = TRDProductUseCases.UseCaseInteractor()) {
        
        string msj = await usecases.UpdateGUID(query).ConfigureAwait(false);

        return new SingleObjectModel(this.Request, msj);
      }
    }


    #endregion Web Apis


  } // class TRDProductController

} // namespace Empiria.Trade.WebApi.Inventory
