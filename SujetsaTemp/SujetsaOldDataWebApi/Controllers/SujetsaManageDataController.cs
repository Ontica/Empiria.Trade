using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ConnectionsToFirebirdSujetsa.Adapters;
using ConnectionsToFirebirdSujetsa.Services;

namespace SujetsaOldDataWebApi.Controllers
{
  public class SujetsaManageDataController : ApiController {

    [HttpPost]
    [Route("manage-data/count-data")]
    public string GetProductsCount() {

      var service = new Services();
      string list = service.GetDataCountFromDb();

      return list;
    }


    [HttpPost]
    [Route("manage-data/list-data")]
    public List<ProductosAdapter> GetProductsList() {

      var service = new Services();
      List<ProductosAdapter> list = service.GetDataFromDb();

      //var json = JsonConvert.SerializeObject(list);

      return list;

    }


    [HttpPost]
    [Route("manage-data/insert-to-sql")]
    public string InsertProductFromFbToSql() {

      var service = new Services();

      List<ProductosAdapter> productsToUpdate = service.GetDataFromDb();

      string message = service.InsertProductToSql(productsToUpdate);

      //var json = JsonConvert.SerializeObject(list);

      return message;

    }


    [HttpPost]
    [Route("manage-data/async-insert-to-sql")]
    public async Task<String> AsyncInsertProductFromFbToSql() {

      var service = new Services();

      List<ProductosAdapter> productsToUpdate = service.GetDataFromDb();

      string message = await service.InsertProductToSqlAsync(productsToUpdate).ConfigureAwait(false);

      //var json = JsonConvert.SerializeObject(list);

      return message;

    }


    [HttpPost]
    [Route("manage-data/get-list-sql")]
    public List<ProductosAdapter> GetListFromSql() {

      var service = new Services();
      return service.GetListFromSql();

    }

  }
}
