/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Use cases Layer                         *
*  Assembly : Empiria.Trade.Orders.dll                   Pattern   : Use case interactor class               *
*  Type     : ProductForOrderUseCases                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases used to management Products for order.                                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Threading.Tasks;
using Empiria.Services;
using Empiria.Trade.Products.Adapters;
using Empiria.Trade.Products.UseCases;

namespace Empiria.Trade.Sales.UseCases {

  /// <summary>Use cases used to management Products for order.</summary>
  public class ProductForOrderUseCases : UseCase {


    #region Constructors and parsers

    protected ProductForOrderUseCases() {
      // no-op
    }

    static public ProductForOrderUseCases UseCaseInteractor() {
      return CreateInstance<ProductForOrderUseCases>();
    }


    #endregion Constructors and parsers


    #region Use cases


    public async Task<FixedList<IProductEntryDto>> GetProductsForOrder(ProductQuery keywords) {

      using (var usecases = TRDProductUseCases.UseCaseInteractor()) {

        FixedList<IProductEntryDto> productDto = await usecases.GetProductsList(keywords)
                                                .ConfigureAwait(false);

        return productDto;
      }
    }


    #endregion Use cases

  } // class ProductForOrderUseCases

} // Use cases used to management Products.
