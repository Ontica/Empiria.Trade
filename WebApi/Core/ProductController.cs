/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Web Api                                 *
*  Assembly : Empiria.Trade.WebApi.Inventory.dll         Pattern   : Controller                              *
*  Type     : ProductController                          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Query web API used to retrieve Products.                                                    *
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
using Empiria.Trade.Sales.Adapters;
using Empiria.Trade.Core.Catalogues;

namespace Empiria.Trade.WebApi.Core
{

    /// <summary>Query web API used to retrieve Products.</summary>
    public class ProductController : WebApiController
    {


        #region Web Apis


        [HttpGet]
        [Route("v4/trade/products/product/{productUID}")]
        public SingleObjectModel GetProduct([FromUri] string productUID)
        {
            RequireBody(productUID);

            using (var usecases = ProductUseCases.UseCaseInteractor())
            {
                IProductEntryDto productDto = usecases.GetProductByUID(productUID);
                return new SingleObjectModel(Request, productDto);
            }
        }


        [HttpPost]
        [Route("v4/trade/products/search-products")]
        public async Task<CollectionModel> GetProductsDto([FromBody] ProductQuery query)
        {
            RequireBody(query);

            using (var usecases = ProductUseCases.UseCaseInteractor())
            {
                FixedList<IProductEntryDto> productDto = await usecases.GetProductsList(query)
                                                        .ConfigureAwait(false);
                return new CollectionModel(Request, productDto);
            }
        }


        [HttpPost]
        [Route("v4/trade/products/search-products-for-inventory")]
        public async Task<CollectionModel> GetProductsForInventory([FromBody] ProductQuery query) {
          RequireBody(query);

          using (var usecases = ProductUseCases.UseCaseInteractor()) {

            query.OnStock = false;
            FixedList<IProductEntryDto> productDto = await usecases.GetProductsList(query)
                                                    .ConfigureAwait(false);
            return new CollectionModel(Request, productDto);
          }
        }


        [HttpPost]
        [Route("v4/trade/products/search-products-for-order")]
        public async Task<CollectionModel> GetProductsByCustomer([FromBody] ProductOrderQuery query)
        {
            RequireBody(query);

            using (var usecases = ProductForOrderUseCases.UseCaseInteractor())
            {
                FixedList<IProductEntryDto> productDto = await usecases.GetProductsForOrder(query)
                                                        .ConfigureAwait(false);
                return new CollectionModel(Request, productDto);
            }
        }


        [HttpPost]
        [Route("v4/trade/catalogues/update-uid")]
        public async Task<SingleObjectModel> UpdateTableUID([FromBody] TableQuery query)
        {
            RequireBody(query);

            using (var usecases = CataloguesUseCases.UseCaseInteractor())
            {
                string msj = await usecases.UpdateGUID(query).ConfigureAwait(false);
                return new SingleObjectModel(Request, msj);
            }
        }


        #endregion Web Apis


    } // class TRDProductController

} // namespace Empiria.Trade.WebApi.Inventory
