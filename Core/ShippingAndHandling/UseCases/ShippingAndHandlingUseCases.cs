/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Packing Management                         Component : Use cases Layer                         *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Use case interactor class               *
*  Type     : PackingUseCases                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases used to build shipping and handling orders.                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using System.Reflection;
using Empiria.Services;
using Empiria.Trade.Products;
using Empiria.Trade.ShippingAndHandling.Adapters;
using Empiria.Trade.ShippingAndHandling.Data;
using Empiria.Trade.ShippingAndHandling.Domain;

namespace Empiria.Trade.ShippingAndHandling.UseCases {


  /// <summary>Use cases used to build shipping and handling orders.</summary>
  public class ShippingAndHandlingUseCases : UseCase {


    #region Constructors and parsers

    protected ShippingAndHandlingUseCases() {
      // no-op
    }

    static public ShippingAndHandlingUseCases UseCaseInteractor() {
      return CreateInstance<ShippingAndHandlingUseCases>();
    }


    #endregion Constructors and parsers


    #region Use cases


    public FixedList<INamedEntity> GetPackageTypeList() {

      var builder = new ShippingAndHandlingBuilder();

      FixedList<INamedEntity> packageTypes = builder.GetPackageTypeList();

      return packageTypes;
    }


    public IShippingAndHandling GetPackingByOrder(string orderUid) {

      var builder = new ShippingAndHandlingBuilder();

      PackingDto packaging = builder.GetPackingByOrder(orderUid);
      
      //return ShippingAndHandlingMapper.MapPackingDto(packaging.ToFixedList());
      return packaging;
    }


    public IShippingAndHandling CreatePackingItem(string orderUID, PackingItemFields orderFields) {

      var packagingOrder = new PackingItem(orderUID, orderFields);

      packagingOrder.Save();

      var builder = new ShippingAndHandlingBuilder();
      PackingDto packaging = builder.GetPackingByOrder(orderUID);

      //return ShippingAndHandlingMapper.MapPackagingOrder(packagingOrder);

      return packaging;
    }


    public IShippingAndHandling UpdatePackingItem(string orderUID, string packingItemUID, PackingItemFields orderFields) {

      var builder = new ShippingAndHandlingBuilder();
      PackingDto packaging = builder.GetPackingByOrder(orderUID);

      return packaging;
    }


    public IShippingAndHandling DeletePackingItem(string orderUID, string packingItemUID) {

      var builder = new ShippingAndHandlingBuilder();
      PackingDto packaging = builder.GetPackingByOrder(orderUID);

      return packaging;
    }


    public IShippingAndHandling CreatePackingOrderItemFields(
              string orderUID, string packingItemUID, MissingItemField missingItemFields) {


      var builder = new ShippingAndHandlingBuilder();
      
      var inventory = builder.GetInventoryEntries(missingItemFields.orderItemUID,
                                                  missingItemFields.WarehouseBinUID);

      var packagingOrder = new PackingOrderItem(orderUID, packingItemUID,
                                inventory.InventoryEntryId, missingItemFields);

      packagingOrder.Save();

      PackingDto packaging = builder.GetPackingByOrder(orderUID);

      return packaging;

    }


    public IShippingAndHandling DeletePackingOrderItem(string orderUID,
                                 string packingItemUID, string packingItemEntryUID) {
      
      var data = new ShippingAndHandlingData();

      data.DeletePackingOrderItem(packingItemEntryUID);

      var builder = new ShippingAndHandlingBuilder();
      PackingDto packaging = builder.GetPackingByOrder(orderUID);

      return packaging;
    }


    #endregion Use cases


  } // class ShippingAndHandlingUseCases

} // namespace Empiria.Trade.ShippingAndHandling.UseCases
