/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Shipping and handling Management              Component : Web Api                              *
*  Assembly : Empiria.Trade.WebApi.ShippingAndHandling.dll  Pattern   : Controller                           *
*  Type     : ShippingLabelsController                      License   : Please read LICENSE.txt file         *
*                                                                                                            *
*  Summary  : Query web API used to manage shippings labels.                                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Web.Http;
using Empiria.Trade.Inventory.Adapters;
using Empiria.Trade.Inventory.UseCases;
using Empiria.WebApi;

namespace Empiria.Trade.WebApi.Reporting {
  
  
  /// <summary></summary>
  public class ReportGeneratorController : WebApiController {


    #region Web apis


    [HttpPost]
    [Route("v4/trade/reporting/inventory/data")]
    public SingleObjectModel GetShippingBilling([FromBody] ReportQuery query) {

      using (var usecases = ReportGeneratorUseCases.UseCaseInteractor()) {

        ReportDataDto billing = usecases.BuildReport(query);

        return new SingleObjectModel(this.Request, billing);
      }
    }


    #endregion Web apis


  }
}
