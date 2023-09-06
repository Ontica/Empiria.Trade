﻿/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Partitioned Type / Information Holder   *
*  Type     : Product                                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Generate data for products.                                                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using Empiria.Trade.Products.Data;

namespace Empiria.Trade.Products.Domain {

  /// <summary>Generate data for products.</summary>
  internal class ProductBuilder {


    internal ProductBuilder() {

    }


    #region Public methods


    public FixedList<ProductFields> Build(string clauses) {
      
      var data = ProductDataService.GetProducts(clauses);

      return data;

    }


    #endregion Public methods


    #region Private methods


    
    #endregion Private methods

  } // class ProductBuilder

} // namespace Empiria.Trade.Products.Domain
