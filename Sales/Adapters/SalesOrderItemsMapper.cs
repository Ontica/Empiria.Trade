/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Order Management                           Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Order.dll                    Pattern   : Mapper class                            *
*  Type     : OrderITemMapper                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Methods used to map OrderItem.                                                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Trade.Products.Adapters;
using Empiria.Trade.Products;

namespace Empiria.Trade.Sales.Adapters {

  /// <summary>Methods used to map OrderItem.  </summary>
  static public class SalesOrderItemsMapper {

    #region Public methods

    static public SalesOrderItemDto Map(SalesOrderItem orderItem) {
      var dto = new SalesOrderItemDto {
        OrderItemUID = orderItem.UID,
        Quantity = orderItem.Quantity,
        BasePrice = orderItem.BasePrice,
        SalesPrice = orderItem.SalesPrice,
        AdditionalDiscount = orderItem.Discount,
        Shipment = orderItem.Shipment,
        Taxes = orderItem.TaxesIVA,
        Total = orderItem.Total,
        Notes = orderItem.Notes,
        //Product = MapProductShortDto(orderItem),
        Presentation = MapPresentation(orderItem),
        Vendor = MapVendor(orderItem)
      };

      return dto;
    }

    #endregion Public methods

    #region Private methods

    static private VendorDto MapVendor(SalesOrderItem orderItem) {
      var dto = new VendorDto {
        VendorProductUID = orderItem.VendorProduct.UID,
        VendorUID = orderItem.VendorProduct.Vendor.UID,
        VendorName = orderItem.VendorProduct.Vendor.Name,
        Sku = orderItem.VendorProduct.SKU,
        Stock = orderItem.VendorProduct.InputQuantity,
        Price = 0
      };

      return dto;
    }

    static private PresentationDto MapPresentation(SalesOrderItem orderItem) {
      var dto = new PresentationDto {
        PresentationUID = orderItem.VendorProduct.ProductPresentation.UID,
        Description = orderItem.VendorProduct.ProductPresentation.PresentationDescription,
        Units = orderItem.VendorProduct.ProductPresentation.QuantityAmount
      };

      return dto;
    }

    static private ProductShortEntryDto MapProductShortDto(SalesOrderItem orderItem) {
      Product product = Product.Parse(orderItem.VendorProduct.ProductFields.ProductId);

      return  TRDProductMapper.MapEntry(product);
    }

    

    #endregion Private methods

  } // class SalesOrderItemsMapper

} // namespace Empiria.Trade.Sales.Adapters
