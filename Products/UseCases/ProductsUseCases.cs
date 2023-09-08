/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Use cases Layer                         *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Use case interactor class               *
*  Type     : ProductsUseCases                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases used to build Products.                                                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Threading.Tasks;

using Empiria.Services;

using Empiria.Trade.Products.Adapters;
using Empiria.Trade.Products.Domain;

namespace Empiria.Trade.Products.UseCases {

  /// <summary>Use cases used to build Products.</summary>
  public class ProductsUseCases : UseCase {

    #region Constructors and parsers

    protected ProductsUseCases() {
      // no-op
    }

    static public ProductsUseCases UseCaseInteractor() {
      return UseCase.CreateInstance<ProductsUseCases>();
    }


    #endregion Constructors and parsers


    #region Use cases


    public async Task<ProductDto> BuildProducts(ProductQuery keywords) {

      var builder = new ProductBuilder();

      FixedList<ProductFields> products = await Task.Run(() => builder.Build(keywords))
                                            .ConfigureAwait(false);

      return ProductMapper.Map(products);
    }


    public async Task<FixedList<IProductEntryDto>> BuildProductsList(ProductQuery keywords) {

      var builder = new ProductBuilder();

      FixedList<ProductFields> products = await Task.Run(() => builder.Build(keywords))
                                            .ConfigureAwait(false);

      return ProductMapper.MapTo(products);
    }


    #endregion Use cases

  } // class ProductsUseCases

} // namespace Empiria.Trade.Products.UseCases
