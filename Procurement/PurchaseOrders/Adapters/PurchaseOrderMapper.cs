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

using Empiria.Trade.Core;
using System.Linq;

namespace Empiria.Trade.Procurement.Adapters {


  /// <summary>Methods used to map purchase order.</summary>
  static internal class PurchaseOrderMapper {

    #region Public methods

    static internal PurchaseOrdersDataDto MapDataDto(FixedList<PurchaseOrder> orders,
                                                      PurchaseOrderQuery query) {

      return new PurchaseOrdersDataDto {
        Query = query,
        Columns = GetColumns(),
        Entries = MapList(orders)
      };
    }


    static internal PurchaseOrderDto MapOrder(PurchaseOrder order) {
      
      return new PurchaseOrderDto {
        UID = order.UID,
        OrderNumber = order.OrderNo,
        Supplier = order.Provider.MapToNamedEntity(),
        Notes = order.Observations,
        PaymentConditions = EnumExtensions.GetPaymentConditionEnum(order.PaymentConditions),
        ShippingMethod = EnumExtensions.GetShippingMethodEnum(order.ShippingMethod),
        OrderTime = order.StartDate,
        ScheduledTime = order.ScheduledTime,
        Status = order.Status.MapToDto(),
        Items = MapItems(order),
        Totals = MapTotals(order)
      };

    }

    #endregion Public methods


    #region Private methods

    static private FixedList<DataTableColumn> GetColumns() {
      List<DataTableColumn> columns = new List<DataTableColumn>();

      columns.Add(new DataTableColumn("orderNo", "Número de orden", "text-link"));
      columns.Add(new DataTableColumn("provider", "Proveedor", "text"));
      columns.Add(new DataTableColumn("orderType", "Tipo", "text"));
      columns.Add(new DataTableColumn("postingTime", "Registro", "date"));
      //columns.Add(new DataTableColumn("requestedTime", "Solicitado", "date"));
      columns.Add(new DataTableColumn("totals.OrderTotal", "Total", "decimal"));
      columns.Add(new DataTableColumn("orderStatus", "Estatus", "text-tag"));

      return columns.ToFixedList();
    }


    static private FixedList<PurchaseOrderDescriptorDto> MapList(FixedList<PurchaseOrder> entries) {

      var mappedList = entries.Select((x) => MapPurchaseOrderDescriptor(x));

      return new FixedList<PurchaseOrderDescriptorDto>(mappedList);
    }


    static private FixedList<PurchaseOrderItemDto> MapItems(PurchaseOrder order) {

      var purchaseOrderItems = PurchaseOrderItem.GetListFor(order);

      var mappedItems = purchaseOrderItems.Select((x) => MapPurchaseOrderItems(x));

      return new FixedList<PurchaseOrderItemDto>(mappedItems);
    }


    static private PurchaseOrderDescriptorDto MapPurchaseOrderDescriptor(PurchaseOrder x) {
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


    private static PurchaseOrderItemDto MapPurchaseOrderItems(PurchaseOrderItem x) {

      return new PurchaseOrderItemDto {
        UID = x.UID,
        VendorProductUID = x.Product.UID,
        Quantity = x.Quantity,
        Total = x.Subtotal,
        ProductCode = x.ProductCode,
        ProductName = x.ProductName,
        PresentationName = x.Product.BaseUnit.Description,
        Price = x.UnitPrice,
        Weight = x.Weight,
        Notes = x.Description
      };
    }


    private static PurchaseOrderTotal MapTotals(PurchaseOrder order) {

      var purchaseOrderItems = order.PurchaseOrderItems;
      
      return new PurchaseOrderTotal {
        ItemsTotal = purchaseOrderItems.Sum(x => x.Subtotal),
        OrderTotal = purchaseOrderItems.Sum(x => x.Subtotal),
        ItemsCount = purchaseOrderItems.Count
      };
    }

    #endregion Private methods


  } // class PurchaseOrderMapper

} // namespace Empiria.Trade.Inventory.PurchaseOrders.Adapters
