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
using Empiria.StateEnums;
using Empiria.Trade.Orders;

namespace Empiria.Trade.Procurement.Adapters {


  /// <summary>Methods used to map purchase order.</summary>
  static internal class PurchaseOrderMapper {

    #region Public methods V2

    static internal PurchaseOrdersDataDto MapDataDto(FixedList<PurchaseOrder> orders,
      PurchaseOrderQuery query) {

      return new PurchaseOrdersDataDto {
        Query = query,
        Columns = GetColumns_(),
        Entries = MapList(orders)
      };
    }


    static private FixedList<PurchaseOrderDescriptorDto> MapList(FixedList<PurchaseOrder> entries) {

      var mappedList = entries.Select((x) => MapPurchaseDescriptor(x));

      return new FixedList<PurchaseOrderDescriptorDto>(mappedList);
    }


    static private PurchaseOrderDescriptorDto MapPurchaseDescriptor(PurchaseOrder x) {
      PurchaseOrderDescriptorDto dto = new PurchaseOrderDescriptorDto();
      
      dto.UID = x.OrderUID;
      dto.OrderNo = x.OrderNo;
      dto.Provider = x.Provider.Name;
      dto.OrderType = x.OrderType.DisplayPluralName;
      dto.PostingTime = x.PostingTime;
      dto.RequestedTime = x.RequestedTime;
      dto.OrderStatus = x.Status.GetName();
      dto.OrderTotal = x.Items != null && x.Taxes != null ? x.Total : 0;

      return dto;
    }


    private static FixedList<DataTableColumn> GetColumns_() {
      List<DataTableColumn> columns = new List<DataTableColumn>();

      columns.Add(new DataTableColumn("orderNo", "Número de orden", "text-link"));
      columns.Add(new DataTableColumn("provider", "Proveedor", "text"));
      columns.Add(new DataTableColumn("orderType", "Tipo", "text"));
      columns.Add(new DataTableColumn("postingTime", "Fecha registro", "date"));
      columns.Add(new DataTableColumn("requestedTime", "Fecha solicitado", "date"));
      columns.Add(new DataTableColumn("orderTotal", "Total", "decimal"));
      columns.Add(new DataTableColumn("orderStatus", "Estatus", "text-tag"));

      return columns.ToFixedList();
    }


    static internal PurchaseOrderDto MapOrder(PurchaseOrder order) {

      return new PurchaseOrderDto {
        UID = order.UID,
        OrderNumber = order.OrderNo,
        Supplier = order.Provider.MapToNamedEntity(),
        Notes = order.Observations,
        PaymentCondition = order.PaymentConditions,
        OrderTime = order.StartDate,
        ScheduledTime = order.EndDate,
        Status = order.Status.MapToDto(),
        Items = MapItems(order.PurchaseOrderItems),
        Totals = MapTotals(order)
      };

    }

    #endregion Public methods V2


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
      dto.UID = order.OrderUID;
      dto.OrderNumber = order.OrderNumber;
      dto.Supplier = new NamedEntityDto(order.Supplier);
     // dto.Customer = new NamedEntityDto(order.Customer);
      dto.Notes = order.Notes;
      dto.PaymentCondition = order.PaymentCondition;
      dto.ShippingMethod = order.ShippingMethod;
      dto.OrderTime = order.OrderTime;
      dto.ScheduledTime = order.ScheduledTime;
      dto.ReceptionTime = order.ReceptionTime;
      dto.Items = MapItems(order.Items);
      dto.Totals = MapTotals(order);
      dto.Status = new NamedEntityDto(order.Status.ToString(), statusName);
      //dto.Contact = new NamedEntityDto(order.CustomerContact.UID, order.CustomerContact.Name);
      //dto.SalesAgent = new NamedEntityDto(order.SalesAgent);
      //dto.Currency = new NamedEntityDto("", "");
      return dto;
    }


    static internal FixedList<PurchaseOrderItemDto> MapItems(FixedList<PurchaseOrderItem> items) {

      var mappedItems = items.Select((x) => MapPurchasOrderItems(x));

      return new FixedList<PurchaseOrderItemDto>(mappedItems);
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
      columns.Add(new DataTableColumn("scheduledTime", "Fecha programada", "date"));
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
      dto.Provider = x.Supplier.Name;
      dto.Customer = x.Customer.Name;
      //dto.OrderType = x.OrderType.Name;
      //dto.Currency = x.Currency.ShortName;
      dto.RequestedTime = x.OrderTime;
      dto.ScheduledTime = x.ScheduledTime;
      dto.OrderStatus = OrderStatusEnumExtensions.GetOrderStatusName(x.Status);
      dto.OrderTotal = x.OrderTotal;

      return dto;
    }


    private static PurchaseOrderItemDto MapPurchasOrderItems(PurchaseOrderItem x) {

      return new PurchaseOrderItemDto {
        UID = x.UID,
        VendorProductUID = x.Product.UID,
        Quantity = x.Quantity,
        Total = x.Subtotal,
        ProductCode = x.ProductCode,
        ProductName = x.ProductName,
        PresentationName = x.Product.BaseUnit.Description,
        Price = x.UnitPrice,
        Notes = x.Description
      };
    }


    private static PurchaseOrderTotal MapTotals(PurchaseOrder order) {

      return new PurchaseOrderTotal {
        ItemsTotal = order.ItemsTotal
      };
    }


    private static PurchaseOrderTotal MapTotals(PurchaseOrderEntry order) {
      var totals = new PurchaseOrderTotal();
      totals.ItemsTotal = order.ItemsTotal;
      totals.ShipmentTotal = order.ShipmentTotal;
      totals.Discount = order.Discount;
      totals.Taxes = order.Taxes;
      totals.OrderTotal = order.OrderTotal;
      return totals;
    }


    #endregion Private methods


  } // class PurchaseOrderMapper

} // namespace Empiria.Trade.Inventory.PurchaseOrders.Adapters
