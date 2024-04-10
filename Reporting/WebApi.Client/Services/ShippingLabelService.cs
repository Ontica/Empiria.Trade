using Empiria.Trade.Reporting.WebApi.Client.Adapters;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Empiria.Trade.Reporting.WebApi.Client.Services {
  public class ShippingLabelService {


    public async Task<List<ShippingLabelByPalletDto>> GetShippingLabelsFromURI([FromRoute] string shippingUID) {

      var httpConfig = new HttpApiClientConfig("http://apps.sujetsa.com.mx:8080", TimeSpan.FromSeconds(240));
      var http = httpConfig.HttpApiClient();
      var uri = $"/api/v4/trade/sales/shipping/{shippingUID}/label-pallets";
      var data = await httpConfig.GetJsonContent(http, uri);

      return await httpConfig.DeserializeObjectList(new List<ShippingLabelByPalletDto>(), data);
    }


    public async Task<List<SupplyLabelDto>> GetShippingLabelFromURI([FromRoute] string shippingUID) {

      var httpConfig = new HttpApiClientConfig("http://apps.sujetsa.com.mx:8080", TimeSpan.FromSeconds(240));
      var http = httpConfig.HttpApiClient();
      var uri = $"/api/v4/trade/sales/shipping/supply/{shippingUID}/labels";
      var data = await httpConfig.GetJsonContent(http, uri);

      return await httpConfig.DeserializeObjectList(new List<SupplyLabelDto>(), data);
    }

  }
}
