﻿/* Empiria Trade *********************************************************************************************
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
using Empiria.DataTypes;

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

    
    [HttpDelete]
    [Route("v4/trade/sales/orders/{orderUID:guid}/cancel")]
    public SingleObjectModel CancelSalesOrder([FromUri] string orderUID, [FromBody] OrderField fields) {

      RequireBody(fields);

      Assertion.Require(orderUID == fields.OrderUID, "Unrecognized Order UID.");

      using (var usecases = SalesOrderUseCases.UseCaseInteractor()) {

        SalesOrderDto orderDto = usecases.CancelSalesOrder(orderUID);

        return new SingleObjectModel(this.Request, orderDto);
        }
        
    }

    [HttpPost]
    [Route("v4/trade/sales/orders/{orderUID:guid}/apply")]
    public SingleObjectModel ApplySalesOrder([FromUri] string orderUID, [FromBody] OrderField fields) {

      RequireBody(fields);

      Assertion.Require(orderUID == fields.OrderUID, "Unrecognized Order UID.");
           
      using (var usecases = SalesOrderUseCases.UseCaseInteractor()) {

        SalesOrderDto orderDto = usecases.ApplySalesOrder(orderUID);

        return new SingleObjectModel(this.Request, orderDto);
      }

    }

    [HttpPut]
    [Route("v4/trade/sales/orders/{orderUID:guid}")]
    public SingleObjectModel UpdateSalesOrder([FromUri] string orderUID, [FromBody] SalesOrderFields fields) {

      base.RequireBody(fields);

      using (var usecases = SalesOrderUseCases.UseCaseInteractor()) {

        SalesOrderDto orderDto = usecases.UpdateSalesOrder(fields);

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
