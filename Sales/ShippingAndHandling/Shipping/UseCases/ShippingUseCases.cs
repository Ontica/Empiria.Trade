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
using Empiria.Trade.Sales.ShippingAndHandling.Data;
using Empiria.Trade.Sales.UseCases;

namespace Empiria.Trade.Sales.ShippingAndHandling.UseCases {

  /// <summary>Use cases used to build shipping.</summary>
  public class ShippingUseCases : UseCase {

    #region Constructors and parsers

    public ShippingUseCases() {
      // no-op
    }

    static public ShippingUseCases UseCaseInteractor() {
      return CreateInstance<ShippingUseCases>();
    }


    #endregion Constructors and parsers


    #region Use cases

    public ShippingDto CreateOrderForShipping(string shippingOrderUID, string orderUID) {
      Assertion.Require(shippingOrderUID, nameof(shippingOrderUID));
      Assertion.Require(orderUID, nameof(orderUID));

      var helper = new ShippingHelper();
      helper.ValidateIfExistOrderForShipping(orderUID);

      string[] orders = new string[] { orderUID };

      helper.ValidateOrdersByCustomer(shippingOrderUID, orderUID);
      
      var builder = new ShippingBuilder();
      
      builder.CreateShippingOrderItems(shippingOrderUID, orders);

      return GetShippingOrder(shippingOrderUID, ShippingQueryType.Shipping);
    }


    public ShippingDto CreateShippingOrder(ShippingFields fields) {
      Assertion.Require(fields, nameof(fields));

      var builder = new ShippingBuilder();

      ShippingEntry shippingOrder = builder.CreateShippingOrder(fields);

      return GetShippingOrder(shippingOrder.UID, ShippingQueryType.Shipping);
    }


    public ShippingDto CreateShippingPallet(string shippingUID, ShippingPalletFields fields) {
      Assertion.Require(shippingUID, nameof(shippingUID));
      Assertion.Require(fields, nameof(fields));

      var builder = new ShippingBuilder();

      builder.CreateShippingPallet(shippingUID, fields);

      return GetShippingOrder(shippingUID, ShippingQueryType.Shipping);
    }


    public ShippingDto DeleteOrderForShipping(string shippingOrderUID, string orderUID) {
      Assertion.Require(shippingOrderUID, "shippingOrderUID");
      Assertion.Require(orderUID, "orderUID");

      ShippingData.DeleteOrderForShipping(orderUID);

      return GetShippingOrder(shippingOrderUID, ShippingQueryType.Shipping);
    }
    

    public void DeleteShippingOrder(string shippingOrderUID) {
      Assertion.Require(shippingOrderUID, "shippingOrderUID");

      FixedList<ShippingPallet> pallets = ShippingData.GetPalletByShippingOrder(shippingOrderUID);

      foreach (var pallet in pallets) {
        ShippingData.DeleteShippingPackagesByPalletUID(pallet.ShippingPalletUID);
      }

      ShippingData.DeleteShippingPalletsByShippingUID(shippingOrderUID);

      ShippingData.DeleteOrdersForShippingByShippingUID(shippingOrderUID);

      ShippingData.DeleteShippingOrder(shippingOrderUID);
    }


    public ShippingDto DeleteShippingPalletByUID(string shippingUID, string shippingPalletUID) {
      Assertion.Require(shippingUID, nameof(shippingUID));
      Assertion.Require(shippingPalletUID, nameof(shippingPalletUID));

      ShippingData.DeleteShippingPackagesByPalletUID(shippingPalletUID);

      ShippingData.DeleteShippingPalletByUID(shippingPalletUID);

      return GetShippingOrder(shippingUID, ShippingQueryType.Shipping);
    }


    public ShippingOrderItem GetOrdersForShippingByUID(string orderForShippingUID) {
      Assertion.Require(orderForShippingUID, nameof(orderForShippingUID));
      return ShippingOrderItem.Parse(orderForShippingUID);
    }


    public ShippingEntryDto GetShippingByOrderUID(string orderUID) {
      Assertion.Require(orderUID, nameof(orderUID));

      var builder = new ShippingBuilder();

      ShippingEntry entry = builder.GetShippingByOrderUID(orderUID);

      return ShippingMapper.Map(entry);
    }


    public ShippingDto GetShippingOrder(string shippingOrderUID, ShippingQueryType queryType) {
      Assertion.Require(shippingOrderUID, "shippingOrderUID");
      Assertion.Require(queryType, nameof(queryType));

      var builder = new ShippingBuilder();

      ShippingEntry shippingEntry = builder.GetShippingByUID(shippingOrderUID);

      ShippingDto shippingDto = ShippingMapper.MapShippingForParcelDelivery(shippingEntry);

      builder.GetActionsByShippingQueryType(shippingDto, queryType);

      return shippingDto;
    }


    public FixedList<ShippingEntryDto> GetShippingsList(ShippingQuery query) {
      Assertion.Require(query, nameof(query));

      var builder = new ShippingBuilder();

      FixedList<ShippingEntry> entries = builder.GetShippingList(query);

      return ShippingMapper.MapShippings(entries);
    }


    public ShippingEntry GetShippingOrderByUID(string shippingUID) {
      Assertion.Require(shippingUID, nameof(shippingUID));

      return ShippingEntry.Parse(shippingUID);
    }


    public ShippingDto GetShippingOrderByQuery(ShippingFieldsQuery query) {
      Assertion.Require(query, nameof(query));

      var builder = new ShippingBuilder();

      ShippingEntry shipping = builder.GetShippingEntry(query.Orders);

      ShippingDto shippingDto= ShippingMapper.MapShippingForParcelDelivery(shipping);

      builder.GetActionsByShippingQueryType(shippingDto, ShippingQueryType.Shipping);

      return shippingDto;
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


    public ShippingDto SetShippingOrderToDelivery(string shippingOrderUID) {
      Assertion.Require(shippingOrderUID, nameof(shippingOrderUID));

      var orders = ShippingBuilder.GetSalesOrdersUID(shippingOrderUID);

      SalesOrderUseCases orderUseCases = new SalesOrderUseCases();

      orderUseCases.SetSalesOrdersToDelivery(orders);

      ShippingData.UpdateShippingStatus(shippingOrderUID, ShippingStatus.EnProceso);

      return GetShippingOrder(shippingOrderUID, ShippingQueryType.Shipping);
    }


    public ShippingDto UpdateShippingOrder(string shippingOrderUID, ShippingFields fields) {
      Assertion.Require(shippingOrderUID, nameof(shippingOrderUID));
      Assertion.Require(fields, nameof(fields));

      fields.ShippingData.ShippingUID = shippingOrderUID;

      var builder = new ShippingBuilder();

      ShippingEntry shippingOrder = builder.UpdateShippingOrder(fields);

      return GetShippingOrder(shippingOrder.UID, ShippingQueryType.Shipping);
    }


    public ShippingDto UpdateShippingPallet(string shippingUID, string shippingPalletUID,
                                            ShippingPalletFields fields) {
      Assertion.Require(shippingUID, nameof(shippingUID));
      Assertion.Require(shippingPalletUID, nameof(shippingPalletUID));
      Assertion.Require(fields, nameof(fields));

      ShippingPallet pallet = ShippingPallet.Parse(shippingPalletUID);

      pallet.Update(shippingUID, fields);

      pallet.Save();

      ShippingBuilder.ComparePackagesToRemoveFromPallet(shippingPalletUID, fields.Packages);

      ShippingBuilder.CreatePackagesIfNotExistInPallet(shippingPalletUID, fields.Packages);

      return GetShippingOrder(shippingUID, ShippingQueryType.Shipping);
    }

    #endregion Use cases


    #region Private methods



    #endregion Private methods

  } // class ShippingUseCases

} // namespace Empiria.Trade.ShippingAndHandling.UseCases
