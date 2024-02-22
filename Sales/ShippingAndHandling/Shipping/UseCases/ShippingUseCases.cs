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

      var builder = new ShippingBuilder();

      var orders = builder.GetOrdersUIDList(shippingOrderUID);

      SalesOrderUseCases orderUseCases = new SalesOrderUseCases();

      orderUseCases.ChangeOrdersToDeliveryStatus(orders);

      return GetShippingByUID(shippingOrderUID);
    }


    public ShippingDto CreateOrderForShipping(string shippingOrderUID, string orderUID) {

      var helper = new ShippingHelper();
      helper.ValidateIfExistOrderForShipping(orderUID);

      string[] orders = new string[] { orderUID };

      helper.ValidateOrdersByCustomer(shippingOrderUID, orderUID);
      
      var builder = new ShippingBuilder();
      
      builder.CreateOrdersForShipping(shippingOrderUID, orders);

      return GetShippingByUID(shippingOrderUID);
    }


    public ShippingDto CreateShippingOrder(ShippingFields fields) {

      var builder = new ShippingBuilder();

      ShippingEntry shippingOrder = builder.CreateShippingOrder(fields);

      return GetShippingByUID(shippingOrder.ShippingUID);
    }


    public ShippingDto DeleteOrderForShipping(string shippingOrderUID, string orderUID) {
      Assertion.Require(shippingOrderUID, "shippingOrderUID");
      Assertion.Require(orderUID, "orderUID");

      ShippingData.DeleteOrderForShipping(orderUID);

      return GetShippingByUID(shippingOrderUID);
    }


    public void DeleteShipping(string shippingOrderUID) {
      
      Assertion.Require(shippingOrderUID, "shippingOrderUID");

      ShippingData.DeleteOrdersForShippingByShippingUID(shippingOrderUID);

      ShippingData.DeleteShippingPalletsByShippingUID(shippingOrderUID);

      ShippingData.DeleteShipping(shippingOrderUID);
    }


    public ShippingDto DeleteShippingPalletByUID(string shippingUID, string shippingPalletUID) {

      ShippingData.DeleteShippingPackageByPalletUID(shippingPalletUID);

      ShippingData.DeleteShippingPalletByUID(shippingPalletUID);

      return GetShippingByUID(shippingUID);
    }


    public ShippingOrderItem GetOrdersForShippingByUID(string orderForShippingUID) {

      return ShippingOrderItem.Parse(orderForShippingUID);
    }


    public FixedList<INamedEntity> GetParcelSupplierList() {

      var catalogueUsecase = CataloguesUseCases.UseCaseInteractor();

      return catalogueUsecase.GetParcelSupplierList();
    }


    public ShippingDto GetShippingByUID(string shippingOrderUID) {

      var builder = new ShippingBuilder();

      ShippingEntry shippingEntry = builder.GetShippingByUID(shippingOrderUID);

      return ShippingMapper.MapShippingForParcelDelivery(shippingEntry);

    }


    public ShippingEntryDto GetShippingByOrderUID(string orderUID) {

      var builder = new ShippingBuilder();

      ShippingEntry entry = builder.GetShippingByOrderUID(orderUID);

      return ShippingMapper.Map(entry);
    }


    public ShippingEntry GetShippingByShippingUID(string shippingUID) {

      return ShippingEntry.Parse(shippingUID);
    }


    public FixedList<ShippingEntryDto> GetShippingsList(ShippingQuery query) {

      var builder = new ShippingBuilder();

      FixedList<ShippingEntry> entries = builder.GetShippingList(query);

      return ShippingMapper.MapShippings(entries);
    }


    public ShippingDto GetShippingOrderByQuery(ShippingFieldsQuery query) {
      var builder = new ShippingBuilder();

      ShippingEntry shipping = builder.GetShippingEntry(query.Orders);

      return ShippingMapper.MapShippingForParcelDelivery(shipping);
    }


    public ShippingDto UpdateShippingOrder(string shippingOrderUID, ShippingFields fields) {

      fields.ShippingData.ShippingUID = shippingOrderUID;
      
      var builder = new ShippingBuilder();

      ShippingEntry shippingOrder = builder.UpdateShippingOrder(fields);

      return GetShippingByUID(shippingOrder.ShippingUID);
    }


    public ShippingPallet GetShippingPalletByUID(string shippingPalletUID) {
      var pallet = ShippingPallet.Parse(shippingPalletUID);
      
      return pallet;
    }


    public ShippingPackage GetShippingPackageByUID(string shippingPackageUID) {
      var package = ShippingPackage.Parse(shippingPackageUID);

      return package;
    }


    public ShippingDto CreateShippingPallet(string shippingUID, ShippingPalletFields fields) {

      var builder = new ShippingBuilder();

      builder.CreateShippingPallet(shippingUID, fields);

      return GetShippingByUID(shippingUID);
    }


    public ShippingDto UpdateShippingPallet(string shippingUID, string shippingPalletUID, ShippingPalletFields fields) {
      var builder = new ShippingBuilder();

      builder.UpdateShippingPallet(shippingUID, shippingPalletUID, fields);

      return GetShippingByUID(shippingUID);
    }


    #endregion Use cases


    #region Private methods



    #endregion Private methods

  } // class ShippingUseCases

} // namespace Empiria.Trade.ShippingAndHandling.UseCases
