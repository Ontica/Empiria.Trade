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
using Empiria.Trade.Core.Common;
using Empiria.Trade.Orders;

namespace Empiria.Trade.Sales.ShippingAndHandling.Adapters {

  /// <summary>Methods used to map Shipping.</summary>
  static internal class ShippingMapper {

    #region Public methods


    static internal ShippingEntryDto Map(ShippingEntry entry) {

      return MapEntry(entry);
    }


    #endregion Public methods


    #region Private methods


    private static ShippingEntryDto MapEntry(ShippingEntry entry) {
      
      if (entry.ShippingOrderId == 0) {
        return new ShippingEntryDto();
      }

      var parcel = SimpleObjectData.Parse(entry.ParcelSupplierId);

      var dto = new ShippingEntryDto {
        ParcelSupplier = new NamedEntityDto(parcel.UID, parcel.Name),
        ShippingGuide = entry.ShippingGuide,
        ParcelAmount = entry.ParcelAmount,
        CustomerAmount = entry.CustomerAmount,
      };

      return dto;
    }


    #endregion Private methods

  } // class ShippingMapper

} // namespace Empiria.Trade.ShippingAndHandling.Adapters
