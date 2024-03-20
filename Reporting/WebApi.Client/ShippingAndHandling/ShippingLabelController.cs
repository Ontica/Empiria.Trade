
using Microsoft.AspNetCore.Mvc;
using Reporting.Web.Pages.Shipping;

namespace Empiria.Trade.Reporting.WebApi.Client.ShippingAndHandling {
    
    
    [ApiController]
    [Route("[controller]")]
    public class ShippingLabelController :ControllerBase {


        [HttpGet("trade/reporting/shipping/{shippingUID}/labels")]
        public async Task<string> GetShippingLabelFromURI([FromRoute] string shippingUID) {

            var apiClientConfig = new HttpApiClientConfig();
            
            var http = apiClientConfig.HttpApiClient("http://apps.sujetsa.com.mx:8080", TimeSpan.FromSeconds(240));

            var tradeController = $"/api/v4/trade/sales/shipping/{shippingUID}/labels";

            var response = await http.GetAsync(tradeController).ConfigureAwait(false);

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return content;
        }

    }
}
