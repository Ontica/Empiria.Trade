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
using Empiria.Trade.Core.Common;
using Empiria.Trade.Orders;
using Empiria.Trade.Sales.ShippingAndHandling.Data;
using Empiria.Trade.Sales.ShippingAndHandling.Domain;

namespace Empiria.Trade.Sales.ShippingAndHandling.Adapters {

  /// <summary>Methods used to map Shipping.</summary>
  static internal class ShippingMapper {

    #region Public methods


    static internal ShippingEntryDto Map(ShippingEntry entry) {
      var shippingOrderItem = new FixedList<ShippingOrderItemDto>();
      return MapEntry(entry, shippingOrderItem);
    }


    internal static ShippingDto MapShippingForParcelDelivery(ShippingEntry entry) {

      ShippingDto shippingDto = new ShippingDto();

      shippingDto.OrdersForShipping = MapToOrderForShippingDto(entry.OrdersForShipping);
      shippingDto.ShippingData = MapEntry(entry, shippingDto.OrdersForShipping);

      return shippingDto;
    }


    #endregion Public methods


    #region Private methods


    private static ShippingEntryDto MapEntry(ShippingEntry entry,
                    FixedList<ShippingOrderItemDto> ordersForShipping) {

      if (entry.ShippingOrderId == 0) {
        return new ShippingEntryDto();
      }

      var parcel = SimpleObjectData.Parse(entry.ParcelSupplierId);
      var parcelName = parcel.UID != "" ? parcel.Name : "";

      var shippingDto = new ShippingEntryDto {
        ShippingUID= entry.ShippingUID,
        ParcelSupplier = new NamedEntityDto(parcel.UID, parcelName),
        ShippingGuide = entry.ShippingGuide,
        ParcelAmount = entry.ParcelAmount,
        CustomerAmount = entry.CustomerAmount,
        ShippingDate = entry.ShippingDate,
        OrdersCount = entry.OrdersForShipping.Count,
        OrdersTotal= ordersForShipping.Sum(x => x.OrderTotal),
        TotalPackages = entry.OrdersForShipping.Sum(x => x.TotalPackages),
        TotalWeight = entry.OrdersForShipping.Sum(x => x.TotalWeight),
        TotalVolume = entry.OrdersForShipping.Sum(x => x.TotalVolume)
      };

      return shippingDto;
    }


    static private FixedList<ShippingOrderItemDto> MapToOrderForShippingDto(
                                FixedList<ShippingOrderItem> orderForShipping) {

      var orderForShippingDto = new List<ShippingOrderItemDto>();

      foreach (var item in orderForShipping) {

        FixedList<SalesOrderItem> salesOrderItems = SalesOrderItem.GetOrderItems(item.Order.Id);

        var itemDto = new ShippingOrderItemDto();
        itemDto.OrderUID = item.Order.UID;
        itemDto.OrderName = item.Order.OrderNumber;
        itemDto.OrderTotal = salesOrderItems.Sum(x => x.Total);
        itemDto.Customer = new NamedEntityDto(item.Order.Customer.UID, item.Order.Customer.Name);
        itemDto.Vendor = new NamedEntityDto(item.Order.SalesAgent.UID, item.Order.SalesAgent.Name);
        itemDto.TotalPackages = item.TotalPackages;
        itemDto.TotalWeight = item.TotalWeight;
        itemDto.TotalVolume = item.TotalVolume;
        orderForShippingDto.Add(itemDto);
      }

      return orderForShippingDto.ToFixedList();
    }


    #endregion Private methods

  } // class ShippingMapper

} // namespace Empiria.Trade.ShippingAndHandling.Adapters
