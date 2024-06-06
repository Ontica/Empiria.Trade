﻿/* Empiria Trade *********************************************************************************************
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
using Newtonsoft.Json;
using Empiria.Trade.Core.Catalogues;

namespace Empiria.Trade.Sales.Adapters {

  /// <summary>Methods used to map OrderItem.  </summary>
  static public class SalesOrderItemsMapper {

    #region Public methods

    static public SalesOrderItemDto Map(SalesOrderItem orderItem) {
      var dto = new SalesOrderItemDto {
        OrderItemUID = orderItem.UID,
        Quantity = orderItem.Quantity,
        UnitPrice = orderItem.BasePrice,
        SalesPrice = orderItem.SalesPrice,
        DiscountPolicy = orderItem.DiscountPolicy,
        Discount1 = orderItem.Discount,
        Discount2 = orderItem.AdditionalDiscount,
        //Shipment = orderItem.Shipment,
        //Taxes = orderItem.TaxesIVA,
        //Total = orderItem.Total,
        Subtotal = orderItem.SubTotal,
        Notes = orderItem.Notes,
        Product = MapBaseProductDto(orderItem),
        Presentation = MapPresentation(orderItem),
        Vendor = MapVendor(orderItem)
      };

      return dto;
    }

    private static ProductDto MapBaseProductDto(SalesOrderItem orderItem) {
      var dto = new ProductDto {
        ProductUID = orderItem.VendorProduct.ProductFields.ProductUID,
        ProductCode = orderItem.VendorProduct.ProductFields.ProductCode,
        Description = orderItem.VendorProduct.ProductFields.ProductName,
        ProductImageUrl = orderItem.VendorProduct.ProductFields.ProductImageUrl,
        ProductType = MapProductType(orderItem)
      };

      return dto;
    }

    private static ProductTypeDto MapProductType(SalesOrderItem orderItem) {
      var dto = new ProductTypeDto {
        ProductTypeUID = "ddddd-dc17-49f5-b378-aa692dc21cdd",
        Name = orderItem.VendorProduct.ProductFields.ProductGroup.Name,
        Attributes = new Attributes().GetAttributes(orderItem.VendorProduct.ProductFields.Attributes) 
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

    static private ProductPresentationDto MapPresentation(SalesOrderItem orderItem) {
      var dto = new ProductPresentationDto {
        PresentationUID = orderItem.VendorProduct.ProductPresentation.UID,
        Description = orderItem.VendorProduct.ProductPresentation.PresentationDescription,
        Units = orderItem.VendorProduct.ProductPresentation.QuantityAmount
      };

      return dto;
    }

    
    

    #endregion Private methods

  } // class SalesOrderItemsMapper

} // namespace Empiria.Trade.Sales.Adapters
