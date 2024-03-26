/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Shipping Management                        Component : Domain Layer                            *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Partitioned Type / Information Holder   *
*  Type     : ShippingLabelBuilder                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Generate data for Shipping labels.                                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using System.Linq;
using Empiria.Trade.Core.Catalogues;
using Empiria.Trade.Core.Common;
using Empiria.Trade.Orders;
using Empiria.Trade.Sales.Adapters;
using Empiria.Trade.Sales.Data;
using Empiria.Trade.Sales.ShippingAndHandling.Adapters;
using Empiria.Trade.Sales.ShippingAndHandling.Data;

namespace Empiria.Trade.Sales.ShippingAndHandling.Domain {


    /// <summary>Generate data for Shipping labels.</summary>
    internal class ShippingLabelBuilder {


        #region Constructor

        public ShippingLabelBuilder() {

        }


        #endregion Constructor


        #region Public methods


        internal BillingDto GetBillingForOrder(SalesOrder order) {

            return MapSalesOrderToBilling(order);
        }


        //internal FixedList<BillingDto> GetShippingBillingList(string shippingUID) {

        //    List<BillingDto> billingList = new List<BillingDto>();

        //    var shippingItems = ShippingData.GetOrdersForShippingByShippingId(shippingUID);

        //    foreach (var item in shippingItems) {

        //        SalesOrder order = SalesOrder.Parse(item.Order.UID);

        //        order.CalculateSalesOrder(QueryType.SalesShipping);

        //        billingList.Add(MapSalesOrderToBilling(order));
        //    }

        //    return billingList.ToFixedList();
        //}


        internal FixedList<ShippingLabel> GetShippingLabels(string shippingUID) {

            FixedList<ShippingOrderItem> ordersForShipping =
              ShippingData.GetOrdersForShippingByShippingId(shippingUID);

            var shippingLabels = new List<ShippingLabel>();

            foreach (var orderForShipping in ordersForShipping) {

                FixedList<SalesOrderItem> orderItems = SalesOrderItemsData.GetOrderItems(orderForShipping.Order.Id);

                shippingLabels.AddRange(MapToShippingLabelByOrderItem(orderItems));
            }

            return shippingLabels.ToFixedList();
        }


        internal FixedList<ShippingLabelByPallet> GetShippingLabelsForPallets(string shippingUID) {

            ShippingEntry shipping = ShippingEntry.Parse(shippingUID);

            FixedList<ShippingLabelByPallet> labelsByPallet = GetLabelsForPallets(shipping);

            return labelsByPallet;
        }


        #endregion Public methods


        #region Private methods


        private FixedList<ShippingLabelByPallet> GetLabelsForPallets(ShippingEntry shipping) {

            FixedList<ShippingPallet> shippingPallets =
              ShippingData.GetPalletByShippingUID(shipping.ShippingUID);

            var labelsByPallet = new List<ShippingLabelByPallet>();

            int palletCount = 0;
            foreach (var shippingPallet in shippingPallets) {
                palletCount = palletCount + 1;
                var label = new ShippingLabelByPallet();


                label.ParcelSupplier = SimpleObjectData.Parse(shipping.ParcelSupplierId).Name;
                label.PalletName = shippingPallet.ShippingPalletName;
                label.PalletCount = $"{palletCount}/{shippingPallets.Count}";
                
                FixedList<ShippingPackage> shippingPackages =
                    ShippingData.GetShippingPackagesByPalletUID(shippingPallet.ShippingPalletUID);

                Order order = Order.Parse(shippingPackages.FirstOrDefault().Order.Id);
                label.ShippingNumber = shipping.ShippingGuide;
                label.ShippingType = order.ShippingMethod;
                label.Customer = order.Customer.Name;
                label.CustomerAddress = $"{order.CustomerAddress.Address1} CP. {order.Customer.ZipCode}.";
                label.CustomerPhoneNumber = order.Customer.PhoneNumbers;

                GetPackingTypeCountByPallet(label, shippingPackages);

                labelsByPallet.Add(label);
            }

            return labelsByPallet.ToFixedList();
        }


        private void GetPackingTypeCountByPallet(ShippingLabelByPallet label,
                                                 FixedList<ShippingPackage> shippingPackages) {

            foreach (var pack in shippingPackages) {
                var packageType = PackageType.Parse(pack.OrderPacking.PackageTypeId);
                
                if (packageType.Name.Contains("Caja")) {
                    label.PackageQuantity++;
                }
                if (packageType.Name.Contains("Atado")) {
                    label.TiedQuantity++;
                }
                if (packageType.Name.Contains("Costal")) {
                    label.BagQuantity++;
                }

            }


        }


        private List<ShippingLabel> MapToShippingLabelByOrderItem(FixedList<SalesOrderItem> orderItems) {

            var shippingLabels = new List<ShippingLabel>();

            foreach (var item in orderItems) {

                var warehouse =
                  CataloguesUseCases.GetWarehouseBinProductByVendorProduct(item.VendorProduct.Id);
                warehouse.GetDescription();

                var shippingLabel = new ShippingLabel {
                    OrderUID = item.Order.UID,
                    ProductCode = item.VendorProduct.ProductFields.ProductCode,
                    ProductPresentation = item.VendorProduct.ProductPresentation.PresentationName,
                    Description = item.VendorProduct.ProductFields.ProductDescription,
                    Comments = "",
                    Quantity = item.Quantity,
                    Ubication = warehouse.Description
                };

                shippingLabels.Add(shippingLabel);
            }

            return shippingLabels;
        }


        private BillingDto MapSalesOrderToBilling(SalesOrder order) {

            BillingDto billing = new BillingDto {
                OrderUID = order.UID,
                OrderNumber = order.OrderNumber,
                Customer = order.Customer.Name,
                CustomerAddress = order.CustomerAddress.Address1,
                CustomerContact = order.CustomerContact.Name,
                CustomerPhone = order.CustomerContact.PhoneNumber,
                Supplier = order.Supplier.Name,
                SupplierAddress = order.Supplier.AddressLine1,
                SupplierPhonoNumber = order.Supplier.PhoneNumbers,
                SalesAgent = order.SalesAgent.Name,
                PaymentCondition = order.PaymentCondition,
                ShippingMethod = order.ShippingMethod,
                OrderNotes = order.Notes,
                ItemsCount = order.ItemsCount,
                BillingSubtotal = Math.Round(order.ItemsTotal,2),
                ShipmentTotal = Math.Round(order.Shipment,2),
                Taxes = Math.Round(order.Taxes, 2),
                BillingTotal = Math.Round(order.OrderTotal, 2),
                BillingItems = GetBillingItems(order)

            };

            return billing;
        }


        private FixedList<BillingItemDto> GetBillingItems(SalesOrder order) {

            List<BillingItemDto> billingItems = new List<BillingItemDto>();
            foreach (var orderItem in order.SalesOrderItems) {
                var item = new BillingItemDto {

                    ProductPresentation = orderItem.VendorProduct.ProductPresentation.PresentationName,
                    ProductCode = orderItem.VendorProduct.ProductFields.ProductCode,
                    ProductName = orderItem.VendorProduct.ProductFields.ProductName,
                    Quantity = orderItem.Quantity,
                    UnitPrice = orderItem.BasePrice,
                    SalesPrice = orderItem.SalesPrice,
                    DiscountPolicy = orderItem.DiscountPolicy,
                    Discount1 = orderItem.Discount,
                    Discount2 = orderItem.AdditionalDiscount,
                    Subtotal = orderItem.SubTotal
                };
                billingItems.Add(item);
            }

            return billingItems.ToFixedList();
        }


        #endregion Private methods

    } // class ShippingLabelBuilder

} // Empiria.Trade.Sales.ShippingAndHandling.Domain
