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


    public ShippingEntryDto CreateShippingOrder(string orderUID, ShippingFields fields) {

      var shippingOrder = new ShippingEntry(orderUID, fields);

      shippingOrder.Save();

      return GetShipping(orderUID);
    }


    public ShippingEntry GetShippingByUID(string shippingUID) {

      return ShippingEntry.Parse(shippingUID);
    }


    public ShippingEntryDto GetShippingByOrderUID(string orderUID) {

      return GetShipping(orderUID);
    }


    public ShippingDto GetCompleteShippingByOrderUIDList(ShippingQuery query) {
      var builder = new ShippingBuilder();

      ShippingEntry entry = builder.GetCompleteShippingByOrderUIDList(query);

      return ShippingMapper.MapShipping(entry);
    }


    #endregion Use cases


    #region Private methods


    private ShippingEntryDto GetShipping(string orderUID) {

      var builder = new ShippingBuilder();

      ShippingEntry entry = builder.GetShippingByOrderUID(orderUID);

      return ShippingMapper.Map(entry);

    }


    #endregion Private methods

  } // class ShippingUseCases

} // namespace Empiria.Trade.ShippingAndHandling.UseCases
