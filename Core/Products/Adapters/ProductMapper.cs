/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Mapper class                            *
*  Type     : ProductMapper                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Methods used to map Products.                                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using Empiria.DataTypes;
using Empiria.Trade.Products.Domain;

namespace Empiria.Trade.Products.Adapters {

  /// <summary>Methods used to map Products.</summary>
  static internal class ProductMapper {


    #region Public methods


    static internal FixedList<IProductEntryDto> MapTo(FixedList<ProductFields> entries) {

      return MapToEntriesDto(entries);

    }


    #endregion Public methods


    #region Private methods


    static private FixedList<IProductEntryDto> MapToEntriesDto(FixedList<ProductFields> entries) {
      var mappedItems = entries.Select((x) => MapShortEntry((ProductFields) x));

      return new FixedList<IProductEntryDto>(mappedItems);
    }


    static private ProductShortEntryDto MapShortEntry(ProductFields entry) {
      var dto = new ProductShortEntryDto();

      dto.ProductUID = entry.UID;
      dto.ProductCode = entry.Product;
      dto.Description = entry.Description;
      dto.ProductType = GetProductType(entry);
      dto.Presentations = GetPresentations(entry);


      return dto;
    }


    static private ProductType GetProductType(ProductFields entry) {

      var type = new ProductType();

      var attributes = GetAttributes(entry);

      type.ProductTypeUID = "ddddd-dc17-49f5-b378-aa692dc21cdd";
      type.Name = entry.GroupName;
      type.Attributes = attributes;

      return type;
    }


    static private FixedList<Attributes> GetAttributes(ProductFields entry) {
      
      var attrs = new List<Attributes>();
      
      if (entry.ViewDetailsName != "") {
        var attr = new Attributes();
        attr.Name = "Terminado";
        attr.Value = entry.ViewDetailsName;
        attrs.Add(attr);
      }
      if (entry.HeadsName != "") {
        var attr = new Attributes();
        attr.Name = "Cabeza";
        attr.Value = entry.HeadsName;
        attrs.Add(attr);
      }
      if (entry.Degree != "") {
        var attr = new Attributes();
        attr.Name = "Grado";
        attr.Value = entry.Degree;
        attrs.Add(attr);
      }
      if (entry.Diameter != "") {
        var attr = new Attributes();
        attr.Name = "Tamaño";
        attr.Value = entry.Diameter;
        attrs.Add(attr);
      }
      if (entry.ThreadsName != "") {
        var attr = new Attributes();
        attr.Name = "Hilos";
        attr.Value = entry.ThreadsName;
        attrs.Add(attr);
      }

      return attrs.ToFixedList();
    }


    static private FixedList<Presentation> GetPresentations(ProductFields entry) {
      var presentations = new List<Presentation>();

      Presentation presentation = new Presentation();

      presentation.PresentationUID = "ead65e0b-90a8-4bb1-859b-53730388c385";
      presentation.Description = $"{entry.GroupName} {entry.ViewDetailsName} {entry.Stock}";
      presentation.Units = entry.Stock;
      presentation.Vendors = GetVendors(entry);

      presentations.Add(presentation);

      return presentations.ToFixedList();
    }


    static private FixedList<Vendor> GetVendors(ProductFields entry) {
      var vendors = new List<Vendor>();

      Vendor vendor = new Vendor() {
        VendorUID = "eed65e0b-79b8-4ab1-859a-53730388c385",
        VendorName = GetVendorName(entry.CompanyId),
        Sku = "sku-000",
        Stock = entry.Stock != "" ? Convert.ToDecimal(entry.Stock) : 0,
        Price = entry.MinimumPrice
      };

      vendors.Add(vendor);

      return vendors.ToFixedList();
    }


    static private string GetVendorName(int companyId) {

      if (companyId == 1) {
        return "Productos NK";
      }
      if (companyId == 2) {
        return "Productos Microsip";
      }
      if (companyId == 3) {
        return "Productos NK Hidroplomex";
      }

      return string.Empty;
    }


    #endregion Private methods

  } // class ProductMapper

} // namespace Empiria.Trade.Products.Adapters
