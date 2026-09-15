/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Partitioned Type / Information Holder   *
*  Type     : ProductPriceType                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a product prices list.                                                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Locations;

namespace Empiria.Trade.Core {
  
  /// <summary></summary>
  public class ProductPriceType : CommonStorage {

    #region Constructor and parsers


    public ProductPriceType() {
      //no-op
    }

    static public ProductPriceType Parse(int id) => ParseId<ProductPriceType>(id);

    static public ProductPriceType Parse(string uid) => ParseKey<ProductPriceType>(uid);

    static public ProductPriceType Empty => ParseEmpty<ProductPriceType>();

    static public FixedList<ProductPriceType> GetList() {
      
      return CommonStorage.GetList<ProductPriceType>().ToFixedList();
    }

    #endregion Constructor and parsers

    #region MyRegion


    #endregion

  } // class ProductPriceType

} // namespace Empiria.Trade.Products
