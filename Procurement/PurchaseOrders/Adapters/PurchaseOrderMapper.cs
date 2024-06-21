/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Mapper class                            *
*  Type     : PurchaseOrderMapper                        License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Methods used to map purchase order.                                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using Empiria.Trade.Core.Common;
using Empiria.Trade.Procurement.Domain;

namespace Empiria.Trade.Procurement.Adapters {


  /// <summary>Methods used to map purchase order.</summary>
  static internal class PurchaseOrderMapper {


    #region Public methods


    static public PurchaseOrderDataDto MapDescriptorList(FixedList<PurchaseOrderEntry> list,
      PurchaseOrderQuery query) {

      return new PurchaseOrderDataDto {
        Query = query,
        Columns = GetColumns(),
        Entries = MapList(list)
      };
    }



    #endregion Public methods


    #region Private methods


    private static FixedList<DataTableColumn> GetColumns() {
      List<DataTableColumn> columns = new List<DataTableColumn>();

      columns.Add(new DataTableColumn("orderTypeName", "Tipo", "text"));
      columns.Add(new DataTableColumn("orderNo", "Número de orden", "text-link"));
      columns.Add(new DataTableColumn("customer", "Cliente", "text"));
      columns.Add(new DataTableColumn("orderTime", "Fecha registro", "date"));
      columns.Add(new DataTableColumn("orderStatus", "Estatus", "text-tag"));

      return columns.ToFixedList();
    }


    static private FixedList<PurchaseOrderDescriptorDto> MapList(
      FixedList<PurchaseOrderEntry> list) {

      var mappedList = list.Select((x) => MapPurchaseDescriptorList(x));

      return new FixedList<PurchaseOrderDescriptorDto>(mappedList);
    }


    static private PurchaseOrderDescriptorDto MapPurchaseDescriptorList(PurchaseOrderEntry x) {
      throw new NotImplementedException();
    }


    #endregion Private methods


  } // class PurchaseOrderMapper

} // namespace Empiria.Trade.Inventory.PurchaseOrders.Adapters
