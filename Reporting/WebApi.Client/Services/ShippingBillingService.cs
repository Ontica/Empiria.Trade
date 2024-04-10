using System.Collections.Generic;
using System.Text.Json;
using Empiria.Trade.Reporting.WebApi.Client.Adapters;

namespace Empiria.Trade.Reporting.WebApi.Client.Services {
  public class ShippingBillingService {


    public async Task<ShippingBillingDto> GetShippingBilling(string shippingUID, string orderUID) {

      var httpConfig = new HttpApiClientConfig("http://apps.sujetsa.com.mx:8080", TimeSpan.FromSeconds(240));
      var http = httpConfig.HttpApiClient();
      var uri = $"/api/v4/trade/sales/shipping/{shippingUID}/billing/{orderUID}";
      var data = await httpConfig.GetJsonContent(http, uri);

      return await httpConfig.DeserializeObject(new ShippingBillingDto(), data);
    }


    public async Task<List<ShippingBillingDto>> GetShippingBillingList(string shippingUID) {

      var httpConfig = new HttpApiClientConfig("http://apps.sujetsa.com.mx:8080", TimeSpan.FromSeconds(240));
      var http = httpConfig.HttpApiClient();
      var uri = $"/api/v4/trade/sales/shipping/{shippingUID}/billing/";
      JsonElement data = await httpConfig.GetJsonContent(http, uri);

      return await httpConfig.DeserializeObjectList(new List<ShippingBillingDto>(), data);
    }


  }
}
