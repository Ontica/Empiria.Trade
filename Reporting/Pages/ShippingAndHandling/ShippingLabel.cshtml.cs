using Empiria.Trade.Reporting.WebApi.Client.ShippingAndHandling;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Reporting.Web.Pages.Shipping {
  public class ShippingLabelModel : PageModel {

    public string LabelContent {
      get; set;
    } = string.Empty;

    public void OnGet(string content) {

      LabelContent = content;

    }
  }
}
