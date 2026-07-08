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
using Empiria.Orders;
using Empiria.Services;
using Empiria.Trade.Core;

using Empiria.Trade.Procurement.Adapters;
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

    public PurchaseOrderDto ClosePurchaseOrder(string purchaseOrderUID) {
      
      PurchaseOrder order = PurchaseOrder.Parse(purchaseOrderUID);

      order.CloseOrder();

      PurchaseOrderBuilder.GenerateInventoryOrder(order);

      return GetPurchaseOrderDto(order.UID);
    }

    

    public PurchaseOrderDto CreatePurchaseOrder(PurchaseOrderFields fields) {

      var orderType = OrderType.PurchaseOrder;

      var order = new PurchaseOrder(fields, orderType);

      order.Save();

      return GetPurchaseOrderDto(order.UID);
    }


    public PurchaseOrderDto CreatePurchaseOrderItem(string purchaseOrderUID,
                                                      PurchaseOrderItemFields fields) {
      Assertion.Require(purchaseOrderUID, nameof(purchaseOrderUID));
      Assertion.Require(fields, nameof(fields));

      var order = PurchaseOrder.Parse(purchaseOrderUID);

      GetDefaultProductFields(order, fields);

      var orderItemType = OrderItemType.PurchaseOrderItemType;

      var orderItem = new PurchaseOrderItem(orderItemType, order, fields.ProductUID);

      orderItem.Update(fields);

      orderItem.Save();

      return GetPurchaseOrderDto(purchaseOrderUID);
    }


    public PurchaseOrderDto DeletePurchaseOrder(string purchaseOrderUID) {

      PurchaseOrder order = PurchaseOrder.Parse(purchaseOrderUID);

      order.DeleteOrder();

      return GetPurchaseOrderDto(purchaseOrderUID);
    }


    public PurchaseOrderDto DeletePurchaseOrderItem(string purchaseOrderUID, string purchaseOrderItemUID) {

      PurchaseOrderItem orderItem = PurchaseOrderItem.Parse(purchaseOrderItemUID);

      orderItem.Delete();

      orderItem.Save();

      return GetPurchaseOrderDto(purchaseOrderUID);
    }


    public PurchaseOrderDto GetPurchaseOrderDto(string purchaseOrderUID) {

      var order = PurchaseOrder.Parse(purchaseOrderUID);
      
      return PurchaseOrderMapper.MapOrder(order);
    }


    public PurchaseOrdersDataDto GetPurchaseOrderDescriptor(PurchaseOrderQuery query) {
      
      var orderType = OrderType.PurchaseOrder;

      var orders = PurchaseOrderData.GetPurchaseOrders(query, orderType.Id);

      return PurchaseOrderMapper.MapDataDto(orders, query);
    }
    
    
    public PurchaseOrderDto UpdatePurchaseOrder(string purchaseOrderUID, PurchaseOrderFields fields) {
      Assertion.Require(purchaseOrderUID, nameof(purchaseOrderUID));
      Assertion.Require(fields, nameof(fields));

      var orderType = OrderType.PurchaseOrder;

      var order = PurchaseOrder.Parse(purchaseOrderUID);

      order.Update(fields, orderType);

      order.Save();

      return GetPurchaseOrderDto(order.OrderUID);
    }


    public PurchaseOrderDto UpdatePurchaseOrderItem(string purchaseOrderUID, string purchaseOrderItemUID,
                                                    PurchaseOrderItemFields fields) {
      Assertion.Require(purchaseOrderUID, nameof(purchaseOrderUID));
      Assertion.Require(purchaseOrderItemUID, nameof(purchaseOrderItemUID));
      Assertion.Require(fields, nameof(fields));

      var order = PurchaseOrder.Parse(purchaseOrderUID);

      var item = PurchaseOrderItem.Parse(purchaseOrderItemUID);

      GetDefaultProductFields(order, fields);

      fields.UID = purchaseOrderItemUID;
      fields.Position = item.Position;

      item.Update(fields);
      item.Save();

      return GetPurchaseOrderDto(purchaseOrderUID);
    }

    #endregion Public methods


    #region Private methods

    private void GetDefaultProductFields(PurchaseOrder order, PurchaseOrderItemFields fields) {
      
      var product = ProductEntry.ParseUID(fields.VendorProductUID);

      Assertion.Require(!product.Id.Equals(-1), $"Por favor ingrese un producto valido");

      fields.UnitPrice = fields.Price;
      fields.ProductUID = product.UID;
      fields.ProductUnitUID = "BJG2T8Q6-4E73-412B-84C7-4F97OPERMV52"; //todo buscar en common storage por nombre
      fields.ProductName = product.Name;
      fields.ProductCode = product.InternalCode;
      fields.Description = product.Description;
    }

    #endregion Private methods

  } // class PurchaseOrderUseCases 

} // namespace Empiria.Trade.Inventory.PurchaseOrders.UseCases
