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
using Empiria.Trade.ShippingAndHandling.Adapters;
using Empiria.Trade.ShippingAndHandling.Domain;

namespace Empiria.Trade.ShippingAndHandling.UseCases {

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

      var builder = new ShippingBuilder();

      return builder.GetParcelSupplierList();
    }


    public ShippingEntryDto CreateShippingOrder(string orderUID, ShippingFields fields) {

      var shippingOrder = new ShippingEntry(orderUID, fields);

      shippingOrder.Save();

      return GetShipping(orderUID);
    }


    public ShippingEntryDto GetShippingOrder(string orderUID) {

      return GetShipping(orderUID);
    }


    #endregion Use cases


    #region Private methods


    private ShippingEntryDto GetShipping(string orderUID) {

      var builder = new ShippingBuilder();

      ShippingEntry entry = builder.GetShippingForOrder(orderUID);

      return ShippingMapper.Map(entry);

    }


    #endregion Private methods

  } // class ShippingUseCases

} // namespace Empiria.Trade.ShippingAndHandling.UseCases
