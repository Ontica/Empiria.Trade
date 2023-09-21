/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Partitioned Type / Information Holder   *
*  Type     : Product                                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Generate data for TRDProducts.                                                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Inventory.Products.Adapters;
using Empiria.Trade.Inventory.Products.Data;

namespace Empiria.Trade.Inventory.Products.Domain {

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


    public void AddOrUpdateTRDProduct(Product product) {

      var data = new TRDProductDataService();
      
      data.AddOrUpdateTRDProduct(product);

    }

    #endregion Public methods

    
    #region Private methods



    #endregion Private methods


  }
}
