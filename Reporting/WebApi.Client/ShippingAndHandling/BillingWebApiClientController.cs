
using Empiria.Trade.Reporting.WebApi.Client.Adapters;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Reporting.Web.Pages.Shipping;

namespace Empiria.Trade.Reporting.WebApi.Client.ShippingAndHandling {


    [ApiController]
    [Route("")]
    public class BillingWebApiClientController : ControllerBase {


        [HttpGet("trade/reporting/shipping/{shippingUID}/billing")]
        public async Task<ShippingBillingDto> GetShippingBillingFromURI(
            [FromRoute] string shippingUID, [FromRoute] string orderUID) {

            var apiClientConfig = new HttpApiClientConfig();

            var http = apiClientConfig.HttpApiClient("http://apps.sujetsa.com.mx:8080", TimeSpan.FromSeconds(240));

            var uri = $"/api/v4/trade/sales/shipping/{shippingUID}/billing/{orderUID}";

            var response = await http.GetAsync(uri).ConfigureAwait(false);

            var content = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

            var data = content.RootElement.GetProperty("data");

            var billingDto = data.Deserialize<ShippingBillingDto>();

            return await Task.FromResult(billingDto).ConfigureAwait(false) ?? new ShippingBillingDto();
        }
    }
}
