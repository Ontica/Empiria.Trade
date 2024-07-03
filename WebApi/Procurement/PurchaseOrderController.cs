/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Procurement Management                     Component : Web Api                                 *
*  Assembly : Empiria.Trade.WebApi.Procurement.dll       Pattern   : Controller                              *
*  Type     : PurchaseOrderController                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Query web API used to retrieve purchase orders.                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.WebApi;
using System.Web.Http;
using Empiria.Trade.Procurement.UseCases;
using Empiria.Trade.Procurement.Adapters;
using Empiria.Trade.Inventory.Adapters;
using Empiria.Trade.Inventory.UseCases;

namespace Empiria.Trade.WebApi.Procurement {


  /// <summary>Query web API used to retrieve purchase orders.</summary>
  public class PurchaseOrderController : WebApiController {


    [HttpPost]
    [Route("v4/trade/procurement/purchase-orders")]
    //[Route("v4/trade/procurement/purchase-orders/orders/")]
    public SingleObjectModel CreatePurchaseOrder([FromBody] PurchaseOrderFields fields) {

      using (var usecases = PurchaseOrderUseCases.UseCaseInteractor()) {

        PurchaseOrderDto inventoryOrder = usecases.CreatePurchaseOrder(fields);

        return new SingleObjectModel(this.Request, inventoryOrder);
      }
    }


    [HttpPost]
    [Route("v4/trade/procurement/purchase-orders/search")]
    public SingleObjectModel GetPurchaseOrderDescriptor([FromBody] PurchaseOrderQuery query) {

      using (var usecases = PurchaseOrderUseCases.UseCaseInteractor()) {

        PurchaseOrderDataDto purchaseOrderDescriptor = usecases.GetPurchaseOrderDescriptor(query);

        return new SingleObjectModel(this.Request, purchaseOrderDescriptor);
      }
    }


  } // class PurchaseOrderController

} // namespace Empiria.Trade.WebApi.Procurement
