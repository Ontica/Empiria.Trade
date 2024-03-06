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
using Empiria.Trade.Orders;
using Empiria.Trade.Sales.UseCases;
using System.Reflection;

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


    public ShippingDto ChangeOrdersForShippingStatus(string shippingOrderUID) {
      Assertion.Require(shippingOrderUID, nameof(shippingOrderUID));

      var builder = new ShippingBuilder();

      var orders = builder.GetOrdersUIDList(shippingOrderUID);

      SalesOrderUseCases orderUseCases = new SalesOrderUseCases();

      orderUseCases.ChangeOrdersToDeliveryStatus(orders);

      return GetShippingByUID(shippingOrderUID);
    }


    public ShippingDto CreateOrderForShipping(string shippingOrderUID, string orderUID) {
      Assertion.Require(shippingOrderUID, nameof(shippingOrderUID));
      Assertion.Require(orderUID, nameof(orderUID));

      var helper = new ShippingHelper();
      helper.ValidateIfExistOrderForShipping(orderUID);

      string[] orders = new string[] { orderUID };

      helper.ValidateOrdersByCustomer(shippingOrderUID, orderUID);
      
      var builder = new ShippingBuilder();
      
      builder.CreateOrdersForShipping(shippingOrderUID, orders);

      return GetShippingByUID(shippingOrderUID);
    }


    public ShippingDto CreateShippingOrder(ShippingFields fields) {
      Assertion.Require(fields, nameof(fields));

      var builder = new ShippingBuilder();

      ShippingEntry shippingOrder = builder.CreateShippingOrder(fields);

      return GetShippingByUID(shippingOrder.ShippingUID);
    }


    public ShippingDto CreateShippingPallet(string shippingUID, ShippingPalletFields fields) {
      Assertion.Require(shippingUID, nameof(shippingUID));
      Assertion.Require(fields, nameof(fields));

      var builder = new ShippingBuilder();

      builder.CreateShippingPallet(shippingUID, fields);

      return GetShippingByUID(shippingUID);
    }


    public ShippingDto DeleteOrderForShipping(string shippingOrderUID, string orderUID) {
      Assertion.Require(shippingOrderUID, "shippingOrderUID");
      Assertion.Require(orderUID, "orderUID");

      ShippingData.DeleteOrderForShipping(orderUID);

      return GetShippingByUID(shippingOrderUID);
    }
    

    public void DeleteShipping(string shippingOrderUID) {
      Assertion.Require(shippingOrderUID, "shippingOrderUID");

      var builder = new ShippingBuilder();

      builder.DeleteShipping(shippingOrderUID);
    }


    public ShippingDto DeleteShippingPalletByUID(string shippingUID, string shippingPalletUID) {
      Assertion.Require(shippingUID, nameof(shippingUID));
      Assertion.Require(shippingPalletUID, nameof(shippingPalletUID));

      ShippingData.DeleteShippingPackageByPalletUID(shippingPalletUID);

      ShippingData.DeleteShippingPalletByUID(shippingPalletUID);

      return GetShippingByUID(shippingUID);
    }


    public ShippingOrderItem GetOrdersForShippingByUID(string orderForShippingUID) {
      Assertion.Require(orderForShippingUID, nameof(orderForShippingUID));
      return ShippingOrderItem.Parse(orderForShippingUID);
    }


    public ShippingDto GetShippingByUID(string shippingOrderUID, ShippingQueryType queryType) {
      Assertion.Require(shippingOrderUID, nameof(shippingOrderUID));
      Assertion.Require(queryType, nameof(queryType));

      //TODO AGREGAR VALIDACION DE ACTIONS EN BASE A QUERY TYPE
      //var builder = new ShippingBuilder();



      return GetShippingByUID(shippingOrderUID);

    }


    public ShippingDto GetShippingByUID(string shippingOrderUID) {
      Assertion.Require(shippingOrderUID, nameof(shippingOrderUID));

      var builder = new ShippingBuilder();

      ShippingEntry shippingEntry = builder.GetShippingByUID(shippingOrderUID);

      return ShippingMapper.MapShippingForParcelDelivery(shippingEntry);

    }


    public ShippingEntryDto GetShippingByOrderUID(string orderUID) {
      Assertion.Require(orderUID, nameof(orderUID));

      var builder = new ShippingBuilder();

      ShippingEntry entry = builder.GetShippingByOrderUID(orderUID);

      return ShippingMapper.Map(entry);
    }


    public ShippingEntry GetShippingByShippingUID(string shippingUID) {
      Assertion.Require(shippingUID, nameof(shippingUID));

      return ShippingEntry.Parse(shippingUID);
    }


    public FixedList<ShippingEntryDto> GetShippingsList(ShippingQuery query) {
      Assertion.Require(query, nameof(query));

      var builder = new ShippingBuilder();

      FixedList<ShippingEntry> entries = builder.GetShippingList(query);

      return ShippingMapper.MapShippings(entries);
    }


    public ShippingDto GetShippingOrderByQuery(ShippingFieldsQuery query) {
      Assertion.Require(query, nameof(query));

      var builder = new ShippingBuilder();

      ShippingEntry shipping = builder.GetShippingEntry(query.Orders);

      return ShippingMapper.MapShippingForParcelDelivery(shipping);
    }


    public ShippingDto UpdateShippingOrder(string shippingOrderUID, ShippingFields fields) {
      Assertion.Require(shippingOrderUID, nameof(shippingOrderUID));
      Assertion.Require(fields, nameof(fields));

      fields.ShippingData.ShippingUID = shippingOrderUID;
      
      var builder = new ShippingBuilder();

      ShippingEntry shippingOrder = builder.UpdateShippingOrder(fields);

      return GetShippingByUID(shippingOrder.ShippingUID);
    }


    public ShippingPackage GetShippingPackageByUID(string shippingPackageUID) {
      Assertion.Require(shippingPackageUID, nameof(shippingPackageUID));

      var package = ShippingPackage.Parse(shippingPackageUID);

      return package;
    }


    public ShippingPallet GetShippingPalletByUID(string shippingPalletUID) {
      Assertion.Require(shippingPalletUID, nameof(shippingPalletUID));

      var pallet = ShippingPallet.Parse(shippingPalletUID);
      
      return pallet;
    }



    public ShippingDto UpdateShippingPallet(string shippingUID, string shippingPalletUID, ShippingPalletFields fields) {
      Assertion.Require(shippingUID, nameof(shippingUID));
      Assertion.Require(shippingPalletUID, nameof(shippingPalletUID));
      Assertion.Require(fields, nameof(fields));

      var builder = new ShippingBuilder();

      builder.UpdateShippingPallet(shippingUID, shippingPalletUID, fields);

      return GetShippingByUID(shippingUID);
    }


    #endregion Use cases


    #region Private methods



    #endregion Private methods

  } // class ShippingUseCases

} // namespace Empiria.Trade.ShippingAndHandling.UseCases
