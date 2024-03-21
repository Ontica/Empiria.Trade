using Empiria.Trade.Reporting.WebApi.Client.Adapters;
using Empiria.Trade.Reporting.WebApi.Client.ShippingAndHandling;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Reporting.Web.Pages.Shipping {
    public class ShippingLabelModel : PageModel {


        public IEnumerable<ShippingLabelDto> Labels {
            get; set;
        } = new List<ShippingLabelDto>();


        public async Task OnGet(string shippingUID) {

            var controller = new ShippingLabelsWebApiClientController();
            
            Labels = await controller.GetShippingLabelFromURI(shippingUID);

        }
    }
}
