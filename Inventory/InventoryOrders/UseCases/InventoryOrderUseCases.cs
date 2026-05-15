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
using Empiria.Inventory.UseCases;
using Empiria.Locations;
using Empiria.Services;
using Empiria.StateEnums;

using Empiria.Trade.Core;
using Empiria.Trade.Core.Catalogues;
using Empiria.Trade.Core.Inventories.Adapters;
using Empiria.Trade.Inventory.Adapters;
using Empiria.Trade.Inventory.Data;
using Empiria.Trade.Inventory.Domain;

namespace Empiria.Trade.Inventory.UseCases {

  /// <summary>Use cases used to build Inventory order.</summary>
  public class InventoryOrderUseCases : UseCase {


    #region Constructors and parsers

    private const string INVENTORYORDERTYPE = "ObjectTypeInfo.Order.InventoryOrder";

    public InventoryOrderUseCases() {
      // no-op
    }

    static public InventoryOrderUseCases UseCaseInteractor() {
      return CreateInstance<InventoryOrderUseCases>();
    }


    #endregion Constructors and parsers


    #region Public methods V2

    public InventoryHolderDto GetInventoryOrder(string orderUID) {
      Assertion.Require(orderUID, nameof(orderUID));

      InventoryOrder inventoryOrder = InventoryUtility.GetInventoryOrder(orderUID);

      InventoryOrderActions actions = InventoryUtility.GetActions(inventoryOrder);

      return InventoryOrderMapper.MapToHolderDto(inventoryOrder, actions);
    }


    public InventoryOrderDataDto SearchInventoryOrder(InventoryOrderQuery query) {
      Assertion.Require(query, nameof(query));

      var filter = query.MapToFilterString();
      var sort = query.MapToSortString();

      FixedList<InventoryOrder> orders = InventoryOrderData.SearchInventoryOrders(filter, sort);

      return InventoryOrderMapper.InventoryOrderDataDto(orders, query);
    }


    public FixedList<NamedEntityDto> GetOrderTypes() {
      return InventoryType.GetList().MapToNamedEntityList();
    }


    public FixedList<NamedEntityDto> GetWarehouses() {
      return CommonStorage.GetList<Location>().FindAll(x =>
                              x.Level == 1 && x.GetStatus<EntityStatus>() != EntityStatus.Deleted)
                          .MapToNamedEntityList();
    }


    public FixedList<NamedEntityDto> GetPartiesByRol(string rol) {
      return Parties.Party.GetPartiesInRole(rol).MapToNamedEntityList();
    }


    public InventoryHolderDto CreateInventoryOrder(string warehouseUID, Core.InventoryOrderFields fields) {
      Assertion.Require(warehouseUID, nameof(warehouseUID));
      Assertion.Require(fields, nameof(fields));

      var orderType = Empiria.Orders.OrderType.Parse(INVENTORYORDERTYPE);
      fields.Priority = Priority.Normal;

      InventoryOrder order = new InventoryOrder(warehouseUID, orderType);

      order.Update(fields);

      order.Save();

      return GetInventoryOrder(order.UID);
    }


    public InventoryHolderDto CreateInventoryOrderItem(string orderUID, Core.InventoryOrderItemFields fields) {
      Assertion.Require(orderUID, nameof(orderUID));
      Assertion.Require(fields, nameof(fields));

      var order = InventoryOrder.Parse(orderUID);

      var orderItemType = Empiria.Orders.OrderItemType.Parse("ObjectTypeInfo.OrderItem.InventoryOrderItem");

      var location = CommonStorage.TryParseNamedKey<Location>(fields.Location);
      var product = Empiria.Trade.Products.Product.TryParseWithCode(fields.Product);
      var ifNotExistProductinLocation = VerifyProductAndLocationInOrder(order.Id, product.Id, location.Id);

      Assertion.Require(location, $"La ubicacion {fields.Location} no existe.");
      Assertion.Require(order.Warehouse == GetRootLocation(location),
                 $"La localización {fields.Location} no existe en el almacen {order.Warehouse.Name}");

      Assertion.Require(product, $"El producto con clave {fields.Product} no existe.");

      Assertion.Require(ifNotExistProductinLocation, $"El producto {product.Name} ya está registrado " +
                                                     $"en la localización {fields.Location}.");

      var maxOrderItem = InventoryOrderData.SearchMaxOrderItemPosition(order);

      fields.Position = maxOrderItem.Count > 0 ? maxOrderItem.First().Position + 1 : 1;
      fields.ProductUID = product.UID;
      fields.Description = product.Description;
      fields.ProductUnitUID = product.BaseUnit.UID;
      fields.LocationUID = fields.LocationUID == string.Empty ? location.UID : fields.LocationUID;

      Core.InventoryOrderItem orderItem = new Core.InventoryOrderItem(orderItemType, order, location);

      orderItem.Update(fields);
      orderItem.Save();
      order.AddItem(orderItem);

      AddInventoryEntry(order, orderItem);

      return GetInventoryOrder(order.UID);
    }


    public InventoryHolderDto CloseInventoryOrder(string orderUID) {
      Assertion.Require(orderUID, nameof(orderUID));

      InventoryOrder order = InventoryOrder.Parse(orderUID);

      order.Close(Parties.Party.ParseWithContact(ExecutionServer.CurrentContact));
      order.Save();

      order.CloseItems();

      OutputInventoryEntriesVW(order);

      var inventoryEntryUseCase = InventoryEntryUseCases.UseCaseInteractor();
      inventoryEntryUseCase.CloseInventoryEntries(order.UID);

      return GetInventoryOrder(order.UID);
    }


    public void DeleteInventoryOrder(string orderUID) {
      Assertion.Require(orderUID, nameof(orderUID));

      InventoryOrder order = InventoryOrder.Parse(orderUID);

      order.Delete();
      order.Save();
    }


    public InventoryHolderDto DeleteInventoryOrderItem(string orderUID, string orderItemUID) {
      Assertion.Require(orderUID, nameof(orderUID));
      Assertion.Require(orderItemUID, nameof(orderItemUID));

      InventoryOrder order = InventoryOrder.Parse(orderUID);

      var item = order.GetItem<Core.InventoryOrderItem>(orderItemUID);

      order.RemoveItem(item);
      item.Save();

      InventoryOrderData.DeleteEntry(order.Id, item.Id);

      return GetInventoryOrder(orderUID);
    }


    public InventoryHolderDto UpdateInventoryOrder(string orderUID, Core.InventoryOrderFields fields) {
      Assertion.Require(orderUID, nameof(orderUID));
      Assertion.Require(fields, nameof(fields));

      fields.EnsureValid();

      var order = InventoryOrder.Parse(orderUID);
      order.Update(fields);

      order.Save();

      return GetInventoryOrder(order.UID);
    }


    public InventoryHolderDto UpdateInventoryOrderItemQuantity(string orderUID, string orderItemUID,
                                               Core.InventoryOrderItemFields fields) {
      var order = InventoryOrder.Parse(orderUID);

      var item = order.GetItem<Core.InventoryOrderItem>(orderItemUID);

      item.UpdateQuantity(fields.Quantity);

      item.Save();

      var inventoryEntry = InventoryEntry.TryParseWithOrderItemId(item.Id);

      inventoryEntry.UpdateCountingQuantity(fields.Quantity);

      inventoryEntry.Save();

      return GetInventoryOrder(order.UID);
    }


    private Location GetRootLocation(Location location) {
      var current = location;
      while (!current.IsRoot) {
        var parent = current.GetParent<Location>();
        current = parent;
      }

      return current;
    }


    private bool VerifyProductAndLocationInOrder(int orderId, int productID, int locationID) {
      if (InventoryOrderData.VerifyProductAndLocationInOrder(orderId, productID, locationID) != 0) {
        return false;
      }
      return true;
    }


    private void AddInventoryEntry(InventoryOrder order, Core.InventoryOrderItem orderItem) {
      var inventoryEntry = new InventoryEntry(order.UID, orderItem.UID);

      inventoryEntry.InitialEntry(orderItem.UnitPrice, orderItem.Location);

      inventoryEntry.Save();
    }


    public void OutputInventoryEntriesVW(InventoryOrder order) {

      foreach (var item in order.GetItems<Core.InventoryOrderItem>()) {

        var inventoryEntry = new InventoryEntry(order, item);

        var price = InventoryOrderData.GetProductPriceFromVirtualWarehouse(item.Product.Id);
        inventoryEntry.OutputEntry(price);

        inventoryEntry.Save();
      }
    }

    #endregion Public methods V2


    #region Public methods



    public InventoryOrderDto CloseInventoryOrderV1(string inventoryOrderUID) {
      Assertion.Require(inventoryOrderUID, nameof(inventoryOrderUID));

      var inventoryOrderId = InventoryOrderEntry.Parse(inventoryOrderUID).Id;

      InventoryOrderData.CloseInventoryOrder(
        inventoryOrderId, EntityStatus.Closed);

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


    public InventoryOrderDto CreateInventoryOrderV1(
                              Empiria.Trade.Inventory.Adapters.InventoryOrderFields fields) {
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

        Empiria.Trade.Inventory.Adapters.InventoryOrderItemFields itemFields =
          builder.MapToInventoryOrderItemFields(inventoryItem, inventoryOrder.InventoryOrderType.Id);

        builder.CreateInventoryOrderItem(inventoryOrder.InventoryOrderUID, itemFields);
      }

    }


    public InventoryOrderDto CreateInventoryOrderItemV1(string inventoryOrderUID,
      Empiria.Trade.Inventory.Adapters.InventoryOrderItemFields fields) {
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


    public InventoryOrderDto UpdateInventoryOrderV1(string inventoryOrderUID,
      Empiria.Trade.Inventory.Adapters.InventoryOrderFields fields) {
      Assertion.Require(inventoryOrderUID, nameof(inventoryOrderUID));
      Assertion.Require(fields, nameof(fields));

      var builder = new InventoryOrderBuilder();
      builder.UpdateInventoryOrder(inventoryOrderUID, fields);

      return GetInventoryOrderByUID(inventoryOrderUID);
    }


    public void UpdateInventoryOrderForPicking(Empiria.Trade.Inventory.Adapters.InventoryOrderFields fields) {
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
