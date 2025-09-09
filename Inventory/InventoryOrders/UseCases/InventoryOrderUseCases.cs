/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Use cases Layer                         *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Use case interactor class               *
*  Type     : InventoryOrderUseCases                     License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases used to build Inventory order.                                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System.Linq;
using Empiria.Services;
using Empiria.Trade.Core.Catalogues;
using Empiria.Trade.Core.Inventories.Adapters;
using Empiria.Trade.Inventory.Adapters;
using Empiria.Trade.Inventory.Data;
using Empiria.Trade.Inventory.Domain;

namespace Empiria.Trade.Inventory.UseCases {

  /// <summary>Use cases used to build Inventory order.</summary>
  public class InventoryOrderUseCases : UseCase {


    #region Constructors and parsers

    public InventoryOrderUseCases() {
      // no-op
    }

    static public InventoryOrderUseCases UseCaseInteractor() {
      return CreateInstance<InventoryOrderUseCases>();
    }


    #endregion Constructors and parsers

    #region Public methods



    public InventoryOrderDto CloseInventoryOrder(string inventoryOrderUID) {
      Assertion.Require(inventoryOrderUID, nameof(inventoryOrderUID));

      var inventoryOrderId = InventoryOrderEntry.Parse(inventoryOrderUID).Id;

      InventoryOrderData.CloseInventoryOrder(
        inventoryOrderId, InventoryStatus.Cerrado);

      var inventoryItems = InventoryOrderData.GetInventoryItemsByInventoryOrder(inventoryOrderId);

      foreach (var item in inventoryItems) {

        var inventoryStock = CataloguesUseCases.GetInventoryStockByClauses(
          item.VendorProduct.Id, item.WarehouseBin.Id);

        var realStock = inventoryStock.Sum(x => x.RealStock);

        decimal quantityDiff = item.CountingQuantity - realStock;

        InventoryOrderData.CloseInventoryItemForInventoryOrder(item.InventoryOrderItemId, quantityDiff);
      }

      return GetInventoryOrderByUID(inventoryOrderUID);
    }


    public InventoryOrderDto CreateInventoryOrder(InventoryOrderFields fields) {
      Assertion.Require(fields, nameof(fields));

      var builder = new InventoryOrderBuilder();
      var inventoryOrder = builder.CreateInventoryOrder(fields, "");
      return GetInventoryOrderByUID(inventoryOrder.InventoryOrderUID);
    }


    public void CreateInventoryOrderBySale(FixedList<InventoryItems> inventoryItems) {
      Assertion.Require(inventoryItems, nameof(inventoryItems));

      var builder = new InventoryOrderBuilder();

      var fields = builder.MapToInventoryOrderFields(inventoryItems.FirstOrDefault());

      InventoryOrderEntry inventoryOrder = builder.CreateInventoryOrder(fields, "");

      foreach (var inventoryItem in inventoryItems) {

        InventoryOrderItemFields itemFields =
          builder.MapToInventoryOrderItemFields(inventoryItem, inventoryOrder.InventoryOrderType.Id);

        builder.CreateInventoryOrderItem(inventoryOrder.InventoryOrderUID, itemFields);
      }

    }


    public InventoryOrderDto CreateInventoryOrderItem(string inventoryOrderUID,
      InventoryOrderItemFields fields) {
      Assertion.Require(inventoryOrderUID, nameof(inventoryOrderUID));
      Assertion.Require(fields, nameof(fields));

      fields.UID = InventoryOrderHelper.GetInventoryOrderItemUIDByVendorProductLocation(
        inventoryOrderUID, fields.VendorProductUID, fields.WarehouseBinUID);

      var builder = new InventoryOrderBuilder();
      builder.CreateInventoryOrderItem(inventoryOrderUID, fields);
      return GetInventoryOrderByUID(inventoryOrderUID);
    }


    public InventoryOrderItem GetInventoryOrderItemByUID(string itemUID) {

      return InventoryOrderItem.Parse(itemUID);
    }


    public void DeleteInventoryCountOrderByUID(string inventoryOrderUID) {
      Assertion.Require(inventoryOrderUID, nameof(inventoryOrderUID));

      InventoryOrderData.DeleteInventoryItemByOrderUID(inventoryOrderUID);
      InventoryOrderData.DeleteInventoryOrderByUID(inventoryOrderUID);
    }


    public InventoryOrderDto DeleteInventoryItemByOrderUID(string inventoryOrderUID) {
      Assertion.Require(inventoryOrderUID, nameof(inventoryOrderUID));

      InventoryOrderData.DeleteInventoryItemByOrderUID(inventoryOrderUID);
      return GetInventoryOrderByUID(inventoryOrderUID);
    }


    public InventoryOrderDto DeleteInventoryItemByUID(string inventoryOrderUID,
      string inventoryOrderItemUID) {
      Assertion.Require(inventoryOrderUID, nameof(inventoryOrderUID));
      Assertion.Require(inventoryOrderItemUID, nameof(inventoryOrderItemUID));

      InventoryOrderData.DeleteInventoryItemByUID(inventoryOrderItemUID);
      return GetInventoryOrderByUID(inventoryOrderUID);
    }


    internal InventoryOrderActions GetActions(InventoryOrderEntry inventoryOrder) {
      Assertion.Require(inventoryOrder, nameof(inventoryOrder));

      var builder = new InventoryOrderBuilder();
      InventoryOrderActions actions = builder.GetActions(inventoryOrder);

      return actions;
    }


    public InventoryOrderDto GetInventoryOrderByUID(string inventoryOrderUID) {
      Assertion.Require(inventoryOrderUID, nameof(inventoryOrderUID));

      var builder = new InventoryOrderBuilder();
      var inventoryOrder = builder.GetInventoryOrderByUID(inventoryOrderUID);

      if (inventoryOrder == null) {
        return new InventoryOrderDto();
      }

      InventoryOrderActions actions = GetActions(inventoryOrder);
      return InventoryOrderMapper.MapInventoryOrder(inventoryOrder, actions);
    }


    public InventoryOrderDataDto SearchInventoryOrders(InventoryOrderQuery query) {
      Assertion.Require(query, nameof(query));

      var clauses =
        InventoryOrderQueryClauses.CreateClausesForInventoryOrder(new InventoryQueryClauses(query));
      var list = InventoryOrderData.GetInventoryOrderList(clauses);

      return InventoryOrderMapper.MapList(list, query);
    }


    public InventoryOrderDto UpdateInventoryOrder(string inventoryOrderUID,
      InventoryOrderFields fields) {
      Assertion.Require(inventoryOrderUID, nameof(inventoryOrderUID));
      Assertion.Require(fields, nameof(fields));

      var builder = new InventoryOrderBuilder();
      builder.UpdateInventoryOrder(inventoryOrderUID, fields);

      return GetInventoryOrderByUID(inventoryOrderUID);
    }


    public void UpdateInventoryOrderForPicking(InventoryOrderFields fields) {
      Assertion.Require(fields, nameof(fields));

      var builder = new InventoryOrderBuilder();
      builder.UpdateInventoryOrderForPicking(fields);
    }


    internal void CloseInventoryOrderForSalesOrder(int inventoryOrderTypeId, int referenceId) {

      InventoryOrderData.CloseInventoryOrderForSalesOrder(inventoryOrderTypeId, referenceId);
    }


    internal void CloseInventoryOrderItemsForSales(
      int inventoryOrderTypeId, int referenceId) {

      var clauses = InventoryOrderQueryClauses.CreateClausesForInventoryOrder(
        new InventoryQueryClauses("", inventoryOrderTypeId, referenceId));

      InventoryOrderEntry inventoryOrder =
        InventoryOrderData.GetInventoryOrderList(clauses).FirstOrDefault();

      InventoryOrderData.CloseInventoryOrderItemsForSales(inventoryOrder.InventoryOrderId);
    }


    #endregion Public methods

    #region Private methods



    #endregion Private methods

  } // class InventoryOrderUseCases

} // namespace Empiria.Trade.Inventory.UseCases
