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
      get; internal set;
    }


    public int ProductTypeId {
      get; internal set;
    }


    public string ProductCode {
      get; internal set;
    }


    public string ProductUPC {
      get; internal set;
    }


    public string ProductName {
      get; internal set;
    }


    public string Description {
      get; internal set;
    }


    public ProductGroup Group {
      get; internal set;
    }


    internal ProductSubgroup Subgroup {
      get;  set;
    }


    public int ProductBrandId {
      get; internal set;
    }


    public string ProductKeywords {
      get; internal set;
    }


    public string ProductExtData {
      get; internal set;
    }


    public decimal ProductWeight {
      get; internal set;
    }


    public decimal ProductLength {
      get; internal set;
    }


    public char ProductStatus {
      get; internal set;
    }
    
  } // class TRDProductsEntryDto

} // namespace Empiria.Trade.Inventory.Products.Adapters
