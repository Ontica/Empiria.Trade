/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Trade Core                                 Component : Data Layer                              *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Data Service                            *
*  Type     : InventoryOrderQueryClauses                 License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides methods to build query data clauses for inventory order.                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Core;
using Empiria.Trade.Inventory.Adapters;

namespace Empiria.Trade.Inventory.Data {


  /// <summary>Provides methods to build query data clauses for inventory order.</summary>
  internal class InventoryOrderQueryClauses {


    #region Public methods


    static internal string CreateClausesForInventoryOrder(InventoryQueryClauses clauses) {

      var filters = new Filter("InventoryOrderId > 0");

      if (clauses.InventoryOrderId > 0) {
        filters.AppendAnd($"InventoryOrderId = {clauses.InventoryOrderId}");
      }

      if (clauses.InventoryOrderTypeId > 0) {
        filters.AppendAnd($"InventoryOrderTypeId = {clauses.InventoryOrderTypeId}");
      }

      if (clauses.AssignedToId > 0) {
        filters.AppendAnd($"AssignedToId = {clauses.AssignedToId}");
      }

      if (clauses.ReferenceId > 0) {
        filters.AppendAnd($"ReferenceId = {clauses.ReferenceId}");
      }

      if (clauses.Keywords != string.Empty) {
        filters.AppendAnd($"" +
          $"{SearchExpression.ParseAndLikeKeywords("InventoryOrderKeywords", clauses.Keywords)}");
      }

      if (clauses.InventoryOrderStatus != InventoryStatus.Todos) {
        filters.AppendAnd($"InventoryOrderStatus = '{(char)clauses.InventoryOrderStatus}'");
      }

      return filters.ToString().Length > 0 ? $"WHERE {filters}" : "";
    }


    internal string CreateClausesForInventoryOrderItem(InventoryItemQueryClauses itemClauses) {

      return string.Empty;
    }


    #endregion Public methods


  } // class InventoryOrderQueryClauses

} // namespace Empiria.Trade.Inventory.Data
