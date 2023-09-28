/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Mapper class                            *
*  Type     : TRDProductMapper                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Methods used to map TRDProducts.                                                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Inventory.Products.Domain;

namespace Empiria.Trade.Inventory.Products.Adapters {

  /// <summary>Methods used to map TRDProducts.</summary>
  internal class TRDProductMapper {


    #region Public methods


    internal static TRDProductsEntryDto MapProduct(Product product) {
      var dto = new TRDProductsEntryDto();

      dto.ProductId = product.ProductId;
      dto.ProductTypeId = product.ProductTypeId;
      dto.ProductUID = product.UID;
      dto.ProductCode = product.Code;
      dto.ProductUPC = product.UPC;
      dto.ProductName = product.Name;
      dto.Description = product.Description;
      dto.Group = product.ProductGroup;
      dto.Subgroup = product.ProductSubgroup;
      dto.ProductKeywords = product.Keywords;
      dto.ProductWeight = product.Weight;
      dto.ProductLength = product.Length;
      dto.ProductStatus = product.Status;

      return dto;
    }


    #endregion Public methods




  } // class TRDProductMapper

} // namespace Empiria.Trade.Inventory.Products.Adapters
