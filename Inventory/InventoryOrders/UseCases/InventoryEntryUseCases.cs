/* Empiria Operations ****************************************************************************************
*                                                                                                            *
*  Module   : Material Management                        Component : Domain Layer                            *
*  Assembly : Empiria.Inventory.Core.dll                 Pattern   : Partitioned Type / Information Holder   *
*  Type     : InventoryEntryUseCases                     License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases to manage inventory entry.                                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.Services;

using Empiria.Locations;
using Empiria.Products;
using Empiria.Trade.Core;
using Empiria.Trade.Inventory.Data;
using Empiria.Trade.Inventory.Adapters;


namespace Empiria.Trade.Inventory.UseCases {

  /// <summary>Use cases to manage inventory entry.</summary>
  public class InventoryEntryUseCases : UseCase {


    #region Constructors and parsers

    protected InventoryEntryUseCases() {
      // no-op
    }

    static public InventoryEntryUseCases UseCaseInteractor() {
      return UseCase.CreateInstance<InventoryEntryUseCases>();
    }

    #endregion Constructors and parsers

    #region Use cases

    public InventoryHolderDto CloseInventoryEntries(string orderUID) {
      Assertion.Require(orderUID, nameof(orderUID));

      InventoryOrder order = InventoryOrder.Parse(orderUID);

      FixedList<InventoryOrderItem> orderItems = order.GetItems<InventoryOrderItem>();

      InventoryUtility.EnsureIsValidToClose(orderItems);

      InventoryOrderData.UpdateEntriesStatusByOrder(order.Id, InventoryStatus.Cerrado);

      var inventoryOrderUseCase = InventoryOrderUseCases.UseCaseInteractor();

      return inventoryOrderUseCase.GetInventoryOrder(orderUID);
    }


    public InventoryHolderDto CreateInventoryEntry(string orderUID, string orderItemUID,
                                                   InventoryEntryFields fields) {
      Assertion.Require(orderUID, nameof(orderUID));
      Assertion.Require(orderItemUID, nameof(orderItemUID));
      Assertion.Require(fields, nameof(fields));

      Products.ProductEntry productEntry = InventoryOrderData.GetProductEntryByName(fields.Product.Trim());
      Location locationEntry = InventoryOrderData.GetLocationEntryByName(fields.Location.Trim());

      //TODO VERIFICAR ERROR EN REFERENCIA A VALIDACION
      //fields.EnsureIsValid(productEntry.Id, orderItemUID);
      fields.ProductUID = Product.Parse(productEntry.Id).UID;
      fields.LocationUID = locationEntry.UID;

      var inventoryEntry = new InventoryEntry(orderUID, orderItemUID);

      var order = InventoryOrder.Parse(orderUID);

      //TODO CAMBIAR ESTA VALIDACION DE InventoryType
      if (order.InventoryType.UID == "0eb5a072-b857-4071-8b06-57a34822ec64") {
        inventoryEntry.OutputEntry(fields);
      } else {
        inventoryEntry.AddEntry(fields);
      }

      inventoryEntry.Save();

      var inventoryOrderUseCase = InventoryOrderUseCases.UseCaseInteractor();
      return inventoryOrderUseCase.GetInventoryOrder(orderUID);
    }


    public InventoryHolderDto DeleteInventoryEntry(string orderUID, string itemUID, string entryUID) {
      Assertion.Require(orderUID, nameof(orderUID));
      Assertion.Require(itemUID, nameof(itemUID));
      Assertion.Require(entryUID, nameof(entryUID));

      InventoryOrder order = InventoryOrder.Parse(orderUID);
      InventoryOrderItem orderItem = InventoryOrderItem.Parse(itemUID);
      InventoryEntry entry = InventoryEntry.Parse(entryUID);

      Assertion.Require(order.Id == entry.Order.Id && orderItem.Order.Id == entry.Order.Id,
                        $"El registro de inventario no coincide con la orden!");

      InventoryOrderData.DeleteEntryStatus(order.Id, orderItem.Id,
                                           entry.Id, InventoryStatus.Deleted);

      var inventoryOrderUseCase = InventoryOrderUseCases.UseCaseInteractor();
      return inventoryOrderUseCase.GetInventoryOrder(orderUID);
    }


    public InventoryEntryDto GetInventoryEntryByUID(string inventoryEntryUID) {
      Assertion.Require(inventoryEntryUID, nameof(inventoryEntryUID));

      InventoryEntry entry = InventoryEntry.Parse(inventoryEntryUID);

      return InventoryOrderMapper.MapToInventoryEntryDto(entry);
    }


    public FixedList<InventoryEntryReportDto> GetInventoryEntryReport(int inventoryOrderId) {
      Assertion.Require(inventoryOrderId, nameof(inventoryOrderId));

      //var inventoryOrder = InventoryOrder.Parse(inventoryOrderId);

      var report = InventoryOrderData.GetProductEntryInventoryReport(inventoryOrderId);

      return InventoryEntriesReportMapper.Map(report);
    }


    public FixedList<InventoryEntryReportDto> GetInventoryEntryReportByLocation(string locationUID) {
      Assertion.Require(locationUID, nameof(locationUID));

      //var inventoryOrder = InventoryOrder.Parse(inventoryOrderUID);

      Location locationEntry = InventoryOrderData.GetLocationEntryByName(locationUID);

      var report = InventoryOrderData.GetProductEntryInventoryReportByLocation(locationEntry.Name);

      return InventoryEntriesReportMapper.Map(report);
    }

    #endregion Use cases

    #region Helpers

    public void OutputInventoryEntriesVW(InventoryOrder order) {

      foreach (var item in order.GetItems<InventoryOrderItem>()) {

        var inventoryEntry = new InventoryEntry(order, item);

        var price = InventoryOrderData.GetProductPriceFromVirtualWarehouse(item.Product.Id);
        inventoryEntry.OutputEntry(price);

        inventoryEntry.Save();
      }
    }

    #endregion Helpers

  } // class InventoryOrderUseCases

} // namespace Empiria.Inventory.UseCases
