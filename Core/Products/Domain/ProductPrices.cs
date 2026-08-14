/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Partitioned Type / Information Holder   *
*  Type     : ProductPrices                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a product prices list.                                                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Core;

namespace Empiria.Trade.Products {

  /// <summary></summary>
  public class ProductPrices {

    [DataField("Product_Price_List_Id")]
    public int Id {
      get; set;
    }


    [DataField("Product_Id")]
    private int ProductId {
      get; set;
    }


    public ProductEntry Product {
      get {
        return ProductEntry.ParseId(ProductId);
      }
    }


    [DataField("Price_List_Type_Id")]
    public ProductPriceType PriceType {
      get; set;
    }


    [DataField("Product_Price")]
    public decimal Price {
      get; set;
    }

  } // class ProductPrices

} // namespace Empiria.Trade.Products
