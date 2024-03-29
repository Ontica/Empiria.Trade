﻿/* Empiria Trade *********************************************************************************************
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
using Empiria.Trade.Core.Catalogues;
using Newtonsoft.Json;

namespace Empiria.Trade.Products.Adapters
{

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
      get; set;
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


    public FixedList<Attributes> Attributes {
      get;  set;
    } = new FixedList<Attributes>();


  } // class ProductTypeDto


} // namespace Empiria.Trade.Products.Adapters
