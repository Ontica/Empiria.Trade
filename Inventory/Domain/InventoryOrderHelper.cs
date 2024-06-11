/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Partitioned Type / Information Holder   *
*  Type     : InventoryOrderHelper                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Helper methods to build inventory orders.                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using System.Linq;
using Empiria.Trade.Core;
using Empiria.Trade.Core.Catalogues;
using Empiria.Trade.Inventory.Adapters;
using Empiria.Trade.Inventory.Data;
using Empiria.Trade.Orders;
using Empiria.Trade.Products;

namespace Empiria.Trade.Inventory.Domain {

  /// <summary>Helper methods to build inventory orders.</summary>
  internal class InventoryOrderHelper {


    static internal void CreateOrUpdateInventoryOrderItemsForPicking(InventoryOrderEntry inventoryOrder) {

      var inventoryItems = InventoryOrderData.GetInventoryItemsByOrderUID(inventoryOrder.InventoryOrderUID);

      foreach (var item in inventoryItems.Where(x => x.WarehouseBin.Id == -1)) {

        GenerateInventoryItemFields(item);
      }

    }


    static private void GenerateInventoryItemFields(InventoryOrderItem item) {

      decimal itemQuantity = item.InProcessOutputQuantity;
      bool doUpdate = true;
      List<int> ignoreWarehouseBinId = new List<int>();

      while (itemQuantity > 0) {

        //TODO PREGUNTAR QUE HACER AL NO HABER EXISTENCIAS, GUARDAR? MOSTRAR MSJ Y CANCELAR?

        var olderLocation = GetFirstLocationFinded(item.VendorProduct.Id, ignoreWarehouseBinId);
        var stockLocations = GetOlderLocationList(olderLocation);

        if (stockLocations.Count == 0) {
          break;
        }

        var olderRealStock = stockLocations.Sum(x => x.RealStock);
        decimal inProcessOutputQuantity = 0;

        if (olderRealStock < itemQuantity && itemQuantity > 0) {
          
          ignoreWarehouseBinId.Add(olderLocation.WarehouseBin.Id);

          inProcessOutputQuantity = olderRealStock;
          itemQuantity = itemQuantity - olderRealStock;

        } else if (olderRealStock >= itemQuantity && itemQuantity > 0) {

          inProcessOutputQuantity = itemQuantity;
          itemQuantity = 0;
        }
        CreateInventoryOrderItemForPicking(item, olderLocation, inProcessOutputQuantity, doUpdate);
        doUpdate = false;
      }

    }


    #region Private methods

    static private void CreateInventoryOrderItemForPicking(
      InventoryOrderItem item, SalesInventoryStock olderLocation,
      decimal inProcessOutputQuantity, bool doUpdate) {

      InventoryOrderItemFields fields = new InventoryOrderItemFields {
        UID = doUpdate ? item.InventoryOrderItemUID : "",
        InventoryOrderTypeItemId = item.InventoryOrderTypeItemId,
        ItemReferenceId = item.ItemReferenceId,
        VendorProductUID = olderLocation.VendorProduct.UID,
        WarehouseBinUID = olderLocation.WarehouseBin.WarehouseBinUID,
        InProcessOutputQuantity = inProcessOutputQuantity
      };

      var builder = new InventoryOrderBuilder();
      builder.CreateInventoryOrderItem(item.InventoryOrder.InventoryOrderUID, fields);
    }


    static private SalesInventoryStock GetFirstLocationFinded(int vendorProductId,
      List<int> ignoreWarehouseBinId) {

      var warehouseBinClauses = GetStringIdList(ignoreWarehouseBinId);

      var inventoryStock = CataloguesUseCases.GetInventoryStockByVendorProduct(
        vendorProductId, warehouseBinClauses);

      return inventoryStock.Where(x => x.WarehouseBin.Id > 0 && x.RealStock > 0).FirstOrDefault();
    }


    static private string GetStringIdList(List<int> arrayList) {
      if (arrayList.Count == 0) {
        return string.Empty;
      }
      string stringList = "";
      foreach (var intData in arrayList) {
        stringList += $"{intData},";
      }
      return stringList.Remove(stringList.Length - 1, 1);
    }


    static private List<SalesInventoryStock> GetOlderLocationList(SalesInventoryStock olderLocation) {

      var inventoryStock = CataloguesUseCases.GetInventoryStockByVendorProduct(olderLocation.VendorProduct.Id, "");

      return inventoryStock.Where(x => x.WarehouseBin.Id == olderLocation.WarehouseBin.Id).ToList();
    }

    #endregion Private methods
  } // class InventoryOrderHelper

} // namespace Empiria.Trade.Inventory.Domain
