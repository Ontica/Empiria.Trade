/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Packing Management                         Component : Use cases Layer                         *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Use case interactor class               *
*  Type     : PackagingUseCases                          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases used to build packaging orders.                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using Empiria.Services;
using Empiria.Trade.Core;
using Empiria.Trade.Core.Catalogues;
using Empiria.Trade.Sales.UseCases;

namespace Empiria.Trade.Packaging.UseCases {


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


    public PackageType GetPackageTypeById(int packageTypeId) {
      var helper = new PackingHelper();
      PackageType packageType = helper.GetPackageTypeById(packageTypeId);

      return packageType;
    }


    public FixedList<INamedEntity> GetPackageTypeList() {

      var builder = new PackagingBuilder();

      return builder.GetPackageTypeList();
    }


    public PackagedData GetPackagedData(string orderUid) {

      var builder = new PackagingBuilder();
      return builder.GetPackagedData(orderUid);
    }


    public FixedList<PackingItem> GetPackingItemsByOrderPackingUID(string orderPackingUID) {

      var builder = new PackagingBuilder();
      return builder.GetPackingItemsByOrderPackingUID(orderPackingUID);
    }


    public PackagingEntry GetPackagingByUID(string Uid) {

      return PackagingEntry.Parse(Uid);
    }


    public PackagingItem GetPackingOrderItemByUID(string Uid) {

      return PackagingItem.Parse(Uid);
    }

    
    public FixedList<PackagedForItem> GetPackagedForItemList(string orderUID) {
      
      var builder = new PackagingBuilder();
      return builder.GetPackagedForItemList(orderUID);

    }


    public PackingDto GetPackagingForOrder(string orderUid) {

      return GetPackaging(orderUid);
    }


    public ISalesOrderDto CreatePackagingEntry(string orderUID, PackagingEntryFields fields) {

      PackagingEntry packagingOrder = new PackagingEntry(orderUID, fields, string.Empty);

      packagingOrder.Save();

      return GetSalesOrder(orderUID);
    }


    public ISalesOrderDto UpdatePackagingEntry(string orderUID, string orderPackingUID,
                                                  PackagingEntryFields fields) {

      var packagingEntry = PackagingEntry.Parse(orderPackingUID);

      packagingEntry.Update(fields, orderPackingUID);

      packagingEntry.Save();

      return GetSalesOrder(orderUID);
    }


    public ISalesOrderDto UpdatePickingEntry(string orderUID) {

      return GetSalesOrder(orderUID);
    }


    public ISalesOrderDto DeletePackageForItem(string orderUID, string packageForItemUID) {

      var data = new PackagingData();

      data.DeletePackageForItem(packageForItemUID);

      return GetSalesOrder(orderUID);
    }


    public ISalesOrderDto CreatePackingOrderItemFields(
              string orderUID, string orderPackingUID, MissingItemField missingItemFields) {

      var packagingOrder = new PackagingItem(orderUID, orderPackingUID, missingItemFields);

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
      var packaging = builder.GetPackagingEntriesWithItemsByOrder(orderUid);
      
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
