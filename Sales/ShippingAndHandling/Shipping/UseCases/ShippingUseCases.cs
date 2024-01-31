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


    public FixedList<INamedEntity> GetParcelSupplierList() {

      var catalogueUsecase = CataloguesUseCases.UseCaseInteractor();

      FixedList<INamedEntity> sut = catalogueUsecase.GetParcelSupplierList();

      return catalogueUsecase.GetParcelSupplierList();
    }


    public ShippingDto CreateShippingOrder(ShippingFields fields) {

      return CreateOrUpdateShippingOrder(fields);
    }


    public ShippingDto UpdateShippingOrder(string shippingOrderUID, ShippingFields fields) {

      fields.ShippingData.ShippingUID = shippingOrderUID;

      return CreateOrUpdateShippingOrder(fields);
    }


    public ShippingEntry GetShippingByUID(string shippingUID) {

      return ShippingEntry.Parse(shippingUID);
    }


    public ShippingOrderItem GetShippingOrderItemByUID(string shippingUID) {

      return ShippingOrderItem.Parse(shippingUID);
    }


    public ShippingEntryDto GetShippingByOrderUID(string orderUID) {

      var builder = new ShippingBuilder();

      ShippingEntry entry = builder.GetShippingByOrderUID(orderUID);

      return ShippingMapper.Map(entry);
    }


    public ShippingDto GetShippingOrderByQuery(ShippingQuery query) {

      return GetShippingByOrders(query.Orders);
    }


    #endregion Use cases


    #region Private methods

    private ShippingDto CreateOrUpdateShippingOrder(ShippingFields fields) {

      var builder = new ShippingBuilder();

      ShippingEntry shippingOrder = builder.CreateOrUpdateShipping(fields);

      return GetShippingByOrders(fields.Orders);
    }


    private ShippingDto GetShippingByOrders(string[] orders) {
      var builder = new ShippingBuilder();
      ShippingEntry entry = new ShippingEntry();
      entry = builder.GetShippingByOrders(orders);

      return ShippingMapper.MapShippingForParcelDelivery(entry);
    }


    //private ShippingEntryDto GetShipping(string orderUID) {

    //  var builder = new ShippingBuilder();

    //  ShippingEntry entry = builder.GetShippingByOrderUID(orderUID);

    //  return ShippingMapper.Map(entry);

    //}



    #endregion Private methods

  } // class ShippingUseCases

} // namespace Empiria.Trade.ShippingAndHandling.UseCases
