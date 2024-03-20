/* Empiria Trade Reporting ***********************************************************************************
*                                                                                                            *
*  Module   : Shipping and handling Management              Component : Web Api                              *
*  Assembly : Empiria.Trade.WebApi.ShippingAndHandling.dll  Pattern   : Controller                           *
*  Type     : ShippingLabelsWebApiClient                    License   : Please read LICENSE.txt file         *
*                                                                                                            *
*  Summary  : Query web API used to manage shippings labels.                                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Empiria.Trade.Reporting.WebApi.Client.Adapters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reporting.Web.Pages.Shipping;

namespace Empiria.Trade.Reporting.WebApi.Client.ShippingAndHandling {


  /// <summary>Query web API used to manage shippings labels.</summary>
  public class ShippingLabelsWebApiClient : ControllerBase {


    private readonly HttpClient httpClient = new HttpClient();


    [AllowAnonymous]
    [HttpGet("trade/reporting/shipping/{shippingUID}/labels")]
    public async Task<HttpResponseMessage> GetShippingLabelFromURI([FromRoute] string shippingUID) {

      var http = HttpApiClient("http://apps.sujetsa.com.mx:8080/api", TimeSpan.FromSeconds(240));

      var tradeController = $"/v4/trade/sales/shipping/{shippingUID}/labels";

      var response = await http.GetAsync(tradeController).ConfigureAwait(false);

      var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

      var label = new ShippingLabelModel();
      label.OnGet(content);

      return response;
    }



    #region Private methods

    public HttpClient HttpApiClient(string baseAddress, TimeSpan timeout) {
      

      try {
        baseAddress = baseAddress.EndsWith("/") ? baseAddress : baseAddress + "/";

        httpClient.BaseAddress = new Uri(baseAddress);
        httpClient.Timeout = timeout;

        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        return httpClient;

      } catch (Exception e) {
        throw new Exception(e.Message);
      }
    }

    #endregion Private methods


  } // class ShippingLabelsWebApiClient


} // namespace Empiria.Trade.Reporting.WebApi.Client.ShippingAndHandling
