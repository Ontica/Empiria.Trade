/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Web Api                                 *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Controller                              *
*  Type     : ProductTests                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Query web API used to retrieve temporary data products.                                        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using System.Web.Http;
using Empiria.Trade.Products.Adapters;
using Empiria.Trade.Products.UseCases;
using Empiria.WebApi;

using TradeDataSchemaManager.Adapters;

namespace Empiria.Trade.WebApi.SujetsaTemp {

  /// <summary>Query web API used to retrieve temporary data products.</summary>
  [AllowAnonymous]
  public class ProductsTempController : WebApiController {


    [HttpPost]
    [Route("manage-data/count-data")]
    public SingleObjectModel GetProductsCount() {

      var service = new TradeDataSchemaManager.Services.Services();
      string list = service.GetDataCountFromDb();

      return new SingleObjectModel(this.Request, list);
    }


    [HttpPost]
    [Route("manage-data/list-data")]
    public SingleObjectModel GetProductsList() {

      var service = new TradeDataSchemaManager.Services.Services();
      List<ProductosAdapter> list = service.GetDataFromDb();

      return new SingleObjectModel(this.Request, list);

    }


    [HttpPost]
    [Route("manage-data/insert-to-sql")]
    public SingleObjectModel InsertProductFromFbToSql() {

      var service = new TradeDataSchemaManager.Services.Services();

      List<ProductosAdapter> productsToUpdate = service.GetDataFromDb();

      string message = service.InsertProductToSql(productsToUpdate);

      return new SingleObjectModel(this.Request, message);

    }


    [HttpPost]
    [Route("manage-data/async-insert-to-sql")]
    public async Task<SingleObjectModel> AsyncInsertProductFromFbToSql() {

      var service = new TradeDataSchemaManager.Services.Services();

      List<ProductosAdapter> productsToUpdate = service.GetDataFromDb();

      string message = await service.InsertProductToSqlAsync(productsToUpdate).ConfigureAwait(false);

      return new SingleObjectModel(this.Request, message);

    }


    [HttpPost]
    [Route("manage-data/get-list-sql")]
    public SingleObjectModel GetListFromSql() {

      var service = new TradeDataSchemaManager.Services.Services();
      var list = service.GetListFromSql();

      return new SingleObjectModel(this.Request, list);

    }


  } // class ProductsTempController

} // namespace Empiria.Trade.WebApi.SujetsaTemp
