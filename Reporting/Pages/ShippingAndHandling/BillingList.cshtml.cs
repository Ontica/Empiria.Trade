using Empiria.Trade.Reporting.WebApi.Client.Adapters;
using Empiria.Trade.Reporting.WebApi.Client.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Empiria.Trade.Reporting.Pages.ShippingAndHandling
{
    public class BillingListModel : PageModel
    {

        public IEnumerable<ShippingBillingDto> Billings {
            get; set;
        } = new List<ShippingBillingDto>();

        public async Task OnGet(string shippingUID)
        {
            //var service = new BillingWebApiClientController();
            //Billings = await service.GetShippingBillingList(shippingUID);

            var service = new ShippingBillingService();
            Billings = await service.GetShippingBillingList(shippingUID);

        }
    }
}
