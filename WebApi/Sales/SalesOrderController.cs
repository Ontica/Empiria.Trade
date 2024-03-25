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


using Empiria.DataTypes;
using System.Security.Cryptography;

namespace Empiria.Trade.Sales.WebApi {

  /// <summary>Web API used to handle sales orders.</summary>
  public class SalesOrderController : WebApiController {

    [HttpPost]
    [Route("v4/trade/sales/orders/process")]
    public SingleObjectModel ProcessSalesOrder([FromBody] SalesOrderFields fields) {

      base.RequireBody(fields);

      using (var usecases = SalesOrderUseCases.UseCaseInteractor()) {

        ISalesOrderDto orderDto = usecases.ProcessSalesOrder(fields);

        return new SingleObjectModel(this.Request, orderDto);
      }
            
    }



    [HttpPost]
    [Route("v4/trade/sales/orders")]
    public SingleObjectModel CreateSalesOrder([FromBody] SalesOrderFields fields) {

      base.RequireBody(fields);

      using (var usecases = SalesOrderUseCases.UseCaseInteractor()) {

        ISalesOrderDto orderDto = usecases.CreateSalesOrder(fields);

        return new SingleObjectModel(this.Request, orderDto);
      }
    }

    
    [HttpDelete]
    [Route("v4/trade/sales/orders/{orderUID:guid}/cancel")]
    public SingleObjectModel CancelSalesOrder([FromUri] string orderUID) {

      base.RequireResource(orderUID, "orderUID");

      using (var usecases = SalesOrderUseCases.UseCaseInteractor()) {

        ISalesOrderDto orderDto = usecases.CancelSalesOrder(orderUID);

        return new SingleObjectModel(this.Request, orderDto);
        }
        
    }

    [HttpPost]
    [Route("v4/trade/sales/orders/{orderUID:guid}/apply")]
    public SingleObjectModel ApplySalesOrder([FromUri] string orderUID) {

      base.RequireResource(orderUID, "orderUID");

      using (var usecases = SalesOrderUseCases.UseCaseInteractor()) {

        ISalesOrderDto orderDto = usecases.ApplySalesOrder(orderUID);

        return new SingleObjectModel(this.Request, orderDto);
      }

    }

    [HttpPut]
    [Route("v4/trade/sales/orders/{orderUID:guid}")]
    public SingleObjectModel UpdateSalesOrder([FromUri] string orderUID, [FromBody] SalesOrderFields fields) {

      base.RequireBody(fields);

      using (var usecases = SalesOrderUseCases.UseCaseInteractor()) {

        ISalesOrderDto orderDto = usecases.UpdateSalesOrder(fields);

        return new SingleObjectModel(this.Request, orderDto);
      }
      
    }

    [HttpGet]
    [Route("v4/trade/sales/orders/{orderUID:guid}/{queryType}")]
    public SingleObjectModel GetSalesOrder([FromUri] string orderUID, [FromUri] QueryType queryType) {

       base.RequireResource(orderUID, "orderUID");

      using (var usecases = SalesOrderUseCases.UseCaseInteractor()) {

        ISalesOrderDto orderDto = usecases.GetSalesOrder(orderUID, queryType);

        return new SingleObjectModel(this.Request, orderDto);
      }

    }



    [HttpPost]
    [Route("v4/trade/sales/orders/search")]
    public SingleObjectModel GetOrders([FromBody] SearchOrderFields fields) {

      base.RequireBody(fields);
      using (var usecases = SalesOrderUseCases.UseCaseInteractor()) {

        SearchSalesOrderDto salesOrders = usecases.GetOrders(fields);
        return new SingleObjectModel(base.Request, salesOrders);


      } // using 

    }

    [HttpGet]
    [Route("v4/trade/sales/orders/search-for-shipping")]
    public SingleObjectModel GetOrdersForShipping([FromUri] string keywords) {
      base.RequireResource(keywords, "keywords");

      using (var usecases = SalesOrderUseCases.UseCaseInteractor()) {

        SearchOrderFields fields = new SearchOrderFields();

        fields.QueryType = QueryType.Sales;
        fields.Status = Orders.OrderStatus.Shipping;
        fields.ShippingMethod = Orders.ShippingMethods.Paqueteria;
        fields.Keywords = keywords;

        var salesOrders = usecases.GetOrdersForShipping(fields);

        return new SingleObjectModel(base.Request, salesOrders);
      } // using 

    }

    [HttpGet]
    [Route("v4/trade/sales/orders/status")]
    public CollectionModel GetOrderStatus() {

      using (var usecases = SalesOrderUseCases.UseCaseInteractor()) {
        FixedList<NamedEntityDto> orderStatusList = usecases.GetStatusList();

        return new CollectionModel(base.Request, orderStatusList);
      }

    }

    [HttpGet]
    [Route("v4/trade/sales/orders/status/authorizations")]
    public CollectionModel GetOrderAuthorizationsStatus() {

      using (var usecases = SalesOrderUseCases.UseCaseInteractor()) {
        FixedList<NamedEntityDto> orderAutorizationStatusList = usecases.GetAuthorizationStatusList();

        return new CollectionModel(base.Request, orderAutorizationStatusList);
      }
    }

    [HttpGet]
    [Route("v4/trade/sales/orders/status/packing")]
    public CollectionModel GetOrderPackingStatus() {

      using (var usecases = SalesOrderUseCases.UseCaseInteractor()) {
        FixedList<NamedEntityDto> orderAutorizationStatusList = usecases.GetPackingStatusList();

        return new CollectionModel(base.Request, orderAutorizationStatusList);
      }
    }

    [HttpPost]
    [Route("v4/trade/sales/orders/{orderUID:guid}/authorize")]
    public SingleObjectModel AuthorizeSalesOrder([FromUri] string orderUID) {

      base.RequireResource(orderUID, "orderUID");

      using (var usecases = SalesOrderUseCases.UseCaseInteractor()) {
        ISalesOrderDto orderDto = usecases.AuthorizeSalesOrder(orderUID);

        return new SingleObjectModel(this.Request, orderDto);
      }

    }

    [HttpPost]
    [Route("v4/trade/sales/orders/{orderUID:guid}/supply")]
    public SingleObjectModel SupplySalesOrder([FromUri] string orderUID) {

      base.RequireResource(orderUID, "orderUID");

      using (var usecases = SalesOrderUseCases.UseCaseInteractor()) {
        ISalesOrderDto orderDto = usecases.SupplySalesOrder(orderUID);

        return new SingleObjectModel(this.Request, orderDto);
      }

    }

    [HttpPost]
    [Route("v4/trade/sales/orders/{orderUID:guid}/delivery")]
    public SingleObjectModel DeliverySalesOrder([FromUri] string orderUID) {
      
      base.RequireResource(orderUID, "orderUID");

     using (var usecases = SalesOrderUseCases.UseCaseInteractor()) {
        ISalesOrderDto orderDto = usecases.DeliverySalesOrderWithMap(orderUID);

        return new SingleObjectModel(this.Request, orderDto);
     }

    }


    } // class SalesOrderController

} // namespace Empiria.Trade.Products.WebApi
