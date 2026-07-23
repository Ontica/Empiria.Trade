/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Partitioned Type / Information Holder   *
*  Type     : Product                                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents total product stock.                                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Products {

  /// <summary>Represents total product stock.</summary>
  internal class ProductsTotals {

    internal ProductsTotals() {
      //no-op
    }


    [DataField("PRODUCT_ID")]
    public int Product_Id{
      get; internal set;
    }


    [DataField("PRODUCT_STOCK")]
    public decimal Stock {
      get; internal set;
    }

  } // class ProductsTotals

} // namespace Empiria.Trade.Products
