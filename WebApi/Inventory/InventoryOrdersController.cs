/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Web Api                                 *
*  Assembly : Empiria.Trade.WebApi.Inventory.dll         Pattern   : Controller                              *
*  Type     : InventoryOrdersController                  License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Query web API used to retrieve Inventory orders.                                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System.Web.Http;

using Empiria.Trade.Inventory.Adapters;
using Empiria.Trade.Inventory.UseCases;
using Empiria.Trade.Core;
using Empiria.WebApi;

namespace Empiria.Trade.WebApi.Inventory {

  /// <summary>Query web API used to retrieve Inventory orders.</summary>
  public class InventoryOrdersController : WebApiController {

    #region V2

    
    [HttpPost]
    [Route("v8/order-management/inventory-orders/search_")]
    public SingleObjectModel SearchInventoryOrderList([FromBody] InventoryOrderQuery query) {

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        InventoryOrderDataDto inventoryOrderDto = usecases.SearchInventoryOrder(query);

        return new SingleObjectModel(this.Request, inventoryOrderDto);
      }
    }


    [HttpGet]
    [Route("v8/order-management/inventory-orders/{orderUID:guid}_")]
    public SingleObjectModel GetInventoryOrder([FromUri] string orderUID) {

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        InventoryHolderDto inventoryOrder = usecases.GetInventoryOrder(orderUID);

        return new SingleObjectModel(this.Request, inventoryOrder);
      }
    }


    [HttpGet]
    [Route("v8/order-management/inventory-types_")]
    public CollectionModel GetInventoryOrderTypes() {

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        FixedList<NamedEntityDto> orderTypes = usecases.GetOrderTypes();

        return new CollectionModel(this.Request, orderTypes);
      }
    }


    [HttpGet]
    [Route("v8/order-management/warehouses_")]
    public CollectionModel GetWarehouses() {

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        FixedList<NamedEntityDto> warehouses = usecases.GetWarehouses();

        return new CollectionModel(this.Request, warehouses);
      }
    }


    [HttpGet]
    [Route("v8/order-management/inventory-orders/inventory-supervisor_")]
    public CollectionModel GetResponsibles() {

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        FixedList<NamedEntityDto> parties = usecases.GetPartiesByRol("Inventory-manager");

        return new CollectionModel(this.Request, parties);
      }
    }


    [HttpGet]
    [Route("v8/order-management/inventory-orders/warehousemen_")]
    public CollectionModel GetWarehouseman() {

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        FixedList<NamedEntityDto> parties = usecases.GetPartiesByRol("Warehouseman");

        return new CollectionModel(this.Request, parties);
      }
    }


    [HttpPost]
    [Route("v8/order-management/inventory-orders_")]
    public SingleObjectModel CreateInventoryOrder([FromBody] Trade.Core.InventoryOrderFields fields) {

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        InventoryHolderDto inventoryOrder = usecases.CreateInventoryOrder(fields.WarehouseUID, fields);

        return new SingleObjectModel(this.Request, inventoryOrder);
      }
    }


    [HttpPost]
    [Route("v8/order-management/inventory-orders/{orderUID}/items_")]
    public SingleObjectModel CreateInventoryOrderItem([FromUri] string orderUID, [FromBody] Trade.Core.InventoryOrderItemFields fields) {

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        InventoryHolderDto inventoryOrder = usecases.CreateInventoryOrderItem(orderUID, fields);

        return new SingleObjectModel(this.Request, inventoryOrder);
      }
    }


    [HttpPost]
    [Route("v8/order-management/inventory-orders/{orderUID:guid}/close_")]
    public SingleObjectModel CloseInventoryOrder([FromUri] string orderUID) {

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        InventoryHolderDto inventoryOrder = usecases.CloseInventoryOrder(orderUID);

        return new SingleObjectModel(this.Request, inventoryOrder);
      }
    }


    [HttpDelete]
    [Route("v8/order-management/inventory-orders/{orderUID:guid}_")]
    public NoDataModel DeleteInventoryOrder([FromUri] string orderUID) {

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        usecases.DeleteInventoryOrder(orderUID);

        return new NoDataModel(this.Request);
      }
    }


    [HttpDelete]
    [Route("v8/order-management/inventory-orders/{orderUID:guid}/items/{orderItemUID:guid}_")]
    public SingleObjectModel DeleteInventoryOrderItem([FromUri] string orderUID,
                                                [FromUri] string orderItemUID) {

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        InventoryHolderDto inventoryOrder = usecases.DeleteInventoryOrderItem(orderUID, orderItemUID);

        return new SingleObjectModel(this.Request, inventoryOrder);
      }
    }


    [HttpPut]
    [Route("v8/order-management/inventory-orders/{orderUID:guid}_")]

    public SingleObjectModel UpdateInventoryOrder([FromUri] string orderUID,
                                                  [FromBody] Trade.Core.InventoryOrderFields fields) {

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        InventoryHolderDto inventoryOrder = usecases.UpdateInventoryOrder(orderUID, fields);

        return new SingleObjectModel(this.Request, inventoryOrder);
      }
    }


    [HttpPut]
    [Route("v8/order-management/inventory-orders/{orderUID:guid}/items/{orderItemUID:guid}_")]

    public SingleObjectModel UpdateInventoryOrderItemQuantity([FromUri] string orderUID, [FromUri] string orderItemUID,
                                                              [FromBody] Trade.Core.InventoryOrderItemFields fields) {

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        InventoryHolderDto inventoryOrder = usecases.UpdateInventoryOrderItemQuantity(orderUID, orderItemUID, fields);

        return new SingleObjectModel(this.Request, inventoryOrder);
      }
    }


    #endregion V2


    [HttpPost]
    [Route("v4/trade/inventory/orders/{inventoryOrderUID:guid}/close")]
    public SingleObjectModel CloseInventoryOrderV1([FromUri] string inventoryOrderUID) {

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        InventoryOrderDto inventoryOrder = usecases.CloseInventoryOrderV1(inventoryOrderUID);

        return new SingleObjectModel(this.Request, inventoryOrder);
      }
    }


    [HttpPost]
    [Route("v4/trade/inventory/orders/")]
    public SingleObjectModel CreateInventoryOrderV1(
      [FromBody] Trade.Inventory.Adapters.InventoryOrderFields fields) {

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        InventoryOrderDto inventoryOrder = usecases.CreateInventoryOrderV1(fields);

        return new SingleObjectModel(this.Request, inventoryOrder);
      }
    }


    [HttpPost]
    [Route("v4/trade/inventory/orders/{inventoryOrderUID:guid}/item")]
    public SingleObjectModel CreateInventoryOrderItemV1([FromUri] string inventoryOrderUID,
      [FromBody] Trade.Inventory.Adapters.InventoryOrderItemFields fields) {

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        InventoryOrderDto inventoryOrder = usecases.CreateInventoryOrderItemV1(inventoryOrderUID, fields);

        return new SingleObjectModel(this.Request, inventoryOrder);
      }
    }


    [HttpDelete]
    [Route("v4/trade/inventory/orders/{inventoryOrderUID:guid}")]
    public NoDataModel DeleteInventoryOrderV1([FromUri] string inventoryOrderUID) {

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


    [HttpGet]
    [Route("v4/trade/inventory/orders/{inventoryOrderUID:guid}")]
    public SingleObjectModel GetInventoryOrder_([FromUri] string inventoryOrderUID) {

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        InventoryOrderDto inventoryOrder = usecases.GetInventoryOrderByUID(inventoryOrderUID);

        return new SingleObjectModel(this.Request, inventoryOrder);
      }
    }


    [HttpPost]
    [Route("v4/trade/inventory/orders/search")]
    public SingleObjectModel SearchInventoryOrders([FromBody] InventoryOrderQuery query) {

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        InventoryOrderDataDto inventoryOrderDto = usecases.SearchInventoryOrders(query);

        return new SingleObjectModel(this.Request, inventoryOrderDto);
      }
    }


    [HttpPut]
    [Route("v4/trade/inventory/orders/{inventoryOrderUID:guid}")]
    public SingleObjectModel UpdateInventoryOrderV1([FromUri] string inventoryOrderUID,
                                          [FromBody] Trade.Inventory.Adapters.InventoryOrderFields fields) {

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        InventoryOrderDto shippingOrder = usecases.UpdateInventoryOrderV1(inventoryOrderUID, fields);

        return new SingleObjectModel(this.Request, shippingOrder);
      }
    }


  } // class InventoryOrdersController

} // namespace Empiria.Trade.WebApi.Inventory
