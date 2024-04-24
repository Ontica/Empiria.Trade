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
using Empiria.Trade.Core.UsesCases;
using Empiria.Trade.Inventory.Adapters;
using Empiria.Trade.Inventory.UseCases;
using Empiria.Trade.Sales.ShippingAndHandling.Adapters;
using Empiria.Trade.Sales.ShippingAndHandling.UseCases;
using Empiria.WebApi;

namespace Empiria.Trade.WebApi.Inventory {

  /// <summary>Query web API used to retrieve Inventory orders.</summary>
  public class InventoryOrdersController : WebApiController {

    //TODO MOVER ESTOS 3 WEB SERVICES A OTRO CONTROLLER DE CATALOGOS (CREAR CONTROLLER)
    //[HttpGet]
    //[Route("v4/trade/inventory/orders/inventory-types")]
    //public CollectionModel GetInventoryTypes() {

    //  using (var usescase = InventoryOrderCataloguesUseCases.UseCaseInteractor()) {
    //    FixedList<NamedEntityDto> inventoryTypes = usescase.GetInventoryOrderTypes();

    //    return new CollectionModel(base.Request, inventoryTypes);
    //  }
    //}


    //[HttpGet]
    //[Route("v4/trade/contacts/inventory-supervisors")]
    //public CollectionModel GetInventorySupervisors() {

    //  using (var usescase = PartyUseCases.UseCaseInteractor()) {
    //    FixedList<NamedEntityDto> responsibles = usescase.GetSalesAgents();

    //    return new CollectionModel(base.Request, responsibles);
    //  }
    //}


    //[HttpGet]
    //[Route("v4/trade/contacts/warehousemen")]
    //public CollectionModel GetWarehousemen() {

    //  using (var usescase = PartyUseCases.UseCaseInteractor()) {
    //    FixedList<NamedEntityDto> assignedToList = usescase.GetSalesAgents();

    //    return new CollectionModel(base.Request, assignedToList);
    //  }
    //}


    [HttpPost]
    [Route("v4/trade/inventory/orders/")]
    public SingleObjectModel CreateInventoryOrder([FromBody] InventoryOrderFields fields) {

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        InventoryOrderDto inventoryOrder = usecases.CreateInventoryCountOrder(fields);

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
    public CollectionModel GetInventoryOrderList([FromBody] InventoryOrderQuery query) {

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        FixedList<InventoryOrderDescriptorDto> inventoryOrderList = 
          usecases.GetInventoryCountOrderList(query);

        return new CollectionModel(this.Request, inventoryOrderList);
      }
    }


    [HttpGet]
    [Route("v4/trade/inventory/orders/{inventoryOrderUID:guid}")]
    public SingleObjectModel GetInventoryOrder([FromUri] string inventoryOrderUID) {

      using (var usecases = InventoryOrderUseCases.UseCaseInteractor()) {

        InventoryOrderDto inventoryOrder = usecases.GetInventoryCountOrderByUID(inventoryOrderUID);

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
