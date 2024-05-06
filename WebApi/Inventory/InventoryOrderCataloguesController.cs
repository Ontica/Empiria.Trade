/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Web Api                                 *
*  Assembly : Empiria.Trade.WebApi.Inventory.dll         Pattern   : Controller                              *
*  Type     : InventoryOrderCataloguesController         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Query web API used to retrieve Inventory order catalogues.                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Web.Http;
using Empiria.Trade.Core.Catalogues;
using Empiria.Trade.Inventory.UseCases;
using Empiria.WebApi;

namespace Empiria.Trade.WebApi.Inventory {

  /// <summary>Query web API used to retrieve Inventory order catalogues.</summary>
  public class InventoryOrderCataloguesController : WebApiController {


    [HttpGet]
    [Route("v4/trade/inventory/orders/inventory-types")]
    public CollectionModel GetInventoryTypes() {

      using (var usescase = InventoryOrderCataloguesUseCases.UseCaseInteractor()) {
        FixedList<NamedEntityDto> inventoryTypes = usescase.GetInventoryOrderTypes();

        return new CollectionModel(base.Request, inventoryTypes);
      }
    }


  } // class InventoryOrderCataloguesController

} // namespace Empiria.Trade.WebApi.Inventory
