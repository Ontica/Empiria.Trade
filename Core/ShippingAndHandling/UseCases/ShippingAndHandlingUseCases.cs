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


    public FixedList<IShippingAndHandling> CreatePackingOrder(PackingOrderFields fields) {

      var packaging = new List<PackagingOrder>();

      foreach (var field in fields.PackingFields) {

        var orderShipping = new PackagingOrder(field);

        orderShipping.Save();

        packaging.Add(orderShipping);

      }

      return ShippingAndHandlingMapper.MapPackagingOrder(packaging.ToFixedList());
    }


    #endregion Use cases


  } // class ShippingAndHandlingUseCases

} // namespace Empiria.Trade.ShippingAndHandling.UseCases
