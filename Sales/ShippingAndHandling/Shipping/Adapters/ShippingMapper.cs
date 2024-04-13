/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Shipping Management                        Component : Interface adapters                      *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Mapper class                            *
*  Type     : ShippingMapper                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Methods used to map Shipping.                                                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using System.Linq;
using Empiria.DataTypes;
using Empiria.Trade.Core.Common;
using Empiria.Trade.Orders;
using Empiria.Trade.Sales.ShippingAndHandling.Data;
using Empiria.Trade.Sales.ShippingAndHandling.Domain;

namespace Empiria.Trade.Sales.ShippingAndHandling.Adapters {

    /// <summary>Methods used to map Shipping.</summary>
    static internal class ShippingMapper {

        #region Public methods


        static internal ShippingEntryDto Map(ShippingEntry entry) {

            return MapEntry(entry);
        }


        internal static FixedList<ShippingEntryDto> MapShippings(FixedList<ShippingEntry> entries) {
            var shippingDto = new List<ShippingEntryDto>();

            foreach (var entry in entries) {
                shippingDto.Add(MapEntry(entry));
            }

            return shippingDto.ToFixedList();
        }


        internal static ShippingDto MapShippingForParcelDelivery(ShippingEntry entry) {

            ShippingDto shippingDto = new ShippingDto();
            shippingDto.OrdersForShipping = MapToOrderForShippingDto(entry.OrdersForShipping);
            shippingDto.ShippingPalletsWithPackages = MapToShippingPalletDto(entry.ShippingPallets);
            shippingDto.ShippingData = MapEntry(entry);

            return shippingDto;
        }


        #endregion Public methods


        #region Private methods


        static private FixedList<PackageForShippingDto> GetPackagesDtoByOrder(
                        FixedList<OrderPackageForShipping> orderPackages) {

            var packagesDto = new List<PackageForShippingDto>();

            foreach (var orderPackage in orderPackages) {
                var packageDto = new PackageForShippingDto();
                packageDto.PackingItemUID = orderPackage.PackingItemUID;
                packageDto.PackageID = orderPackage.PackageID;
                packageDto.PackageTypeName = orderPackage.PackageTypeName;
                packageDto.TotalVolume = orderPackage.TotalVolume;
                packageDto.TotalWeight = orderPackage.TotalWeight;
                packagesDto.Add(packageDto);
            }

            return packagesDto.ToFixedList();
        }


        static private ShippingEntryDto MapEntry(ShippingEntry entry) {

            var parcel = SimpleObjectData.Parse(entry.ParcelSupplierId != 0 ? entry.ParcelSupplierId : -1);
            var parcelName = parcel.UID != "" ? parcel.Name : "";

            var shippingDto = new ShippingEntryDto {
                Customer = new NamedEntityDto(entry.Customer.UID, entry.Customer.Name),
                ShippingUID = entry.ShippingOrderId == -1 ? "" : entry.ShippingUID,
                ParcelSupplier = new NamedEntityDto(parcel.UID, parcelName),
                ShippingGuide = entry.ShippingGuide,
                ShippingNumber = entry.ShippingNumber,
                DeliveryNumber = entry.DeliveryNumber,
                ParcelAmount = entry.ParcelAmount,
                CustomerAmount = entry.CustomerAmount,
                ShippingDate = entry.ShippingDate,
                OrdersCount = entry.OrdersForShipping.Count,
                OrdersTotal = entry.OrdersTotal,
                TotalPackages = entry.OrdersForShipping.Sum(x => x.TotalPackages),
                TotalWeight = entry.OrdersForShipping.Sum(x => x.TotalWeight),
                TotalVolume = entry.OrdersForShipping.Sum(x => x.TotalVolume),
                ShippingMethod = entry.ShippingMethod,
                Status = entry.Status,

                ShippingLabelsMedia = new MediaData("text/html",
                    $"http://apps.sujetsa.com.mx:8080/reporting.api/ShippingAndHandling/" +
                    $"ShippingLabels?shippingUID={entry.ShippingUID}"),

                BillingsMedia = new MediaData("text/html",
                    $"http://apps.sujetsa.com.mx:8080/reporting.api/ShippingAndHandling/" +
                    $"BillingList?shippingUID={entry.ShippingUID}")
            };

            return shippingDto;
        }


        static private FixedList<ShippingOrderItemDto> MapToOrderForShippingDto(
                                    FixedList<ShippingOrderItem> orderForShipping) {

            var orderForShippingDto = new List<ShippingOrderItemDto>();

            foreach (var item in orderForShipping) {

                var itemDto = new ShippingOrderItemDto();
                itemDto.OrderUID = item.Order.UID;
                itemDto.OrderNumber = item.Order.OrderNumber;
                itemDto.OrderTotal = item.OrderTotal;
                itemDto.Customer = new NamedEntityDto(item.Order.Customer.UID, item.Order.Customer.Name);
                itemDto.Vendor = new NamedEntityDto(item.Order.SalesAgent.UID, item.Order.SalesAgent.Name);
                itemDto.TotalPackages = item.TotalPackages;
                itemDto.TotalWeight = item.TotalWeight;
                itemDto.TotalVolume = item.TotalVolume;
                itemDto.Packages = GetPackagesDtoByOrder(item.OrderPackages);

                itemDto.BillingMedia = new MediaData("text/html",
                $"http://apps.sujetsa.com.mx:8080/reporting.api/ShippingAndHandling/" +
                $"Billing?shippingUID={item.ShippingOrder.ShippingUID}&orderUID={item.Order.UID}");
      
        orderForShippingDto.Add(itemDto);
            }

            return orderForShippingDto.ToFixedList();
        }


        static private FixedList<ShippingPalletDto> MapToShippingPalletDto(
                        FixedList<ShippingPallet> shippingPallets) {

            var palletsDto = new List<ShippingPalletDto>();

            foreach (var pallet in shippingPallets) {

                var palletDto = new ShippingPalletDto();
                palletDto.ShippingPalletUID = pallet.ShippingPalletUID;
                palletDto.ShippingPalletName = pallet.ShippingPalletName;
                palletDto.Packages = pallet.ShippingPackages;
                palletDto.TotalPackages = pallet.TotalPackages;
                palletDto.TotalWeight = pallet.TotalWeight;
                palletDto.TotalVolume = pallet.TotalVolume;
                palletsDto.Add(palletDto);
            }

            return palletsDto.ToFixedList();
        }


        #endregion Private methods

    } // class ShippingMapper

} // namespace Empiria.Trade.ShippingAndHandling.Adapters
