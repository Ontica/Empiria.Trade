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
using System.Net.NetworkInformation;
using Empiria.Trade.Core.Common;
using Empiria.Trade.Orders;
using Empiria.Trade.Procurement.Domain;

namespace Empiria.Trade.Procurement.Adapters {


  /// <summary>Methods used to map purchase order.</summary>
  static internal class PurchaseOrderMapper {


    #region Public methods


    static internal PurchaseOrdersDataDto MapDataDto(FixedList<PurchaseOrderEntry> entries,
      PurchaseOrderQuery query) {

      return new PurchaseOrdersDataDto {
        Query = query,
        Columns = GetColumns(),
        Entries = MapList(entries)
      };
    }


    static internal PurchaseOrderDto MapOrder(PurchaseOrderEntry order) {

      var statusName = OrderStatusEnumExtensions.GetOrderStatusName(order.Status);

      var dto = new PurchaseOrderDto();
      dto.OrderNumber = order.OrderNumber;
      dto.Supplier = new NamedEntityDto(order.Supplier);
      dto.Customer = new NamedEntityDto(order.Customer);
      dto.Notes = order.Notes;
      dto.PaymentCondition = order.PaymentCondition;
      dto.ShippingMethod = order.ShippingMethod;
      dto.OrderTime = order.OrderTime;
      dto.ScheduledTime = order.ScheduledTime;
      dto.ReceptionTime = order.ReceptionTime;
      dto.Items = MapItems(order);
      dto.Totals = MapTotals(order);
      dto.Status = new NamedEntityDto(order.Status.ToString(), statusName);
      //dto.Contact = new NamedEntityDto(order.CustomerContact.UID, order.CustomerContact.Name);
      //dto.SalesAgent = new NamedEntityDto(order.SalesAgent);
      //dto.Currency = new NamedEntityDto("", "");
      return dto;
    }

    private static PurchaseOrderTotal MapTotals(PurchaseOrderEntry order) {
      var totals = new PurchaseOrderTotal();
      totals.ItemsTotal = order.ItemsTotal;
      totals.ShipmentTotal = order.ShipmentTotal;
      totals.Discount = order.Discount;
      totals.Taxes = order.OrderTotal;
      totals.OrderTotal = order.OrderTotal;
      return totals;
    }

    static internal FixedList<PurchaseOrderItemDto> MapItems(PurchaseOrderEntry order) {

      return new FixedList<PurchaseOrderItemDto>();
    }



    #endregion Public methods


    #region Private methods


    private static FixedList<DataTableColumn> GetColumns() {
      List<DataTableColumn> columns = new List<DataTableColumn>();

      columns.Add(new DataTableColumn("orderNo", "Número de orden", "text-link"));
      columns.Add(new DataTableColumn("supplier", "Proveedor", "text"));
      //columns.Add(new DataTableColumn("customer", "Cliente", "text"));
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
      dto.OrderTotal = x.OrderTotal;

      return dto;
    }


    #endregion Private methods


  } // class PurchaseOrderMapper

} // namespace Empiria.Trade.Inventory.PurchaseOrders.Adapters
