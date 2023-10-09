/* Empiria Trade *********************************************************************************************
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


    static internal IProductEntryDto MapToDto(Product entry) {
      var mappedItems = MapEntry(entry);

      return mappedItems;
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


    static private ProductShortEntryDto MapEntry(Product entry) {
      var dto = new ProductShortEntryDto();

      dto.ProductUID = entry.UID;
      dto.ProductCode = entry.Code;
      dto.Description = entry.Description;
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

      AttributesList attrs = JsonConvert.DeserializeObject<AttributesList>(entry.Attributes);

      return attrs.Attributes.ToFixedList();
    }


    static private FixedList<Presentation> GetPresentations(Product entry) {
      
      Random rnd = new Random();
      int num = rnd.Next();

      var presentations = new List<Presentation>();

      Presentation presentation = new Presentation();

      presentation.PresentationUID = "ead65e0b-90a8-4bb1-859b-53730388c385";
      presentation.Description = entry.Description; // grupo, terminado, stock
      presentation.Units = num.ToString(); //Stock;
      presentation.Vendors = GetVendors(entry);

      presentations.Add(presentation);

      return presentations.ToFixedList();
    }


    static private FixedList<Vendor> GetVendors(Product entry) {

      Random rnd = new Random(1000);
      decimal num = rnd.Next();

      var vendors = new List<Vendor>();

      Vendor vendor = new Vendor() {
        VendorUID = "eed65e0b-79b8-4ab1-859a-53730388c385",
        VendorName = GetVendorName(1),
        Sku = $"sku-000-{num}",
        Stock = num,
        Price = num
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


  } // class TRDProductMapper

} // namespace Empiria.Trade.Products.Adapters
