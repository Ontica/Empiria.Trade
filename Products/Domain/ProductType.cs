/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Powertype                               *
*  Type     : ProductType                                License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Power type that describes a product.                                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Ontology;

namespace Empiria.Trade.Products {

  /// <summary>Powertype that describes a product.</summary>
  [Powertype(typeof(Product))]
  public class ProductType : Powertype {

    #region Constructors and parsers

    private ProductType() {
      // Empiria powertype types always have this constructor.
    }

    static public new ProductType Parse(int typeId) => ObjectTypeInfo.Parse<ProductType>(typeId);

    static internal new ProductType Parse(string typeName) => ProductType.Parse<ProductType>(typeName);

    static public ProductType Empty => ProductType.Parse("ObjectType.ProductType");

    #endregion Constructors and parsers

    #region Public methods

    /// <summary>Factory method for create product instances of this product type.</summary>
    internal Product CreateInstance() {
      return base.CreateObject<Product>();
    }

    #endregion Public methods

  }  // class ProductType

}  // namespace Empiria.Trade.Products
