/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Sales Management                           Component : Web Api                                 *
*  Assembly : Empiria.Trade.WebApi.dll                   Pattern   : Controller                              *
*  Type     : SalesOrderController                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Web API used to handle sales orders.                                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using System.Web.Http;

using Empiria.WebApi;

using Empiria.Trade.Sales.Adapters;
using Empiria.Trade.Sales.UseCases;

namespace Empiria.Trade.Sales.WebApi {

  /// <summary>Web API used to handle sales orders.</summary>
  public class SalesOrderController : WebApiController {

    [HttpPost]
    [Route("v4/trade/sales/process-sales-order")]
    public SingleObjectModel ProcessSalesOrder([FromBody] SalesOrderFields fields) {

      base.RequireBody(fields);

      using (var usecases = SalesOrderUseCases.UseCaseInteractor()) {

        SalesOrderDto orderDto = usecases.ProcessSalesOrder(fields);

        return new SingleObjectModel(this.Request, orderDto);
      }
    }

    [HttpPost]
    [Route("v4/trade/sales/create-sales-order")]
    public SingleObjectModel CreateSalesOrder([FromBody] SalesOrderFields fields) {

      base.RequireBody(fields);

      using (var usecases = SalesOrderUseCases.UseCaseInteractor()) {

        SalesOrderDto orderDto = usecases.CreateSalesOrder(fields);

        return new SingleObjectModel(this.Request, orderDto);
      }
    }

    [HttpPost]
    [Route("v4/trade/sales/search-sales-order")]
    public CollectionModel GetOrders([FromBody] SearchOrderFields fields) {

      base.RequireBody(fields);
      using (var usecases = SalesOrderUseCases.UseCaseInteractor()) {
        FixedList<SalesOrderDto> salesOrders = usecases.GetOrders(fields);

        return new CollectionModel(base.Request, salesOrders);
      }
        
    }

  } // class SalesOrderController

} // namespace Empiria.Trade.Products.WebApi
