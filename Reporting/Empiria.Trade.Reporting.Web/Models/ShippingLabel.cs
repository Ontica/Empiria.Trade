

namespace Empiria.Trade.Reporting.Web.Models {

    public class ShippingLabel {

    }


    public class ShippingDataLabel {


        public string ShippingUID {
            get; set;
        } = string.Empty;


        public string ShippingGuide {
            get; set;
        } = string.Empty;


        public int TotalPackages {
            get; set;
        }


        public int OrdersCount {
            get; set;
        }


        public decimal OrdersTotal {
            get; set;
        }


    }
}
