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
using Empiria.Financial;
using Empiria.Orders;
using Empiria.Parties;
using Empiria.Trade.Procurement.Adapters;

namespace Empiria.Trade.Procurement {

  /// <summary>Represents a purchase order entry.</summary>
  public class PurchaseOrder : Order {

    #region Constructors and parsers

    public PurchaseOrder(OrderType orderType) : base(orderType) {

      base.OrderNo = "OC-" + EmpiriaString.BuildRandomString(8)
                                  .ToUpperInvariant();
    }


    public override FixedList<IPayableEntity> GetPayableEntities() {

      return new FixedList<IPayableEntity>();
    }

    #endregion Constructors and parsers


    internal void Update(PurchaseOrderFields fields) {
      
      base.Update(fields);
    }

  } // class PurchaseOrder

} // namespace Empiria.Trade.Procurement
