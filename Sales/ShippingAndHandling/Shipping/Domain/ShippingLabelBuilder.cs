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
using System.Reflection.Emit;
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


        internal FixedList<BillingDto> GetShippingBillingList(string shippingUID) {

            List<BillingDto> billingList = new List<BillingDto>();

            var shippingItems = ShippingData.GetOrdersForShippingByShippingId(shippingUID);

            foreach (var item in shippingItems) {

                SalesOrder order = SalesOrder.Parse(item.Order.UID);

                order.CalculateSalesOrder();

                billingList.Add(MapSalesOrderToBilling(order));
            }

            return billingList.ToFixedList();
        }


        internal FixedList<ShippingLabel> GetShippingLabels(string shippingUID) {

            ShippingEntry shipping = ShippingEntry.Parse(shippingUID);

            var helper = new ShippingHelper();

            helper.GetShippingNumber(shipping);

            FixedList<ShippingLabel> labelsByPallet = GetLabelsForPallets(shipping);
            FixedList<ShippingLabel> labelsByPackage = GetLabelsForPackages(shipping, labelsByPallet);
            //FixedList<ShippingLabel> labelsByOrders = GetLabelsForOrderItems(shipping, labelsByPallet);

            return labelsByPackage;
        }


        internal FixedList<SupplyLabel> GetSupplyLabels(string shippingUID) {

            FixedList<ShippingOrderItem> ordersForShipping =
              ShippingData.GetOrdersForShippingByShippingId(shippingUID);

            var shippingLabels = new List<SupplyLabel>();

            foreach (var orderForShipping in ordersForShipping) {

                FixedList<SalesOrderItem> orderItems = SalesOrderItemsData.GetOrderItems(orderForShipping.Order.Id);

                shippingLabels.AddRange(MapToShippingLabelByOrderItem(orderItems));
            }

            return shippingLabels.ToFixedList();
        }


        #endregion Public methods


        #region Private methods


        private FixedList<ShippingLabel> GetLabelsForPackages(ShippingEntry shipping,
            FixedList<ShippingLabel> labelsByPallet) {

            var labelsByItem = labelsByPallet.ToList();

            FixedList<ShippingOrderItem> shippingItems =
              ShippingData.GetOrdersForShippingByShippingId(shipping.ShippingUID);

            
            foreach (var item in shippingItems) {

                var existPallet = labelsByPallet.FindAll(x => x.ShippingUID == item.ShippingOrder.ShippingUID);

                if (existPallet.Count == 0) {

                    labelsByItem.AddRange(GetPackagesDataForLabels(item, shipping));
                }
            }

            return labelsByItem.ToFixedList();
        }


        private IEnumerable<ShippingLabel> GetPackagesDataForLabels(ShippingOrderItem item, ShippingEntry shipping) {

            var packagings = PackagingData.GetPackagesForItemsByOrder(item.Order.UID);
            var labels = new List<ShippingLabel>();

            int packingCount = 0;
            foreach (var packing in packagings) {

                packingCount = packingCount + 1;
                var packageType = PackageType.Parse(packing.PackageTypeId);

                var label = new ShippingLabel();
                label.ShippingUID = item.ShippingOrder.ShippingUID;
                label.ShippingNumber = item.ShippingOrder.ShippingNumber;
                label.DeliveryNumber = item.ShippingOrder.DeliveryNumber;
                label.ParcelSupplier = SimpleObjectData.Parse(shipping.ParcelSupplierId).Name;
                label.PackingName = $"{packing.PackageID} / {packageType.Name}";
                label.PackingCount = $"({packingCount}/{packagings.Count}) ";
                label.ShippingGuide = shipping.ShippingGuide;
                label.ShippingType = item.Order.ShippingMethod;
                label.Customer = item.Order.Customer.Name;
                label.CustomerAddress = $"{item.Order.CustomerAddress.Address1} " +
                                        $"CP. {item.Order.Customer.ZipCode}.";
                label.CustomerPhoneNumber = item.Order.Customer.PhoneNumbers;

                labels.Add(label);
            }
            
            return labels.ToFixedList();
        }


        private FixedList<ShippingLabel> GetLabelsForOrderItems(ShippingEntry shipping,
          FixedList<ShippingLabel> labelsByPallet) {

            var labelsByItem = labelsByPallet.ToList();

            FixedList<ShippingOrderItem> shippingItems =
              ShippingData.GetOrdersForShippingByShippingId(shipping.ShippingUID);

            int packingCount = 0;
            foreach (var item in shippingItems) {

                var existPallet = labelsByPallet.FindAll(x => x.ShippingUID == item.ShippingOrder.ShippingUID);

                if (existPallet.Count == 0) {
                    packingCount = packingCount + 1;
                    var label = new ShippingLabel();
                    label.ShippingUID = item.ShippingOrder.ShippingUID;
                    label.ShippingNumber = item.ShippingOrder.ShippingNumber;
                    label.DeliveryNumber = item.ShippingOrder.DeliveryNumber;
                    label.ParcelSupplier = SimpleObjectData.Parse(shipping.ParcelSupplierId).Name;
                    label.PackingName = $"Orden: ";
                    label.PackingCount = $"{item.Order.OrderNumber} ({packingCount}/{shippingItems.Count}) ";
                    label.ShippingGuide = shipping.ShippingGuide;
                    label.ShippingType = item.Order.ShippingMethod;
                    label.Customer = item.Order.Customer.Name;
                    label.CustomerAddress = $"{item.Order.CustomerAddress.Address1} " +
                                            $"CP. {item.Order.Customer.ZipCode}.";
                    label.CustomerPhoneNumber = item.Order.Customer.PhoneNumbers;

                    GetPackingTypesByOrder(label, item.Order.UID);

                    labelsByItem.Add(label);
                }
            }

            return labelsByItem.ToFixedList();
        }


        private FixedList<ShippingLabel> GetLabelsForPallets(ShippingEntry shipping) {

            FixedList<ShippingPallet> shippingPallets =
              ShippingData.GetPalletByShippingUID(shipping.ShippingUID);

            var labelsByPallet = new List<ShippingLabel>();

            int palletCount = 0;
            foreach (var shippingPallet in shippingPallets) {
                palletCount = palletCount + 1;
                var label = new ShippingLabel();

                label.ShippingUID = shipping.ShippingUID;
                label.ShippingNumber = shipping.ShippingNumber;
                label.DeliveryNumber = shipping.DeliveryNumber;
                label.ParcelSupplier = SimpleObjectData.Parse(shipping.ParcelSupplierId).Name;
                label.PackingName = $"Tarima: ";
                label.PackingCount = $"({palletCount}/{shippingPallets.Count}) "+
                                     $"{shippingPallet.ShippingPalletName} ";

                FixedList<ShippingPackage> shippingPackages =
                    ShippingData.GetShippingPackagesByPalletUID(shippingPallet.ShippingPalletUID);

                Order order = Order.Parse(shippingPackages.FirstOrDefault().Order.Id);
                label.ShippingGuide = shipping.ShippingGuide;
                label.ShippingType = order.ShippingMethod;
                label.Customer = order.Customer.Name;
                label.CustomerAddress = $"{order.CustomerAddress.Address1} CP. {order.Customer.ZipCode}.";
                label.CustomerPhoneNumber = order.Customer.PhoneNumbers;
                label.WithPallets = true;
                GetPackingTypeCountByPallet(label, shippingPackages);

                labelsByPallet.Add(label);
            }

            return labelsByPallet.ToFixedList();
        }


        private void GetPackingTypesByOrder(ShippingLabel label, string orderUID) {

            FixedList<PackageForItem> packingOrder =
                PackagingData.GetPackagesForItemsByOrder(orderUID);

            foreach (var pack in packingOrder) {
                var packageType = PackageType.Parse(pack.PackageTypeId);

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


        private void GetPackingTypeCountByPallet(ShippingLabel label,
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


        private List<SupplyLabel> MapToShippingLabelByOrderItem(FixedList<SalesOrderItem> orderItems) {

            var shippingLabels = new List<SupplyLabel>();

            foreach (var item in orderItems) {

                var warehouse =
                  CataloguesUseCases.GetWarehouseBinProductByVendorProduct(item.VendorProduct.Id);
                warehouse.GetDescription();

                var shippingLabel = new SupplyLabel {
                    OrderUID = item.Order.UID,
                    ProductCode = item.VendorProduct.ProductFields.ProductCode,
                    ProductPresentation = item.VendorProduct.ProductPresentation.PresentationName,
                    Description = item.VendorProduct.ProductFields.ProductDescription,
                    Comments = "",
                    Quantity = item.Quantity,
                    Ubication = warehouse.Description // TODO RENAME FOR Location AND GET INFO FROM WAREHOUSEBIN
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
                BillingSubtotal = Math.Round(order.ItemsTotal, 2),
                ShipmentTotal = Math.Round(order.Shipment, 2),
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
