/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Delivery Management                        Component : Use cases Layer                         *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Use case interactor class               *
*  Type     : DeliveryUseCase                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases used to build delivery.                                                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Linq;
using Empiria.Services;
using Empiria.Trade.Inventory.UseCases;
using Empiria.Trade.Sales.ShippingAndHandling.Adapters;
using Empiria.Trade.Sales.ShippingAndHandling.Data;
using Empiria.Trade.Sales.ShippingAndHandling.Domain;
using Empiria.Trade.Sales.UseCases;

namespace Empiria.Trade.Sales.ShippingAndHandling.UseCases {

  /// <summary>Use cases used to build delivery.</summary>
  public class DeliveryUseCase : UseCase {


    #region Constructors and parsers

    protected DeliveryUseCase() {
      // no-op
    }

    static public DeliveryUseCase UseCaseInteractor() {
      return CreateInstance<DeliveryUseCase>();
    }


    #endregion Constructors and parsers


    #region Public methods


    public FixedList<ShippingEntryDto> GetShippingsForDelivery(ShippingQuery query) {
      Assertion.Require(query, nameof(query));

      var shippingBuilder = new ShippingBuilder();

      FixedList<ShippingEntry> entries = shippingBuilder.GetShippingList(query);

      return ShippingMapper.MapShippings(entries);
    }


    public ShippingDto UpdateShippingStatus(string shippingOrderUID) {
      Assertion.Require(shippingOrderUID, nameof(shippingOrderUID));

      var builder = new ShippingBuilder();

      var orders = builder.GetOrdersUIDList(shippingOrderUID);

      SalesOrderUseCases orderUseCases = new SalesOrderUseCases();

      orderUseCases.ChangeOrdersToCloseStatus(orders);

      ShippingData.UpdateShippingStatus(shippingOrderUID, ShippingStatus.Cerrado);

      CloseInventoryOrdersForSales(orders);
     
      var shippingUseCase = new ShippingUseCases();

      var shipping = shippingUseCase.GetShippingByUID(shippingOrderUID, ShippingQueryType.Delivery);

      return shipping;
    }


    private void CloseInventoryOrdersForSales(string[] orders) {

      InventoryOrderUseCases inventoryUseCases = new InventoryOrderUseCases();
      foreach (var orderUID in orders) {

        int orderId = SalesOrder.Parse(orderUID).Id;

        inventoryUseCases.CloseInventoryOrderForSalesOrder(504, orderId);

        CloseInventoryOrderItemsForSales(504, orderId);
      }
    }

    
    private void CloseInventoryOrderItemsForSales(int inventoryOrderTypeId, int orderId) {

      InventoryOrderUseCases inventoryUseCases = new InventoryOrderUseCases();

      inventoryUseCases.CloseInventoryOrderItemsForSales(
        inventoryOrderTypeId, orderId);
    }


    #endregion Public methods


  } // class DeliveryUseCase

} // namespace Empiria.Trade.Sales.ShippingAndHandling.UseCases
