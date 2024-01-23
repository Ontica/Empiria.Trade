/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Packaging Management                       Component : Interface adapters                      *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Mapper class                            *
*  Type     : PackagingMapper                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Methods used to map packaging.                                                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Empiria.Trade.Core;
using Empiria.Trade.Core.Catalogues;
using Empiria.Trade.Orders;
using Empiria.Trade.Products;
using Empiria.Trade.Products.Adapters;

namespace Empiria.Trade.Sales.ShippingAndHandling.Adapters {


  /// <summary>Methods used to map packaging.</summary>
  static internal class PackagingMapper {


    #region Public methods


    internal static PackingDto MapPackingDto(PackingEntry packaging) {

      var packagedItems = MapToPackagedItems(packaging);
      var packingData = MapPackingData(packagedItems, packaging.PackagedItems);
      var missingItems = MapToMissingItems_(packaging.MissingItems);

      return new PackingDto() {
        Data = packingData,
        PackagedItems = packagedItems,
        MissingItems = missingItems
      };

    }


    #endregion Public methods


    #region Private methods


    static private FixedList<PackingItemDto> GetOrderItems(PackagedForItem entry) {
      var packingOrderItems = new List<PackingItemDto>();

      List<PackingItem> items = new List<PackingItem>();

      foreach (var _item in entry.OrderItems) {
        items.Add(_item);
      }

      foreach (var item in items) {

        var packingOrderItem = new PackingItemDto();
        packingOrderItem.UID = item.UID;
        packingOrderItem.Quantity = item.Quantity;
        
        packingOrderItem.OrderItemUID= item.OrderItemUID;
        packingOrderItem.Product = GetProductDto(item.Product, item.ProductImageUrl);
        packingOrderItem.Presentation = GetPresentationDto(item.Presentation);
        packingOrderItem.Vendor = GetVendorDto(item.VendorProductId);

        MergeWarehousesDto(packingOrderItem, item);

        packingOrderItems.Add(packingOrderItem);
      }

      return packingOrderItems.ToFixedList();
    }


    static private ProductPresentationDto GetPresentationDto(ProductPresentation presentation) {
      var presentationDto = new ProductPresentationDto();
      
      presentationDto.PresentationUID = presentation.UID;
      presentationDto.Description = presentation.PresentationDescription;
      presentationDto.Units = presentation.QuantityAmount;

      return presentationDto;
    }


    static private ProductDto GetProductDto(ProductFields product, string productImageUrl) {

      ProductTypeDto type = new ProductTypeDto {
        ProductTypeUID = product.UID,
        Name = product.ProductGroup.Name,
        Attributes = new Attributes().GetAttributes(product.Attributes)
      };

      var productDto = new ProductDto();

      productDto.ProductUID = product.UID;
      productDto.ProductCode = product.ProductCode;
      productDto.Description = product.ProductName;
      productDto.ProductImageUrl = productImageUrl;
      productDto.ProductType = type;

      return productDto;
    }


    static private VendorDto GetVendorDto(int vendorProductId) {
      var vendorDto = new VendorDto();

      var vendorProduct = VendorProduct.Parse(vendorProductId);
      vendorDto.VendorProductUID = vendorProduct.VendorProductUID;
      vendorDto.VendorUID = vendorProduct.Vendor.UID;
      vendorDto.VendorName = vendorProduct.Vendor.Name;
      vendorDto.Sku = vendorProduct.SKU;
      vendorDto.Stock = 0; // TODO SACAR STOCK
      vendorDto.Price = 0; // TODO SACAR PRICE

      return vendorDto;
    }


    static private FixedList<WarehouseBinDto> GetWarehouseBinList(
                      FixedList<WarehouseBinForPacking> warehouseBins) {

      var whBinDto = new List<WarehouseBinDto>();

      foreach (var bin in warehouseBins) {
        var whBin = new WarehouseBinDto();
        whBin.UID = bin.UID;
        whBin.OrderItemUID = bin.OrderItemUID;
        whBin.Name = bin.Name;
        whBin.WarehouseName = bin.WarehouseName;
        whBin.Stock = bin.Stock;
        whBinDto.Add(whBin);
      }

      return whBinDto.ToFixedList();
    }


    static private PackagedDataDto MapPackingData(FixedList<PackageForItemDto> packingItems,
                                                  FixedList<PackagedForItem> packagedItems) {
      var data = new PackagedDataDto();

      decimal _vol = 0;
      foreach (var item in packingItems) {
        var type = PackageType.Parse(item.PackageTypeUID);

        if (type != null) {
          type.GetVolumeAttributes();
          _vol += type.TotalVolume;
        }

      }
      if (packingItems.Count>0) {
        data.OrderUID = packingItems.Select(x => x.OrderUID).First();
      }
      
      data.TotalVolume = _vol;
      data.TotalWeight = packagedItems.Sum(x=>x.PackageWeight);
      data.TotalPackages = packingItems.Count();

      return data;
    }


    static private FixedList<MissingItemDto> MapToMissingItems_(FixedList<MissingItem> missing) {

      var missingItems = new List<MissingItemDto>();

      foreach (var miss in missing) {
        var missingItem = new MissingItemDto();
        missingItem.OrderItemUID = miss.OrderItemUID;
        missingItem.Quantity = miss.Quantity;
        missingItem.Product = GetProductDto(miss.Product, miss.ProductImageUrl);
        missingItem.Presentation = GetPresentationDto(miss.Presentation);
        missingItem.Vendor = GetVendorDto(miss.VendorProductId);
        missingItem.WarehouseBins = GetWarehouseBinList(miss.WarehouseBins);
        missingItems.Add(missingItem);
      }

      return missingItems.ToFixedList();
    }


    private static FixedList<PackageForItemDto> MapToPackagedItems(PackingEntry packaging) {

      var packingItems = new List<PackageForItemDto>();

      foreach (var entry in packaging.PackagedItems) {
        var item = new PackageForItemDto();

        item.UID = entry.UID;
        item.OrderUID = entry.OrderUID;
        item.PackageID = entry.PackageID;
        item.PackageTypeUID = entry.PackageTypeUID;
        item.PackageTypeName = entry.PackageTypeName;

        var exist = packingItems.Find(x => x.UID == item.UID);

        if (exist == null) {
          item.OrderItems = GetOrderItems(entry);
          packingItems.Add(item);
        }
      }

      return packingItems.ToFixedList();
    }


    static private void MergeWarehousesDto(PackingItemDto packingOrderItem, PackingItem item) {

      var warehouse = new WarehouseDto();
      warehouse.UID = item.WarehouseForPacking.UID;
      warehouse.Code = item.WarehouseForPacking.Code;
      warehouse.Name = item.WarehouseForPacking.Name;
      warehouse.Stock = item.WarehouseForPacking.Stock;

      var warehouseBin = new WarehouseBinDto();
      warehouseBin.UID = item.WarehouseBinForPacking.UID;
      warehouseBin.OrderItemUID = item.WarehouseBinForPacking.OrderItemUID;
      warehouseBin.Name = item.WarehouseBinForPacking.Name;
      warehouseBin.WarehouseName = item.WarehouseBinForPacking.WarehouseName;
      warehouseBin.Stock = item.WarehouseBinForPacking.Stock;

      packingOrderItem.Warehouse = warehouse;
      packingOrderItem.WarehouseBin = warehouseBin;

    }


    #endregion Private methods


  } // class PackagingMapper

} // namespace Empiria.Trade.ShippingAndHandling.Adapters
