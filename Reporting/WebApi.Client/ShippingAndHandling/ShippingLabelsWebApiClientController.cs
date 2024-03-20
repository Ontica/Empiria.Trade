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
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using Empiria.Trade.Reporting.WebApi.Client.Adapters;
using Microsoft.AspNetCore.Mvc;
using Reporting.Web.Pages.Shipping;

namespace Empiria.Trade.Reporting.WebApi.Client.ShippingAndHandling {
    
    
    [ApiController]
    [Route("")]
    public class ShippingLabelsWebApiClientController :ControllerBase {


        [HttpGet("trade/reporting/shipping/{shippingUID}/labels")]
        public async Task<List<ShippingLabelDto>> GetShippingLabelFromURI([FromRoute] string shippingUID) {

            var apiClientConfig = new HttpApiClientConfig();
            
            var http = apiClientConfig.HttpApiClient("http://apps.sujetsa.com.mx:8080", TimeSpan.FromSeconds(240));

            var uri = $"/api/v4/trade/sales/shipping/{shippingUID}/labels";

            var response = await http.GetAsync(uri).ConfigureAwait(false);

            var content = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

            var data = content.RootElement.GetProperty("data");

            var shippingLabelDto = data.Deserialize<List<ShippingLabelDto>>();

            //var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return await Task.FromResult(shippingLabelDto).ConfigureAwait(false) ?? new List<ShippingLabelDto>();
        }
    }
}
