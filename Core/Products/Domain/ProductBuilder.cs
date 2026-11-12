/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Partitioned Type / Information Holder   *
*  Type     : Product                                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Generate data for Products.                                                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Linq;

using Empiria.Trade.Core;
using Empiria.Trade.Products.Adapters;

namespace Empiria.Trade.Products {

  /// <summary>Generate data for roducts.</summary>
  public class ProductBuilder {

    #region Constructors and parsers

    private readonly ProductQuery query;


    public ProductBuilder(ProductQuery _query) {
      query = _query;
    }

    #endregion Constructors and parsers


    #region Public methods

    static public decimal GetStockByPackingSmallBag(FixedList<InventoryEntryStock> productsStock,
                                                      ProductEntry product) {

      var stockAndLocation = productsStock.Where(x => x.ProductId == product.Id && x.Location.Id != -1)
                                                 .ToList();

      decimal stockByPresentation = 0;

      var groupsByUnit = stockAndLocation.GroupBy(x => x.InventoryUnit);

      foreach (var group in groupsByUnit) {

        var stockByProductUnit = stockAndLocation.FindAll(x => x.InventoryUnit.Id == group.Key.Id);

        if (stockByProductUnit.Count > 0) {

          //var _stock = product.GetByPackingSmallBag(stockByProductUnit.ToFixedList(), byUnit.Key);

          stockByPresentation += product.GetByPackingSmallBag(stockByProductUnit.ToFixedList(), group.Key);
        }
      }
      return stockByPresentation;
    }

    #endregion Public methods



    #region Private methods

    #endregion Private methods


  } // class ProductBuilder


} // Empiria.Trade.Products.Domain
