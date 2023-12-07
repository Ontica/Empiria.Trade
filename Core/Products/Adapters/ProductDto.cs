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

namespace Empiria.Trade.Products.Adapters {

  /// <summary>Output DTO used to return the entries of Products.</summary>
  public class ProductDto : IProductEntryDto {
    
    
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

    //TODO PROBAR QUE NO GENERE PROBLEMA EN SALES
    public FixedList<PresentationDto> Presentations {
      get; set;
    } = new FixedList<PresentationDto>();

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


  public class PresentationDto {

    public string PresentationUID {
      get; set;
    } = string.Empty;


    public string Description {
      get; set;
    }

    public decimal Units {
      get; set;
    }

    //TODO PROBAR QUE NO GENERE PROBLEMA EN SALES
    public List<VendorDto> Vendors {
      get; set;
    } = new List<VendorDto>();

  } // class PresentationDto


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


  } // class ProductType


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


  } // class AttributesDto


} // namespace Empiria.Trade.Products.Adapters
