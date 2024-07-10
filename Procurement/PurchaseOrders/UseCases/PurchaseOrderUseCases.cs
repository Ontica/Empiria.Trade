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
using Empiria.Trade.Orders;
using Empiria.Trade.Procurement.Adapters;
using Empiria.Trade.Procurement.Data;
using Empiria.Trade.Procurement.Domain;
using Empiria.Trade.Products;

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

      var order = new PurchaseOrderEntry(fields, "");
      order.Save();

      return GetPurchaseOrder(order.OrderUID);
    }


    public PurchaseOrderDto CreatePurchaseOrderItem(
      string purchaseOrderUID, PurchaseOrderItemFields fields) {

      Assertion.Require(purchaseOrderUID, nameof(purchaseOrderUID));
      Assertion.Require(fields, nameof(fields));

      ValidationsForItem(fields);

      var orderItem = new PurchaseOrderItem(purchaseOrderUID, fields);
      orderItem.Save();

      return GetPurchaseOrder(purchaseOrderUID);
    }


    public void DeletePurchaseOrder(string purchaseOrderUID) {
      var order = PurchaseOrderEntry.Parse(purchaseOrderUID);

      order.Items = GetPurchaseOrderItems(order.Id);
      
      foreach (var item in order.Items) {
        PurchaseOrderData.DeletePurchaseOrderItem(item.UID);
      }
      
      PurchaseOrderData.DeletePurchaseOrder(order.Id);
    }


    public PurchaseOrderDto DeletePurchaseOrderItem(string purchaseOrderUID, string purchaseOrderItemUID) {

      PurchaseOrderData.DeletePurchaseOrderItem(purchaseOrderItemUID);

      return GetPurchaseOrder(purchaseOrderUID);
    }


    public PurchaseOrderDto GetPurchaseOrder(string purchaseOrderUID) {

      var order = PurchaseOrderEntry.Parse(purchaseOrderUID);
      order.Items = GetPurchaseOrderItems(order.Id);
      order.SetTotals();
      return PurchaseOrderMapper.MapOrder(order);
    }


    public PurchaseOrderItem GetPurchaseOrderItem(string purchaseOrderItemUID) {
      return PurchaseOrderItem.Parse(purchaseOrderItemUID);
    }


    public FixedList<PurchaseOrderItem> GetPurchaseOrderItems(int orderId) {
      return PurchaseOrderData.GetPurchaseOrderItems(orderId);
    }


    public PurchaseOrdersDataDto GetPurchaseOrderDescriptor(PurchaseOrderQuery query) {
      var purchaseOrderEntries = GetPurchaseOrderList(query);

      return PurchaseOrderMapper.MapDataDto(purchaseOrderEntries, query);
    }


    public PurchaseOrderDto UpdatePurchaseOrder(string purchaseOrderUID, PurchaseOrderFields fields) {

      var order = new PurchaseOrderEntry(fields, purchaseOrderUID);
      order.Save();

      return GetPurchaseOrder(order.OrderUID);
    }


    public PurchaseOrderDto UpdatePurchaseOrderItem(string purchaseOrderUID, string purchaseOrderItemUID,
                                                    PurchaseOrderItemFields fields) {
      Assertion.Require(purchaseOrderUID, nameof(purchaseOrderUID));
      Assertion.Require(fields, nameof(fields));

      fields.UID = purchaseOrderItemUID;

      return CreatePurchaseOrderItem(purchaseOrderUID, fields);
    }


    #endregion Public methods

    #region Private methods

    private FixedList<PurchaseOrderEntry> GetPurchaseOrderList(PurchaseOrderQuery query) {

      return PurchaseOrderBuilder.GetPurchaseOrderEntries(query);
    }


    private void ValidationsForItem(PurchaseOrderItemFields fields) {

      if (fields.VendorProductUID == string.Empty) {

        Assertion.EnsureFailed($"Por favor especifique un producto.");
      }

      if (fields.Quantity <= 0) {
        var item = VendorProduct.Parse(fields.VendorProductUID);
        Assertion.EnsureFailed($"La cantidad para el producto " +
          $"{item.ProductFields.ProductCode} " +
          $"({item.ProductPresentation.PresentationName}) debe ser mayor a 0.");
      }
    }


    #endregion Private methods

  } // class PurchaseOrderUseCases 

} // namespace Empiria.Trade.Inventory.PurchaseOrders.UseCases
