/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Use cases Layer                         *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Use case interactor class               *
*  Type     : ProductUseCases                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases used to build Products.                                                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;
using Empiria.DataTypes;
using Empiria.Products;
using Empiria.Services;
using Empiria.Trade.Products.Adapters;
using Empiria.Trade.Products.Data;
using Empiria.Trade.Products.Domain;

namespace Empiria.Trade.Products.UseCases {

  /// <summary>Use cases used to build Products.</summary>
  public class ProductUseCases : UseCase {


    #region Constructors and parsers

    protected ProductUseCases() {
      // no-op
    }

    static public ProductUseCases UseCaseInteractor() {
      return CreateInstance<ProductUseCases>();
    }


    #endregion Constructors and parsers

    #region Use cases

    public FixedList<ProductForSearchingDto> GetProductsForPurchaseOrder(ProductQuery query) {

      if (query.Keywords == string.Empty) {
        Assertion.RequireFail("Por favor escribe una palabra clave para iniciar búsqueda.");
      }

      var builder = new ProductBuilder(query);

      FixedList<Product> products = builder.GetProducts();

      return ProductMapper.Map(products, query.SupplierUID);
    }


    public async Task<FixedList<IProductEntryDto>> GetProductsListV1(ProductQuery query) {
      var builder = new ProductBuilder(query);

      FixedList<Product> products = await Task.Run(() => builder.GetProductsList())
                                            .ConfigureAwait(false);

      return ProductMapper.MapToEntriesDto(products);
    }


    public async Task<FixedList<IProductEntryDto>> GetProductsForOrder(ProductQuery query) {
      var builder = new ProductBuilder(query);

      FixedList<Product> products = await Task.Run(() => builder.GetProductsForOrder())
                                            .ConfigureAwait(false);

      return ProductMapper.MapToEntriesDto(products);
    }


    public ProductGroup GetProductGroup(string productGroupUid) {
      Assertion.Require(productGroupUid, "productGroupUid");

      //return ProductGroup.Parse(productGroupUid);
      return new ProductGroup();
    }


    public ProductSubgroup GetProductSubgroup(string productSubgroupUid) {
      Assertion.Require(productSubgroupUid, "productSubgroupUid");

      return ProductSubgroup.Parse(productSubgroupUid);
    }


    internal ProductPresentation GetProductPresentation(string presentationUid) {
      Assertion.Require(presentationUid, "presentationUid");

      return ProductPresentation.Parse(presentationUid);
    }


    static internal FixedList<ProductPresentation> GetProductPresentations() {

      var presentations = ProductPresentation.List().Where(x => x.Id > 0).ToList();
      return presentations.ToFixedList();
    }


    static public FixedList<VendorProduct> GetVendorProductByProduct(string productsIn) {

      return ProductDataService.GetVendorProductByProduct(productsIn);
    }


    #endregion Use cases


  } // class ProductUseCases

} // namespace Empiria.Trade.Products.UseCases
