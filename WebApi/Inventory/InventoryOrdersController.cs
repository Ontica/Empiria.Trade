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

using Empiria.Trade.Core;
using Empiria.Trade.Inventory.Adapters;
using Empiria.Trade.Inventory.UseCases;
using Empiria.WebApi;

namespace Empiria.Trade.WebApi.Inventory {

  /// <summary>Query web API used to retrieve Inventory orders.</summary>
  public class InventoryOrdersController : WebApiController {

    #region V2

    
    [HttpPost]
    [Route("v8/order-management/inventory-orders/search")]
    public SingleObjectModel SearchInventoryOrderList([FromBody] InventoryOrderQuery query) {

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        InventoryOrderDataDto inventoryOrderDto = usecases.SearchInventoryOrder(query);

        return new SingleObjectModel(this.Request, inventoryOrderDto);
      }
    }


    [HttpGet]
    [Route("v8/order-management/inventory-orders/{orderUID:guid}")]
    public SingleObjectModel GetInventoryOrder([FromUri] string orderUID) {

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        InventoryHolderDto inventoryOrder = usecases.GetInventoryOrder(orderUID);

        return new SingleObjectModel(this.Request, inventoryOrder);
      }
    }


    [HttpGet]
    [Route("v8/order-management/inventory-types")]
    public CollectionModel GetInventoryOrderTypes() {

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        FixedList<NamedEntityDto> orderTypes = usecases.GetOrderTypes();

        return new CollectionModel(this.Request, orderTypes);
      }
    }


    [HttpGet]
    [Route("v8/order-management/warehouses")]
    public CollectionModel GetWarehouses() {

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        FixedList<NamedEntityDto> warehouses = usecases.GetWarehouses();

        return new CollectionModel(this.Request, warehouses);
      }
    }


    [HttpGet]
    [Route("v8/order-management/inventory-orders/inventory-supervisor")]
    public CollectionModel GetResponsibles() {

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        FixedList<NamedEntityDto> parties = usecases.GetPartiesByRol("Inventory-manager");

        return new CollectionModel(this.Request, parties);
      }
    }


    [HttpGet]
    [Route("v8/order-management/inventory-orders/warehousemen")]
    public CollectionModel GetWarehouseman() {

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        FixedList<NamedEntityDto> parties = usecases.GetPartiesByRol("Warehouseman");

        return new CollectionModel(this.Request, parties);
      }
    }


    [HttpPost]
    [Route("v8/order-management/inventory-orders")]
    public SingleObjectModel CreateInventoryOrder([FromBody] Trade.Core.InventoryOrderFields fields) {

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        InventoryHolderDto inventoryOrder = usecases.CreateInventoryOrder(fields);

        return new SingleObjectModel(this.Request, inventoryOrder);
      }
    }


    [HttpPost]
    [Route("v8/order-management/inventory-orders/{orderUID}/items")]
    public SingleObjectModel CreateInventoryOrderItem([FromUri] string orderUID, [FromBody] Trade.Core.InventoryOrderItemFields fields) {

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        InventoryHolderDto inventoryOrder = usecases.CreateInventoryOrderItem(orderUID, fields);

        return new SingleObjectModel(this.Request, inventoryOrder);
      }
    }


    [HttpPost]
    [Route("v8/order-management/inventory-orders/{orderUID:guid}/close")]
    public SingleObjectModel CloseInventoryOrder([FromUri] string orderUID) {

      Assertion.RequireFail("Funcionalidad en proceso de desarrollo.");

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        InventoryHolderDto inventoryOrder = usecases.CloseInventoryOrder(orderUID);

        return new SingleObjectModel(this.Request, inventoryOrder);
      }
    }


    [HttpDelete]
    [Route("v8/order-management/inventory-orders/{orderUID:guid}")]
    public NoDataModel DeleteInventoryOrder([FromUri] string orderUID) {

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        usecases.DeleteInventoryOrder(orderUID);

        return new NoDataModel(this.Request);
      }
    }


    [HttpDelete]
    [Route("v8/order-management/inventory-orders/{orderUID:guid}/items/{orderItemUID:guid}")]
    public SingleObjectModel DeleteInventoryOrderItem([FromUri] string orderUID,
                                                [FromUri] string orderItemUID) {

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        InventoryHolderDto inventoryOrder = usecases.DeleteInventoryOrderItem(orderUID, orderItemUID);

        return new SingleObjectModel(this.Request, inventoryOrder);
      }
    }


    [HttpPut]
    [Route("v8/order-management/inventory-orders/{orderUID:guid}")]

    public SingleObjectModel UpdateInventoryOrder([FromUri] string orderUID,
                                                  [FromBody] Trade.Core.InventoryOrderFields fields) {

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        InventoryHolderDto inventoryOrder = usecases.UpdateInventoryOrder(orderUID, fields);

        return new SingleObjectModel(this.Request, inventoryOrder);
      }
    }


    [HttpPut]
    [Route("v8/order-management/inventory-orders/{orderUID:guid}/items/{orderItemUID:guid}")]

    public SingleObjectModel UpdateInventoryOrderItemQuantity([FromUri] string orderUID, [FromUri] string orderItemUID,
                                                              [FromBody] InventoryOrderItemFields fields) {

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        InventoryHolderDto inventoryOrder = usecases.UpdateInventoryOrderItemQuantity(orderUID, orderItemUID, fields);

        return new SingleObjectModel(this.Request, inventoryOrder);
      }
    }


    #endregion V2

  } // class InventoryOrdersController

} // namespace Empiria.Trade.WebApi.Inventory
