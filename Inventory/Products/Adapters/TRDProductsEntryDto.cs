/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Data Transfer Object                    *
*  Type     : TRDProductsEntryDto                        License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return the entries of TRDProducts.                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Inventory.Products.Adapters {

  /// <summary>Output DTO used to return the entries of TRDProducts.</summary>
  public class TRDProductsEntryDto {

    public string ProductUID {
      get; set;
    }


    public int ProductTypeId {
      get; set;
    }


    public string ProductCode {
      get; set;
    }


    public string ProductUPC {
      get; set;
    }


    public string ProductName {
      get; set;
    }


    public string Description {
      get; set;
    }


    public ProductLine ProductLine {
      get; internal set;
    }


    public int ProductBrandId {
      get; set;
    }


    public string ProductKeywords {
      get; set;
    }


    public string ProductExtData {
      get; set;
    }


    public decimal ProductWeight {
      get; set;
    }


    public decimal ProductLength {
      get; set;
    }


    public char ProductStatus {
      get; set;
    }


  } // class TRDProductsEntryDto

} // namespace Empiria.Trade.Inventory.Products.Adapters
