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


    public PurchaseOrderDataDto GetPurchaseOrderDescriptor(PurchaseOrderQuery query) {
      var purchaseOrderEntries = PurchaseOrderBuilder.GetPurchaseOrderEntries(query);

      return PurchaseOrderMapper.MapDescriptorList(purchaseOrderEntries, query);
    }


    public FixedList<PurchaseOrderEntry> GetPurchaseOrderList(PurchaseOrderQuery query) {
      
      return PurchaseOrderBuilder.GetPurchaseOrderEntries(query);
    }

    #endregion Public methods

  } // class PurchaseOrderUseCases 

} // namespace Empiria.Trade.Inventory.PurchaseOrders.UseCases
