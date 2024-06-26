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


    static public PurchaseOrderDataDto MapDescriptorList(FixedList<PurchaseOrderEntry> entries,
      PurchaseOrderQuery query) {

      return new PurchaseOrderDataDto {
        Query = query,
        Columns = GetColumns(),
        Entries = MapList(entries)
      };
    }



    #endregion Public methods


    #region Private methods


    private static FixedList<DataTableColumn> GetColumns() {
      List<DataTableColumn> columns = new List<DataTableColumn>();

      columns.Add(new DataTableColumn("orderNo", "Número de orden", "text-link"));
      columns.Add(new DataTableColumn("supplier", "Proveedor", "text"));
      columns.Add(new DataTableColumn("customer", "Cliente", "text"));
      //columns.Add(new DataTableColumn("orderType", "Tipo", "text"));
      //columns.Add(new DataTableColumn("currency", "Moneda", "text"));
      columns.Add(new DataTableColumn("orderTime", "Fecha registro", "date"));
      columns.Add(new DataTableColumn("ScheduledTime", "Fecha programada", "date"));
      columns.Add(new DataTableColumn("orderStatus", "Estatus", "text-tag"));
      columns.Add(new DataTableColumn("orderTotal", "Total", "decimal"));

      return columns.ToFixedList();
    }


    static private FixedList<PurchaseOrderDescriptorDto> MapList(
      FixedList<PurchaseOrderEntry> entries) {

      var mappedList = entries.Select((x) => MapPurchaseDescriptorList(x));

      return new FixedList<PurchaseOrderDescriptorDto>(mappedList);
    }


    static private PurchaseOrderDescriptorDto MapPurchaseDescriptorList(PurchaseOrderEntry x) {
      PurchaseOrderDescriptorDto dto = new PurchaseOrderDescriptorDto();

      dto.UID = x.OrderUID;
      dto.OrderNo = x.OrderNumber;
      dto.Supplier = x.Supplier.ShortName;
      dto.Customer = x.Customer.ShortName;
      //dto.OrderType = x.OrderType.Name;
      //dto.Currency = x.Currency.ShortName;
      dto.OrderTime = x.OrderTime;
      dto.ScheduledTime = x.ScheduledTime;
      dto.OrderStatus = x.Status;
      dto.OrderTotal = x.Total;

      return dto;
    }


    #endregion Private methods


  } // class PurchaseOrderMapper

} // namespace Empiria.Trade.Inventory.PurchaseOrders.Adapters
