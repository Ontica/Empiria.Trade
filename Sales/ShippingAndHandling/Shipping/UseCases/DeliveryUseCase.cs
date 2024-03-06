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
using Empiria.Services;
using Empiria.Trade.Sales.ShippingAndHandling.Adapters;
using Empiria.Trade.Sales.ShippingAndHandling.Domain;

namespace Empiria.Trade.Sales.ShippingAndHandling.UseCases {

  /// <summary>Use cases used to build delivery.</summary>
  internal class DeliveryUseCase : UseCase {


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

      //TODO QUITAR CUANDO SE EMPIECE A MANDAR STATUS DESDE FRONT
      query.Status = ShippingStatus.Close;
      FixedList<ShippingEntry> entries = shippingBuilder.GetShippingList(query);

      return ShippingMapper.MapShippings(entries);
    }


    #endregion Public methods


  } // class DeliveryUseCase

} // namespace Empiria.Trade.Sales.ShippingAndHandling.UseCases
