/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Packing Management                         Component : Use cases Layer                         *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Use case interactor class               *
*  Type     : PackagingUseCases                          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases used to build packaging orders.                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using System.Reflection;
using Empiria.Services;
using Empiria.Trade.Sales.Adapters;
using Empiria.Trade.Sales.ShippingAndHandling.Adapters;
using Empiria.Trade.Sales.ShippingAndHandling.Data;
using Empiria.Trade.Sales.ShippingAndHandling.Domain;
using Empiria.Trade.Sales.UseCases;

namespace Empiria.Trade.Sales.ShippingAndHandling.UseCases {


  /// <summary>Use cases used to build packaging orders.</summary>
  public class PackagingUseCases : UseCase {


    #region Constructors and parsers

    protected PackagingUseCases() {
      // no-op
    }

    static public PackagingUseCases UseCaseInteractor() {
      return CreateInstance<PackagingUseCases>();
    }


    #endregion Constructors and parsers


    #region Use cases


    public FixedList<INamedEntity> GetPackageTypeList() {

      var builder = new PackagingBuilder();

      FixedList<INamedEntity> packageTypes = builder.GetPackageTypeList();

      return packageTypes;
    }


    public PackagedData GetPackagedData(string orderUid) {

      var builder = new PackagingBuilder();
      return builder.GetPackagedData(orderUid);
    }


    public PackageForItem GetPackagingByUID(string Uid) {

      return PackageForItem.Parse(Uid);
    }


    public FixedList<PackagedForItem> GetPackagedForItemList(string orderUID) {
      
      var builder = new PackagingBuilder();
      return builder.GetPackagedForItemList(orderUID);

    }


    public PackingDto GetPackagingForOrder(string orderUid) {

      return GetPackaging(orderUid);
    }


    public ISalesOrderDto CreatePackageForItem(string orderUID, PackingItemFields orderFields) {

      PackagingBuilder.ValidateIfExistPackagesForItems(
                                  orderUID, orderFields.PackageID, string.Empty);

      var packagingOrder = new PackageForItem(orderUID, orderFields, string.Empty);

      packagingOrder.Save();

      return GetSalesOrder(orderUID);
    }


    public ISalesOrderDto UpdatePackageForItem(string orderUID, string packageForItemUID,
                                                  PackingItemFields orderFields) {

      PackagingBuilder.ValidateIfExistPackagesForItems(
                                  orderUID, orderFields.PackageID, packageForItemUID);

      var packagingOrder = new PackageForItem(orderUID, orderFields, packageForItemUID);

      PackagingData.WritePacking(packagingOrder);

      return GetSalesOrder(orderUID);
    }


    public ISalesOrderDto DeletePackageForItem(string orderUID, string packageForItemUID) {

      var data = new PackagingData();

      data.DeletePackageForItem(packageForItemUID);

      return GetSalesOrder(orderUID);
    }


    public ISalesOrderDto CreatePackingOrderItemFields(
              string orderUID, string packingItemUID, MissingItemField missingItemFields) {

      var builder = new PackagingBuilder();

      var inventory = builder.GetInventoryEntries(missingItemFields.orderItemUID,
                                                  missingItemFields.WarehouseBinUID);

      var packagingOrder = new PackingOrderItem(orderUID, packingItemUID,
                                inventory.InventoryEntryId, missingItemFields);

      packagingOrder.Save();

      return GetSalesOrder(orderUID);

    }


    public ISalesOrderDto DeletePackingOrderItem(string orderUID,
                                 string packingItemUID, string packingItemEntryUID) {

      var data = new PackagingData();

      data.DeletePackingOrderItem(packingItemEntryUID);

      return GetSalesOrder(orderUID);
    }


    #endregion Use cases


    #region Private methods


    private PackingDto GetPackaging(string orderUid) {
      var builder = new PackagingBuilder();

      var packaging = builder.GetPackagesAndItemsForOrder(orderUid);
      
      return PackagingMapper.MapPackingDto(packaging);

    }


    private ISalesOrderDto GetSalesOrder(string orderUID) {

      using (var usecases = SalesOrderUseCases.UseCaseInteractor()) {

        return usecases.GetSalesOrder(orderUID, QueryType.SalesPacking);
      }
    }

    #endregion Private methods

  } // class PackagingUseCases

} // namespace Empiria.Trade.ShippingAndHandling.UseCases
