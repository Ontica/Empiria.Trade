using Empiria.Trade.Reporting.WebApi.Client.Adapters;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Empiria.Trade.Reporting.WebApi.Client.Services {
    public class ShippingLabelService {


        public async Task<List<ShippingLabelByPalletDto>> GetShippingLabelsFromURI([FromRoute] string shippingUID) {

            var apiClientConfig = new HttpApiClientConfig("http://apps.sujetsa.com.mx:8080", TimeSpan.FromSeconds(240));

            var http = apiClientConfig.HttpApiClient();

            var uri = $"/api/v4/trade/sales/shipping/{shippingUID}/label-pallets";

            return await apiClientConfig.DeserializeObjectList(new List<ShippingLabelByPalletDto>(), http, uri);
        }


        public async Task<List<SupplyLabeDto>> GetShippingLabelFromURI([FromRoute] string shippingUID) {

            var apiClientConfig = new HttpApiClientConfig("http://apps.sujetsa.com.mx:8080", TimeSpan.FromSeconds(240));

            var http = apiClientConfig.HttpApiClient();

            var uri = $"/api/v4/trade/sales/shipping/supply/{shippingUID}/labels";

            return await apiClientConfig.DeserializeObjectList(new List<SupplyLabeDto>(), http, uri);
        }

    }
}
