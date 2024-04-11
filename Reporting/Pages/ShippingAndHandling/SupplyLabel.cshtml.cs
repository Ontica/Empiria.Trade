using Empiria.Trade.Reporting.WebApi.Client.Adapters;
using Empiria.Trade.Reporting.WebApi.Client.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Reporting.Web.Pages.Shipping {
    public class SupplyLabelModel : PageModel {


        public IEnumerable<SupplyLabelDto> Labels {
            get; set;
        } = new List<SupplyLabelDto>();


        public async Task OnGet(string shippingUID) {

            var service = new ShippingLabelService();
            Labels = await service.GetShippingLabelFromURI(shippingUID);

        }
    }
}
