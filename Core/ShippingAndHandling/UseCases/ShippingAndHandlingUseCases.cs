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
using Empiria.Services;
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

      FixedList<Packing> packaging = builder.GetPackingByOrder(orderUid);

      return ShippingAndHandlingMapper.MapPackingDto(packaging.ToFixedList());

    }


    public IShippingAndHandling CreatePackingOrder(string orderUID, PackingOrderFields orderFields) {

      var packagingOrder = new PackingItem(orderUID, orderFields);

      packagingOrder.Save();

      return ShippingAndHandlingMapper.MapPackagingOrder(packagingOrder);
    }


    public IShippingAndHandling UpdatePackingOrder(string packingItemUID, PackingOrderFields order) {

      return new PackingOrderDto();
    }


    public IShippingAndHandling DeletePackingOrder(string packingItemUID) {
      
      return new PackingOrderDto();
    }


    #endregion Use cases


  } // class ShippingAndHandlingUseCases

} // namespace Empiria.Trade.ShippingAndHandling.UseCases
