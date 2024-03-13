using System.Collections.Generic;
using Empiria.Trade.Reporting.Web.Models;
using Empiria.Trade.Reporting.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Empiria.Trade.Reporting.Web.Pages.ShippingAndHandling {
    
    public class shipping_labelModel : PageModel {

        public List<ShippingDataLabel> ShippingData {
            get; set;
        } = new List<ShippingDataLabel>();


        public void OnGet() {

            ShippingData = GetOrders();
        }


        private List<ShippingDataLabel> GetOrders() {

            ReportsService useCase = new ReportsService();
            var shippingData = useCase.GetOrdersDataForLabel();

            List<ShippingDataLabel> labels = new List<ShippingDataLabel>();
            foreach (var item in shippingData) {
                var label = new ShippingDataLabel() {
                    ShippingGuide = item.ShippingGuide,
                    TotalPackages = item.TotalPackages,
                    OrdersCount= item.OrdersCount,
                    OrdersTotal= item.OrdersTotal
                };

                labels.Add(label);

            }

            return labels;
        }


    }
}
