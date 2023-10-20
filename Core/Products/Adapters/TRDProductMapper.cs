﻿/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Mapper class                            *
*  Type     : TRDProductMapper                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Methods used to map TRDProducts.                                                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Empiria.Trade.Products.Adapters {

  /// <summary>Methods used to map TRDProducts.</summary>
  internal class TRDProductMapper {


    #region Public methods


    static internal IProductEntryDto MapToDto(ProductFields entry) {

      return MapProductFields(entry);

    }


    //static internal FixedList<IProductEntryDto> MapToListDto(FixedList<Product> products) {
    //  return MapToEntriesDto(products);
    //}


    static internal FixedList<IProductEntryDto> MapToEntriesDto(FixedList<Product> entries) {

      var mappedItems = entries.Select((x) => MapEntry((Product) x));

      return new FixedList<IProductEntryDto>(mappedItems);
    }


    #endregion Public methods

    #region Private methods


    static private IProductEntryDto MapProductFields(ProductFields entry) {
      var dto = new ProductEntryDto();

      dto.ProductUID = entry.ProductUID;
      dto.ProductGroupUID = entry.ProductGroup.UID;
      dto.ProductSubgroupUID = entry.ProductSubgroup.UID;
      dto.ProductCode = entry.ProductCode;
      dto.ProductUPC = entry.ProductUPC;
      dto.ProductName = entry.ProductName;
      dto.ProductDescription = entry.ProductDescription;
      dto.Category = entry.Category;
      dto.ProductWeight = entry.ProductWeight;
      dto.ProductLength = entry.ProductLength;
      dto.ProductStatus = entry.Status;

      return dto;
    }


    static private ProductShortEntryDto MapEntry(Product entry) {
      var dto = new ProductShortEntryDto();

      dto.ProductUID = entry.ProductUID;
      dto.ProductCode = entry.Code;
      dto.Description = entry.ProductName;
      dto.ProductType = GetProductType(entry);
      dto.Presentations = GetPresentations(entry);


      return dto;
    }


    static private ProductType GetProductType(Product entry) {

      var type = new ProductType();

      var attributes = GetAttributes(entry);

      type.ProductTypeUID = "ddddd-dc17-49f5-b378-aa692dc21cdd";
      type.Name = entry.ProductGroup.Name; //GroupName
      type.Attributes = attributes;

      return type;
    }


    static private FixedList<Attributes> GetAttributes(Product entry) {

      AttributesList attrs = new AttributesList();

      if (entry.Attributes != "") {
        attrs = JsonConvert.DeserializeObject<AttributesList>(entry.Attributes);
      }

      return attrs.Attributes.ToFixedList();

    }


    static private FixedList<Presentation> GetPresentations(Product entry) {

      var presentations = new List<Presentation>();

      Presentation presentation = new Presentation();

      presentation.PresentationUID = entry.ProductPresentation.PresentationUID;
      presentation.Description = entry.ProductPresentation.PresentationDescription;
      presentation.Units = Convert.ToString(entry.ProductPresentation.QuantityAmount); //CANTIDAD QUE CONTIENE LA PRESENTACION;
      presentation.Vendors = GetVendors(entry);

      presentations.Add(presentation);

      return presentations.ToFixedList();
    }


    static private FixedList<Vendor> GetVendors(Product entry) {

      var vendors = new List<Vendor>();

      Vendor vendor = new Vendor() {
        VendorUID = entry.Vendor.UID,
        VendorName = entry.Vendor.Name,
        Sku = entry.SKU,
        Stock = entry.InventoryEntry.InputQuantity,
        Price = entry.Vendor.Id == 1 ? entry.PriceList1 : entry.PriceList7 // TODO VALIDAR PRECIOS EN DOMINIO
      };

      vendors.Add(vendor);

      return vendors.ToFixedList();
    }


    #endregion Private methods


  } // class TRDProductMapper

} // namespace Empiria.Trade.Products.Adapters
