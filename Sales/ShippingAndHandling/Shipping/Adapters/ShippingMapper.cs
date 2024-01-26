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

      return MapEntry(entry);
    }


    internal static ShippingDto MapShippingForParcelDelivery(ShippingEntry entry) {

      ShippingDto shippingDto = new ShippingDto();

      shippingDto.ShippingData = MapEntry(entry);
      shippingDto.OrderForShipping = MapToOrderForShippingDto(entry.OrderForShipping);

      return shippingDto;
    }


    #endregion Public methods


    #region Private methods


    private static ShippingEntryDto MapEntry(ShippingEntry entry) {

      if (entry.ShippingOrderId == 0) {
        return new ShippingEntryDto();
      }

      var parcel = SimpleObjectData.Parse(entry.ParcelSupplierId);

      var shippingDto = new ShippingEntryDto {
        ParcelSupplier = new NamedEntityDto(parcel.UID, parcel.Name),
        ShippingGuide = entry.ShippingGuide,
        ParcelAmount = entry.ParcelAmount,
        CustomerAmount = entry.CustomerAmount,
        ShippingDate = entry.ShippingDate,
        TotalOrders = entry.OrderForShipping.Count,
        TotalPackages = entry.OrderForShipping.Sum(x => x.TotalPackages),
        TotalWeight = entry.OrderForShipping.Sum(x => x.TotalWeight),
        TotalVolume = entry.OrderForShipping.Sum(x => x.TotalVolume)
      };

      return shippingDto;
    }


    static private FixedList<ShippingOrderItemDto> MapToOrderForShippingDto(
                                FixedList<ShippingOrderItem> orderForShipping) {

      var orderForShippingDto = new List<ShippingOrderItemDto>();

      foreach (var item in orderForShipping) {
        var itemDto = new ShippingOrderItemDto();
        itemDto.OrderUID = item.Order.UID;
        itemDto.OrderName = item.Order.OrderNumber;
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
