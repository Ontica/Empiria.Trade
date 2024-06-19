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


    static internal void CreateOrUpdateInventoryItemsForPicking(string inventoryOrderUID) {

      var inventoryOrderId = InventoryOrderEntry.Parse(inventoryOrderUID).InventoryOrderId;
      var inventoryItems = InventoryOrderData.GetInventoryItemsByInventoryOrder(inventoryOrderId);

      foreach (var item in inventoryItems.Where(x => x.WarehouseBin.Id == -1)) {

        GenerateInventoryItem(item);
      }

    }


    static private void GenerateInventoryItem(InventoryOrderItem item) {

      decimal itemQuantity = item.InProcessOutputQuantity;
      bool doUpdate = true;
      List<int> ignoredWhBinId = new List<int>();

      while (itemQuantity > 0) {

        //TODO PREGUNTAR QUE HACER AL NO HABER EXISTENCIAS, GUARDAR? MOSTRAR MSJ Y CANCELAR?

        var olderLocation = GetFirstLocationFinded(item.VendorProduct.Id, ignoredWhBinId);
        var stockLocations = GetOlderLocationList(olderLocation);

        if (stockLocations.Count == 0) {
          break;
        }

        var olderRealStock = stockLocations.Sum(x => x.RealStock);
        decimal inProcessOutputQuantity = 0;

        if (olderRealStock < itemQuantity && itemQuantity > 0) {
          
          ignoredWhBinId.Add(olderLocation.WarehouseBin.Id);

          inProcessOutputQuantity = olderRealStock;
          itemQuantity = itemQuantity - olderRealStock;

        } else if (olderRealStock >= itemQuantity && itemQuantity > 0) {

          inProcessOutputQuantity = itemQuantity;
          itemQuantity = 0;
        }

        var fields = GenerateInventoryItemFields(item, olderLocation, inProcessOutputQuantity, doUpdate);
        CreateInventoryOrderItemForPicking(item.InventoryOrder.InventoryOrderUID, fields);
        doUpdate = false;
      }
    }


    static private InventoryOrderItemFields GenerateInventoryItemFields(InventoryOrderItem item,
      SalesInventoryStock olderLocation, decimal inProcessOutputQuantity, bool doUpdate) {
      
      return new InventoryOrderItemFields {
        UID = doUpdate ? item.InventoryOrderItemUID : "",
        InventoryOrderTypeItemId = item.InventoryOrderTypeItemId,
        ItemReferenceId = item.ItemReferenceId,
        VendorProductUID = olderLocation.VendorProduct.UID,
        WarehouseBinUID = olderLocation.WarehouseBin.WarehouseBinUID,
        InProcessOutputQuantity = inProcessOutputQuantity
      };

    }


    static internal string GetInventoryOrderItemUIDByVendorProductLocation(
      string inventoryOrderUID, string vendorProductUID, string warehouseBinUID) {

      var inventoryOrder = InventoryOrderEntry.Parse(inventoryOrderUID);
      var items = InventoryOrderData.GetInventoryItemsByInventoryOrder(inventoryOrder.Id);

      var itemByVendorProductLocation = items.Where(x =>
                                          x.VendorProduct.UID == vendorProductUID &&
                                          x.WarehouseBin.UID == warehouseBinUID).FirstOrDefault();

      if (itemByVendorProductLocation is null) {
        return string.Empty;
      }
      return itemByVendorProductLocation.InventoryOrderItemUID;
    }


    #region Private methods

    static private void CreateInventoryOrderItemForPicking(
      string inventoryOrderUID, InventoryOrderItemFields fields) {

      var builder = new InventoryOrderBuilder();
      builder.CreateInventoryOrderItem(inventoryOrderUID, fields);
    }


    static private SalesInventoryStock GetFirstLocationFinded(int vendorProductId,
      List<int> ignoreWarehouseBinId) {

      var warehouseBinClauses = SimpleObjects.ConcatIntListIntoString(ignoreWarehouseBinId);

      var inventoryStock = CataloguesUseCases.GetInventoryStockByVendorProduct(
        vendorProductId, warehouseBinClauses);

      return inventoryStock.Where(x => x.WarehouseBin.Id > 0 && x.RealStock > 0).FirstOrDefault();
    }


    static private List<SalesInventoryStock> GetOlderLocationList(SalesInventoryStock olderLocation) {

      var inventoryStock = CataloguesUseCases.GetInventoryStockByVendorProduct(olderLocation.VendorProduct.Id, "");

      return inventoryStock.Where(x => x.WarehouseBin.Id == olderLocation.WarehouseBin.Id).ToList();
    }


    #endregion Private methods
  } // class InventoryOrderHelper

} // namespace Empiria.Trade.Inventory.Domain
