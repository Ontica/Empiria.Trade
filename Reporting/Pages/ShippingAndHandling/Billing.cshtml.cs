using Empiria.Trade.Reporting.WebApi.Client.Adapters;
using Empiria.Trade.Reporting.WebApi.Client.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Reporting.Web.Pages.Shipping
{
    public class BillingModel : PageModel
    {

        public ShippingBillingDto Billing {
            get; set;
        } = new ShippingBillingDto();


        public async Task OnGet(string shippingUID, string orderUID) {
            
            //var controller = new BillingWebApiClientController();
            //Billing = await controller.GetShippingBilling(shippingUID, orderUID);

            var service = new ShippingBillingService();
            Billing = await service.GetShippingBilling(shippingUID, orderUID);
        }
    }
}
