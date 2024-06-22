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

namespace Empiria.Trade.WebApi.Procurement {


  /// <summary>Query web API used to retrieve purchase orders.</summary>
  public class PurchaseOrderController : WebApiController {


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
