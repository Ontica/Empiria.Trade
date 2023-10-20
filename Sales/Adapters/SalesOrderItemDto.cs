﻿/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Order Management                           Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Sales.dll                    Pattern   : Data Transfer Object                    *
*  Type     : OrderItemDto                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return order items.                                                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.StateEnums;

using Empiria.Trade.Products.Adapters;

namespace Empiria.Trade.Sales.Adapters {

  /// <summary>Output DTO used to return order items </summary>
  public class SalesOrderItemDto {

    #region Public properties


    public string OrderItemUID {
      get; set;
    } = string.Empty;

    public decimal Quantity {
      get; set;
    }
       
    public decimal BasePrice {
      get; set;
    }

    public decimal SpecialPrice {
      get; set;
    }

    public decimal SalesPrice {
      get; set;
    }

    public decimal AdditionalDiscount {
      get; set;
    }

    public decimal AdditionalDiscountToApply {
      get; set;
    } 

    public decimal Shipment {
      get; set;
    }

    public decimal Taxes {
      get; set;
    }

    public decimal Total {
      get; set;
    }

    public string Notes {
      get; set;
    } 

    public VendorDto Vendor {
      get; set;
    }

    public PresentationDto Presentation {
      get; set;
    }

    public ProductShortEntryDto Product {
      get; set;
    }


    #endregion

  } //  public class OrderItemDto


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

  } // class PresentationDto

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

    public ProductShortEntryDto ProductType {
      get; set;
    }

  } // class ProductDto

  public class ProductTypeDto {


    public string ProductTypeUID {
      get; set;
    }


    public string Name {
      get; set;
    }


    public FixedList<Attributes> Attributes {
      get; internal set;
    } = new FixedList<Attributes>();


  } // class ProductType


} // namespace Empiria.Trade.Sales.Adapters
