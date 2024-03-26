using System.Text.Json;
using Empiria.Trade.Reporting.WebApi.Client.Adapters;

namespace Empiria.Trade.Reporting.WebApi.Client.Services {
    public class ShippingBillingService {


        public async Task<ShippingBillingDto> GetShippingBilling(string shippingUID, string orderUID) {

            var apiClientConfig = new HttpApiClientConfig("http://apps.sujetsa.com.mx:8080", TimeSpan.FromSeconds(240));

            var http = apiClientConfig.HttpApiClient();

            var uri = $"/api/v4/trade/sales/shipping/{shippingUID}/billing/{orderUID}";

            return await apiClientConfig.DeserializeObject(new ShippingBillingDto(), http, uri);

            //var response = await http.GetAsync(uri).ConfigureAwait(false);

            //var content = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

            //var data = content.RootElement.GetProperty("data");

            //var billingDto = data.Deserialize<ShippingBillingDto>();

            //return await Task.FromResult(billingDto).ConfigureAwait(false) ?? new ShippingBillingDto();
        }


        public async Task<List<ShippingBillingDto>> GetShippingBillingList(string shippingUID) {

            var apiClientConfig = new HttpApiClientConfig("http://apps.sujetsa.com.mx:8080", TimeSpan.FromSeconds(240));

            var http = apiClientConfig.HttpApiClient();

            var uri = $"/api/v4/trade/sales/shipping/{shippingUID}/billing/";

            return await apiClientConfig.DeserializeObjectList(new List<ShippingBillingDto>(), http, uri);
        }

        
    }
}
