/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Partitioned Type / Information Holder   *
*  Type     : Product                                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Generate data for Products.                                                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System.Linq;
using Empiria.Trade.Products.Adapters;
using Empiria.Trade.Products.Data;

namespace Empiria.Trade.Products.Domain {

  /// <summary>Generate data for roducts.</summary>
  internal class ProductBuilder {

    #region Constructors and parsers

    private readonly ProductQuery query;


    public ProductBuilder(ProductQuery _query) {
      query = _query;
    }

    #endregion Constructors and parsers


    #region Public methods

    #endregion Public methods


    #region Private methods

    #endregion Private methods


  } // class ProductBuilder


} // Empiria.Trade.Products.Domain
