/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Shipping And Handling Management           Component : Interface adapters                      *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Mapper class                            *
*  Type     : ShippingAndHandlingMapper                  License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Methods used to map Shipping And Handling.                                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.ShippingAndHandling.Adapters {


  /// <summary>Methods used to map Shipping And Handling.</summary>
  static internal class ShippingAndHandlingMapper {


    #region Public methods


    static internal FixedList<IShippingAndHandling> MapPackagingOrder(FixedList<PackagingOrder> packagings) {

      return MapPackaging(packagings);
    }


    #endregion Public methods


    #region Private methods


    static private FixedList<IShippingAndHandling> MapPackaging(FixedList<PackagingOrder> packagings) {

      var mappedItems = packagings.Select((x) => MapEntry(x));

      return new FixedList<IShippingAndHandling>(mappedItems);
    }


    static private PackingOrderDto MapEntry(PackagingOrder packaging) {

      var dto = new PackingOrderDto();
      dto.Item = packaging.OrderItem;
      dto.PackageQuantity = packaging.PackageQuantity;
      dto.PackageID= packaging.PackageID;

      return dto;
    }


    #endregion Private methods


  } // class ShippingAndHandlingMapper

} // namespace Empiria.Trade.ShippingAndHandling.Adapters
