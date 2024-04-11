using Empiria.Trade.Reporting.WebApi.Client.Adapters;
using Empiria.Trade.Reporting.WebApi.Client.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Empiria.Trade.Reporting.Pages.ShippingAndHandling
{
    public class ShippingLabelsModel : PageModel
    {

        public IEnumerable<ShippingLabelDto> Labels {
            get; set;
        } = new List<ShippingLabelDto>();


        public async Task OnGet(string shippingUID) {
            
            var service = new ShippingLabelService();

            Labels = await service.GetShippingLabelsFromURI(shippingUID);
        }
    }
}
