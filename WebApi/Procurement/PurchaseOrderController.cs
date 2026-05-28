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
using Empiria.Trade.Core;

namespace Empiria.Trade.WebApi.Procurement {


  /// <summary>Query web API used to retrieve purchase orders.</summary>
  public class PurchaseOrderController : WebApiController {

    [HttpPost]
    [Route("v4/trade/procurement/purchase-orders/{purchaseOrderUID:guid}/close")]
    public SingleObjectModel ClosePurchaseOrder([FromUri] string purchaseOrderUID) {

      Assertion.RequireFail("Funcionalidad en proceso de desarrollo.");

      using (var usecases = PurchaseOrderUseCases.UseCaseInteractor()) {

        PurchaseOrderDto purchaseOrder = usecases.ClosePurchaseOrder(purchaseOrderUID);

        return new SingleObjectModel(this.Request, purchaseOrder);
      }
    }


    [HttpPost]
    [Route("v4/trade/procurement/purchase-orders")]
    public SingleObjectModel CreatePurchaseOrder([FromBody] PurchaseOrderFields fields) {

      using (var usecases = PurchaseOrderUseCases.UseCaseInteractor()) {

        PurchaseOrderDto purchaseOrder = usecases.CreatePurchaseOrder(fields);

        return new SingleObjectModel(this.Request, purchaseOrder);
      }
    }


    [HttpPost]
    [Route("v4/trade/procurement/purchase-orders/{purchaseOrderUID:guid}/item")]
    public SingleObjectModel CreatePurchaseOrderItem([FromUri] string purchaseOrderUID,
                                                     [FromBody] PurchaseOrderItemFields fields) {

      using (var usecases = PurchaseOrderUseCases.UseCaseInteractor()) {

        PurchaseOrderDto purchaseOrder = usecases.CreatePurchaseOrderItem(purchaseOrderUID, fields);

        return new SingleObjectModel(this.Request, purchaseOrder);
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


    [HttpDelete]
    [Route("v4/trade/procurement/purchase-orders/{purchaseOrderUID:guid}/item/{purchaseOrderItemUID:guid}")]
    public SingleObjectModel DeletePurchaseOrderItem([FromUri] string purchaseOrderUID,
                                                     [FromUri] string purchaseOrderItemUID) {
      
      using (var usecases = PurchaseOrderUseCases.UseCaseInteractor()) {

        PurchaseOrderDto purchaseOrder = usecases.DeletePurchaseOrderItem(purchaseOrderUID,
                                                                          purchaseOrderItemUID);

        return new SingleObjectModel(this.Request, purchaseOrder);
      }
    }


    [HttpGet]
    [Route("v4/trade/procurement/purchase-orders/{purchaseOrderUID:guid}")]
    public SingleObjectModel GetPurchaseOrder([FromUri] string purchaseOrderUID) {
      
      using (var usecases = PurchaseOrderUseCases.UseCaseInteractor()) {

        PurchaseOrderDto purchaseOrder = usecases.GetPurchaseOrderDto(purchaseOrderUID);

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

        PurchaseOrderDto purchaseOrder = usecases.UpdatePurchaseOrder(purchaseOrderUID, fields);

        return new SingleObjectModel(this.Request, purchaseOrder);
      }
    }


    [HttpPut]
    [Route("v4/trade/procurement/purchase-orders/{purchaseOrderUID:guid}/item/{purchaseOrderItemUID:guid}")]
    public SingleObjectModel UpdatePurchaseOrderItem([FromUri] string purchaseOrderUID,
                                                     [FromUri] string purchaseOrderItemUID,
                                                     [FromBody] PurchaseOrderItemFields fields) {

      Assertion.RequireFail("Funcionalidad en proceso de desarrollo.");

      using (var usecases = PurchaseOrderUseCases.UseCaseInteractor()) {

        PurchaseOrderDto purchaseOrder =
          usecases.UpdatePurchaseOrderItem(purchaseOrderUID, purchaseOrderItemUID, fields);

        return new SingleObjectModel(this.Request, purchaseOrder);
      }
    }

  } // class PurchaseOrderController

} // namespace Empiria.Trade.WebApi.Procurement
