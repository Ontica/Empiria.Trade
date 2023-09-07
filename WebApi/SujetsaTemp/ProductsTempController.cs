using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Http;
using Empiria.Trade.Products.Adapters;
using Empiria.Trade.Products.UseCases;
using Empiria.WebApi;

using TradeDataSchemaManager.Adapters;

namespace Empiria.Trade.WebApi.SujetsaTemp {
  public class ProductsTempController : WebApiController {



    [HttpPost]
    [Route("trade/products/products-list")]
    public async Task<SingleObjectModel> GetProductsList([FromBody] string clauses) {

      base.RequireBody(clauses);

      using (var usecases = ProductsUseCases.UseCaseInteractor()) {

        ProductDto productDto = await usecases.BuildProducts(clauses).ConfigureAwait(false);

        return new SingleObjectModel(this.Request, productDto);
      }
    }


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


  }
}
