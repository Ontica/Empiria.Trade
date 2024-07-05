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


    public PurchaseOrderDto CreatePurchaseOrder(PurchaseOrderFields fields) {

      var order = new PurchaseOrderEntry(fields);
      order.Save();

      return GetPurchaseOrder(order.OrderUID);
    }


    public PurchaseOrderDto GetPurchaseOrder(string purchaseOrderUID) {

      var order = PurchaseOrderEntry.Parse(purchaseOrderUID);
      return PurchaseOrderMapper.MapOrder(order);
    }


    public PurchaseOrderItem GetPurchaseOrderItem(string purchaseOrderItemUID) {
      return PurchaseOrderItem.Parse(purchaseOrderItemUID);
    }


    public PurchaseOrdersDataDto GetPurchaseOrderDescriptor(PurchaseOrderQuery query) {
      var purchaseOrderEntries = GetPurchaseOrderList(query);

      return PurchaseOrderMapper.MapDataDto(purchaseOrderEntries, query);
    }


    #endregion Public methods

    #region Private methods

    public FixedList<PurchaseOrderEntry> GetPurchaseOrderList(PurchaseOrderQuery query) {

      return PurchaseOrderBuilder.GetPurchaseOrderEntries(query);
    }


    #endregion Private methods

  } // class PurchaseOrderUseCases 

} // namespace Empiria.Trade.Inventory.PurchaseOrders.UseCases
