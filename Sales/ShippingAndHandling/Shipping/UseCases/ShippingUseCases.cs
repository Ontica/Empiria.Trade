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


    public ShippingDto CreateShippingOrder(ShippingFields fields) {

      return CreateOrUpdateShippingOrder(fields);
    }


    public ShippingDto DeleteShippingOrderItem(string shippingOrderUID, string shippingOrderItemUID) {

      ShippingData.DeleteShippingOrderItem(shippingOrderItemUID);

      var builder = new ShippingBuilder();

      ShippingEntry shippingEntry = builder.GetShippingByUID(shippingOrderUID);

      return ShippingMapper.MapShippingForParcelDelivery(shippingEntry);
    }


    public FixedList<INamedEntity> GetParcelSupplierList() {

      var catalogueUsecase = CataloguesUseCases.UseCaseInteractor();

      return catalogueUsecase.GetParcelSupplierList();
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


    public ShippingDto GetShippingOrderByQuery(ShippingQuery query) {

      return GetShippingByOrders(query.Orders);
    }


    public ShippingDto UpdateShippingOrder(string shippingOrderUID, ShippingFields fields) {

      fields.ShippingData.ShippingUID = shippingOrderUID;

      return CreateOrUpdateShippingOrder(fields);
    }


    #endregion Use cases


    #region Private methods


    private ShippingDto CreateOrUpdateShippingOrder(ShippingFields fields) {

      var builder = new ShippingBuilder();

      ShippingEntry shippingOrder = builder.CreateOrUpdateShipping(fields);

      return ShippingMapper.MapShippingForParcelDelivery(shippingOrder);
    }


    private ShippingDto GetShippingByOrders(string[] orders) {
      
      var builder = new ShippingBuilder();
      
      ShippingEntry entry = builder.GetShippingByOrders(orders);

      return ShippingMapper.MapShippingForParcelDelivery(entry);
    }


    #endregion Private methods

  } // class ShippingUseCases

} // namespace Empiria.Trade.ShippingAndHandling.UseCases
