using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Http;
using Empiria.WebApi;

using Empiria.Trade.Products.Adapters;
using Empiria.Trade.Products.UseCases;

namespace Empiria.Sicofin.Banobras.WebApi.Products {
  class ProductsController : WebApiController {


    [HttpPost]
    [Route("trade/products/products-list")]
    public async Task<SingleObjectModel> GetTrialBalance([FromBody] string clauses) {
      
      base.RequireBody(clauses);

      using (var usecases = ProductsUseCases.UseCaseInteractor()) {

        ProductDto productDto = await usecases.BuildProducts(clauses).ConfigureAwait(false);

        return new SingleObjectModel(this.Request, productDto);
      }
    }


  }
}
