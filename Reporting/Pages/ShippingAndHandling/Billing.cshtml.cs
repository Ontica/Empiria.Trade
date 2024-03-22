using Empiria.Trade.Reporting.WebApi.Client.Adapters;
using Empiria.Trade.Reporting.WebApi.Client.ShippingAndHandling;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Reporting.Web.Pages.Shipping
{
    public class BillingModel : PageModel
    {

        public IEnumerable<ShippingLabelByPalletDto> Labels {
            get; set;
        } = new List<ShippingLabelByPalletDto>();


        public async Task OnGet(string shippingUID) {
            var controller = new ShippingLabelsWebApiClientController();

            Labels = await controller.GetShippingLabelByPalletFromURI(shippingUID);
        }
    }
}
