﻿/* Empiria Trade *********************************************************************************************
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
    //[Route("v4/trade/procurement/purchase-orders/order/")]
    public SingleObjectModel CreatePurchaseOrder([FromBody] PurchaseOrderFields fields) {

      using (var usecases = PurchaseOrderUseCases.UseCaseInteractor()) {

        PurchaseOrderDto inventoryOrder = usecases.CreatePurchaseOrder(fields);

        return new SingleObjectModel(this.Request, inventoryOrder);
      }
    }


    [HttpDelete]
    [Route("v4/trade/procurement/purchase-orders/{purchaseOrderUID:guid}")]
    public NoDataModel DeletePurchaseOrder([FromUri] string purchaseOrderUID) {

      using (var usecases = PurchaseOrderUseCases.UseCaseInteractor()) {

        usecases.DeletePurchaseOrder(purchaseOrderUID);

        return new NoDataModel(this.Request);
      }
    }


    [HttpGet]
    [Route("v4/trade/procurement/purchase-orders/{purchaseOrderUID:guid}")]
    public SingleObjectModel GetPurchaseOrder([FromUri] string purchaseOrderUID) {

      using (var usecases = PurchaseOrderUseCases.UseCaseInteractor()) {

        PurchaseOrderDto purchaseOrder = usecases.GetPurchaseOrder(purchaseOrderUID);

        return new SingleObjectModel(this.Request, purchaseOrder);
      }
    }


    [HttpPost]
    [Route("v4/trade/procurement/purchase-orders/search")]
    public SingleObjectModel GetPurchaseOrderDescriptor([FromBody] PurchaseOrderQuery query) {

      using (var usecases = PurchaseOrderUseCases.UseCaseInteractor()) {

        PurchaseOrdersDataDto purchaseOrderDescriptor = usecases.GetPurchaseOrderDescriptor(query);

        return new SingleObjectModel(this.Request, purchaseOrderDescriptor);
      }
    }


    [HttpPut]
    [Route("v4/trade/procurement/purchase-orders/{purchaseOrderUID:guid}")]
    public SingleObjectModel UpdatePurchaseOrder([FromUri] string purchaseOrderUID,
                                                 [FromBody] PurchaseOrderFields fields) {

      using (var usecases = PurchaseOrderUseCases.UseCaseInteractor()) {

        PurchaseOrderDto shippingOrder = usecases.UpdatePurchaseOrder(purchaseOrderUID, fields);

        return new SingleObjectModel(this.Request, shippingOrder);
      }
    }


  } // class PurchaseOrderController

} // namespace Empiria.Trade.WebApi.Procurement
