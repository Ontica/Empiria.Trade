/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Information Holder                      *
*  Type     : PurchaseOrder                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a purchase order entry.                                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Billing;
using Empiria.Financial;
using Empiria.Orders;
using Empiria.Parties;
using Empiria.Trade.Procurement.Adapters;

namespace Empiria.Trade.Procurement {

  /// <summary>Represents a purchase order entry.</summary>
  public class PurchaseOrder : Order {

    #region Constructors and parsers

    public PurchaseOrder() {
      // Required by Empiria Framework for all partitioned types.
    }


    protected PurchaseOrder(OrderType orderType) : base(orderType) {
      // Required by Empiria Framework for all partitioned types.
    }


    public PurchaseOrder(PurchaseOrderFields fields, OrderType orderType) : base(orderType) {
      Assertion.Require(fields, nameof(fields));

      base.Update(fields);
      base.OrderNo = "OC-" + EmpiriaString.BuildRandomString(8)
                                  .ToUpperInvariant();
    }


    static public new PurchaseOrder Parse(int id) => ParseId<PurchaseOrder>(id);

    static public new PurchaseOrder Parse(string uid) => ParseKey<PurchaseOrder>(uid);

    static public new PurchaseOrder Empty => ParseEmpty<PurchaseOrder>();

    public override FixedList<IPayableEntity> GetPayableEntities() {

      return new FixedList<IPayableEntity>();
    }

    #endregion Constructors and parsers



  } // class PurchaseOrder

} // namespace Empiria.Trade.Procurement
