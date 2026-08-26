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
using System.Collections.Generic;
using Empiria.Trade.Core;
using Empiria.Trade.Core.Catalogues;
using Empiria.Trade.Products;
using Empiria.Trade.Products.Adapters;
using Empiria.Trade.Sales.UseCases;

namespace Empiria.Trade.Sales.Adapters {

  /// <summary>Methods used to map OrderItem.  </summary>
  static public class SalesOrderItemsMapper {

    #region Public methods

    static public SalesOrderItemDto Map(SalesOrderItem orderItem) {
      
      var dto = new SalesOrderItemDto {
        OrderItemUID = orderItem.UID == string.Empty ? orderItem.OrderItemUID : orderItem.UID,
        Quantity = orderItem.ItemQuantity,
        UnitPrice = orderItem.ProductPrice,
        SalesPrice = orderItem.SalesPrice,
        DiscountPolicy = orderItem.DiscountPolicy,
        Discount1 = orderItem.ItemDiscount,
        Discount2 = orderItem.AdditionalDiscount,
        //Shipment = orderItem.Shipment,
        //Taxes = orderItem.TaxesIVA,
        //Total = orderItem.Total,
        Subtotal = orderItem.ItemSubtotal,
        Notes = orderItem.Notes,
        Product = MapBaseProductDto(orderItem.ProductEntry),
        Presentation = MapPresentation(orderItem),
        Vendor = MapVendor(orderItem.ProductEntry)
      };

      return dto;
    }

    private static ProductDto MapBaseProductDto(ProductEntry product) {
      var dto = new ProductDto {
        ProductUID = product.UID,
        ProductCode = product.InternalCode,
        Description = product.Description,
        ProductImageUrl = string.Empty,
        ProductType = MapProductType(product)
      };

      return dto;
    }

    private static ProductTypeDto MapProductType(ProductEntry product) {

      var dto = new ProductTypeDto {
        ProductTypeUID = "-1",//product.ProductType.UID,
        Name = product.ProductType.DisplayName,
        Attributes = GetProductAttributes(product)
      };

      return dto;
    }

    #endregion Public methods

    #region Private methods


    private static FixedList<Attributes> GetProductAttributes(ProductEntry product) {

      List<Attributes> attrs = new List<Attributes>();

      if (product.Diametro != string.Empty) {
        attrs.Add(
          new Attributes {
            Name = "Diametro",
            Value = product.Diametro
          });
      }

      if (product.Largo != string.Empty) {
        attrs.Add(
          new Attributes {
            Name = "Largo",
            Value = product.Largo
          });
      }

      if (product.Hilos != string.Empty) {
        attrs.Add(
          new Attributes {
            Name = "Hilos",
            Value = product.Hilos
          });
      }

      if (product.Peso > 0) {
        attrs.Add(
          new Attributes {
            Name = "Peso",
            Value = product.Peso.ToString()
          });
      }

      return attrs.ToFixedList();
    }


    static private VendorDto MapVendor(ProductEntry productEntry) {

      var productStock = SalesOrderUseCases.GetItemExistence(productEntry.Id);

      return new VendorDto {
        VendorProductUID = productEntry.UID,
        VendorUID = productEntry.Vendor.UID,
        VendorName = productEntry.Vendor.Name,
        //Sku = productEntry.SKU,
        Stock = productStock,
        Price = 0
      };
    }

    static private ProductPresentationDto MapPresentation(SalesOrderItem orderItem) {
      
      return new ProductPresentationDto {
        PresentationUID = orderItem.ProductEntry.BaseUnit.UID,
        Description = orderItem.ProductEntry.BaseUnit.Description,
        Units = orderItem.ProductEntry.PackagingSize * orderItem.ItemQuantity
      };
    }

    #endregion Private methods

  } // class SalesOrderItemsMapper

} // namespace Empiria.Trade.Sales.Adapters
