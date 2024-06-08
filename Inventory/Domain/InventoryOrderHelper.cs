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
using System.Linq;
using Empiria.Trade.Core.Catalogues;
using Empiria.Trade.Inventory.Adapters;
using Empiria.Trade.Inventory.Data;

namespace Empiria.Trade.Inventory.Domain {

  /// <summary>Helper methods to build inventory orders.</summary>
  internal class InventoryOrderHelper {


    static internal void CreateOrUpdateInventoryOrderItemsForPicking(string inventoryOrderUID) {

      var inventoryOrder = InventoryOrderData.GetInventoryOrderByUID(inventoryOrderUID);
      var inventoryItems = InventoryOrderData.GetInventoryItemsByOrderUID(inventoryOrderUID);

      if (inventoryOrder.AssignedToId < 0) {

        AssignInventoryItemsByWarehouseBin(inventoryOrder, inventoryItems);
      }

    }


    static private void AssignInventoryItemsByWarehouseBin(
      InventoryOrderEntry inventoryOrder, FixedList<InventoryOrderItem> inventoryItems) {

      foreach (var item in inventoryItems.Where(x => x.WarehouseBin.Id == -1)) {

        decimal itemQuantity = item.InProcessOutputQuantity;
        decimal outputQuantity = 0;

        var inventoryStock = CataloguesUseCases.GetInventoryStockByVendorProduct(item.VendorProduct.Id);
        var older = inventoryStock.Where(x => x.WarehouseBin.Id > 0 && x.RealStock > 0).FirstOrDefault();
        var olderStockByWarehouseBin = inventoryStock.Where(x=>x.WarehouseBin.Id == older.WarehouseBin.Id).Sum(x => x.Stock);
        var olderRealStockByWarehouseBin = inventoryStock.Where(x => x.WarehouseBin.Id == older.WarehouseBin.Id).Sum(x => x.RealStock);

        decimal quantity = 15;

        if (quantity < itemQuantity) {

          outputQuantity = quantity;
          itemQuantity = itemQuantity - outputQuantity;
        }
        
        if (olderRealStockByWarehouseBin >= item.InProcessOutputQuantity) {

        }

        InventoryOrderItemFields fields = new InventoryOrderItemFields {
          
        };


        GenerateInventoryOrderItemFields(item, older.VendorProduct.Id, older.WarehouseBin.Id, outputQuantity);

      }

    }

    private static void GenerateInventoryOrderItemFields(InventoryOrderItem item, int id1, int id2, decimal outputQuantity) {
      throw new NotImplementedException();
    }
  } // class InventoryOrderHelper

} // namespace Empiria.Trade.Inventory.Domain
