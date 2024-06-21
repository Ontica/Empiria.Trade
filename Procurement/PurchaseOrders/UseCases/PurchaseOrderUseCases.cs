﻿/* Empiria Trade *********************************************************************************************
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
using Empiria.Trade.Procurement.Adapters;
using Empiria.Trade.Procurement.Domain;

namespace Empiria.Trade.Procurement.UseCases {


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


    public PurchaseOrderEntry GetPurchaseOrder(string purchaseOrderUID) {
      return PurchaseOrderEntry.Parse(purchaseOrderUID);
    }


    public PurchaseOrderItem GetPurchaseOrderItem(string purchaseOrderItemUID) {
      return PurchaseOrderItem.Parse(purchaseOrderItemUID);
    }


    public PurchaseOrderDataDto GetPurchaseOrderList(PurchaseOrderQuery query) {
      var purchaseOrderList = PurchaseOrderBuilder.GetPurchaseOrderList(query);

      return PurchaseOrderMapper.MapDescriptorList(purchaseOrderList, query);
    }


    public FixedList<PurchaseOrderEntry> GetPurchaseOrderList_(PurchaseOrderQuery query) {
      
      return PurchaseOrderBuilder.GetPurchaseOrderList(query);
    }

    #endregion Public methods

  } // class PurchaseOrderUseCases 

} // namespace Empiria.Trade.Inventory.PurchaseOrders.UseCases
