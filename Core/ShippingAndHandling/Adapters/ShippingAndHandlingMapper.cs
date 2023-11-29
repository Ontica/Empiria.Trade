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
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Empiria.Trade.Orders;
using Empiria.Trade.Products;

namespace Empiria.Trade.ShippingAndHandling.Adapters {


  /// <summary>Methods used to map Shipping And Handling.</summary>
  static internal class ShippingAndHandlingMapper {


    #region Public methods


    static internal IShippingAndHandling MapPackagingOrder(PackagingOrder packagings) {

      return MapEntry(packagings);
    }


    internal static IShippingAndHandling MapPackingDto(FixedList<Packing> packings) {

      return new PackingDto {
        PackingData = MapPackingData(packings),
        PackingItem = MapToPacking(packings),
        MissingItems = MapToMissingItems(packings)
      };

      //return MapToPacking(packings);
    }

    
    #endregion Public methods


    #region Private methods


    static private PackingData MapPackingData(FixedList<Packing> packings) {
      var data = new PackingData();

      data.Size++; //TODO AGREGAR VOLUMEN A CARACTERISTICAS DE CAJA
      data.Count = packings.Select(x => x.OrderPackingId).Count();

      return data;
    }


    private static FixedList<PackingItem> MapToPacking(FixedList<Packing> packings) {

      var items = new List<PackingItem>();

      foreach (var entry in packings) {
        var item = new PackingItem();
        item.UID= entry.OrderPackingUID;
        item.OrderUID = entry.Order.UID;
        item.Name = entry.PackageID;
        item.PackageTypeUID = entry.Size; // TODO SE TOMARA DEL OBJETO PackageType
        item.OrderItems = GetOrderItems(entry.OrderPackingUID, packings);

        items.Add(item);
      }

      return items.ToFixedList();
    }


    static private FixedList<PackingOrderItem> GetOrderItems(string orderPackingUID,
                                                             FixedList<Packing> packings) {
      var packingOrderItems = new List<PackingOrderItem>();

      var items = packings.FindAll(x=>x.OrderPackingUID==orderPackingUID);

      foreach (var item in items) {
        
        var packingOrderItem = new PackingOrderItem();

        packingOrderItem.WarehouseBin = WarehouseBin.Parse(item.InventoryEntry.WarehouseBinId);

        packingOrderItem.MergeFieldsData(item.OrderItemId);

        packingOrderItems.Add(packingOrderItem);
      }

      return packingOrderItems.ToFixedList();
    }


    static private FixedList<MissingItem> MapToMissingItems(FixedList<Packing> packings) {

      var missingItems = new List<MissingItem>();

      //foreach (var item in packings) {
      //  var items = packings.FindAll(x => x.OrderPackingUID == item.OrderPackingUID);
      //}

      return missingItems.ToFixedList();
    }


    static private PackingOrderDto MapEntry(PackagingOrder packaging) {

      var dto = new PackingOrderDto();
      dto.Order = packaging.Order;
      dto.PackageID = packaging.PackageID;
      dto.Size = packaging.Size;

      return dto;
    }




    #endregion Private methods


  } // class ShippingAndHandlingMapper

} // namespace Empiria.Trade.ShippingAndHandling.Adapters
