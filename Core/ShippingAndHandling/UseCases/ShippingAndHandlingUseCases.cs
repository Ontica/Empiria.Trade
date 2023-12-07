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


    public PackageForItem GetPackagingByUID(string Uid) {

      return PackageForItem.Parse(Uid);
    }


    public IShippingAndHandling GetPackagingForOrder(string orderUid) {

      return GetPackaging(orderUid);
    }

    
    public IShippingAndHandling CreatePackageForItem(string orderUID, PackingItemFields orderFields) {

      var packagingOrder = new PackageForItem(orderUID, orderFields, string.Empty);

      packagingOrder.Save();

      return GetPackaging(orderUID);
    }


    public IShippingAndHandling UpdatePackageForItem(string orderUID, string packageForItemUID,
                                                  PackingItemFields orderFields) {

      var packagingOrder = new PackageForItem(orderUID, orderFields, packageForItemUID);

      ShippingAndHandlingData.WritePacking(packagingOrder);

      return GetPackaging(orderUID);
    }


    public IShippingAndHandling DeletePackageForItem(string orderUID, string packingItemUID) {

      var data = new ShippingAndHandlingData();

      return GetPackaging(orderUID);
    }


    public IShippingAndHandling CreatePackingOrderItemFields(
              string orderUID, string packingItemUID, MissingItemField missingItemFields) {


      var builder = new ShippingAndHandlingBuilder();
      
      var inventory = builder.GetInventoryEntries(missingItemFields.orderItemUID,
                                                  missingItemFields.WarehouseBinUID);

      var packagingOrder = new PackingOrderItem(orderUID, packingItemUID,
                                inventory.InventoryEntryId, missingItemFields);

      packagingOrder.Save();

      return GetPackaging(orderUID);

    }


    public IShippingAndHandling DeletePackingOrderItem(string orderUID,
                                 string packingItemUID, string packingItemEntryUID) {
      
      var data = new ShippingAndHandlingData();

      data.DeletePackingOrderItem(packingItemEntryUID);

      return GetPackaging(orderUID);
    }


    #endregion Use cases


    #region Private methods


    private PackingDto GetPackaging(string orderUid) {
      var builder = new ShippingAndHandlingBuilder();

      return builder.GetPackagesAndItemsForOrder(orderUid);
    }


    #endregion Private methods

  } // class ShippingAndHandlingUseCases

} // namespace Empiria.Trade.ShippingAndHandling.UseCases
