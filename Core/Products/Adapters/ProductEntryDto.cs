/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Data Transfer Object                    *
*  Type     : ProductEntryDto                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return the entries of Products.                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Products.Adapters {


  public interface IProductEntryDto {

  }


  /// <summary>Output DTO used to return the entries of Products.</summary>
  public class ProductEntryDto : IProductEntryDto {


    public string ProductUID {
      get; set;
    } = string.Empty;
    
    
    public string ProductGroupUID {
      get; set;
    } = string.Empty;


    public string ProductSubgroupUID {
      get; set;
    } = string.Empty;


    public string ProductCode {
      get; set;
    } = string.Empty;


    public string ProductUPC {
      get; set;
    } = string.Empty;


    public string ProductName {
      get; set;
    } = string.Empty;


    public string ProductDescription {
      get; set;
    } = string.Empty;


    public string Category {
      get; set;
    } = string.Empty;


    public decimal ProductWeight {
      get; set;
    }


    public decimal ProductLength {
      get; set;
    }


    public bool FragileProduct {
      get; set;
    }


    public StateEnums.EntityStatus ProductStatus {
      get; set;
    }
    
  } // class ProductEntryDto


  public class PriceListOfProduct {


    public string Name {
      get; internal set;
    }


    public decimal Value {
      get; internal set;
    }

  } // class PriceListOfProduct


}
