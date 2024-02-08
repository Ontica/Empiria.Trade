/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Shipping Management                        Component : Use cases Layer                         *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Use case interactor class               *
*  Type     : ShippingUseCases                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases used to build shipping.                                                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Services;
using Empiria.Trade.Sales.ShippingAndHandling.Domain;
using Empiria.Trade.Sales.ShippingAndHandling.Adapters;
using Empiria.Trade.Core.Catalogues;
using Empiria.Trade.Sales.ShippingAndHandling.Data;

namespace Empiria.Trade.Sales.ShippingAndHandling.UseCases {

  /// <summary>Use cases used to build shipping.</summary>
  public class ShippingUseCases : UseCase {

    #region Constructors and parsers

    protected ShippingUseCases() {
      // no-op
    }

    static public ShippingUseCases UseCaseInteractor() {
      return CreateInstance<ShippingUseCases>();
    }


    #endregion Constructors and parsers


    #region Use cases


    public ShippingDto CreateOrderForShipping(string shippingOrderUID, string orderUID) {

      var builder = new ShippingBuilder();
      string[] orders = new string[] { orderUID};

      builder.CreateOrUpdateOrderForShipping(shippingOrderUID, orders);

      ShippingEntry shippingEntry = builder.GetShippingByUID(shippingOrderUID);

      return ShippingMapper.MapShippingForParcelDelivery(shippingEntry);
    }


    public ShippingDto CreateShippingOrder(ShippingFields fields) {

      var builder = new ShippingBuilder();

      ShippingEntry shippingOrder = builder.CreateOrUpdateShipping(fields);

      builder.CreateOrUpdateOrderForShipping(shippingOrder.ShippingUID, fields.Orders);

      return ShippingMapper.MapShippingForParcelDelivery(shippingOrder);
    }


    public ShippingDto DeleteShippingOrderItem(string shippingOrderUID, string orderUID) {

      ShippingData.DeleteShippingOrderItem(orderUID);

      var builder = new ShippingBuilder();

      ShippingEntry shippingEntry = builder.GetShippingByUID(shippingOrderUID);

      return ShippingMapper.MapShippingForParcelDelivery(shippingEntry);
    }


    public FixedList<INamedEntity> GetParcelSupplierList() {

      var catalogueUsecase = CataloguesUseCases.UseCaseInteractor();

      return catalogueUsecase.GetParcelSupplierList();
    }


    public FixedList<ShippingEntryDto> GetShippingsList(ShippingQuery query) {

      var builder = new ShippingBuilder();

      FixedList<ShippingEntry> entries = builder.GetShippingList(query);

      return ShippingMapper.MapShippings(entries);
    }


    public ShippingEntryDto GetShippingByOrderUID(string orderUID) {

      var builder = new ShippingBuilder();

      ShippingEntry entry = builder.GetShippingByOrderUID(orderUID);

      return ShippingMapper.Map(entry);
    }


    public ShippingEntry GetShippingByUID(string shippingUID) {

      return ShippingEntry.Parse(shippingUID);
    }


    public ShippingOrderItem GetShippingOrderItemByUID(string shippingUID) {

      return ShippingOrderItem.Parse(shippingUID);
    }


    public ShippingDto GetShippingOrderByQuery(ShippingFieldsQuery query) {
      var builder = new ShippingBuilder();

      FixedList<ShippingOrderItem> orderForShippingList = builder.GetOrdersForShipping(query.Orders);

      ShippingEntry shipping = builder.GetShippingWithOrders(orderForShippingList);

      return ShippingMapper.MapShippingForParcelDelivery(shipping);
    }


    public ShippingDto UpdateShippingOrder(string shippingOrderUID, ShippingFields fields) {

      fields.ShippingData.ShippingUID = shippingOrderUID;
      
      var builder = new ShippingBuilder();

      ShippingEntry shippingOrder = builder.CreateOrUpdateShipping(fields);

      return ShippingMapper.MapShippingForParcelDelivery(shippingOrder);
    }


    #endregion Use cases


    #region Private methods


    private ShippingDto CreateOrUpdateShippingOrder(ShippingFields fields) {

      var builder = new ShippingBuilder();

      ShippingEntry shippingOrder = builder.CreateOrUpdateShipping(fields);

      return ShippingMapper.MapShippingForParcelDelivery(shippingOrder);
    }


    #endregion Private methods

  } // class ShippingUseCases

} // namespace Empiria.Trade.ShippingAndHandling.UseCases
