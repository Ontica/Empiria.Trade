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
using Empiria.Locations;
using Empiria.Services;
using Empiria.StateEnums;

using Empiria.Orders;
using Empiria.Trade.Core;
using Empiria.Trade.Products;
using Empiria.Trade.Inventory.Data;
using Empiria.Trade.Inventory.Adapters;

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


    #region Public methods

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


    public InventoryHolderDto CreateInventoryOrder(InventoryOrderFields fields) {
      Assertion.Require(fields, nameof(fields));

      var orderType = OrderType.InventoryOrder;
      fields.Priority = Priority.Normal;

      InventoryOrder order = new InventoryOrder(fields.WarehouseUID, orderType);

      order.Update(fields);

      order.Save();

      return GetInventoryOrder(order.UID);
    }


    public InventoryHolderDto CreateInventoryOrderItem(string orderUID, InventoryOrderItemFields fields) {
      Assertion.Require(orderUID, nameof(orderUID));
      Assertion.Require(fields, nameof(fields));

      var order = InventoryOrder.Parse(orderUID);

      var location = CommonStorage.TryParseNamedKey<Location>(fields.Location);

      var product = ProductEntry.TryParseWithCode(fields.Product);
      var ifNotExistProductinLocation = VerifyProductAndLocationInOrder(order.Id, product.Id, location.Id);

      Assertion.Require(location, $"La ubicacion {fields.Location} no existe.");
      Assertion.Require(order.Warehouse == GetRootLocation(location),
                 $"La localización {fields.Location} no existe en el almacen {order.Warehouse.Name}");

      Assertion.Require(product, $"El producto con clave {fields.Product} no existe.");

      Assertion.Require(ifNotExistProductinLocation, $"El producto {product.Name} ya está registrado " +
                                                     $"en la localización {fields.Location}.");

      var maxOrderItem = InventoryData.SearchMaxOrderItemPosition(order);

      fields.Position = maxOrderItem.Count > 0 ? maxOrderItem.First().Position + 1 : 1;
      fields.ProductUID = product.UID;
      fields.Description = product.Description;
      fields.ProductUnitUID = product.BaseUnit.UID;
      fields.LocationUID = fields.LocationUID == string.Empty ? location.UID : fields.LocationUID;

      var orderItemType = OrderItemType.InventoryOrderItemType;

      InventoryOrderItem orderItem = new InventoryOrderItem(orderItemType, order, location);

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

      var item = order.GetItem<InventoryOrderItem>(orderItemUID);

      order.RemoveItem(item);
      item.Save();

      InventoryOrderData.DeleteEntry(order.Id, item.Id);

      return GetInventoryOrder(orderUID);
    }


    public InventoryHolderDto UpdateInventoryOrder(string orderUID, InventoryOrderFields fields) {
      Assertion.Require(orderUID, nameof(orderUID));
      Assertion.Require(fields, nameof(fields));

      fields.EnsureValid();

      var order = InventoryOrder.Parse(orderUID);
      order.Update(fields);

      order.Save();

      return GetInventoryOrder(order.UID);
    }


    public InventoryHolderDto UpdateInventoryOrderItemQuantity(string orderUID, string orderItemUID,
                                               InventoryOrderItemFields fields) {
      var order = InventoryOrder.Parse(orderUID);

      var item = order.GetItem<InventoryOrderItem>(orderItemUID);

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


    private void AddInventoryEntry(InventoryOrder order, InventoryOrderItem orderItem) {

      var inventoryEntryType = InventoryEntryType.InventoryEntryItemType;

      var inventoryEntry = new InventoryEntry(inventoryEntryType,
                                              order.UID, orderItem.UID);

      inventoryEntry.InitialEntry(orderItem.UnitPrice, orderItem.Location);

      inventoryEntry.Save();
    }


    public void OutputInventoryEntriesVW(InventoryOrder order) {

      foreach (var item in order.GetItems<InventoryOrderItem>()) {

        var inventoryEntry = new InventoryEntry(InventoryEntryType.InventoryEntryItemType, order, item);

        var price = InventoryOrderData.GetProductPriceFromVirtualWarehouse(item.Product.Id);
        inventoryEntry.OutputEntry(price);

        inventoryEntry.Save();
      }
    }

    #endregion Public methods

  } // class InventoryOrderUseCases

} // namespace Empiria.Trade.Inventory.UseCases
