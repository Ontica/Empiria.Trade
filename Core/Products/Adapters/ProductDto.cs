/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Data Transfer Object                    *
*  Type     : ProductDto                                 License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return the entries of Products.                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Empiria.Trade.Products.Adapters {

  /// <summary>Output DTO used to return the entries of Products.</summary>
  public class ProductDto {
    
    
    public string ProductUID {
      get; set;
    }


    public string ProductCode {
      get; set;
    }


    public string Description {
      get; set;
    }


    public ProductTypeDto ProductType {
      get; set;
    }


    public string ProductImageUrl {
      get;
      internal set;
    }


  } // class ProductDto


  public class VendorDto {


    public string VendorProductUID {
      get; set;
    } = string.Empty;


    public string VendorUID {
      get; set;
    } = string.Empty;


    public string VendorName {
      get; set;
    } = string.Empty;


    public string Sku {
      get; set;
    }


    public decimal Stock {
      get; set;
    }


    public decimal Price {
      get; set;
    }


  } // class VendorDto


  public class ProductPresentationDto {

    public string PresentationUID {
      get; set;
    } = string.Empty;


    public string Description {
      get; set;
    } = string.Empty;


    public decimal Units {
      get; set;
    }


  } // class ProductPresentationDto


  public class ProductTypeDto {


    public string ProductTypeUID {
      get; set;
    }


    public string Name {
      get; set;
    }


    public FixedList<AttributesDto> Attributes {
      get;  set;
    } = new FixedList<AttributesDto>();


  } // class ProductTypeDto


  public class AttributesListDto {

    public FixedList<AttributesDto> Attributes {
      get; set;
    } = new FixedList<AttributesDto>();

  } // AttributesListDto


  public class AttributesDto {

    public string Name {
      get; set;
    } = string.Empty;


    public string Value {
      get; set;
    } = string.Empty;


    public FixedList<AttributesDto> GetAttributes(string attributes) {

      AttributesListDto attrs = new AttributesListDto();

      if (attributes != "") {
        attrs = JsonConvert.DeserializeObject<AttributesListDto>(attributes);
      }

      return attrs.Attributes.ToFixedList<AttributesDto>();

    }

  } // class AttributesDto


} // namespace Empiria.Trade.Products.Adapters
