using System;
/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Partitioned Type / Information Holder   *
*  Type     : TRDProductHelper                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Helper methods to build product structure.                                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/


using Empiria.Trade.Products.Adapters;
using Empiria.Trade.Core;

namespace Empiria.Trade.Products.Domain {

  /// <summary>Helper methods to build product structure.</summary>
  internal class TRDProductHelper {

    private ProductQuery query;

    internal TRDProductHelper(ProductQuery _query) {
      query = _query;

    }


    #region public methods


    public FixedList<Product> GetPriceByCustomer(FixedList<Product> products) {

      var customerPriceNumber = GetCustomerAsignedPrice();

      return new FixedList<Product>();
    }


    private string GetCustomerAsignedPrice() {

      var customer = Party.Parse(query.CustomerUID);
      var extData = customer.ExtData;



      return extData;
    }


    #endregion public methods


  } // class TRDProductHelper

} // namespace Empiria.Trade.Products.Domain
