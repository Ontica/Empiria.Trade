/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Use cases Layer                         *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Use case interactor class               *
*  Type     : TRDProductUseCases                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases used to build TRDProducts.                                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;
using Empiria.DataTypes;
using Empiria.Services;
using Empiria.Trade.Products.Adapters;
using Empiria.Trade.Products.Data;
using Empiria.Trade.Products.Domain;

namespace Empiria.Trade.Products.UseCases {

  /// <summary>Use cases used to build TRDProducts.</summary>
  public class TRDProductUseCases : UseCase {


    #region Constructors and parsers

    protected TRDProductUseCases() {
      // no-op
    }

    static public TRDProductUseCases UseCaseInteractor() {
      return CreateInstance<TRDProductUseCases>();
    }


    #endregion Constructors and parsers


    #region Use cases


    public IProductEntryDto GetTRDProduct(string productUID) {
      Assertion.Require(productUID, "productUID");

      var product = Product.Parse(productUID);

      return TRDProductMapper.MapToDto(product);
    }


    public async Task<FixedList<IProductEntryDto>> GetProductsList(ProductQuery query) {
      var builder = new TRDProductBuilder();

      FixedList<Product> products = await Task.Run(() => builder.GetProductsList(query))
                                            .ConfigureAwait(false);

      return TRDProductMapper.MapToEntriesDto(products);
    }


    public async Task<FixedList<IProductEntryDto>> GetProductsForOrder(ProductQuery query) {
      var builder = new TRDProductBuilder();

      FixedList<Product> products = await Task.Run(() => builder.GetProductsForOrder(query))
                                            .ConfigureAwait(false);

      return TRDProductMapper.MapToEntriesDto(products);
    }


    public ProductGroup GetTRDProductGroup(string productGroupUid) {
      Assertion.Require(productGroupUid, "productGroupUid");

      return ProductGroup.Parse(productGroupUid);
    }


    public ProductSubgroup GetTRDProductSubgroup(string productSubgroupUid) {
      Assertion.Require(productSubgroupUid, "productSubgroupUid");

      return ProductSubgroup.Parse(productSubgroupUid);
    }


    public async Task<string> UpdateGUID(TableQuery query) {
      
      try {

        return await Task.Run(() => TRDProductDataService.UpdateTableGUID(
                              query.TableName, query.IdName, query.UidName))
                              .ConfigureAwait(false);

      } catch (Exception ex) {
        throw new Exception(ex.Message, ex);
      }
    }


    #endregion Use cases


  } // class TRDProductUseCases

} // namespace Empiria.Trade.Products.UseCases
