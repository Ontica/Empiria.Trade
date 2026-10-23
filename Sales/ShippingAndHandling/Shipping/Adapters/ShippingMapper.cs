/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Shipping Management                        Component : Interface adapters                      *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Mapper class                            *
*  Type     : ShippingMapper                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Methods used to map Shipping.                                                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System.Collections.Generic;
using System.Linq;
using Empiria.Parties;
using Empiria.Storage;

using Empiria.Trade.Core.Common;

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
      shippingDto.ShippingData = MapEntry(entry);
      shippingDto.OrdersForShipping = MapToOrderForShippingDto(entry.ShippingOrderItems);
      shippingDto.ShippingPalletsWithPackages = MapToShippingPalletDto(entry.ShippingPallets);

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

      var customer = entry.Customer != null ? entry.Customer : Party.Parse(-1);

      var shippingDto = new ShippingEntryDto {
        Customer = new NamedEntityDto(customer.UID, customer.Name),
        ShippingUID = entry.Id == -1 ? "" : entry.ShippingOrderUID,
        ParcelSupplier = new NamedEntityDto(entry.ParcelSupplier.UID, entry.ParcelSupplier.Name),
        ShippingGuide = entry.ShippingGuide,
        ShippingNumber = entry.ShippingNumber,
        DeliveryNumber = entry.DeliveryNumber,
        ParcelAmount = entry.ParcelAmount,
        CustomerAmount = entry.CustomerAmount,
        ShippingDate = entry.ShippingDate,
        OrdersCount = entry.ShippingOrderItems.Count,
        OrdersTotal = entry.OrdersTotal,
        TotalPackages = entry.ShippingOrderItems.Sum(x => x.TotalPackages),
        TotalWeight = entry.ShippingOrderItems.Sum(x => x.TotalWeight),
        TotalVolume = entry.ShippingOrderItems.Sum(x => x.TotalVolume),
        ShippingMethod = entry.ShippingMethod,
        Status = entry.Status,

        ShippingLabelsMedia = new MediaData("text/html",
              $"http://192.168.1.209:8085/reporting.api/ShippingAndHandling/" +
              $"ShippingLabels?shippingUID={entry.ShippingOrderUID}"),

        BillingsMedia = new MediaData("text/html",
              $"http://192.168.1.209:8085/reporting.api/ShippingAndHandling/" +
              $"BillingList?shippingUID={entry.ShippingOrderUID}")
      };

      return shippingDto;
    }


    static private FixedList<ShippingOrderItemDto> MapToOrderForShippingDto(
                                FixedList<ShippingOrderItem> shippingOrderItems) {

      var shippingOrderItemsDto = new List<ShippingOrderItemDto>();

      foreach (var shippingOrderItem in shippingOrderItems) {

        var salesAgent = shippingOrderItem.SalesOrder.SalesAgent =
                          Party.Parse(shippingOrderItem.SalesOrder.SalesAgentId);

        var shippingOrderItemDto = new ShippingOrderItemDto();

        shippingOrderItemDto.OrderUID = shippingOrderItem.SalesOrder.UID;
        shippingOrderItemDto.OrderNumber = shippingOrderItem.SalesOrder.OrderNo;
        shippingOrderItemDto.OrderTotal = shippingOrderItem.OrderTotal;
        shippingOrderItemDto.Customer = shippingOrderItem.SalesOrder.Beneficiary.MapToNamedEntity();
        shippingOrderItemDto.Vendor = salesAgent.MapToNamedEntity();
        shippingOrderItemDto.TotalPackages = shippingOrderItem.TotalPackages;
        shippingOrderItemDto.TotalWeight = shippingOrderItem.TotalWeight;
        shippingOrderItemDto.TotalVolume = shippingOrderItem.TotalVolume;
        shippingOrderItemDto.Packages = GetPackagesDtoByOrder(shippingOrderItem.OrderPackages);

        shippingOrderItemDto.BillingMedia = new MediaData("text/html",
            $"http://192.168.1.209:8085/reporting.api/ShippingAndHandling/" +
            $"Billing?shippingUID={shippingOrderItem.ShippingOrder.ShippingOrderUID}" +
            $"&orderUID={shippingOrderItem.SalesOrder.OrderUID}");

        shippingOrderItemsDto.Add(shippingOrderItemDto);
      }

      return shippingOrderItemsDto.ToFixedList();
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
