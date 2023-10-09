/* Empiria Trade *********************************************************************************************
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
using Empiria.Trade.Products.Adapters;

namespace Empiria.Trade.Products.Domain
{

    /// <summary>Generate data for products.</summary>
    internal class ProductBuilder
    {


        internal ProductBuilder()
        {

        }


        #region Public methods


        public FixedList<ProductFields> Build(ProductQuery query)
        {

            var data = ProductDataService.GetProducts(query.Keywords);

            return data;

        }


        #endregion Public methods


        #region Private methods



        #endregion Private methods

    } // class ProductBuilder

} // namespace Empiria.Trade.Products.Domain
