/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Use cases Layer                         *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Use case interactor class               *
*  Type     : PurchaseOrderUseCases                      License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases used to build purchase order.                                                        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Services;
using Empiria.Trade.Inventory.PurchaseOrders.Adapters;
using Empiria.Trade.Inventory.PurchaseOrders.Domain;
using Empiria.Trade.Inventory.UseCases;

namespace Empiria.Trade.Inventory.PurchaseOrders.UseCases {


  /// <summary>Use cases used to build purchase order.</summary>
  public class PurchaseOrderUseCases : UseCase {


    #region Constructors and parsers

    public PurchaseOrderUseCases() {
      // no-op
    }

    static public PurchaseOrderUseCases UseCaseInteractor() {
      return CreateInstance<PurchaseOrderUseCases>();
    }


    #endregion Constructors and parsers


    #region Public methods


    public PurchaseOrderDataDto GetPurchaseOrderList(PurchaseOrderQuery query) {
      var purchaseOrderList = PurchaseOrderBuilder.GetPurchaseOrderList(query);

      return PurchaseOrderMapper.MapDescriptorList(purchaseOrderList, query);
    }


    #endregion Public methods

  } // class PurchaseOrderUseCases 

} // namespace Empiria.Trade.Inventory.PurchaseOrders.UseCases
