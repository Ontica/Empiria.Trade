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
using System.Linq;
using Empiria.StateEnums;
using Empiria.Trade.Core;
using Empiria.Trade.Core.Common;
using Empiria.Trade.Products;

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

      var orderItems = PurchaseOrderItem.GetListFor(order);

      return new PurchaseOrderDto {
        UID = order.UID,
        OrderNumber = order.OrderNo,
        Supplier = order.Provider.MapToNamedEntity(),
        PaymentConditions = EnumExtensions.GetPaymentConditionEnum(order.PaymentConditions),
        ShippingMethod = EnumExtensions.GetShippingMethodEnum(order.ShippingMethod),
        Currency = order.Currency.MapToNamedEntity(),
        ExchangeRate = order.ExchangeRate,
        OrderTime = order.StartDate,
        ScheduledTime = order.ScheduledTime,
        PostingTime = order.PostingTime,
        Notes = order.Observations,
        Status = order.Status.MapToDto(),
        Items = orderItems.Count > 0 ? MapItems(orderItems) : new FixedList<PurchaseOrderItemDto>(),
        Totals = MapTotals(orderItems),
        Actions = MapActions(order.Status)
      };

    }

    private static PurchaseOrderActions MapActions(EntityStatus status) {
      return new PurchaseOrderActions {
        CanClose = status != EntityStatus.Closed ? true : false,
        CanDelete = status != EntityStatus.Closed ? true : false,
        CanEdit = status != EntityStatus.Closed ? true : false,
        CanEditItems = status != EntityStatus.Closed ? true : false,
        CanExport = true //status == EntityStatus.Closed ? true : false
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
      columns.Add(new DataTableColumn("orderTotal", "Total", "decimal"));
      columns.Add(new DataTableColumn("orderStatus", "Estatus", "text-tag"));

      return columns.ToFixedList();
    }


    private static string GetProductAttributes(ProductEntry product) {

      string attrDiametro = product.Diametro != string.Empty ?
                            $"diametro {product.Diametro}. " : "";

      string attrLargo = product.Largo != string.Empty ?
                            $"largo {product.Largo}. " : "";

      return attrDiametro + attrLargo;
    }



    private static object GetProductAttributesShort(ProductEntry product) {
      string attrDiametro = product.Diametro != string.Empty ?
                            $"{product.Diametro} " : "";

      string _by = attrDiametro != string.Empty ? "X " : string.Empty;

      string attrLargo = product.Largo != string.Empty ?
                            $"{_by}{product.Largo}" : "";

      return attrDiametro + attrLargo;
    }


    static private FixedList<PurchaseOrderDescriptorDto> MapList(FixedList<PurchaseOrder> entries) {

      var mappedList = entries.Select((x) => MapPurchaseOrderDescriptor(x));

      return new FixedList<PurchaseOrderDescriptorDto>(mappedList);
    }


    static private FixedList<PurchaseOrderItemDto> MapItems(FixedList<PurchaseOrderItem> items) {

      var mappedItems = items.Select((x) => MapPurchaseOrderItems(x));

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
      dto.OrderTotal = Math.Round(x.PurchaseOrderItems.Sum(a => a.CalculateTotalPrice()), 2);

      return dto;
    }


    private static PurchaseOrderItemDto MapPurchaseOrderItems(PurchaseOrderItem x) {

      var product = ProductEntry.ParseUID(x.Product.UID);
      
      return new PurchaseOrderItemDto {
        UID = x.UID,
        VendorProductUID = x.Product.UID,
        ProductCode = x.ProductCode,
        ProductName = x.ProductName,
        PresentationName = x.Product.BaseUnit.Description,
        Description = x.Description,
        ProductAttrs = GetProductAttributes(product),
        ProductAttrsShort = GetProductAttributesShort(product),
        Notes = x.Notes,
        PackingSmallBag = x.PackingSmallBag,
        PackagingSize = x.PackagingSize,
        Quantity = x.Quantity,
        TotalUnits = x.CalculateTotalUnits(),
        Price = x.UnitPrice,
        Total = x.CalculateTotalPrice()
      };
    }


    private static PurchaseOrderTotal MapTotals(FixedList<PurchaseOrderItem> orderItems) {

      var _total = orderItems.Sum(x => x.CalculateTotalPrice());

      return new PurchaseOrderTotal {
        ItemsTotal = Math.Round(_total, 2, MidpointRounding.AwayFromZero),
        OrderTotal = Math.Round(_total, 2, MidpointRounding.AwayFromZero),
        ItemsCount = orderItems.Count
      };
    }

    #endregion Private methods


  } // class PurchaseOrderMapper

} // namespace Empiria.Trade.Inventory.PurchaseOrders.Adapters
