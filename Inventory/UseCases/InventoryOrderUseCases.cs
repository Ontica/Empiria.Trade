/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Use cases Layer                         *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Use case interactor class               *
*  Type     : InventoryOrderUseCases                     License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases used to build Inventory order.                                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Linq;
using System.Reflection;
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


    public InventoryOrderItem GetInventoryOrderItemByUID(string itemUID) {

      return InventoryOrderItem.Parse(itemUID);
    }


    public void CreateInventoryOrderBySale(FixedList<InventoryItems> inventoryItems) {
      Assertion.Require(inventoryItems, nameof(inventoryItems));

      var builder = new InventoryOrderBuilder();

      var fields = builder.MapToInventoryOrderFields(inventoryItems.FirstOrDefault());

      InventoryOrderEntry inventoryOrder = builder.CreateInventoryOrder(fields, "");

      foreach (var inventoryItem in inventoryItems) {

        InventoryOrderItemFields itemFields = builder.MapToInventoryOrderItemFields(inventoryItem);

        builder.CreateInventoryOrderItem(inventoryOrder.InventoryOrderUID, itemFields);
      }

    }


    public InventoryOrderDto CloseInventoryOrderStatus(string inventoryOrderUID) {
      Assertion.Require(inventoryOrderUID, nameof(inventoryOrderUID));

      InventoryOrderData.UpdateInventoryOrderStatus(
        inventoryOrderUID, DateTime.Now, InventoryStatus.Cerrado);

      var inventoryOrderItems = InventoryOrderData.GetInventoryItemsByOrderUID(inventoryOrderUID);

      foreach (var item in inventoryOrderItems) {

        var inventoryStock = CataloguesUseCases.GetInventoryStockByVendorProduct(item.VendorProduct.Id, "");

        decimal quantityDifference = item.CountingQuantity - inventoryStock.Sum(x => x.RealStock);

        InventoryOrderData.UpdateInventoryOrderItemsStatusByOrder(
                    item.InventoryOrderItemId, quantityDifference, DateTime.Now);
      }

      return GetInventoryOrderByUID(inventoryOrderUID);
    }


    public InventoryOrderDto CreateInventoryOrder(InventoryOrderFields fields) {
      Assertion.Require(fields, nameof(fields));

      var builder = new InventoryOrderBuilder();
      var inventoryOrder = builder.CreateInventoryOrder(fields, "");
      return GetInventoryOrderByUID(inventoryOrder.InventoryOrderUID);
    }


    public InventoryOrderDto CreateInventoryOrderItem(string inventoryOrderUID,
      InventoryOrderItemFields fields) {
      Assertion.Require(inventoryOrderUID, nameof(inventoryOrderUID));
      Assertion.Require(fields, nameof(fields));

      var builder = new InventoryOrderBuilder();
      builder.CreateInventoryOrderItem(inventoryOrderUID, fields);
      return GetInventoryOrderByUID(inventoryOrderUID);
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
      InventoryOrderActions actions = GetActions(inventoryOrder);

      return InventoryOrderMapper.MapInventoryOrder(inventoryOrder, actions);
    }


    public InventoryOrderDataDto GetInventoryCountOrderList(InventoryOrderQuery query) {
      Assertion.Require(query, nameof(query));

      var list = InventoryOrderData.GetInventoryOrderList(query);
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



    internal void UpdateInventoryOrderByTypeAndReferenceId(int inventoryOrderTypeId, int referenceId) {

      InventoryOrderData.UpdateInventoryOrderByTypeAndReferenceId(inventoryOrderTypeId, referenceId);
    }


    internal void UpdateInventoryOrderItemsByTypeAndReferenceId(
      int inventoryOrderTypeId, int referenceId) {

      InventoryOrderEntry inventoryOrder =
        InventoryOrderData.GetInventoryOrderByTypeAndReferenceId(inventoryOrderTypeId, referenceId);

      InventoryOrderData.UpdateInventoryOrderItemsByOrder(inventoryOrder.InventoryOrderId, DateTime.Now);
    }


    #endregion Public methods

  } // class InventoryOrderUseCases

} // namespace Empiria.Trade.Inventory.UseCases
