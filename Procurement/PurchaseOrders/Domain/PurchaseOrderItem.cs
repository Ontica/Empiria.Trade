/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Information Holder                      *
*  Type     : PurchaseOrderItem                          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a purchase order item.                                                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Orders;
using Empiria.Trade.Procurement.Adapters;

namespace Empiria.Trade.Procurement {


  /// <summary>Represents a purchase order item.</summary>
  public class PurchaseOrderItem : OrderItem {


    #region Constructors and parsers


    public PurchaseOrderItem() {
      // no-op
    }


    public PurchaseOrderItem(PurchaseOrderItemFields fields) {
      MapToPurchaseOrderItem(fields);
    }

    
    static public new PurchaseOrderItem Parse(int id) {
      return BaseObject.ParseId<PurchaseOrderItem>(id);
    }


    static public new PurchaseOrderItem Parse(string uid) {
      return BaseObject.ParseKey<PurchaseOrderItem>(uid);
    }


    static public PurchaseOrderItem ParseEmpty() {
      return new PurchaseOrderItem();
    }


    #endregion Constructors and parsers


    #region Properties





    #endregion Properties


    #region Private methods


    private void MapToPurchaseOrderItem(PurchaseOrderItemFields fields) {
      throw new NotImplementedException();
    }


    #endregion Private methods

  } // class PurchaseOrderItem

} // namespace Empiria.Trade.Procurement.PurchaseOrders.Domain
