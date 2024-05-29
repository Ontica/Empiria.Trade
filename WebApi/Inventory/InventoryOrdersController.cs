/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Web Api                                 *
*  Assembly : Empiria.Trade.WebApi.Inventory.dll         Pattern   : Controller                              *
*  Type     : InventoryOrdersController                  License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Query web API used to retrieve Inventory orders.                                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Web.Http;
using Empiria.Trade.Inventory.Adapters;
using Empiria.Trade.Inventory.UseCases;
using Empiria.Trade.Sales.ShippingAndHandling.Adapters;
using Empiria.Trade.Sales.ShippingAndHandling.UseCases;
using Empiria.WebApi;

namespace Empiria.Trade.WebApi.Inventory {

  /// <summary>Query web API used to retrieve Inventory orders.</summary>
  public class InventoryOrdersController : WebApiController {


    [HttpPost]
    [Route("v4/trade/inventory/orders/{inventoryOrderUID:guid}/close")]
    public SingleObjectModel CloseInventoryOrderStatus([FromUri] string inventoryOrderUID) {

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        InventoryOrderDto inventoryOrder = usecases.CloseInventoryOrderStatus(inventoryOrderUID);

        return new SingleObjectModel(this.Request, inventoryOrder);
      }
    }


    [HttpPost]
    [Route("v4/trade/inventory/orders/")]
    public SingleObjectModel CreateInventoryOrder([FromBody] InventoryOrderFields fields) {

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        InventoryOrderDto inventoryOrder = usecases.CreateInventoryOrder(fields);

        return new SingleObjectModel(this.Request, inventoryOrder);
      }
    }


    [HttpPost]
    [Route("v4/trade/inventory/orders/{inventoryOrderUID:guid}/item")]
    public SingleObjectModel CreateInventoryOrderItem([FromUri] string inventoryOrderUID,
      [FromBody] InventoryOrderItemFields fields) {

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        InventoryOrderDto inventoryOrder = usecases.CreateInventoryOrderItem(inventoryOrderUID, fields);

        return new SingleObjectModel(this.Request, inventoryOrder);
      }
    }


    [HttpDelete]
    [Route("v4/trade/inventory/orders/{inventoryOrderUID:guid}")]
    public NoDataModel DeleteInventoryOrder([FromUri] string inventoryOrderUID) {

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        usecases.DeleteInventoryCountOrderByUID(inventoryOrderUID);

        return new NoDataModel(this.Request);
      }
    }


    [HttpDelete]
    [Route("v4/trade/inventory/orders/{inventoryOrderUID:guid}/item/{inventoryOrderItemUID:guid}")]
    public SingleObjectModel DeleteInventoryItemByUID([FromUri] string inventoryOrderUID,
                                                      [FromUri] string inventoryOrderItemUID) {

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        InventoryOrderDto inventoryOrder = 
          usecases.DeleteInventoryItemByUID(inventoryOrderUID, inventoryOrderItemUID);

        return new SingleObjectModel(this.Request, inventoryOrder);
      }
    }


    [HttpPost]
    [Route("v4/trade/inventory/orders/search")]
    public SingleObjectModel GetInventoryOrderList([FromBody] InventoryOrderQuery query) {

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        InventoryOrderDataDto inventoryOrderDto = usecases.GetInventoryCountOrderList(query);

        return new SingleObjectModel(this.Request, inventoryOrderDto);
      }
    }


    [HttpGet]
    [Route("v4/trade/inventory/orders/{inventoryOrderUID:guid}")]
    public SingleObjectModel GetInventoryOrder([FromUri] string inventoryOrderUID) {

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        InventoryOrderDto inventoryOrder = usecases.GetInventoryOrderByUID(inventoryOrderUID);

        return new SingleObjectModel(this.Request, inventoryOrder);
      }
    }


    [HttpPut]
    [Route("v4/trade/inventory/orders/{inventoryOrderUID:guid}")]
    public SingleObjectModel UpdateInventoryOrder([FromUri] string inventoryOrderUID,
                                                 [FromBody] InventoryOrderFields fields) {

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        InventoryOrderDto shippingOrder = usecases.UpdateInventoryCountOrder(inventoryOrderUID, fields);

        return new SingleObjectModel(this.Request, shippingOrder);
      }
    }


  } // class InventoryOrdersController

} // namespace Empiria.Trade.WebApi.Inventory
