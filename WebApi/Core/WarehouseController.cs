/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Core Management                            Component : Web Api                                 *
*  Assembly : Empiria.Trade.WebApi.Core.dll              Pattern   : Controller                              *
*  Type     : WarehouseController                        License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Query web API used to retrieve warehouses.                                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Core.Catalogues;
using Empiria.WebApi;
using System.Web.Http;
using Empiria.Trade.Core.Catalogues.Adapters;

namespace Empiria.Trade.WebApi.Core {


  /// <summary>Query web API used to retrieve warehouses.</summary>
  public class WarehouseController : WebApiController {


    [HttpGet]
    [Route("v4/trade/warehouse-bin/for-inventory")]
    public CollectionModel GetWarehouseBinsForInventory() {

      using (var usescase = CataloguesUseCases.UseCaseInteractor()) {
        FixedList<WarehouseBinForInventoryDto> inventoryTypes = usescase.GetWarehouseBinsForInventory();

        return new CollectionModel(base.Request, inventoryTypes);
      }
    }


    [HttpGet]
    [Route("v4/trade/warehouse-bin/locations/search/{keywords}")]
    public CollectionModel GetWarehouseBinLocations([FromUri] string keywords) {

      using (var usescase = CataloguesUseCases.UseCaseInteractor()) {
        FixedList<NamedEntityDto> inventoryTypes = usescase.GetWarehouseBinLocations(keywords);

        return new CollectionModel(base.Request, inventoryTypes);
      }
    }


  } // class WarehouseController

} // namespace Empiria.Trade.WebApi.Core
