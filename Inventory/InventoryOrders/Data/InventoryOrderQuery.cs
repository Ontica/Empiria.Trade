/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Data Transfer Object                    *
*  Type     : InventoryOrderQuery                        License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Query used to get inventory orders.                                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Inventory;
using Empiria.Locations;
using Empiria.StateEnums;
using Empiria.Trade.Core;

namespace Empiria.Trade.Inventory.Adapters {


  /// <summary>Query used to get inventory orders.</summary>
  public class InventoryOrderQuery {

    public string WarehouseUID {
      get; set;
    } = string.Empty;


    public string InventoryTypeUID {
      get; set;
    } = string.Empty;


    public string AssignedToUID {
      get; set;
    } = string.Empty;


    public string Keywords {
      get; set;
    } = string.Empty;


    public EntityStatus Status {
      get; set;
    } = EntityStatus.All;

  }


  static internal class InventoryOrderQueryExtensions {

    static internal string MapToFilterString(this InventoryOrderQuery query) {

      string keywords = BuildKeywordsFilter(query.Keywords);
      string warehouse = BuildWarehouseFilter(query.WarehouseUID);
      string inventoryType = BuildInventoryTypeFilter(query.InventoryTypeUID);
      string status = BuildStatusFilter(query.Status);

      var filter = new Filter(status);

      filter.AppendAnd(keywords);
      filter.AppendAnd("Order_Type_Id = 5011"); // TODO
      filter.AppendAnd(warehouse);
      filter.AppendAnd(inventoryType);

      return filter.ToString();
    }


    static internal string MapToSortString(this InventoryOrderQuery query) {

      return string.Empty;
    }

    #region Private methods

    private static string BuildInventoryTypeFilter(string invetoryTypeUID) {
      if (invetoryTypeUID.Length == 0) {
        return string.Empty;
      }

      var invetoryType = InventoryType.Parse(invetoryTypeUID);

      return $" Order_Category_Id = {invetoryType.Id}";
    }


    private static string BuildKeywordsFilter(string keywords) {

      if (keywords != string.Empty) {
        return $"{SearchExpression.ParseAndLikeKeywords("Order_Keywords", keywords)} ";
      }

      return string.Empty;
    }


    private static string BuildStatusFilter(EntityStatus status) {

      if (status == EntityStatus.All) {
        return $"Order_Status <> 'X'";
      }

      return $"(Order_Status = '{(char) status}' AND Order_Id <> -1)";
    }


    private static string BuildWarehouseFilter(string warehouseUID) {
      if (warehouseUID.Length == 0) {
        return string.Empty;
      }

      var warehouse = Location.Parse(warehouseUID);

      return $"Order_Location_Id = {warehouse.Id}";
    }

    #endregion Private methods
  }

}
