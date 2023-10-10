﻿/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Partitioned Type / Information Holder   *
*  Type     : Product                                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Generate data for TRDProducts.                                                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using System.Linq;
using Empiria.Json;
using Empiria.Storage;
using Empiria.Trade.Products.Adapters;
using Empiria.Trade.Products.Data;
using Newtonsoft.Json;

namespace Empiria.Trade.Products.Domain {

  /// <summary>Generate data for TRDProducts.</summary>
  internal class TRDProductBuilder {

    #region Constructor

    public TRDProductBuilder() {
    
    }


    #endregion Constructor


    #region Public methods

    public object BuildProduct(Product product) {
    
      return product;
    }


    internal FixedList<Product> GetProductsList(ProductQuery query) {

      FixedList<Product> data = TRDProductDataService.GetProductsList(query.Keywords);

      return data;
    }

    #endregion Public methods


    #region Private methods



    #endregion Private methods


  } // class TRDProductBuilder


} // Empiria.Trade.Products.Domain